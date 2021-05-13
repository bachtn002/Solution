using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class TOrder
    {
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public long ShopId { get; set; }
        public long CustomerId { get; set; }
        public sbyte OrderStatusId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal PaymentMethodId { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public DateTime ModifiedUtcDate { get; set; }
    }
}
