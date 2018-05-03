using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataModel
{
    public class ProductPropertyName
    {
        [Key]
        public Guid propertyNameID { get; set; }
        public string propertyName { get; set; }
        public Guid fk_categoryID { get; set; }
        public bool isKey { get; set; }
        public bool isSale { get; set; }
        public bool isSearch { get; set; }
        public bool isNecesarry { get; set; }
        public int sort { get; set; }
        public bool enable { get; set; }
        public Guid cID { get; set; }
        public DateTime ctime { get; set; }
        public DateTime uptime { get; set; }
        public Guid upID { get; set; }
    }
}
