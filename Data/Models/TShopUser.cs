using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class TShopUser
    {
        public long ShopUserId { get; set; }
        public long ShopId { get; set; }
        public long UserId { get; set; }
        public int RoleId { get; set; }
        public ulong IsDelete { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public DateTime? RetiredUtcDate { get; set; }
    }
}
