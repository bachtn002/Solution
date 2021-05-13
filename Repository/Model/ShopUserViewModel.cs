using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class ShopUserViewModel
    {
        [Display(Name="Name")]
        public string FullName { get; set; }
        [Display(Name ="Role")]
        public string RoleName { get; set; }
        [Display(Name ="Shop name")]
        public string Name { get; set; }
        [Display(Name = "Shop status")]
        public string ShopStatusName { get; set; }
        [Display(Name = "Date created")]
        public DateTime CreatedUtcDate { get; set; }
    }
}
