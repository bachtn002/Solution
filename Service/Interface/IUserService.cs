using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserService
    {
        public Task<UserViewModel> GetUserDetails(long userId);
        public Task<bool> CreateUser(UserCreateModel request);
        public Task<bool> RegisterUser(UserRegisterModel request);
        public Task<string> LoginUser(UserLoginModel request);
        public long GetUserId();
    }
}
