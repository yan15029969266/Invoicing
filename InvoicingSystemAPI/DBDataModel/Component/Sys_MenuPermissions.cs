using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataModel
{
    public class Sys_MenuPermissions
    {
        [Key]
        public int id { get; set; }
        public int fk_roleID { get; set; }
        public Guid fk_menuID { get; set; }
        public DateTime ctime { get; set; }
        public Guid cid { get; set; }
        public DateTime uptime { get; set; }
        public Guid upid { get; set; }
    }
}
