using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model.ShopModel
{
    public class CollabCreateModel
    {
        [Display(Name ="Name")]
        [Required(ErrorMessage ="FullName is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"(^\d+$)", ErrorMessage = "Mobile contains only number 0-9")]
        [Phone(ErrorMessage = "Mobile contains only number 0-9")]
        public string Mobile { get; set; }
        public long ShopId { get; set; }

        [Display(Name = "Gender")]
        public sbyte GenderId { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "DOB is required")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Choose a photo for your avatar")]
        [Required(ErrorMessage = "Choose a photo")]
        public string Avatar { get; set; }
    }
}
