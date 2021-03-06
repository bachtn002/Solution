using Repository.Interface;
using Repository.Model.CategoryModel;
using Repository.Model.ProductModel;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<bool> CreateCategory(CategoryCreateModel request)
        {
            return await _productRepo.CreateCategory(request);
        }

        public async Task<bool> CreateProduct(ProductCreateModel request)
        {
            return await _productRepo.CreateProduct(request);
        }

        public async Task<bool> DeleteCategory(CategoryUpdateModel request)
        {
            return await _productRepo.DeleteCategory(request);
        }

        public async Task<bool> DeleteProduct(ProductUpdateModel request)
        {
            return await _productRepo.DeleteProduct(request);
        }

        public async Task<List<CategoryViewModel>> GetCategoryByShopId(long shopId)
        {
            return await _productRepo.GetCategoryByShopId(shopId);
        }

        public async Task<List<CategoryViewModel>> GetParentCategory(long shopId)
        {
            return await _productRepo.GetParentCategory(shopId);
        }

        public async Task<List<ProductViewModel>> GetProduct(long shopId)
        {
            return await _productRepo.GetProduct(shopId);
        }

        public async Task<ProductDetailModel> GetProductDetails(long productId)
        {
            return await _productRepo.GetProductDetails(productId);
        }

        public async Task<CategoryUpdateModel> GetUpdateCategory(long categoryId)
        {
            return await _productRepo.GetUpdateCategory(categoryId);
        }

        public async Task<ProductUpdateModel> GetUpdateProduct(long productId, long categoryId, long shopId)
        {
            return await _productRepo.GetUpdateProduct(productId,categoryId, shopId);
        }

        public async Task<bool> UpdateCategory(CategoryUpdateModel request)
        {
            return await _productRepo.UpdateCategory(request);

        }

        public async Task<bool> UpdateProduct(ProductUpdateModel request)
        {
            return await _productRepo.UpdateProduct(request);
        }
    }
}
