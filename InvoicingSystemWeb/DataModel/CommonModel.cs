using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class CommonModel
    {
        public CommonModel()
        {
            employ = new EmployeModel();
            MenuList = new List<Sys_MenuModel>();
        }
        public EmployeModel employ { get; set; }
        public List<Sys_MenuModel> MenuList { get; set; }
    }
}
