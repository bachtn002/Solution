using Repository.Model.CartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICartRepo
    {
        public Task<bool> CreateCart(CartCreateModel request);
        public Task<List<CartViewModel>> GetCart();
        public Task<bool> DeleteCart();
    }
}
