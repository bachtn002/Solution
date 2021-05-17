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
        public Task<List<ShopViewModel>> GetShopUser();
        public Task<bool> CreateShop(ShopCreateModel request);
        public Task<bool> InsertShopUser();
        public Task<bool> CreateCollab(UserRegisterModel request);
        public Task<List<CollabViewModel>> GetCollabByShopId(long shopId);
    }
}
