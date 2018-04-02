using CoreLogic.Interface;
using DBDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace CoreLogic.Implementation
{
    public class AccountLogic : BasicOperation, IAccountLogic
    {
        
        #region Role
        public List<Role> GetRoleList()
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.GetList<Role>().ToList();
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
                int row = conn.Delete<Role>(id);
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
        #endregion
        #region Employer
        public Employe Login(string account, string pwd)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.GetList<Employe>(new { employeAccount = account, employePwd = pwd }).FirstOrDefault();
            }
        }
        public List<Employe> GetEmployeList()
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.GetList<Employe>().ToList();
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
        #endregion
    }
}
