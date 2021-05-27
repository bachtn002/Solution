using Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            if (await _dataDbContext.TCategories.AnyAsync(x => x.NameCategory == request.NameCategory 
            && x.ShopId == request.ShopId))
            {
                return false;
            }
            else
            {
                await _dataDbContext.TCategories.AddAsync(new TCategory()
                {
                    ShopId = request.ShopId,
                    NameCategory = request.NameCategory,
                    ParentId=request.ParentId,
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
            if (await _dataDbContext.TProducts.AnyAsync(x => x.Name == request.Name && x.IsDelete == 0
            && x.ShopId == request.ShopId))
            {
                return false;
            }
            await _dataDbContext.TProducts.AddAsync(new TProduct()
            {
                ShopId = request.ShopId,
                Name = request.Name,
                Price = request.Price,
                Images = request.Images,
                Properties = request.Properties,
                Description = request.Description,
                ProductStatusId = 1,
                CreatedUtcDate = DateTime.UtcNow
            });
            var result = await _dataDbContext.SaveChangesAsync();
            await _dataDbContext.TProductCategories.AddAsync(new TProductCategory()
            {
                CategoryId = request.CategoryId,
                ProductId = await _dataDbContext.TProducts.Select(x => x.ProductId).MaxAsync(),
                CreatedUtcDate = DateTime.UtcNow
            });
            var result1 = await _dataDbContext.SaveChangesAsync();
            if (result <= 0 && result1 <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteCategory(CategoryUpdateModel request)
        {
            var category = await _dataDbContext.TCategories.SingleOrDefaultAsync(x => x.CategoryId == request.CategoryId);
            if (category != null)
            {
                category.IsDelete = 1;
                category.ModifiedUtcDate = DateTime.UtcNow;
                _dataDbContext.TCategories.Update(category);
            }
            var result = await _dataDbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;
        }


        public async Task<bool> DeleteProduct(ProductUpdateModel request)
        {
            var product = await _dataDbContext.TProducts.FirstOrDefaultAsync(x => x.ProductId == request.ProductId);
            var productCategory = await _dataDbContext.TProductCategories.FirstOrDefaultAsync(x => x.ProductId == request.ProductId && x.CategoryId == request.CategoryId);
            if (product != null)
            {
                product.IsDelete = 1;
                product.ModifiedUtcDate = DateTime.Now;
                productCategory.IsDelete = 1;
                productCategory.ModifiedUtcDate = DateTime.Now;
                _dataDbContext.TProducts.Update(product);
                _dataDbContext.TProductCategories.Update(productCategory);
            }
            var result = await _dataDbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;

        }

        public async Task<List<CategoryViewModel>> GetCategoryByShopId(long shopId)
        {
            var query = from c in _dataDbContext.TCategories
                        where c.ShopId == shopId && c.IsDelete == 0
                        select new { c };
            var data = await query.Select(x => new CategoryViewModel()
            {
                CategoryId = x.c.CategoryId,
                ShopId = x.c.ShopId,
                NameCategory = x.c.NameCategory,
                ParentCategory = (from c in _dataDbContext.TCategories
                                  where c.CategoryId == x.c.ParentId && c.IsDelete == 0
                                  select c.NameCategory).FirstOrDefault()
            }).ToListAsync();
            return data;
        }

        public async Task<List<CategoryViewModel>> GetParentCategory(long shopId)
        {
            var query = from c in _dataDbContext.TCategories
                        where c.ShopId == shopId
                        select new {c };
            var result = await query.Select(x => new CategoryViewModel()
            {
                NameCategory=x.c.NameCategory,
                CategoryId=x.c.CategoryId
            }).ToListAsync();
            return result;
        }

        public async Task<List<ProductViewModel>> GetProduct(long shopId)
        {
            var query = from s in _dataDbContext.TShops
                        join p in _dataDbContext.TProducts on s.ShopId equals p.ShopId
                        join pc in _dataDbContext.TProductCategories on p.ProductId equals pc.ProductId
                        join c in _dataDbContext.TCategories on pc.CategoryId equals c.CategoryId
                        join tm in _dataDbContext.TmProductStatuses on p.ProductStatusId equals tm.ProductStatusId
                        where s.ShopId == shopId && p.IsDelete == 0
                        select new { s, p, pc, c, tm };

            var data = await query.Select(x => new ProductViewModel()
            {
                ProductId = x.p.ProductId,
                CategoryId = x.pc.CategoryId,
                Name = x.p.Name,
                NameCategory = x.c.NameCategory,
                Price = x.p.Price,
                ProductStatusName = x.tm.ProductStatusName,
                ShopId=x.s.ShopId
            }).ToListAsync();
            return data;
        }

        public async Task<List<ProductViewModel>> GetProductByShopId(long shopId)
        {
            var query = from p in _dataDbContext.TProducts
                        join pc in _dataDbContext.TProductCategories on p.ProductId equals pc.ProductId
                        join c in _dataDbContext.TCategories on pc.CategoryId equals c.CategoryId
                        where p.ShopId == shopId && c.ShopId == shopId
                        select new { p, c };
            var data = await query.Select(x => new ProductViewModel()
            {

            }).ToListAsync();
            return data;
        }

        public async Task<ProductDetailModel> GetProductDetails(long productId)
        {
            var product = await _dataDbContext.TProducts.FirstOrDefaultAsync(x => x.ProductId == productId);
            var result = new ProductDetailModel()
            {
                ProductId = product.ProductId,
                NameProduct = product.Name,
                Price = product.Price,
                Images = product.Images,
                Properties = product.Properties,
                Description = product.Description,
                CreatedUtcDate = product.CreatedUtcDate,
                ProductStatusName = (from p in _dataDbContext.TProducts
                                     join tm in _dataDbContext.TmProductStatuses on p.ProductStatusId equals tm.ProductStatusId
                                     where tm.ProductStatusId == product.ProductStatusId
                                     select tm.ProductStatusName).FirstOrDefault(),
                NameShop = (from p in _dataDbContext.TProducts
                            join s in _dataDbContext.TShops on p.ShopId equals s.ShopId
                            where s.ShopId == product.ShopId
                            select s.Name).FirstOrDefault(),
                NameCategory = (from p in _dataDbContext.TProducts
                                join pc in _dataDbContext.TProductCategories on p.ProductId equals pc.ProductId
                                join c in _dataDbContext.TCategories on pc.CategoryId equals c.CategoryId
                                where p.ProductId == product.ProductId
                                select c.NameCategory).FirstOrDefault()
            };
            return result;
        }

        public async Task<CategoryUpdateModel> GetUpdateCategory(long categoryId)
        {
            var category = await _dataDbContext.TCategories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);

            var categoryVM = new CategoryUpdateModel()
            {
                CategoryId = category.CategoryId,
                ShopId = category.ShopId,
                NameCategory = category.NameCategory,
                ParentId = category.ParentId.GetValueOrDefault()
            };
            var query = from c in _dataDbContext.TCategories
                        where c.ShopId == category.ShopId
                        select new { c };
            var result = await query.Select(x => new CategoryParent()
            {
                NameCategory = x.c.NameCategory,
                ParentId = (int)x.c.CategoryId
            }).ToListAsync();
            /*var items = _dataDbContext.TCategories.Where(x => x.CategoryId == category.CategoryId)
                .Select(x => new SelectListItem
                {
                    Value=x.CategoryId.ToString(),
                    Text=x.NameCategory
                }).ToList();*/
            /*categoryVM.CategoryParentList = items;*/
            categoryVM.CategoryParentList = result;
            return categoryVM;
        }

        public async Task<ProductUpdateModel> GetUpdateProduct(long productId, long categoryId, long shopId)
        {
            var product = await _dataDbContext.TProducts.FirstOrDefaultAsync(x => x.ProductId == productId);
            var result = new ProductUpdateModel()
            {
                Name = product.Name,
                Price = product.Price,
                ProductStatusId = product.ProductStatusId,
                Description = product.Description,
                Images = product.Images,
                Properties = product.Properties,
                ShopId=product.ShopId
            };
            return result;
        }

        public async Task<bool> UpdateCategory(CategoryUpdateModel request)
        {
            if (await _dataDbContext.TCategories.AnyAsync(x => x.NameCategory == request.NameCategory
             && x.ShopId == request.ShopId && x.CategoryId != request.CategoryId))
            {
                return false;
            }
            var category = await _dataDbContext.TCategories.FirstOrDefaultAsync(x => x.CategoryId == request.CategoryId);
            if (category != null)
            {

                category.NameCategory = request.NameCategory;
                category.ParentId = request.ParentId;
                category.ModifiedUtcDate = DateTime.UtcNow;
            }
            _dataDbContext.TCategories.Update(category);
            var result = await _dataDbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateProduct(ProductUpdateModel request)
        {
            if (await _dataDbContext.TProducts.AnyAsync(x => x.Name == request.Name && x.ProductId != request.ProductId && x.IsDelete == 0))
            {
                return false;
            }
            var product = await _dataDbContext.TProducts.FirstOrDefaultAsync(x => x.ProductId == request.ProductId);
            product.Name = request.Name;
            product.Price = request.Price;
            product.ProductStatusId = request.ProductStatusId;
            product.Images = request.Images;
            product.Properties = request.Properties;
            product.Description = request.Description;
            product.ModifiedUtcDate = DateTime.UtcNow;
            _dataDbContext.TProducts.Update(product);
            var result = await _dataDbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
