using CoreLogic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBDataModel;
using Dapper;
using System.Data;

namespace CoreLogic.Implementation
{
    public class ComponentLogic : BasicOperation, IComponentLogic
    {
        public List<Sys_Menu> GetMenuList()
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.GetList<Sys_Menu>().ToList();
            }
        }
        public List<Sys_Button> GetButtonByRole(Guid roleID)
        {
            throw new NotImplementedException();
        }
    }
}
