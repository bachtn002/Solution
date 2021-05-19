using Repository.Interface;
using Repository.Model;
using Repository.Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ShopService : IShopService
    {
        private readonly IShopRepo _shopRepo;
        public ShopService(IShopRepo shopRepo)
        {
            _shopRepo = shopRepo;
        }

        public Task<bool> CreateCollab(long shopId, UserRegisterModel request)
        {
            return _shopRepo.CreateCollab(request.ShopId, request);
        }

        public Task<bool> CreateShop(ShopCreateModel request)
        {
            return _shopRepo.CreateShop(request);
        }

        public async Task<bool> DeleteShop(ShopCreateModel request)
        {
            return await _shopRepo.DeleteShop(request);
        }

        public async Task<ShopCreateModel> GetShopDetails(long shopId)
        {
            return await _shopRepo.GetShopDetails(shopId);
        }

        public async Task<UserRegisterModel> GetCollab(long shopId)
        {
            return await _shopRepo.GetCollab(shopId);
        }

        public async Task<PagedResult<CollabViewModel>> GetCollabByShopId(long shopId)
        {
            return await _shopRepo.GetCollabByShopId(shopId);
        }

        

        public async Task<List<ShopViewModel>> GetShopUser()
        {
            return await _shopRepo.GetShopUser();
        }

        public async Task<ShopCreateModel> GetUpdateShop(long shopId)
        {
            return await _shopRepo.GetUpdateShop(shopId);
        }

        public async Task<bool> InsertShopUser()
        {
            return await _shopRepo.InsertShopUser();
        }

        public async Task<bool> UpdateShop( ShopCreateModel request)
        {
            return await _shopRepo.UpdateShop(request);
        }
    }
}
