﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
    }
}
