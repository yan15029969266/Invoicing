using DBDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLogic.Interface
{
    public interface IComponentLogic
    {
        List<Sys_Menu> GetMenuList();
        List<Sys_Button> GetButtonByRole(Guid roleID, Guid menuID);

        List<Sys_Menu> GetMenuListByRole(Guid roleID);

        Sys_Menu GetMenuListByUrl(string url);

        bool InsertMenu(Sys_Menu menu);
    }
}
