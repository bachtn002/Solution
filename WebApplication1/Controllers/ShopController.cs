using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var result = await _shopService.GetShopUser();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ShopCreateModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _shopService.CreateShop(request);

            if (result == true)
                return RedirectToAction("Index");
            return View();
        }
    }
}
