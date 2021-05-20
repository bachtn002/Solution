using Microsoft.AspNetCore.Mvc;
using Repository.Model.CategoryModel;
using Repository.Model.ProductModel;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
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
                ModelState.AddModelError(string.Empty, "Created Failed");
                return View();
            }
            return RedirectToAction("Index", "Shop");
        }

        [HttpGet] 
        public IActionResult CreateCategory(long shopId)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateModel request)
        {
            var result = await _productService.CreateCategory(request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "Created Failed");
                return View(request);
            }
            return RedirectToAction("Index", "Shop");
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(long shopId, long categoryId)
        {
            ViewBag.ShopId = shopId;
            ViewBag.CategoryId = categoryId;
            var result = await _productService.GetProduct(shopId, categoryId);
            return View(result);
        }

        
    }
}
