using Repository.Interface;
using Repository.Model.OrderModel;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        public OrderService(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public async Task<bool> CreateOrder(OrderCreateModel request)
        {
            return await _orderRepo.CreateOrder(request);
        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            return await _orderRepo.GetOrder();
        }
    }
}
