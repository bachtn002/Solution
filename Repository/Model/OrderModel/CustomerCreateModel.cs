using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model.OrderModel
{
    public class CustomerCreateModel
    {
        public long UserId { get; set; }
        public long ShopId { get; set; }
        public sbyte GenderId { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public sbyte CustomerTypeId { get; set; }
        public sbyte PaymentMethodId { get; set; }
        
    }
}
