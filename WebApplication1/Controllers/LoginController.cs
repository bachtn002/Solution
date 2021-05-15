using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Repository.Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        public readonly IUserService _userService;
        public readonly IConfiguration _configuration;
        public LoginController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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
                ModelState.AddModelError(string.Empty, "Incorrect mobile or password");
                return View();
            }
            else
            {
                // Giải mã token 
                IdentityModelEventSource.ShowPII = true;
                SecurityToken securityToken;
                TokenValidationParameters tokenValidatio = new TokenValidationParameters();
                tokenValidatio.ValidateLifetime = true;
                tokenValidatio.ValidAudience = _configuration["JWT:ValidAudience"];
                tokenValidatio.ValidIssuer = _configuration["JWT:ValidIssuer"];
                tokenValidatio.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidatio, out securityToken);

                var authenProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(150),// trạng thái phiên xác thực
                    IsPersistent = false
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal, authenProperties);
                

                return RedirectToAction("Index", "Home");
            }

        }

    }
}
