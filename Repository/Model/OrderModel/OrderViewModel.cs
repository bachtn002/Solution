using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model.OrderModel
{
    public class OrderViewModel
    {
        public string FullName { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public sbyte OrderStatusId { get; set; }
        public sbyte PaymentMethodId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedUtcDate { get; set; }
    }
}
