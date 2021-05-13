using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class UserLoginModel
    {
        [Required(ErrorMessage ="Mobile is required")]
        
        [RegularExpression(@"(^\d+$)", ErrorMessage ="Mobile contains only numbers 0-9")]
        public string Mobile { get; set; }
        [Required(ErrorMessage ="Password is required")]
        [MinLength(6,ErrorMessage ="Password at least 6 character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        
    }
}
