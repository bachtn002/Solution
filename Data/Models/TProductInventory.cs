using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class TProductInventory
    {
        public long ProductInventoryId { get; set; }
        public long ProductId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public double DiscountRate { get; set; }
        public string Note { get; set; }
        public ulong IsDelete { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public DateTime? ModifiedUtcDate { get; set; }
    }
}
