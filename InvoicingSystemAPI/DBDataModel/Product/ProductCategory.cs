using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataModel
{
    public class ProductCategory
    {
        [Key]
        public Guid categoryID { get; set; }
        public string categoryName { get; set; }
        public Guid parentID { get; set; }
        public int depth { get; set; }
        public int sort { get; set; }
        public bool enable { get; set; }
        public Guid cID { get; set; }
        public DateTime ctime { get; set; }
        public DateTime uptime { get; set; }
        public Guid upID { get; set; }
    }
}
