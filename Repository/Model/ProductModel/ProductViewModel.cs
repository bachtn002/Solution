using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model.ProductModel
{
    public class ProductViewModel
    {
        public long ProductId { get; set; }
        public long ShopId { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }

        [Display(Name="Category")]
        public string NameCategory { get; set; }
        public Decimal Price { get; set; }
        public string Images { get; set; }
        public string Properties { get; set; }
        public string Description { get; set; }

        [Display(Name = "Status")]
        public string ProductStatusName { get; set; }
        
    }
}
