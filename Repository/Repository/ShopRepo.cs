using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ShopRepo : IShopRepo
    {
        private readonly QL_CTVContext _dataDbContext;
        public ShopRepo(QL_CTVContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }
        public Task<bool> CreateShop(ShopCreateModel request)
        {
            throw new NotImplementedException();
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
    }
}

