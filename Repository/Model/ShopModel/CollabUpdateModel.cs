using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model.ShopModel
{
    public class CollabUpdateModel
    {
        public long UserId { get; set; }
        public long ShopId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Fullname is required")]
        public string FullName { get; set; }
        
        /*[Display(Name ="Mobile")]
        [RegularExpression(@"(^\d+$)", ErrorMessage = "Mobile contains only number 0-9")]
        [Phone(ErrorMessage = "Mobile contains only number 0-9")]
        public string Mobile { get; set; }*/

        [Display(Name ="Status")]
        public sbyte UserStatusId { get; set; }

        /*[Display(Name = "Gender")]
        public sbyte GenderId { get; set; }*/

        [Display(Name ="Role")]
        public int RoleId { get; set; }
        public DateTime ModifiedUtcDate { get; set; }
    }
}
