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
        [Route("api/Component/GetMenuList")]
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
        [Route("api/Component/GetMenuListByRole")]
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
        [Route("api/Component/GetMenuListByUrl")]
        [HttpGet]
        public Sys_MenuModel GetMenuListByUrl(string url)
        {
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            Sys_Menu menu = IComponent.GetMenuListByUrl(url);
            Mapper.CreateMap<Sys_Menu, Sys_MenuModel>(); // 配置
            Sys_MenuModel model = Mapper.Map<Sys_Menu, Sys_MenuModel>(menu); // 使用AutoMapper自动映射
            return model;
        }
        [Route("api/Component/InsertMenu")]
        [HttpPost]
        public bool InsertMenu([FromBody]Sys_MenuModel model)
        {
            Mapper.CreateMap<Sys_MenuModel,Sys_Menu>(); // 配置
            Sys_Menu menu = Mapper.Map<Sys_MenuModel, Sys_Menu>(model); // 使用AutoMapper自动映射
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            return IComponent.InsertMenu(menu);
        }
        [Route("api/Component/GetMenu")]
        public Sys_MenuModel GetMenu(Guid id)
        {
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            Sys_Menu menu = IComponent.GetMenu(id);
            Mapper.CreateMap<Sys_Menu, Sys_MenuModel>(); // 配置
            Sys_MenuModel model = Mapper.Map<Sys_Menu,Sys_MenuModel>(menu); // 使用AutoMapper自动映射
            return model;
        }
        [Route("api/Component/UpdateMenu")]
        [HttpPost]
        public bool UpdateMenu([FromBody]Sys_MenuModel model)
        {
            Mapper.CreateMap<Sys_MenuModel, Sys_Menu>(); // 配置
            Sys_Menu menu = Mapper.Map<Sys_MenuModel, Sys_Menu>(model); // 使用AutoMapper自动映射
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            return IComponent.UpdateMenu(menu);
        }
        [Route("api/Component/DeleteMenu")]
        public bool DeleteMenu(Guid id)
        {
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            return IComponent.DeleteMenu(id);
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
        [Route("api/Component/GetButtonByRoleAndUrl")]
        [HttpGet]
        public List<Sys_ButtonModel> GetButtonByRoleAndUrl(Guid roleID, Guid menuID)
        {
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            List<Sys_Button> list = IComponent.GetButtonByRole(roleID, menuID);
            Mapper.CreateMap<Sys_Button, Sys_ButtonModel>(); // 配置
            List<Sys_ButtonModel> resultList = Mapper.Map<List<Sys_Button>, List<Sys_ButtonModel>>(list); // 使用AutoMapper自动映射
            return resultList;
        }
        [Route("api/Component/GetButtonByMenu")]
        [HttpGet]
        public List<MenuButtonModel> GetButtonByMenu(Guid menuID)
        {
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            List<MenuButtonModel> list = IComponent.GetButtonByMenu(menuID);
            //Mapper.CreateMap<Sys_Button, Sys_ButtonModel>(); // 配置
            //List<Sys_ButtonModel> resultList = Mapper.Map<List<Sys_Button>, List<Sys_ButtonModel>>(list); // 使用AutoMapper自动映射
            return list;
        }
        [Route("api/Component/GetButtonList")]
        [HttpGet]
        public List<Sys_ButtonModel> GetButtonList(int pageIndex,int pageSize)
        {
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            List<Sys_Button> list = IComponent.GetButtonList(pageIndex, pageSize);
            Mapper.CreateMap<Sys_Button, Sys_ButtonModel>(); // 配置
            List<Sys_ButtonModel> resultList = Mapper.Map<List<Sys_Button>, List<Sys_ButtonModel>>(list); // 使用AutoMapper自动映射
            return resultList;
        }
        [Route("api/Component/InsertButton")]
        [HttpPost]
        public bool InsertButton([FromBody]Sys_ButtonModel model)
        {
            Mapper.CreateMap<Sys_ButtonModel, Sys_Button>(); // 配置
            Sys_Button button = Mapper.Map<Sys_ButtonModel, Sys_Button>(model); // 使用AutoMapper自动映射
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            return IComponent.InsertButton(button);
        }
        [Route("api/Component/UpdateButton")]
        [HttpPost]
        public bool UpdateButton([FromBody]Sys_ButtonModel model)
        {
            Mapper.CreateMap<Sys_ButtonModel, Sys_Button>(); // 配置
            Sys_Button button = Mapper.Map<Sys_ButtonModel, Sys_Button>(model); // 使用AutoMapper自动映射
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            return IComponent.UpdateButton(button);
        }
        [Route("api/Component/DeleteButton")]
        public bool DeleteButton(Guid id)
        {
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            return IComponent.DeleteButton(id);
        }
        [HttpPost]
        [Route("api/Component/SetMenuButton")]
        public bool SetMenuButton([FromBody]List<MenuButtonModel> list)
        {
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            return IComponent.SetMenuButton(list);
        }
        #endregion
    }
}
