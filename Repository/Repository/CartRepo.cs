using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Model.CartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CartRepo : ICartRepo
    {
        private readonly QL_CTVContext _dataDbContext;
        private readonly IUserRepo _userRepo;
        public CartRepo(QL_CTVContext dataDbContext, IUserRepo userRepo)
        {
            _dataDbContext = dataDbContext;
            _userRepo = userRepo;
        }
        public async Task<bool> CreateCart(CartCreateModel request)
        {
            var cartItem = await _dataDbContext.TCarts.FirstOrDefaultAsync(x => x.ProductId == request.ProductId && x.OrderId == null && x.UserId == _userRepo.GetUserId() && x.IsDelete == 0);
            if (cartItem != null)
            {
                cartItem.Qty = request.Qty + cartItem.Qty;
                cartItem.DiscountAmount = ((from p in _dataDbContext.TProducts
                                            where p.ProductId == request.ProductId
                                            select p.Price).FirstOrDefault()) * (request.Qty) * Convert.ToDecimal(((request.DiscountRate) / 100));
                cartItem.Amount = ((from p in _dataDbContext.TProducts
                                    where p.ProductId == request.ProductId
                                    select p.Price).FirstOrDefault()) * (cartItem.Qty) - (((from p in _dataDbContext.TProducts
                                                                                            where p.ProductId == request.ProductId
                                                                                            select p.Price).FirstOrDefault()) * (request.Qty) * Convert.ToDecimal(((request.DiscountRate) / 100)));
                
                _dataDbContext.TCarts.Update(cartItem);
            }
            else
            {
                await _dataDbContext.TCarts.AddAsync(new TCart()
                {
                    ProductId = request.ProductId,
                    UserId = _userRepo.GetUserId(),
                    Qty = request.Qty,
                    Price = (from p in _dataDbContext.TProducts
                             where p.ProductId == request.ProductId
                             select p.Price).FirstOrDefault(),
                    DiscountRate = request.DiscountRate,
                    DiscountAmount = ((from p in _dataDbContext.TProducts
                                       where p.ProductId == request.ProductId
                                       select p.Price).FirstOrDefault())*(request.Qty)*Convert.ToDecimal(((request.DiscountRate) / 100)),
                    Amount = ((from p in _dataDbContext.TProducts
                               where p.ProductId == request.ProductId
                               select p.Price).FirstOrDefault()) * (request.Qty)-(((from p in _dataDbContext.TProducts
                                                                                    where p.ProductId == request.ProductId
                                                                                    select p.Price).FirstOrDefault()) * (request.Qty) * Convert.ToDecimal(((request.DiscountRate) / 100))),
                    Note = request.Note,
                    CreatedUtcDate = DateTime.UtcNow
                });
            }
            var result = await _dataDbContext.SaveChangesAsync();
            if (result <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteCart()
        {
            /*var query = from c in _dataDbContext.TCarts
                        
                        where c.OrderId == null && c.UserId == _userRepo.GetUserId() && c.IsDelete == 0
                        select new { c };
            var result = await query.Select(x => new CartViewModel()
            {
                IsDelete = 1,
                ModifiedUtcDate = DateTime.UtcNow
            }).ToListAsync();*/
            /*var cart = await _dataDbContext.TCarts.FirstOrDefaultAsync(x => x.IsDelete == 0 &&
            x.UserId == _userRepo.GetUserId() && x.OrderId == null);
            if (cart != null)
            {
                cart.IsDelete = 1;
                cart.ModifiedUtcDate = DateTime.UtcNow;
            }
            _dataDbContext.Update(cart);*/
            var cart = await _dataDbContext.TCarts.Where(x => x.UserId == _userRepo.GetUserId()
             && x.IsDelete == 0 && x.OrderId == null).ToListAsync();
            cart.ForEach(x =>
            {
                x.IsDelete = 1;
                x.ModifiedUtcDate = DateTime.UtcNow;
            });
            var res = await _dataDbContext.SaveChangesAsync();
            if (res <= 0) return false;
            return true;

        }

        public async Task<List<CartViewModel>> GetCart()
        {
            var query = from c in _dataDbContext.TCarts
                        join p in _dataDbContext.TProducts on c.ProductId equals p.ProductId
                        where c.OrderId == null && c.UserId == _userRepo.GetUserId() && c.IsDelete == 0
                        select new { c, p };
            var result = await query.Select(x => new CartViewModel()
            {
                ShopId = x.p.ShopId,
                NameProduct = x.p.Name,
                Qty = x.c.Qty,
                Price = x.c.Price,
                DiscountRate = x.c.DiscountRate,
                DiscountAmount = x.c.DiscountAmount,
                Amount = x.c.Amount,
                Note = x.c.Note,
                CreatedUtcDate = x.c.CreatedUtcDate
            }).ToListAsync();
            return result;
        }
    }
}
