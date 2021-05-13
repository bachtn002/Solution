using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class TCustomer
    {
        public long CustomerId { get; set; }
        public long UserId { get; set; }
        public sbyte GenderId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public sbyte CustomerTypeId { get; set; }
        public ulong IsDelete { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public DateTime? ModifiedUtcDate { get; set; }
    }
}
