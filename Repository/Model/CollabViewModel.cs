﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class CollabViewModel
    {
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Gender")]
        public string GenderName { get; set; }

        [Display(Name ="DOB")]
        public DateTime DOB { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }

        [Display(Name ="Join")]
        public DateTime CreatedUtcDate { get; set; }
    }
}