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
        public long ShopId { get; set; }
        [Display(Name="Created Date")]
        public DateTime CreatedUtcDate { get; set; }
        public DateTime ModifiedUtcDate { get; set; }
        public long UserId { get; set; }
        [Display(Name="Name Shop")]
        public string Name { get; set; }
        [Display(Name="Address")]
        public string Address { get; set; }
        [Display(Name ="Description")]
        public string Description { get; set; }
        [Display(Name= "Choose a photo for your avatar")]
        public string Avatar { get; set; }
        [Display(Name="Shop Status")]
        public sbyte ShopStatusId { get; set; }
        [Display(Name="Shop Status")]
        public string ShopStatusName { get; set; }
        [Display(Name="Owner")]
        public string FullName { get; set; }
        [Display(Name= "Number of collaborators")]
        public long SumUser { get; set; }
    }
}
