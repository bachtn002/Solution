using Repository.Model.CategoryModel;
using Repository.Model.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IProductRepo
    {
        public Task<List<ProductViewModel>> GetProduct(long shopId, long categoryId);
        public Task<bool> CreateProduct(ProductCreateModel request);
        public Task<bool> DeleteProduct(long productId);
        public Task<bool> UpdateProduct(ProductUpdateModel request);
        public Task<ProductUpdateModel> GetUpdateProduct(long productId);
        public Task<ProductDetailModel> GetProductDetails(long productId);
        public Task<List<CategoryViewModel>> GetCategoryByShopId(long shopId);
        public Task<bool> CreateCategory(CategoryCreateModel request);
    }
}
