using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage ="Mobile is required")]
        [RegularExpression(@"(^\d+$)",ErrorMessage ="Mobile contains only number 0-9")]
        public string Mobile { get; set; }

        [Display(Name="Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is required")]
        [MinLength(6,ErrorMessage ="Password at least 6 character")]
        public string PasswordHash { get; set; }
        
        [Display(Name = "Name")]
        public string FullName { get; set; }
        [Display(Name = "Gender")]
        public sbyte GenderId { get; set; }
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Choose a photo for your avatar")]
        public string Avatar { get; set; }
        
       
    }
}
