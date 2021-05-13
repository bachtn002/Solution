
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly QL_CTVContext _dataDbContext;
        public UserRepo(QL_CTVContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public async Task<bool> RegisterUser(UserRegisterModel request)
        {
            var user = await _dataDbContext.TUsers.FindAsync(request.Mobile);
            if (user != null)
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
                RoleId = 1,
                CreatedUtcDate = DateTime.Now,

            });
            var result = await _dataDbContext.SaveChangesAsync();
            if (result < 0)
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


    }
}
