using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class UserCreateModel
    {
        public string Mobile { get; set; }
        [Display(Name = "Name")]
        public string FullName { get; set; }
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Gender")]
        public sbyte GenderId { get; set; }
        [Display(Name = "Role")]
        public int RoleId { get; set; }
        public DateTime CreatedUtcDate { get; set; }
    }
}
