using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IShopService
    {
        public Task<List<ShopUserViewModel>> GetShopUser();
        public Task<bool> CreateShop(ShopCreateModel request);
    }
}
