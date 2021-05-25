using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model.ProductModel
{
    public class ProductDetailModel
    {
        public string NameShop { get; set; }
        public string NameProduct { get; set; }
        public decimal Price { get; set; }
        public string Images { get; set; }
        public string Properties { get; set; }
        public string Description { get; set; }
        public sbyte ProductStatusId { get; set; }
        public string ProductStatusName { get; set; }
        public string NameCategory { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public long ProductId { get; set; }
        public long CategoryId { get; set; }
        public long ShopId { get; set; }
    }
}
