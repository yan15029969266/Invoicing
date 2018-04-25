using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewDataModel
{
    public class ChangePwdModel
    {
        public Guid employerID { get; set; }
        public string oldPwd { get; set; }
        public string newPwd { get; set; }
        public string rePwd { get; set; }
    }
}
