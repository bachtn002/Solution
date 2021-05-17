using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Model;
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

        public async Task<bool> CreateCollab(UserRegisterModel request)
        {
            var user = await _dataDbContext.TUsers.FirstOrDefaultAsync(x => x.Mobile == request.Mobile);
            if (user != null)
            {
                var userId = user.UserId;
                await _dataDbContext.TShopUsers.AddAsync(new TShopUser()
                {

                });
            }
            await _dataDbContext.TUsers.AddAsync(new TUser()
            {
                Mobile = request.Mobile,
                FullName = request.FullName,
                PasswordHash = "123456",
                GenderId = request.GenderId,
                DateOfBirth = request.DateOfBirth,
                Avatar = request.Avatar
            });
            var result = await _dataDbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CreateShop(ShopCreateModel request)
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
            if (result < 0)
            {
                return false;
            }
            return true;
        }

        public async Task<List<CollabViewModel>> GetCollabByShopId(long shopId)
        {
            //var shop = await _dataDbContext.TShops.FirstOrDefaultAsync(x => x.ShopId == shopId);

            var query = from u in _dataDbContext.TUsers
                        join su in _dataDbContext.TShopUsers on u.UserId equals su.UserId
                        join s in _dataDbContext.TShops on su.ShopId equals s.ShopId
                        join r in _dataDbContext.TRoles on su.RoleId equals r.RoleId
                        join g in _dataDbContext.TmGenders on u.GenderId equals g.GenderId
                        join us in _dataDbContext.TmUserStatuses on u.UserStatusId equals us.UserStatusId
                        where s.ShopId == shopId
                        select new { u, su, s, r, g, us };
            var data = await query.Select(x => new CollabViewModel()
            {
                FullName=x.u.FullName,
                Mobile=x.u.Mobile,
                Status=x.us.UserStatusName,
                GenderName=x.g.GenderName,
                DOB=x.u.DateOfBirth,
                RoleName=x.r.RoleName,
                CreatedUtcDate=x.su.CreatedUtcDate
            }).ToListAsync();
            return data;
        }

        public async Task<List<ShopViewModel>> GetShopUser()
        {
            var query = from u in _dataDbContext.TUsers
                        join su in _dataDbContext.TShopUsers on u.UserId equals su.UserId
                        join s in _dataDbContext.TShops on su.ShopId equals s.ShopId
                        join ss in _dataDbContext.TmShopStatuses on s.ShopStatusId equals ss.ShopStatusId
                        join r in _dataDbContext.TRoles on su.RoleId equals r.RoleId
                        where u.UserId == _userRepo.GetUserId()
                        select new { u, su, s, ss, r };

            var data = await query.Select(x => new ShopViewModel()
            {
                FullName = x.u.FullName,
                RoleName = x.r.RoleName,
                Name = x.s.Name,
                ShopStatusName = x.ss.ShopStatusName,
                ShopId = x.s.ShopId
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
    }
}

