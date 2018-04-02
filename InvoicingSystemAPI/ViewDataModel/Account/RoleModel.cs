using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewDataModel
{
    public class RoleModel:BaseModel
    {
        public Guid roleID { get; set; }
        public string roleName { get; set; }
        public bool enable { get; set; }
    }
}
