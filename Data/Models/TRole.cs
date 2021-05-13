using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class TRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string GrantAccess { get; set; }
        public ulong IsSystemRole { get; set; }
        public ulong IsDelete { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public DateTime? ModifiedUtcDate { get; set; }
    }
}
