﻿using DBDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewDataModel.Component;

namespace CoreLogic.Interface
{
    public interface IComponentLogic
    {
        #region Menu
        List<Sys_Menu> GetMenuList();
        List<Sys_Menu> GetMenuListByRole(Guid roleID);
        Sys_Menu GetMenuListByUrl(string url);
        bool InsertMenu(Sys_Menu menu);
        Sys_Menu GetMenu(Guid id);
        bool UpdateMenu(Sys_Menu menu);
        bool DeleteMenu(Guid id);
        #endregion
        #region Button
        List<Sys_Button> GetButtonByRole(Guid roleID, Guid menuID);
        List<MenuButtonModel> GetButtonByMenu(Guid menuID);
        List<Sys_Button> GetButtonList(int pageIndex, int pageSize);
        bool InsertButton(Sys_Button button);
        bool UpdateButton(Sys_Button button);
        bool DeleteButton(Guid id);
        bool SetMenuButton(List<MenuButtonModel> list);
        Sys_Button GetButton(Guid btnID);
        #endregion



        
    }
}
