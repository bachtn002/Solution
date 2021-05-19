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
        public Task<List<ProductViewModel>> GetProductByShopId(long shopId);
        public Task<bool> CreateProduct(ProductCreateModel request);
        public Task<bool> DeleteProduct(long productId);
        public Task<bool> UpdateProduct(ProductUpdateModel request);
        public Task<ProductUpdateModel> GetUpdateProduct(long productId);
        public Task<ProductDetailModel> GetProductDetails(long productId);

    }
}
