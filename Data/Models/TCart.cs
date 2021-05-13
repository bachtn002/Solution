using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class TCart
    {
        public long CartId { get; set; }
        public long UserId { get; set; }
        public long? OrderId { get; set; }
        public long ProductId { get; set; }
        public float Qty { get; set; }
        public decimal Price { get; set; }
        public double DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public ulong IsDelete { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public DateTime ModifiedUtcDate { get; set; }
    }
}
