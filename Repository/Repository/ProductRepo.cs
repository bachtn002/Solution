﻿using Data.Models;
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
            if (await _dataDbContext.TCategories.AnyAsync(x => x.NameCategory == request.NameCategory && x.ShopId == request.ShopId))
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
            var category = await _dataDbContext.TCategories.FirstOrDefaultAsync(x => x.CategoryId == request.CategoryId);
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

        public Task<bool> DeleteProduct(long productId)
        {
            throw new NotImplementedException();
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
                NameCategory = x.c.NameCategory
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
                        select new { s, p, pc, c, tm };
            var data = await query.Select(x => new ProductViewModel()
            {
                ProductId = x.p.ProductId,
                Name = x.p.Name,
                NameCategory = x.c.NameCategory,
                Price = x.p.Price,
                Images = x.p.Images,
                ProductStatusName = x.tm.ProductStatusName,
                Properties = x.p.Properties,
                Description = x.p.Description
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

        public Task<ProductDetailModel> GetProductDetails(long productId)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryUpdateModel> GetUpdateCategory(long categoryId)
        {
            var category = await _dataDbContext.TCategories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);

            var categoryVM = new CategoryUpdateModel()
            {
                CategoryId = category.CategoryId,
                ShopId = category.ShopId,
                NameCategory = category.NameCategory,
                ParentId=(int)category.ParentId
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

        public async Task<ProductUpdateModel> GetUpdateProduct(long productId)
        {
            var product = await _dataDbContext.TProducts.FirstOrDefaultAsync(x => x.ProductId == productId);
            var result = new ProductUpdateModel()
            {
                Name = product.Name,
                Price = product.Price,
                ProductStatusId = product.ProductStatusId,
                Description = product.Description,
                Images=product.Images,
                Properties=product.Properties,

            };
            return result;
        }

        public async Task<bool> UpdateCategory(CategoryUpdateModel request)
        {
            if (await _dataDbContext.TCategories.AnyAsync(x => x.NameCategory == request.NameCategory
             && x.CategoryId != request.CategoryId))
            {
                return false;
            }
            var category = await _dataDbContext.TCategories.FirstOrDefaultAsync(x => x.CategoryId == request.CategoryId);
            if (category != null)
            {
                /*category.CategoryId = request.CategoryId;*/
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
