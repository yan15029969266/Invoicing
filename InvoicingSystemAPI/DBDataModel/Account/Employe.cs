using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataModel
{
    public class Employe
    {
        [Key]
        public Guid employeID { get; set; }
        public Guid fk_employeID { get; set; }
        public string employeNo { get; set; }
        public string employeName { get; set; }
        public string employePost { get; set; }
        public string employeAccount { get; set; }
        public string employePwd { get; set; }
        public DateTime entryTime { get; set; }
        public string employPhone { get; set; }
        public string employeImage { get; set; }
        public Guid fk_roleID { get; set; }
        //public int fk_areaID { get; set; }
        public Guid fk_organizeID { get; set; }
        public bool enable { get; set; }
        public DateTime ctime { get; set; }
        public Guid cid { get; set; }
        public DateTime uptime { get; set; }
        public Guid upid { get; set; }
    }
}
