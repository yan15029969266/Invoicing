﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataModel
{
    public class ProductSku
    {
        public Guid skuID { get; set; }
        public Guid fk_productID { get; set; }
        public string properties { get; set; }
        public string propertiesName { get; set; }
        public int num { get; set; }
        public decimal price { get; set; }
    }
}
