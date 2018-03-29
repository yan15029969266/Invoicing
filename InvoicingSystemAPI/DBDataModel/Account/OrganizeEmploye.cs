using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataModel
{
    /// <summary>
    /// 一个Organize映射多个Employe 
    /// </summary>
    public class OrganizeEmploye
    {
        [Key]
        public Guid ID { get; set; }
        public Guid fk_organizeID { get; set; }
        public Guid fk_employeID { get; set; }
        public Guid is_Manger { get; set; }
        public DateTime ctime { get; set; }
        public Guid cid { get; set; }
        public DateTime uptime { get; set; }
        public Guid upid { get; set; }
    }
}
