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
        public List<Sys_Button> GetButtonByRole(Guid roleID,Guid menuID)
        {
            using (IDbConnection conn = OpenConnection())
            {
                string query = @"SELECT btn.* FROM dbo.Sys_ButtonPermissions a
LEFT JOIN dbo.Sys_MenuButton mb ON a.fk_menu_btnID=mb.menu_btnID
LEFT JOIN dbo.Sys_Button btn ON mb.fk_btnID=btn.btnId
WHERE a.fk_roleID=@roleID AND fk_menuID=menuID";
                return conn.Query<Sys_Button>(query, new { userID = roleID, menuID = menuID }).OrderBy(t => t.sort).ToList();
                //return conn.GetList<Sys_Button>().ToList();
            }
        }

        public List<Sys_Menu> GetMenuListByRole(Guid roleID)
        {
            using (IDbConnection conn = OpenConnection())
            {
                string query = "select m.* from  Sys_MenuPermissions mp left join Sys_Menu m  on mp.fk_menuID=m.menuID where mp.fk_roleID=@fk_roleID";
                return conn.Query<Sys_Menu>(query, new { fk_roleID = roleID }).ToList();
            }
        }
    }
}
