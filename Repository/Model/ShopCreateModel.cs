using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class ShopCreateModel
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        [Display(Name= "Choose a photo for your avatar")]
        public string Avatar { get; set; }
    }
}
