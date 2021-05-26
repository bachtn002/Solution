using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model.CategoryModel
{
    public class CategoryViewModel
    {
        [Display(Name ="CODE")]
        public long CategoryId { get; set; }
        public long ShopId { get; set; }
        [Display(Name ="NAME")]
        public string NameCategory { get; set; }
        [Display(Name ="PARENT CATEGORY")]
        public string ParentCategory { get; set; }

    }
}
