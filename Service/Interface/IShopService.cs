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
        public Task<List<ShopViewModel>> GetShopUser();
        public Task<bool> CreateShop(ShopCreateModel request);
        public Task<bool> InsertShopUser();
        public Task<bool> CreateCollab(long shopId, UserRegisterModel request);
        public Task<List<CollabViewModel>> GetCollabByShopId(long shopId);
        public Task<UserRegisterModel> GetCollab(long shopId);
        public Task<ShopCreateModel> GetUpdateShop(long shopId);
        public Task<bool> UpdateShop(ShopCreateModel request);
        public Task<bool> DeleteShop(ShopCreateModel request);
        public Task<ShopCreateModel> GetShopDetails(long shopId);
        
    }
}
