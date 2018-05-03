using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataModel
{
    public class ProductBaseProperty
    {
        public Guid propertyID { get; set; }
        public Guid fk_productID { get; set; }
        public Guid fk_propertyNameID { get; set; }
        public Guid fk_propertyValueID { get; set; }
        public bool isSku { get; set; }
        public bool fk_skuID { get; set; }
        public Guid cID { get; set; }
        public DateTime ctime { get; set; }
        public DateTime uptime { get; set; }
        public Guid upID { get; set; }
    }
}
