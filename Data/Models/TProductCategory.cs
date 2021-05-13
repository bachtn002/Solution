using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class TProductCategory
    {
        public long ProductCategoryId { get; set; }
        public long ProductId { get; set; }
        public long CategoryId { get; set; }
        public ulong IsDelete { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public DateTime? ModifiedUtcDate { get; set; }
    }
}
