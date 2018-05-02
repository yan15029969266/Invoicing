using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataModel
{
    public class ProductPropertyValue
    {
        [Key]
        public Guid propertyValueID { get; set; }
        public string propertyValue { get; set; }
        public Guid fk_propertyNameID { get; set; }
        public int sort { get; set; }
        public bool enable { get; set; }
        public Guid cID { get; set; }
        public DateTime ctime { get; set; }
        public DateTime uptime { get; set; }
        public Guid upID { get; set; }
    }
}
