using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model.ProductModel
{
    public class ProductCreateModel
    {
        public long CategoryId { get; set; }
        public long ShopId { get; set; }

        [Display(Name="Name")]
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        public Decimal Price { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Image is required")]
        public string Images { get; set; }

        [Display(Name = "Properties")]
        [Required(ErrorMessage = "Properties is required")]
        public string Properties { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public sbyte ProductStatusId { get; set; }
        
    }
}
