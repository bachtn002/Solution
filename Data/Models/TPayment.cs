using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class TPayment
    {
        public long PaymentId { get; set; }
        public long OrderId { get; set; }
        public sbyte PaymentStatusId { get; set; }
        public decimal PaymentAmount { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public DateTime? ModifiedUtcDate { get; set; }
    }
}
