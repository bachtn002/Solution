using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class TCategory
    {
        public long CategoryId { get; set; }
        public ulong ShopId { get; set; }
        public string NameCategory { get; set; }
        public int? ParentId { get; set; }
        public ulong IsDelete { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public DateTime? ModifiedUtcDate { get; set; }
    }
}
