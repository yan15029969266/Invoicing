using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewDataModel
{
    public class EmployeModel : BaseModel
    {
        public Guid employeID { get; set; }
        public Guid fk_employeID { get; set; }
        public string employeNo { get; set; }
        public string employeName { get; set; }
        public string employePost { get; set; }
        public string employeAccount { get; set; }
        public string employePwd { get; set; }
        public DateTime entryTime { get; set; }
        public string employeImage { get; set; }
        public Guid fk_roleID { get; set; }
        public bool enable { get; set; }
    }
}
