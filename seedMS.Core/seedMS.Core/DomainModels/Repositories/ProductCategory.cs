﻿using System;
using System.Collections.Generic;

namespace seedMS.Core.DomainModels.Repositories
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}