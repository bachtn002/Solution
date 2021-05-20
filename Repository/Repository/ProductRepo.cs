using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Model.CategoryModel;
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
        private readonly QL_CTVContext _dataDbContext;
        public ProductRepo(QL_CTVContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public async Task<bool> CreateCategory(CategoryCreateModel request)
        {
            var category = await _dataDbContext.TCategories.FirstOrDefaultAsync(x => x.NameCategory == request.NameCategory && x.IsDelete==0);
            if (category != null)
            {
                return false;
            }
            else
            {
                await _dataDbContext.TCategories.AddAsync(new TCategory()
                {
                    ShopId = request.ShopId,
                    NameCategory = request.NameCategory,
                    CreatedUtcDate = DateTime.UtcNow
                });
                var result = await _dataDbContext.SaveChangesAsync();
                if (result <= 0)
                {
                    return false;
                }
                return true;
            }
        }

        public async Task<bool> CreateProduct(ProductCreateModel request)
        {
            await _dataDbContext.TProducts.AddAsync(new TProduct()
            {
                ShopId=request.ShopId,
                Name=request.Name,
                Price=request.Price,
                Images=request.Images,
                Properties=request.Properties,
                Description=request.Description,
                ProductStatusId=1,
                CreatedUtcDate=DateTime.UtcNow
            });
            await _dataDbContext.TProductCategories.AddAsync(new TProductCategory()
            {
                CategoryId = request.CategoryId,
                ProductId = await _dataDbContext.TProducts.Select(x => x.ProductId).MaxAsync() + 1,
                CreatedUtcDate=DateTime.UtcNow
            }) ;
            var result = await _dataDbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;
        }

        public Task<bool> DeleteProduct(long productId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryViewModel>> GetCategoryByShopId(long shopId)
        {
            var query = from c in _dataDbContext.TCategories
                        where c.ShopId == shopId
                        select new { c };
            var data = await query.Select(x => new CategoryViewModel()
            {
                CategoryId=x.c.CategoryId,
                ShopId=x.c.ShopId,
                NameCategory=x.c.NameCategory
            }).ToListAsync();
            return data;
        }

        public async Task<List<ProductViewModel>> GetProduct(long shopId, long categoryId)
        {
            var query = from s in _dataDbContext.TShops
                        join p in _dataDbContext.TProducts on s.ShopId equals p.ShopId
                        join pc in _dataDbContext.TProductCategories on p.ProductId equals pc.ProductId
                        join c in _dataDbContext.TCategories on pc.CategoryId equals c.CategoryId
                        join tm in _dataDbContext.TmProductStatuses on p.ProductStatusId equals tm.ProductStatusId
                        where s.ShopId == shopId && c.CategoryId == categoryId && pc.IsDelete == 0
                        select new {s,p,pc,c,tm };
            var data = await query.Select(x => new ProductViewModel()
            {
                ProductId=x.p.ProductId,
                Name=x.p.Name,
                NameCategory=x.c.NameCategory,
                Price=x.p.Price,
                Images=x.p.Images,
                ProductStatusName=x.tm.ProductStatusName,
                Properties=x.p.Properties,
                Description=x.p.Description
            }).ToListAsync();
            return data;
        }

        public async Task<List<ProductViewModel>> GetProductByShopId(long shopId)
        {
            var query = from p in _dataDbContext.TProducts
                        join pc in _dataDbContext.TProductCategories on p.ProductId equals pc.ProductId
                        join c in _dataDbContext.TCategories on pc.CategoryId equals c.CategoryId
                        where p.ShopId == shopId && c.ShopId==shopId
                        select new { p,c };
            var data = await query.Select(x => new ProductViewModel()
            {
                
            }).ToListAsync();
            return data;
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
