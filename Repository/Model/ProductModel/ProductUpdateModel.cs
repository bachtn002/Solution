using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model.ProductModel
{
    public class ProductUpdateModel
    {
        public long ProductId { get; set; }
        public long ShopId { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public string Images { get; set; }
        public string Properties { get; set; }
        public string Description { get; set; }
        public sbyte ProductStatusId { get; set; }
        public DateTime ModifiedUtcDate { get; set; }
        
    }
}
