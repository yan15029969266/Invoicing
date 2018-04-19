using DBDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewDataModel;

namespace CoreLogic.Interface
{
    public interface IAccountLogic
    {
       
        #region Role
        List<Role> GetRoleList(int pageIndex, int pageSize);
        bool InsertRole(Role role);
        bool UpdateRole(Role role);
        bool DeleteRole(Guid id);
        AuthModel GetRoleAuth(Guid roleID);
        bool SetRoleAuth(AuthModel model);
        Role GetRole(Guid id);
        #endregion
        #region Employer
        Employe Login(string account, string pwd);
        List<Employe> GetEmployeList(int pageIndex, int pageSize);
        bool InsertEmploye(Employe e);
        bool UpdateEmploye(Employe e);
        bool DeleteEmploye(Guid id);
        string GetNewEmployeNo();
        Employe GetEmploye(Guid id);
        #endregion
        #region Organize
        List<Organize> GetOrganizeList();
        Organize GetOrganize(Guid id);
        bool InsertOrganize(OrganizeModel model);
        bool UpdateOrganize(OrganizeModel model);
        bool DeleteOrganize(Guid id);
        #endregion
    }
}
