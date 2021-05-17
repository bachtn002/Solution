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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateCollab()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCollab(UserRegisterModel request)
        {
            var result = await _shopService.CreateCollab(request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "Mobile already exists");
                return View();
            }
            return RedirectToAction("Index","Shop");
        }

        [HttpGet]
        public async Task<IActionResult> Show()
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
            var resultInsert = await _shopService.InsertShopUser();

            if (result == true && resultInsert==true)
                return RedirectToAction("Index");
            return View();
        }

        
    }
}
