using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model.CategoryModel
{
    public class CategoryCreateModel
    {
        public long ShopId { get; set; }
        [Display(Name ="Name Category")]
        [Required(ErrorMessage ="Name is required")]
        public string NameCategory { get; set; }
       

    }
}
