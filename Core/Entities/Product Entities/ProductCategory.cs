﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Product_Entities
{
    public class ProductCategory: BaseEntity
    {
        public string Name { get; set; }

        //public ICollection<Product> Products { get; set; } = new HashSet<Product>();
        // -- We Don't Bring Products From Product Brand So We Don't Need Navigation Property
        // -- But EF Consider This Relation ONE-ONE And We Need It ONE-MANY So We Modified It In Fluent API
    }
}
