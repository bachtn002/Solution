using Repository.Model.CartModel;
using Repository.Model.CategoryModel;
using Repository.Model.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICartService
    {
        public Task<bool> CreateCart(CartCreateModel request);
        public Task<List<CategoryViewModel>> GetCategoryByShopId(long shopId);
        public Task<List<ProductViewModel>> GetProduct(long shopId);
        public Task<List<CartViewModel>> GetCart();
        public long GetUserId();
        public Task<bool> DeleteCart();
    }
}
