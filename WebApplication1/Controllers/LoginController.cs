using Microsoft.AspNetCore.Mvc;
using Repository.Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        public readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var token = await _userService.LoginUser(request);
            if (string.IsNullOrEmpty(token))
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
