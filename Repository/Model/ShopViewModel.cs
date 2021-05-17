using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class ShopViewModel
    {
        public long ShopId { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }

        [Display(Name = "Shop name")]
        public string Name { get; set; }

        [Display(Name = "Shop status")]
        public string ShopStatusName { get; set; }

        
    }
}
