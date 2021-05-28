using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Model.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly QL_CTVContext _dataDbContext;
        private readonly IUserRepo _userRepo;
        public OrderRepo(QL_CTVContext dataDbContext, IUserRepo userRepo)
        {
            _dataDbContext = dataDbContext;
            _userRepo = userRepo;
        }
        public async Task<bool> CreateOrder(CustomerCreateModel request)
        {
            var customer = await _dataDbContext.TCustomers.FirstOrDefaultAsync(
                x => x.Mobile == request.Mobile && x.IsDelete == 0);
            if (customer != null)
            {
                var totalAmount = await _dataDbContext.TCarts.Where(x => x.UserId == _userRepo.GetUserId()
              && x.IsDelete == 0 && x.OrderId == null).Select(x => x.Amount).SumAsync();

                var totalDiscount = await _dataDbContext.TCarts.Where(x => x.UserId == _userRepo.GetUserId()
               && x.IsDelete == 0 && x.OrderId == null).Select(x => x.DiscountAmount).SumAsync();

                await _dataDbContext.TOrders.AddAsync(new TOrder()
                {
                    UserId = _userRepo.GetUserId(),
                    ShopId = request.ShopId,
                    CustomerId = customer.CustomerId,
                    OrderStatusId = 1,
                    TotalAmount = totalAmount,
                    TotalDiscountAmount = totalDiscount,
                    CreatedUtcDate = DateTime.UtcNow,
                    PaymentMethodId = request.PaymentMethodId
                });
                var res1 = await _dataDbContext.SaveChangesAsync();
                var cart = await _dataDbContext.TCarts.Where(x => x.UserId == _userRepo.GetUserId()
             && x.IsDelete == 0 && x.OrderId == null).ToListAsync();
                cart.ForEach(x =>
                {
                    x.OrderId = _dataDbContext.TOrders.Select(x => x.OrderId).Max();
                    x.ModifiedUtcDate = DateTime.UtcNow;
                });
                var res2 = await _dataDbContext.SaveChangesAsync();
                if (res2 <=0)
                    return false;
                return true;
            }
            else
            {
                await _dataDbContext.TCustomers.AddAsync(new TCustomer()
                {
                    UserId = _userRepo.GetUserId(),
                    GenderId = request.GenderId,
                    Mobile = request.Mobile,
                    FullName = request.FullName,
                    DateOfBirth = request.DOB,
                    Address = request.Address,
                    CreatedUtcDate = DateTime.UtcNow,
                    CustomerTypeId = 1
                });
                var result = await _dataDbContext.SaveChangesAsync();

                var totalAmount = await _dataDbContext.TCarts.Where(x => x.UserId == _userRepo.GetUserId()
              && x.IsDelete == 0 && x.OrderId == null).Select(x => x.Amount).SumAsync();

                var totalDiscount = await _dataDbContext.TCarts.Where(x => x.UserId == _userRepo.GetUserId()
               && x.IsDelete == 0 && x.OrderId == null).Select(x => x.DiscountAmount).SumAsync();
                await _dataDbContext.TOrders.AddAsync(new TOrder()
                {
                    UserId = _userRepo.GetUserId(),
                    ShopId = request.ShopId,
                    CustomerId =await _dataDbContext.TCustomers.Select(c=>c.CustomerId).MaxAsync(),
                    OrderStatusId = 1,
                    TotalAmount = totalAmount,
                    TotalDiscountAmount = totalDiscount,
                    CreatedUtcDate = DateTime.UtcNow,
                    PaymentMethodId = request.PaymentMethodId
                });
                var result1 = await _dataDbContext.SaveChangesAsync();

                var cart = await _dataDbContext.TCarts.Where(x => x.UserId == _userRepo.GetUserId()
                    && x.IsDelete == 0 && x.OrderId == null).ToListAsync();
                cart.ForEach(x =>
                {
                    x.OrderId = _dataDbContext.TOrders.Select(x => x.OrderId).Max();
                    x.ModifiedUtcDate = DateTime.UtcNow;
                });
                var result2 = await _dataDbContext.SaveChangesAsync();
                if (result2 <= 0)
                    return false;
                return true;
            }
            
        }
    }
}
