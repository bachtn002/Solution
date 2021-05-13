using Repository.Interface;
using Repository.Model;
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
        public Task<bool> CreateShop(ShopCreateModel request)
        {
            return _shopRepo.CreateShop(request);
        }

        public async Task<List<ShopUserViewModel>> GetShopUser()
        {
            return await _shopRepo.GetShopUser();
        }
    }
}
