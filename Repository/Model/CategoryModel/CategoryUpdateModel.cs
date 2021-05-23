using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model.CategoryModel
{
    public class CategoryUpdateModel
    {
        public long CategoryId { get; set; }
        public long ShopId { get; set; }
        [Display(Name ="Name")]
        [Required(ErrorMessage ="Name is required")]
        public string NameCategory { get; set; }
        public int ParentId { get; set; }
        
    }
}
