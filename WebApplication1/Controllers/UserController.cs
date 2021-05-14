using Data.EFContext;
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
            var result = await _userService.GetAllUser();
            return View(result);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _userService.RegisterUser(request);
            if (result == true)
            {
                return RedirectToAction("Login","Login");
            }
            return View();
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


