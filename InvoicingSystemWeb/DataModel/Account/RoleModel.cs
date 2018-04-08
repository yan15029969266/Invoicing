using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class RoleModel : BaseModel
    {
        public Guid roleID { get; set; }
        public string roleName { get; set; }
        public bool enable { get; set; }
        public string status
        {
            get
            {
                return enable ? "<span class='label label-success'>激活</span>" : "<span class='label label-danger'>禁止</span>";
            }
        }
    }
}
