using CoreLogic.Interface;
using DBDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using ViewDataModel;

namespace CoreLogic.Implementation
{
    public class AccountLogic : BasicOperation, IAccountLogic
    {
        
        #region Role
        public List<Role> GetRoleList(int pageIndex, int pageSize)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.GetListPaged<Role>(pageIndex,pageSize,"where 1=1","ctime").ToList();
            }
        }
        public bool InsertRole(Role role)
        {
            using (IDbConnection conn = OpenConnection())
            {
                Guid id = conn.Insert<Guid>(role);
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
        public bool UpdateRole(Role role)
        {
            using (IDbConnection conn = OpenConnection())
            {
                int row = conn.Update(role);
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
        public bool DeleteRole(Guid id)
        {
            using (IDbConnection conn = OpenConnection())
            {
                IDbTransaction tranc = conn.BeginTransaction();
                try
                {
                    int row = conn.Delete<Role>(id,tranc);
                    row += conn.Delete<Sys_MenuPermissions>(new { fk_roleID = id },tranc);
                    row += conn.Delete<Sys_ButtonPermissions>(new { fk_roleID = id }, tranc);
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
                catch(Exception ex)
                {
                    tranc.Rollback();
                    return false;
                }
            }
        }
        public AuthModel GetRoleAuth(Guid roleID)
        {
            AuthModel model = new AuthModel { roleId = roleID };
            using (IDbConnection conn = OpenConnection())
            {
                string sqlQuery = @"SELECT m.menuID,m.menuName,
                                    CASE WHEN mp.id IS  null  THEN 0 ELSE 1 END isSelected
                                    FROM dbo.Sys_Menu m
                                    LEFT JOIN (SELECT * FROM dbo.Sys_MenuPermissions WHERE fk_roleID=@roleId) mp ON m.menuID=mp.fk_menuID
                                    WHERE m.enable=1 AND m.menuLevel=1 ORDER BY m.sort";
                model.menuList = conn.Query<PMenuAuth>(sqlQuery, new { roleId = roleID }).ToList();
                foreach (PMenuAuth m in model.menuList)
                {
                    sqlQuery = @"SELECT m.menuID,m.menuName,
                                    CASE WHEN mp.id IS  null  THEN 0 ELSE 1 END isSelected
                                    FROM dbo.Sys_Menu m
                                    LEFT JOIN (SELECT * FROM dbo.Sys_MenuPermissions WHERE fk_roleID=@roleId) mp ON m.menuID=mp.fk_menuID
                                    WHERE m.enable=1 AND m.parentID=@parentID ORDER BY m.sort";
                    m.cmenuList = conn.Query<CMenuAuth>(sqlQuery, new { roleId = roleID, parentID=m.menuID }).ToList();
                    foreach (CMenuAuth cm in m.cmenuList)
                    {

                        sqlQuery = @"SELECT mb.menu_btnID,b.btnName,
                                 CASE WHEN bp.id IS  null  THEN 0 ELSE 1 END isSelected
                                 FROM dbo.Sys_MenuButton mb
                                 LEFT JOIN dbo.Sys_Button b ON mb.fk_btnID=b.btnId
                                 LEFT JOIN (SELECT * FROM dbo.Sys_ButtonPermissions WHERE fk_roleID=@roleId) bp ON mb.menu_btnID=bp.fk_menu_btnID
                                 WHERE fk_menuID=@menuID AND b.enable=1 ORDER BY b.sort";
                        cm.buttonList = conn.Query<ButtonAuth>(sqlQuery, new { roleId = roleID, menuID = cm.menuID }).ToList();
                    }
                }
                return model;
            }
        }
        public bool SetRoleAuth(AuthModel model)
        {
            using (IDbConnection conn = OpenConnection())
            {
                IDbTransaction tranc = conn.BeginTransaction();
                try
                {
                    string query = "";
                    foreach (PMenuAuth menu in model.menuList)
                    {
                        if(menu.isSelected)
                        {
                            IEnumerable<Sys_MenuPermissions> pMenuList = conn.GetList<Sys_MenuPermissions>(new { fk_roleID = model.roleId, fk_menuID = menu.menuID }, tranc);
                            if (pMenuList.Count() <= 0)
                            {
                                Sys_MenuPermissions sys_MenuPermissions = new Sys_MenuPermissions { id = Guid.NewGuid(), fk_menuID = menu.menuID, fk_roleID = model.roleId };
                                conn.Insert<Guid>(sys_MenuPermissions, tranc);
                            }
                        }
                        else
                        {
                            conn.DeleteList<Sys_MenuPermissions>(new { fk_roleID = model.roleId, fk_menuID = menu.menuID }, tranc);
                        }
                        foreach (CMenuAuth cmenu in menu.cmenuList)
                        {
                            if (cmenu.isSelected)
                            {
                                IEnumerable<Sys_MenuPermissions> cMenuList = conn.GetList<Sys_MenuPermissions>(new { fk_roleID = model.roleId, fk_menuID = cmenu.menuID }, tranc);
                                if (cMenuList.Count() <= 0)
                                {
                                    Sys_MenuPermissions sys_MenuPermissions = new Sys_MenuPermissions { id = Guid.NewGuid(), fk_menuID = cmenu.menuID, fk_roleID = model.roleId };
                                    conn.Insert<Guid>(sys_MenuPermissions, tranc);
                                }
                            }
                            else
                            {
                                conn.DeleteList<Sys_MenuPermissions>(new { fk_roleID = model.roleId, fk_menuID = cmenu.menuID }, tranc);
                            }
                            foreach (ButtonAuth btn in cmenu.buttonList)
                            {
                                if(btn.isSelected)
                                {
                                    IEnumerable<Sys_ButtonPermissions> bList = conn.GetList<Sys_ButtonPermissions>(new { fk_roleID = model.roleId, fk_menu_btnID = btn.menu_btnID }, tranc);
                                    if (bList.Count() <= 0)
                                    {
                                        Sys_ButtonPermissions sys_ButtonPermissions = new Sys_ButtonPermissions { id = Guid.NewGuid(), fk_menu_btnID = btn.menu_btnID, fk_roleID = model.roleId };
                                        conn.Insert<Guid>(sys_ButtonPermissions, tranc);
                                    }
                                }
                                else
                                {
                                    conn.DeleteList<Sys_ButtonPermissions>(new { fk_roleID = model.roleId, fk_menu_btnID = btn.menu_btnID }, tranc);
                                }
                            }
                        }
                    }
                    tranc.Commit();
                    return true;
                }
                catch(Exception ex)
                {
                    tranc.Rollback();
                    return false;
                }
            }
        }
        public Role GetRole(Guid id)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.Get<Role>(id);
            }
        }
        #endregion
        #region Employer
        public Employe Login(string account, string pwd)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.GetList<Employe>(new { employeAccount = account, employePwd = pwd }).FirstOrDefault();
            }
        }
        public List<Employe> GetEmployeList(int pageIndex, int pageSize)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.GetListPaged<Employe>(pageIndex, pageSize, "where 1=1", "ctime").ToList();
            }
        }
        public bool InsertEmploye(Employe e)
        {
            using (IDbConnection conn = OpenConnection())
            {
                Guid id = conn.Insert<Guid>(e);
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
        public bool UpdateEmploye(Employe e)
        {
            using (IDbConnection conn = OpenConnection())
            {
                int row = conn.Update(e);
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
        public bool DeleteEmploye(Guid id)
        {
            using (IDbConnection conn = OpenConnection())
            {
                int row = conn.Delete<Employe>(id);
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
        public string GetNewEmployeNo()
        {
            using (IDbConnection conn = OpenConnection())
            {
                string NewNo = "";
                string sqlQuery = "Select employeNo from Employe order by CAST(SUBSTRING(employeNo,2,LEN(employeNo))AS INT) DESC";
                string lastNo=conn.Query<string>(sqlQuery).FirstOrDefault();
                if(lastNo=="")
                {
                    NewNo = "E00001";
                }
                else
                {
                    int cout = Convert.ToInt32(lastNo.Substring(1));
                    if(cout>99999)
                    {
                        throw new Exception("员工编号超长，请联系管理员！");
                    }
                    else
                    {
                        NewNo = "E" + (cout + 1).ToString("00000");
                    }
                }
                return NewNo;
            }
        }
        public Employe GetEmploye(Guid id)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.Get<Employe>(id);
            }
        }
        #endregion
    }
}
