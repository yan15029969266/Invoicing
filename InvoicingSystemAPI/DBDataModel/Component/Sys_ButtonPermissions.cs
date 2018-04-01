using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataModel
{
    public class Sys_ButtonPermissions
    {
        [Key]
        public Guid id { get; set; }
        public Guid fk_roleID { get; set; }
        public Guid fk_menu_btnID { get; set; }
        //public DateTime ctime { get; set; }
        //public Guid cid { get; set; }
        //public DateTime uptime { get; set; }
        //public Guid upid { get; set; }
    }
}
