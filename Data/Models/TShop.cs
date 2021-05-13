using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class TShop
    {
        public long ShopId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public ulong IsDelete { get; set; }
        public sbyte ShopStatusId { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public DateTime ModifiedUtcDate { get; set; }
    }
}
