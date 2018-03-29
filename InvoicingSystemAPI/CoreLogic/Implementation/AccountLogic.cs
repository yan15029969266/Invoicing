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
        public Employe Login(string account, string pwd)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.GetList<Employe>(new { employeAccount = account, employePwd = pwd }).FirstOrDefault();
            }
        }
    }
}
