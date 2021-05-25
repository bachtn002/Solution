using Repository.Interface;
using Repository.Model;
using Repository.Model.ShopModel;
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

        public Task<bool> CreateCollab(long shopId, CollabCreateModel request)
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

        public async Task<CollabCreateModel> GetCollabCreateModel(long shopId)
        {
            return await _shopRepo.GetCollabCreateModel(shopId);
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

        public async Task<bool> UpdateCollab(CollabUpdateModel request)
        {
            return await _shopRepo.UpdateCollab(request);
        }

        public async Task<CollabUpdateModel> GetCollabUpdate(long userId, long shopId)
        {
            return await _shopRepo.GetCollabUpdate(userId, shopId);
        }

        public async Task<bool> DeleteCollab(CollabUpdateModel request)
        {
            return await _shopRepo.DeleteCollab(request);
        }

        public async Task<CollabViewModel> GetCollabDetails(long userId)
        {
            return await _shopRepo.GetCollabDetails(userId);
        }
    }
}
