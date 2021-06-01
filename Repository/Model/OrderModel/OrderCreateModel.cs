using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model.OrderModel
{
    public class OrderCreateModel
    {
        public long UserId { get; set; }
        public long ShopId { get; set; }
        [Display(Name ="Gender")]
        public sbyte GenderId { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public sbyte CustomerTypeId { get; set; }
        [Display(Name = "Payment Method")]
        public sbyte PaymentMethodId { get; set; }
        
    }
}
