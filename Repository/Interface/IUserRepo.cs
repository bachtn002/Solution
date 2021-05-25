using Microsoft.AspNetCore.Http;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserRepo
    {
        public Task<string> LoginUser(UserLoginModel request);
        public Task<UserViewModel> GetUserDetails(long userId);
        public Task<bool> RegisterUser(UserRegisterModel request);
        public long GetUserId();
        
    }
}
