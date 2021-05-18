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
        public async Task<IActionResult> Delete(long shopId)
        {
            var result = await _shopService.GetUpdateShop(shopId);
            
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ShopCreateModel request)
        {
            var result = await _shopService.DeleteShop(request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "Delete Failed");
                return View();
            }
            return RedirectToAction("Index","Shop");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long shopId)
        {
            var result = await _shopService.GetUpdateShop(shopId);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ShopCreateModel request)
        {
            var result = await _shopService.UpdateShop(request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "Update Failed");
                return View();
            }
            return RedirectToAction("Index","Shop");
        }

        [HttpGet]
        public async Task<IActionResult> ShowCollab(long shopId)
        {
            var result = await _shopService.GetCollabByShopId(shopId);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCollab(long shopId)
        {
            var result = await _shopService.GetCollab(shopId);
            var user = new UserRegisterModel()
            {
                ShopId=result.ShopId
            };
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCollab(UserRegisterModel request)
        {
            var result = await _shopService.CreateCollab(request.ShopId, request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "Mobile is already");
                return View();
            }
            return RedirectToAction("ShowCollab", "Shop");
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

            if (result == true && resultInsert == true)
                return RedirectToAction("Index");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(long shopId)
        {
            var result = await _shopService.GetShopDetails(shopId);
            return View(result);
        }
    }
}
