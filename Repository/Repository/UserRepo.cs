using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Interface;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly QL_CTVContext _dataDbContext;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepo(QL_CTVContext dataDbContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _dataDbContext = dataDbContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> RegisterUser(UserRegisterModel request)
        {
            
            if (await _dataDbContext.TUsers.AnyAsync(x => x.Mobile == request.Mobile))
            {
                return false;
            }

            await _dataDbContext.TUsers.AddAsync(new TUser()
            {
                Mobile = request.Mobile,
                PasswordHash = request.PasswordHash,
                FullName = request.FullName,
                GenderId = request.GenderId,
                Avatar = request.Avatar,
                DateOfBirth = request.DateOfBirth,
                UserStatusId = 1,
                CreatedUtcDate = DateTime.Now

            });
            var result = await _dataDbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<List<UserViewModel>> GetAllUser()
        {
            var query = from p in _dataDbContext.TUsers
                        join g in _dataDbContext.TmGenders on p.GenderId equals g.GenderId
                        join r in _dataDbContext.TRoles on p.RoleId equals r.RoleId
                        select new { p, g, r };
            var data = await query.Select(x => new UserViewModel()
            {
                UserId = x.p.UserId,
                Mobile = x.p.Mobile,
                FullName = x.p.FullName,
                DateOfBirth = x.p.DateOfBirth,
                GenderName = x.g.GenderName,
                RoleName = x.r.RoleName
            }).ToListAsync();
            return data;
        }

        public async Task<string> LoginUser(UserLoginModel request)
        {
            var user = await _dataDbContext.TUsers.FirstOrDefaultAsync(x => x.Mobile == request.Mobile);
            var data = await _dataDbContext.TUsers.Where(x => x.Mobile.Equals(request.Mobile) && x.PasswordHash.Equals(request.Password)).ToListAsync();
            if (data.Count() > 0)
            {
                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name,user.FullName),
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.GivenName, user.Mobile)
                };
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var token = new JwtSecurityToken
                    (
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.UtcNow.AddDays(1),
                        claims: claims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
                return tokenstring;
            }
            else
            {
                return null;
            }
        }
        public long GetUserId()
        {
            var userId = Convert.ToInt64(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return userId;
        }

}
}
