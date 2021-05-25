using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class UserViewModel
    {
        public long UserId { get; set; }
        public string Mobile { get; set; }
        [Display(Name = "Name")]
        public string FullName { get; set; }
        [Display(Name = "Gender")]
        public string GenderName { get; set; }
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Role")]
        public string RoleName { get; set; }
        public string Avatar { get; set; }
        public string UserStatusName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }
    }
}
