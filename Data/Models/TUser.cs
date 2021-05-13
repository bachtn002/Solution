using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Data.Models
{
    public partial class TUser
    {
        
        public long UserId { get; set; }
        public string Mobile { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public sbyte GenderId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public sbyte UserStatusId { get; set; }
        public int RoleId { get; set; }
        public ulong IsDelete { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public DateTime ModifiedUtcDate { get; set; }
    }
}
