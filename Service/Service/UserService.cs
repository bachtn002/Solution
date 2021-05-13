using Repository.Interface;
using Repository.Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public Task<bool> CreateUser(UserCreateModel request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserViewModel>> GetAllUser()
        {
            return await _userRepo.GetAllUser();
        }

        public async Task<string> LoginUser(UserLoginModel request)
        {
            return await _userRepo.LoginUser(request);
        }

        public async Task<bool> RegisterUser(UserRegisterModel request)
        {
            return await _userRepo.RegisterUser(request);
        }
    }
}
