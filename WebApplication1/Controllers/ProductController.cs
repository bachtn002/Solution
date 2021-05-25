using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Model.CategoryModel;
using Repository.Model.ProductModel;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        
        private readonly IProductService _productService;
        
        public ProductController(IProductService productService)
        {
            _productService = productService;
            
        }

        
        public async Task<IActionResult> Index(long shopId)
        {
            ViewBag.ShopId = shopId;
            var result = await _productService.GetCategoryByShopId(shopId);
            return View(result);
        }

        [HttpGet]
        public IActionResult CreateProduct(long shopId, long categoryId)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateModel request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Created Failed");
                return View(request);
            }
            var result = await _productService.CreateProduct(request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "That name product is taken. Try another");
                return View();
            }
            return RedirectToAction("GetProduct", "Product", new { shopId = request.ShopId, categoryId = request.CategoryId });
        }

        [HttpGet]
        public IActionResult CreateCategory(long shopId)
        {
            ViewBag.ShopId = shopId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateModel request)
        {
            var result = await _productService.CreateCategory(request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "That name category is taken. Try another");
                return View(request);
            }
            return RedirectToAction("Index", "Product", new { shopId = request.ShopId });
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(long shopId, long categoryId)
        {
            ViewBag.ShopId = shopId;
            ViewBag.CategoryId = categoryId;
            var result = await _productService.GetProduct(shopId, categoryId);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(long categoryId)
        {
            var result = await _productService.GetUpdateCategory(categoryId);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(CategoryUpdateModel request)
        {
            var result = await _productService.DeleteCategory(request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "Delete Failed");
                return View(request);
            }
            return RedirectToAction("Index", "Product", new {shopId=request.ShopId });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(long categoryId)
        {
            var result = await _productService.GetUpdateCategory(categoryId);
            ViewBag.ListTest = result.CategoryParentList.Select(x => new SelectListItem()
            {
                Value=x.ParentId.ToString(),
                Text=x.NameCategory
            });
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateModel request)
        {
            var result = await _productService.UpdateCategory(request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "That name category is taken");
                return View(request);
            }
            return RedirectToAction("Index","Product",new {shopId=request.ShopId, categoryId=request.CategoryId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(long productId, long categoryId, long shopId)
        {
            var result = await _productService.GetUpdateProduct(productId, categoryId,shopId);
            
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductUpdateModel request)
        {
            var result = await _productService.DeleteProduct(request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "Delete Failed");
                return View(request);
            }
            return RedirectToAction("GetProduct", "Product", new {shopId=request.ShopId, categoryId=request.CategoryId});
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(long productId, long categoryId, long shopId)
        {
            var result = await _productService.GetUpdateProduct(productId, categoryId, shopId);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductUpdateModel request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Update Failed");
                return View(request);
            }
            var result = await _productService.UpdateProduct(request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "That name product is taken. Try another");
                return View(request);
            }
            return RedirectToAction("", "", new { });
        }

        [HttpGet]
        public async Task<IActionResult> DetailProduct(long productId)
        {
            var result = await _productService.GetProductDetails(productId);
            return View(result);
        }

    }
}
