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
        #region menu
        public List<Sys_Menu> GetMenuList()
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.GetList<Sys_Menu>().ToList();
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

        public Sys_Menu GetMenuListByUrl(string url)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.GetList<Sys_Menu>(new { menuUrl = url }).FirstOrDefault();
            }
        }
        public bool InsertMenu(Sys_Menu menu)
        {
            using (IDbConnection conn = OpenConnection())
            {
                Guid id = conn.Insert<Guid>(menu);
                if (id != null && id != Guid.Empty)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public Sys_Menu GetMenu(Guid id)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.Get<Sys_Menu>(id);
            }
        }
        public bool UpdateMenu(Sys_Menu menu)
        {
            using (IDbConnection conn = OpenConnection())
            {
                int row = conn.Update(menu);
                if (row > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool DeleteMenu(Guid id)
        {
            using (IDbConnection conn = OpenConnection())
            {
                IDbTransaction tranc = conn.BeginTransaction();
                try
                {
                    Sys_Menu menu = conn.Get<Sys_Menu>(id,tranc);
                    int row = 0;
                    if (menu.menuLevel > 1)
                    {
                        row = conn.Delete<Sys_Menu>(id,tranc);
                    }
                    else
                    {
                        row = conn.DeleteList<Sys_Menu>(new { parentID=menu.menuID }, tranc);
                        row += conn.Delete<Sys_Menu>(id, tranc);
                    }
                    tranc.Commit();
                    if (row > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    tranc.Rollback();
                    return false;
                }
            }
        }
        #endregion
        #region button
        public List<Sys_Button> GetButtonByRole(Guid roleID, Guid menuID)
        {
            using (IDbConnection conn = OpenConnection())
            {
                string query = @"SELECT btn.* FROM dbo.Sys_ButtonPermissions a
LEFT JOIN dbo.Sys_MenuButton mb ON a.fk_menu_btnID=mb.menu_btnID
LEFT JOIN dbo.Sys_Button btn ON mb.fk_btnID=btn.btnId
WHERE a.fk_roleID=@roleID AND fk_menuID=@menuID";
                return conn.Query<Sys_Button>(query, new { roleID = roleID, menuID = menuID }).OrderBy(t => t.sort).ToList();
                //return conn.GetList<Sys_Button>().ToList();
            }
        }
        public List<Sys_Button> GetButtonByMenu(Guid menuID)
        {
            using (IDbConnection conn = OpenConnection())
            {
                string query = @"SELECT btn.* FROM dbo.Sys_MenuButton mb 
LEFT JOIN dbo.Sys_Button btn ON mb.fk_btnID=btn.btnId
WHERE mb.fk_menuID=@menuID";
                return conn.Query<Sys_Button>(query, new {menuID = menuID }).OrderBy(t => t.sort).ToList();
                //return conn.GetList<Sys_Button>().ToList();
            }
        }
        public bool InsertButton(Sys_Button button)
        {
            using (IDbConnection conn = OpenConnection())
            {
                Guid id = conn.Insert<Guid>(button);
                if (id != null && id != Guid.Empty)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool UpdateButton(Sys_Button button)
        {
            using (IDbConnection conn = OpenConnection())
            {
                int row = conn.Update(button);
                if (row > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool DeleteButton(Guid id)
        {
            using (IDbConnection conn = OpenConnection())
            {
                int row = conn.Delete<Sys_Button>(id);
                if (row > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public List<Sys_Button> GetButtonList(int pageIndex,int pageSize)
        {
            using (IDbConnection conn = OpenConnection())
            {
                //return conn.GetList<Sys_Button>().ToList();
                return conn.GetListPaged<Sys_Button>(pageIndex, pageSize, "where 1=1", "ctime").ToList();
            }
        }
        #endregion
    }
}
