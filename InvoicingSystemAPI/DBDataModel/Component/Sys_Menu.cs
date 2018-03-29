using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataModel
{
    public class Sys_Menu
    {
        [Key]
        public Guid menuID { get; set; }
        public string menuName { get; set; }
        public string menuUrl { get; set; }
        public string menuIcon { get; set; }
        public int menuLevel { get; set; }
        public Guid parentID { get; set; }
        public int sort { get; set; }
        public bool enable { get; set; }
        public Guid cID { get; set; }
        public DateTime ctime { get; set; }
        public DateTime uptime { get; set; }
        public Guid upID { get; set; }
    }
}
