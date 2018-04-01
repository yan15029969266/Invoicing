using AutoMapper;
using CoreLogic.Interface;
using DBDataModel;
using InvoicingSystemAPI.Filter;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Unity;
using ViewDataModel;
using ViewDataModel.Component;

namespace InvoicingSystemAPI.Controllers
{
    [BasicAuthorizeAttribute]
    public class ComponentController : BaseController
    {
        #region Menu
        [HttpGet]
        public List<Sys_MenuModel> GetMenuList()
        {
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            List<Sys_Menu> list = IComponent.GetMenuList();
            List<Sys_MenuModel> resultList = GetMenuStructure(list);
            //Mapper.CreateMap<Sys_Menu, Sys_MenuModel>(); // 配置
            //List<Sys_MenuModel> resultList = Mapper.Map<List<Sys_Menu>, List<Sys_MenuModel>>(list); // 使用AutoMapper自动映射
            return resultList;
        }
        [HttpGet]
        public List<Sys_MenuModel> GetMenuListByRole(Guid roleID)
        {
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            List<Sys_Menu> list = IComponent.GetMenuListByRole(roleID);
            List<Sys_MenuModel> resultList = GetMenuStructure(list);
            //Mapper.CreateMap<Sys_Menu, Sys_MenuModel>(); // 配置
            //List<Sys_MenuModel> resultList = Mapper.Map<List<Sys_Menu>, List<Sys_MenuModel>>(list); // 使用AutoMapper自动映射
            return resultList;
        }
        [HttpGet]
        public Sys_MenuModel GetMenuListByUrl(string url)
        {
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            Sys_Menu menu = IComponent.GetMenuListByUrl(url);
            Mapper.CreateMap<Sys_Menu, Sys_MenuModel>(); // 配置
            Sys_MenuModel model = Mapper.Map<Sys_Menu, Sys_MenuModel>(menu); // 使用AutoMapper自动映射
            return model;
        }
        public bool InsertMenu([FromBody]Sys_MenuModel model)
        {
            Mapper.CreateMap<Sys_MenuModel,Sys_Menu>(); // 配置
            Sys_Menu menu = Mapper.Map<Sys_MenuModel, Sys_Menu>(model); // 使用AutoMapper自动映射
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            return IComponent.InsertMenu(menu);
        }

        #region 生成菜单实体
        private List<Sys_MenuModel> GetMenuStructure(List<Sys_Menu> list)
        {
            List<Sys_MenuModel> reusltList = new List<Sys_MenuModel>();
            var MainMenu = from menu in list
                           where menu.menuLevel == 1
                           select new Sys_MenuModel
                           {
                               menuID = menu.menuID,
                               menuName = menu.menuName,
                               menuUrl = menu.menuUrl,
                               menuIcon = menu.menuIcon,
                               menuLevel = menu.menuLevel,
                               parentID = menu.parentID,
                               enable = menu.enable,
                               sort = menu.sort,
                               cid = menu.cID,
                               ctime = menu.ctime,
                               upid = menu.upID,
                               uptime = menu.uptime
                           };
            foreach (Sys_MenuModel model in MainMenu)
            {
                var SubMenu = from menu in list
                              where menu.menuLevel == 2 && menu.parentID == model.menuID
                              select new Sys_MenuModel
                              {
                                  menuID = menu.menuID,
                                  menuName = menu.menuName,
                                  menuUrl = menu.menuUrl,
                                  menuIcon = menu.menuIcon,
                                  menuLevel = menu.menuLevel,
                                  parentID = menu.parentID,
                                  enable = menu.enable,
                                  sort = menu.sort,
                                  cid = menu.cID,
                                  ctime = menu.ctime,
                                  upid = menu.upID,
                                  uptime = menu.uptime
                              };
                model.subMenuList = SubMenu.OrderBy(t => t.sort).ToList();
                reusltList.Add(model);
            }
            return reusltList;
        }
        #endregion

        #endregion
        #region Button
        [HttpGet]
        public List<Sys_ButtonModel> GetButtonByRoleAndUrl(Guid roleID, Guid menuID)
        {
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            List<Sys_Button> list = IComponent.GetButtonByRole(roleID, menuID);
            Mapper.CreateMap<Sys_Button, Sys_ButtonModel>(); // 配置
            List<Sys_ButtonModel> resultList = Mapper.Map<List<Sys_Button>, List<Sys_ButtonModel>>(list); // 使用AutoMapper自动映射
            return resultList;
        }
        #endregion


    }
}
