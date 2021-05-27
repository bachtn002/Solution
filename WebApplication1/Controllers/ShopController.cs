using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Model;
using Repository.Model.ShopModel;
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
            return RedirectToAction("Index", "Shop");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCollab(long userId, long shopId)
        {
            var result = await _shopService.GetCollabUpdate(userId, shopId);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCollab(CollabUpdateModel request)
        {
            var result = await _shopService.DeleteCollab(request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "Delete Failed");
                return View();
            }
            return RedirectToAction("ShowCollab", "Shop", new { shopId = request.ShopId });
        }

        [HttpGet]
        public async Task<IActionResult> EditCollab(long userId, long shopId)
        {
            var result = await _shopService.GetCollabUpdate(userId, shopId);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> EditCollab(CollabUpdateModel request)
        {
            var result = await _shopService.UpdateCollab(request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "That mobile is taken. Try another");
                return View();
            }
            return RedirectToAction("ShowCollab", "Shop", new { shopId = request.ShopId });
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
                ModelState.AddModelError(string.Empty, "That name shop is taken. Try another");
                return View(request);
            }
            return RedirectToAction("Details", "Shop", new { shopId=request.ShopId});
        }

        [HttpGet]
        public async Task<IActionResult> ShowCollab(long shopId)
        {
            ViewBag.ShopId = shopId;
            var result = await _shopService.GetCollabByShopId(shopId);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCollab(long shopId)
        {

            var result = await _shopService.GetCollabCreateModel(shopId);
            var user = new CollabCreateModel()
            {
                ShopId = result.ShopId
            };
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCollab(CollabCreateModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _shopService.CreateCollab(request.ShopId, request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "That mobile is taken");
                return View();
            }
            return RedirectToAction("ShowCollab", "Shop", new { shopId = request.ShopId });
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
            if (result == false)
            {

                ModelState.AddModelError(string.Empty, "That name shop is taken. Try another");
                return View(request);
            }
            else
            {
                var resultInsert = await _shopService.InsertShopUser();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
       
        public async Task<IActionResult> Details(long shopId)
        {
            var result = await _shopService.GetShopDetails(shopId);
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult>DetailsCollab(long userId)
        {
            var result = await _shopService.GetCollabDetails(userId);
            return View(result);
        }
    }
}
