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

        [Display(Name ="Confirm password")]
        [DataType(DataType.Password)]
        [Compare("PasswordHash",ErrorMessage ="Confirm password incorrect")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage ="Full name is required")]
        public string FullName { get; set; }

        [Display(Name = "Gender")]
        public sbyte GenderId { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="DOB is required")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Choose a photo for your avatar")]
        [Required(ErrorMessage ="Choose a photo")]
        public string Avatar { get; set; }
        
       
    }
}
