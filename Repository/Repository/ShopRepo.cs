using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository.Interface;
using Repository.Model;
using Repository.Model.ShopModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ShopRepo : IShopRepo
    {
        private readonly QL_CTVContext _dataDbContext;
        private readonly IUserRepo _userRepo;
        public ShopRepo(QL_CTVContext dataDbContext, IUserRepo userRepo)
        {
            _dataDbContext = dataDbContext;
            _userRepo = userRepo;
        }

        public async Task<bool> CreateCollab(long shopId, CollabCreateModel request)
        {
            var user = await _dataDbContext.TUsers.FirstOrDefaultAsync(x => x.Mobile == request.Mobile && x.IsDelete == 0);
            if (user != null)
            {
                return false;
            }
            else
            {
                await _dataDbContext.TUsers.AddAsync(new TUser()
                {
                    Mobile = request.Mobile,
                    FullName = request.FullName,
                    PasswordHash = "123456",
                    GenderId = request.GenderId,
                    DateOfBirth = request.DateOfBirth,
                    Avatar = request.Avatar,
                    UserStatusId = 1,
                    CreatedUtcDate = DateTime.UtcNow
                });
                await _dataDbContext.TShopUsers.AddAsync(new TShopUser()
                {
                    ShopId = shopId,
                    RoleId = 2,
                    UserId = await _dataDbContext.TUsers.Select(x => x.UserId).MaxAsync() + 1,
                    CreatedUtcDate = DateTime.UtcNow
                });
            }
            var result = await _dataDbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CreateShop(ShopCreateModel request)
        {
           /* var shop = await _dataDbContext.TShops.FirstOrDefaultAsync(x => x.Name == request.Name && x.IsDelete == 0);*/
            if (await _dataDbContext.TShops.AnyAsync(x=>x.Name==request.Name && x.IsDelete==0))
            {
                return false;
            }
            else
            {
                await _dataDbContext.TShops.AddAsync(new TShop()
                {
                    UserId = _userRepo.GetUserId(),
                    Name = request.Name,
                    Address = request.Address,
                    Description = request.Description,
                    Avatar = request.Avatar,
                    ShopStatusId = 1,
                    CreatedUtcDate = DateTime.UtcNow
                });
                var result = await _dataDbContext.SaveChangesAsync();
                if (result <= 0)
                {
                    return false;
                }
                return true;
            }
        }

        public async Task<PagedResult<CollabViewModel>> GetCollabByShopId(long shopId)
        {

            var query = from u in _dataDbContext.TUsers
                        join su in _dataDbContext.TShopUsers on u.UserId equals su.UserId
                        join s in _dataDbContext.TShops on su.ShopId equals s.ShopId
                        join r in _dataDbContext.TRoles on su.RoleId equals r.RoleId
                        join g in _dataDbContext.TmGenders on u.GenderId equals g.GenderId
                        join us in _dataDbContext.TmUserStatuses on u.UserStatusId equals us.UserStatusId
                        where s.ShopId == shopId && su.RoleId == 2 && su.IsDelete == 0 && u.IsDelete == 0
                        select new { u, su, s, r, g, us };
            var data = await query.Select(x => new CollabViewModel()
            {
                UserId = x.su.UserId,
                ShopId = x.s.ShopId,
                FullName = x.u.FullName,
                Mobile = x.u.Mobile,
                Status = x.us.UserStatusName,
                GenderName = x.g.GenderName,
                DOB = x.u.DateOfBirth,
                RoleName = x.r.RoleName,
                CreatedUtcDate = x.su.CreatedUtcDate
            }).ToListAsync();
            var pagedResult = new PagedResult<CollabViewModel>()
            {
                Items = data
            };
            return pagedResult;
            /*return result;*/
        }

        public async Task<List<ShopViewModel>> GetShopUser()
        {
            var query = from u in _dataDbContext.TUsers
                        join su in _dataDbContext.TShopUsers on u.UserId equals su.UserId
                        join s in _dataDbContext.TShops on su.ShopId equals s.ShopId
                        join r in _dataDbContext.TRoles on su.RoleId equals r.RoleId
                        join tm in _dataDbContext.TmShopStatuses on s.ShopStatusId equals tm.ShopStatusId
                        where u.UserId == _userRepo.GetUserId() && s.IsDelete == 0
                        select new { u, su, s, r, tm };
            var data = await query.Select(x => new ShopViewModel()
            {
                RoleName = x.r.RoleName,
                Name = x.s.Name,
                ShopStatusName = x.tm.ShopStatusName,
                ShopId = x.s.ShopId,
                Address = x.s.Address
            }).ToListAsync();
            return data;
        }

        public async Task<bool> InsertShopUser()
        {
            var shopId = await _dataDbContext.TShops.Select(x => x.ShopId).MaxAsync();
            await _dataDbContext.TShopUsers.AddAsync(new TShopUser()
            {
                UserId = _userRepo.GetUserId(),
                ShopId = shopId,
                RoleId = 3,
                CreatedUtcDate = DateTime.UtcNow
            });
            var result = await _dataDbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<CollabCreateModel> GetCollabCreateModel(long shopId)
        {
            var userId = await (from s in _dataDbContext.TShops where s.ShopId == shopId select s.UserId).FirstOrDefaultAsync();
            var user = await _dataDbContext.TUsers.FirstOrDefaultAsync(x => x.UserId == userId);
            var userCM = new CollabCreateModel()
            {
                ShopId = shopId,
                Mobile = user.Mobile,
                FullName = user.FullName,
                GenderId = user.GenderId,
                DateOfBirth = user.DateOfBirth,
                Avatar = user.Avatar
            };
            return userCM;
        }

        public async Task<bool> UpdateShop(ShopCreateModel request)
        {
            if (await _dataDbContext.TShops.AnyAsync(x => x.Name == request.Name && x.ShopId != request.ShopId))
            {
                return false;
            }
            var shop = await _dataDbContext.TShops.FirstOrDefaultAsync(x => x.ShopId == request.ShopId);
            if (shop != null)
            {
                shop.Name = request.Name;
                shop.ShopId = request.ShopId;
                shop.Address = request.Address;
                shop.Avatar = request.Avatar;
                shop.Description = request.Description;
                shop.ShopStatusId = request.ShopStatusId;
                shop.ModifiedUtcDate = DateTime.UtcNow;
                _dataDbContext.TShops.Update(shop);
            }
            var result = await _dataDbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<ShopCreateModel> GetUpdateShop(long shopId)
        {
            var shop = await _dataDbContext.TShops.FirstOrDefaultAsync(x => x.ShopId == shopId);
            var shopVM = new ShopCreateModel()
            {
                ShopId = shopId,
                Name = shop.Name,
                Address = shop.Address,
                Avatar = shop.Avatar,
                Description = shop.Description,
                ShopStatusId = shop.ShopStatusId
            };
            return shopVM;
        }

        public async Task<bool> DeleteShop(ShopCreateModel request)
        {
            var shop = await _dataDbContext.TShops.FirstOrDefaultAsync(x => x.ShopId == request.ShopId);
            if (shop != null)
            {
                shop.IsDelete = 1;
                shop.ModifiedUtcDate = DateTime.UtcNow;
                _dataDbContext.TShops.Update(shop);
            }
            
            var result = await _dataDbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<ShopCreateModel> GetShopDetails(long shopId)
        {
            var shop = await _dataDbContext.TShops.FirstOrDefaultAsync(x => x.ShopId == shopId);

            var result = new ShopCreateModel()
            {
                ShopId = shop.ShopId,
                Name = shop.Name,
                ShopStatusName = (from tmss in _dataDbContext.TmShopStatuses
                                  where tmss.ShopStatusId == shop.ShopStatusId
                                  select tmss.ShopStatusName).FirstOrDefault(),
                Address = shop.Address,
                Avatar = shop.Avatar,
                Description = shop.Description,
                FullName = (from u in _dataDbContext.TUsers
                            where u.UserId == shop.UserId
                            select u.FullName).FirstOrDefault(),
                CreatedUtcDate = shop.CreatedUtcDate,
                SumUser = (from s in _dataDbContext.TShops
                           join su in _dataDbContext.TShopUsers on s.ShopId equals su.ShopId
                           where s.ShopId == shop.ShopId && su.RoleId == 2 && su.IsDelete==0
                           select new { su }).Count()
            };
            return result;

        }

        public async Task<bool> UpdateCollab(CollabUpdateModel request)
        {
            if (await _dataDbContext.TUsers.AnyAsync(x => x.Mobile == request.Mobile && x.UserId != request.UserId))
            {
                return false;
            }
            var user = await _dataDbContext.TUsers.FirstOrDefaultAsync(x => x.UserId == request.UserId);
            user.FullName = request.FullName;
            user.Mobile = request.Mobile;
            user.GenderId = request.GenderId;
            user.UserStatusId = request.UserStatusId;
            user.ModifiedUtcDate = DateTime.UtcNow;
            _dataDbContext.TUsers.Update(user);
            var result = await _dataDbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;
        }
        public async Task<CollabUpdateModel> GetCollabUpdate(long userId, long shopId)
        {
            var user = await _dataDbContext.TUsers.FirstOrDefaultAsync(x => x.UserId == userId);
            var result = new CollabUpdateModel()
            {
                ShopId = shopId,
                UserId = userId,
                FullName = user.FullName,
                Mobile = user.Mobile,
                GenderId = user.GenderId,
                UserStatusId = user.UserStatusId
            };
            return result;
        }

        public async Task<bool> DeleteCollab(CollabUpdateModel request)
        {
            var user = await _dataDbContext.TUsers.FirstOrDefaultAsync(x => x.UserId == request.UserId);
            var shopUser = await _dataDbContext.TShopUsers.FirstOrDefaultAsync(x => x.UserId == request.UserId 
            && x.ShopId == request.ShopId);
            if (user != null && shopUser != null)
            {
                user.IsDelete = 1;
                shopUser.IsDelete = 1;
                user.ModifiedUtcDate = DateTime.UtcNow;
                shopUser.RetiredUtcDate = DateTime.UtcNow;
            }
            _dataDbContext.TUsers.Update(user);
            var result = await _dataDbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<CollabViewModel> GetCollabDetails(long userId)
        {
            var user = await _dataDbContext.TUsers.FirstOrDefaultAsync(x => x.UserId == userId);
            var result = new CollabViewModel()
            {
                Mobile=user.Mobile,
                FullName=user.FullName,
                Avatar=user.Avatar,
                DOB=user.DateOfBirth,
                JoinDate=user.CreatedUtcDate,
                GenderName=(from u in _dataDbContext.TUsers
                            join tm in _dataDbContext.TmGenders on u.GenderId equals tm.GenderId
                            where tm.GenderId==user.GenderId
                            select tm.GenderName).FirstOrDefault(),
                UserStatusName=(from u in _dataDbContext.TUsers
                                join tm in _dataDbContext.TmUserStatuses on u.UserStatusId equals tm.UserStatusId
                                where tm.UserStatusId==user.UserStatusId
                                select tm.UserStatusName).FirstOrDefault()
            };
            return result;
        }
    }
}

