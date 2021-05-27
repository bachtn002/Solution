using Repository.Interface;
using Repository.Model.CartModel;
using Repository.Model.CategoryModel;
using Repository.Model.ProductModel;
using Repository.Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class CartService : ICartService
    {
        public readonly ICartRepo _cartRepo;
        public readonly IProductRepo _productRepo;
        public readonly IUserRepo _userRepo;
        public CartService(ICartRepo cartRepo, IProductRepo productRepo, IUserRepo userRepo)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
            _userRepo = userRepo;
        }
        public async Task<bool> CreateCart(CartCreateModel request)
        {
            return await _cartRepo.CreateCart(request);
        }

        public async Task<bool> DeleteCart()
        {
            return await _cartRepo.DeleteCart();
        }

        public async Task<List<CartViewModel>> GetCart()
        {
            return await _cartRepo.GetCart();
        }

        public async Task<List<CategoryViewModel>> GetCategoryByShopId(long shopId)
        {
            return await _productRepo.GetCategoryByShopId(shopId);
        }

        public async Task<List<ProductViewModel>> GetProduct(long shopId)
        {
            return await _productRepo.GetProduct(shopId);
        }

        public long GetUserId()
        {
            return _userRepo.GetUserId();
        }
    }

}