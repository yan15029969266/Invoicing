using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataModel
{
    public class ProductImage
    {
        public Guid imageID { get; set; }
        public string imagePath { get; set; }
        public Guid fk_productID { get; set; }
        public int sort { get; set; }
    }
}
