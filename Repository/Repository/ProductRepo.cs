using Repository.Interface;
using Repository.Model.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ProductRepo : IProductRepo
    {
        public Task<bool> CreateProduct(ProductCreateModel request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(long productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetProductByShopId(long shopId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDetailModel> GetProductDetails(long productId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductUpdateModel> GetUpdateProduct(long productId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProduct(ProductUpdateModel request)
        {
            throw new NotImplementedException();
        }
    }
}
