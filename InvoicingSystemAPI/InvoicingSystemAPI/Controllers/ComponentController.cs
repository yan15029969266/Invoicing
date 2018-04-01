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
        [HttpGet]
        public List<Sys_MenuModel> GetMenuList()
        {
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            List<Sys_Menu> list = IComponent.GetMenuList();
            Mapper.CreateMap<Sys_Menu, Sys_MenuModel>(); // 配置
            List<Sys_MenuModel> resultList = Mapper.Map<List<Sys_Menu>, List<Sys_MenuModel>>(list); // 使用AutoMapper自动映射
            return resultList;
        }
        [HttpGet]
        public List<Sys_ButtonModel> GetButtonByRole(Guid roleID,Guid menuID)
        {
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            List<Sys_Button> list = IComponent.GetButtonByRole(roleID,menuID);
            Mapper.CreateMap<Sys_Button, Sys_ButtonModel>(); // 配置
            List<Sys_ButtonModel> resultList = Mapper.Map<List<Sys_Button>, List<Sys_ButtonModel>>(list); // 使用AutoMapper自动映射
            return resultList;
        }
        [HttpGet]
        public List<Sys_MenuModel> GetMenuListByRole(Guid roleID)
        {
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            List<Sys_Menu> list = IComponent.GetMenuListByRole(roleID);
            Mapper.CreateMap<Sys_Menu, Sys_MenuModel>(); // 配置
            List<Sys_MenuModel> resultList = Mapper.Map<List<Sys_Menu>, List<Sys_MenuModel>>(list); // 使用AutoMapper自动映射
            return resultList;
        }
    }
}
