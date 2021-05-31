using Microsoft.AspNetCore.Mvc;
using Repository.Model.OrderModel;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public IActionResult CreateOrder(long shopId)
        {
            ViewBag.ShopId = shopId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateModel request)
        {
            var result = await _orderService.CreateOrder(request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "Order Failed");
                return View();
            }
            return RedirectToAction("GetOrder","Order",new {});
        }
        
        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            var result = await _orderService.GetOrder();
            return View(result);
        }
    }
}
