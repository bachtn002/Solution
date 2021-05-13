using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class TDebt
    {
        public long DebtId { get; set; }
        public long UserId { get; set; }
        public long ShopId { get; set; }
        public int OrderId { get; set; }
        public decimal? Advance { get; set; }
        public DateTime? DeadlinedUtcDate { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public DateTime? ModifiedUtcDate { get; set; }
    }
}
