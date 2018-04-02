using DBDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLogic.Interface
{
    public interface IAccountLogic
    {
       
        #region Role
        List<Role> GetRoleList();
        bool InsertRole(Role role);
        bool UpdateRole(Role role);
        bool DeleteRole(Guid id);
        #endregion
        #region Employer
        Employe Login(string account, string pwd);
        List<Employe> GetEmployeList();
        bool InsertEmploye(Employe e);
        bool UpdateEmploye(Employe e);
        bool DeleteEmploye(Guid id);
        #endregion
    }
}
