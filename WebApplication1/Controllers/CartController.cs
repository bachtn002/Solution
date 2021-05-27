using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Model.CartModel;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartServie;
        public CartController(ICartService cartService)
        {
            _cartServie = cartService;
        }
        [HttpGet]
        public async Task<IActionResult> CreateCart(long shopId)
        {
            var result1 = await _cartServie.GetProduct(shopId);
            ViewBag.ListProduct = result1.Select(x => new SelectListItem()
            {
                Value = x.ProductId.ToString(),
                Text = x.Name
            });

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCart(CartCreateModel request)
        {
            var result1 = await _cartServie.CreateCart(request);
            var result2 = await _cartServie.GetProduct(request.ShopId);
            if (result1 == false)
            {
                
                ViewBag.ListProduct = result2.Select(x => new SelectListItem()
                {
                    Value = x.ProductId.ToString(),
                    Text = x.Name
                });
                return View();
            }
            return RedirectToAction("GetCart","Cart", new {shopId=request.ShopId});
        }

        [HttpGet]
        public async Task<IActionResult> GetCart(long shopId)
        {
            ViewBag.ShopId = shopId;
            var result = await _cartServie.GetCart();
            return View(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> DeleteCart(long shopId)
        {
            var result = await _cartServie.DeleteCart();
            if (result == false)
            {
                return RedirectToAction("", "", new { });
            }
            return RedirectToAction("Index","Shop",new { });
        }
    }
}
