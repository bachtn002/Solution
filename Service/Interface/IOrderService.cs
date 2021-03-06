using Repository.Model.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IOrderService
    {
        public Task<bool> CreateOrder(OrderCreateModel request);
        public Task<List<OrderViewModel>> GetOrder();
    }
}
