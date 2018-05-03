using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataModel
{
    public class Product
    {
        [Key]
        public Guid productID { get; set; }
        public string prductName { get; set; }
        public Guid fk_categoryID { get; set; }
        public decimal titlePrice { get; set; }
        public bool enable { get; set; }
        public Guid cID { get; set; }
        public DateTime ctime { get; set; }
        public DateTime uptime { get; set; }
        public Guid upID { get; set; }
    }
}
