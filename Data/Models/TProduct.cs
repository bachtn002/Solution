using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class TProduct
    {
        public long ProductId { get; set; }
        public long ShopId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Images { get; set; }
        public string Properties { get; set; }
        public string Description { get; set; }
        public sbyte ProductStatusId { get; set; }
        public ulong IsDelete { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public DateTime? ModifiedUtcDate { get; set; }
    }
}
