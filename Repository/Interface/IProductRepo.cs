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
        public Task<List<ProductViewModel>> GetProduct(long shopId);
        public Task<bool> CreateProduct(ProductCreateModel request);
        public Task<bool> DeleteProduct(ProductUpdateModel request);
        public Task<bool> UpdateProduct(ProductUpdateModel request);
        public Task<ProductUpdateModel> GetUpdateProduct(long productId, long categoryId, long shopId);
        public Task<ProductDetailModel> GetProductDetails(long productId);
        public Task<List<CategoryViewModel>> GetCategoryByShopId(long shopId);
        public Task<bool> CreateCategory(CategoryCreateModel request);
        public Task<CategoryUpdateModel> GetUpdateCategory(long shopId);
        public Task<bool> DeleteCategory(CategoryUpdateModel request);
        public Task<bool> UpdateCategory(CategoryUpdateModel request);
        public Task<List<CategoryViewModel>> GetParentCategory(long shopId);
        
        
    }
}
