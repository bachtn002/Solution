using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
        public ShopRepo(QL_CTVContext dataDbContext,IUserRepo userRepo)
        {
            _dataDbContext = dataDbContext;
            _userRepo = userRepo;
        }

        public async Task<long> GetUserId(UserLoginModel request)
        {
            var token = await _userRepo.LoginUser(request);
            var tokeRead = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = Convert.ToInt64(tokeRead.Claims.First(x => x.Type == "UserId").Value);
            return userId;
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
                ShopStatusId=1,
                CreatedUtcDate = DateTime.UtcNow
            }); 
            var result = await _dataDbContext.SaveChangesAsync();
            if (result < 0)
            {
                return false;
            }
            return true;
        }
        
        public async Task<List<ShopUserViewModel>> GetShopUser()
        {
            var query = from u in _dataDbContext.TUsers
                        join su in _dataDbContext.TShopUsers on u.UserId equals su.UserId
                        join s in _dataDbContext.TShops on su.ShopId equals s.ShopId
                        join ss in _dataDbContext.TmShopStatuses on s.ShopId equals ss.ShopStatusId
                        join r in _dataDbContext.TRoles on su.RoleId equals r.RoleId
                        select new { u, s, r, ss };
            var data = await query.Select(x => new ShopUserViewModel()
            {
                FullName = x.u.FullName,
                RoleName = x.r.RoleName,
                Name = x.s.Name,
                ShopStatusName = x.ss.ShopStatusName
            }).ToListAsync();
            return data;

        }

        public Task<bool> InsertShopUser(ShopUserViewModel request)
        {
            throw new NotImplementedException();
        }
    }
}

