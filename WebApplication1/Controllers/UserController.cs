using Data.EFContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            long userId = _userService.GetUserId();
            var result = await _userService.GetUserDetails(userId);
            return View(result);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _userService.RegisterUser(request);
            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "That mobile is taken");
                return View();
            }
            return RedirectToAction("Login","Login");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateModel request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _userService.CreateUser(request);
            if (result == true)
                return RedirectToAction("Index");
            return View();

        }

        
    }
}


