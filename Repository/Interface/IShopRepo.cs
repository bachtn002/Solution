using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IShopRepo
    {
        public Task<List<ShopUserViewModel>> GetShopUser();
        public Task<bool> CreateShop(ShopCreateModel request);
        public Task<long> GetUserId(UserLoginModel request);
        public Task<bool> InsertShopUser(ShopUserViewModel request);
    }
}
