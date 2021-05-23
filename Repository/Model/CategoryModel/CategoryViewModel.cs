﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model.CategoryModel
{
    public class CategoryViewModel
    {
        public long CategoryId { get; set; }
        public long ShopId { get; set; }
        [Display(Name ="Name")]
        public string NameCategory { get; set; }

    }
}
