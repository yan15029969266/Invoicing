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
        #endregion
    }
}
