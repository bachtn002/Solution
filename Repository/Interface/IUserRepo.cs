
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
        public Task<List<UserViewModel>> GetAllUser();
        public Task<bool> RegisterUser(UserRegisterModel request);
    }
}
