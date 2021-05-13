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
        public readonly IConfiguration configuration;
        public LoginController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            this.configuration = configuration;
        }
        [HttpGet]
        [AllowAnonymous]
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
                ModelState.AddModelError("", "Incorrect mobile or password");
                return View();
            }
            else
            {
                var userPrincipal = this.ValidateToken(token);
                var authenProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(30),
                    IsPersistent = false
                };
                HttpContext.Session.SetString("Token", token);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal, authenProperties);

                return RedirectToAction("Index", "Home");
            }
            
        }
        private ClaimsPrincipal ValidateToken(string ValidToken)
        {
            IdentityModelEventSource.ShowPII = true;
            SecurityToken securityToken;
            TokenValidationParameters tokenValidatio = new TokenValidationParameters();
            tokenValidatio.ValidateLifetime = true;
            tokenValidatio.ValidAudience = configuration["JWT:ValidAudience"];
            tokenValidatio.ValidIssuer = configuration["JWT:ValidIssuer"];
            tokenValidatio.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(ValidToken, tokenValidatio, out securityToken);
            return claimsPrincipal;
        }
    }
}
