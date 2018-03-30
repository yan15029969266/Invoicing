using AutoMapper;
using CoreLogic.Interface;
using DBDataModel;
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
    public class ComponentController : ApiController
    {
        [HttpGet]
        public List<Sys_MenuModel> GetMenuList()
        {
            //创建容器
            UnityContainer container = new UnityContainer();
            UnityConfigurationSection config = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
            //加载到容器
            config.Configure(container, "MyContainer");
            //返回调用者
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            List<Sys_Menu> list = IComponent.GetMenuList();
            Mapper.CreateMap<Sys_Menu, Sys_MenuModel>(); // 配置
            List<Sys_MenuModel> resultList = Mapper.Map<List<Sys_Menu>, List<Sys_MenuModel>>(list); // 使用AutoMapper自动映射
            return resultList;
        }
        public List<Sys_ButtonModel> GetButtonByRole(Guid roleID)
        {
            UnityContainer container = new UnityContainer();
            UnityConfigurationSection config = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
            //加载到容器
            config.Configure(container, "MyContainer");
            //返回调用者
            IComponentLogic IComponent = container.Resolve<IComponentLogic>();
            List<Sys_Button> list = IComponent.GetButtonByRole(roleID);
        }
    }
}
