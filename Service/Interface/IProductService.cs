using Repository.Model.CategoryModel;
using Repository.Model.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IProductService
    {
        public Task<bool> CreateProduct(ProductCreateModel request);
        public Task<bool> CreateCategory(CategoryCreateModel request);
        public Task<List<CategoryViewModel>> GetCategoryByShopId(long shopId);
        public Task<List<ProductViewModel>> GetProduct(long shopId, long categoryId);
    }
}
