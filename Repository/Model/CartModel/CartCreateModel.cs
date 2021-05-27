using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model.CartModel
{
    public class CartCreateModel
    {
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public long ShopId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public double DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
    }
}
