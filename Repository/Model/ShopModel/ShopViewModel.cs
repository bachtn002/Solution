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

        [Display(Name = "Owner")]
        public string FullName { get; set; }

        [Display(Name = "ROLE")]
        public string RoleName { get; set; }

        [Display(Name = "SHOP NAME")]
        public string Name { get; set; }

        [Display(Name = "SHOP STATUS")]
        public string ShopStatusName { get; set; }
        [Display(Name = "ADDRESS")]
        public string Address { get; set; }


    }
}
