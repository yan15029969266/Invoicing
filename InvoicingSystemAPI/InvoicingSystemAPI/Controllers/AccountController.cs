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

namespace InvoicingSystemAPI.Controllers
{
    [BasicAuthorizeAttribute]
    public class AccountController : BaseController
    {
        #region 登录
        [HttpGet]
        public EmployeModel Login(string account, string pwd)
        {
            ////创建容器
            //UnityContainer container = new UnityContainer();
            //UnityConfigurationSection config = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
            ////加载到容器
            //config.Configure(container, "MyContainer");
            //返回调用者
            IAccountLogic IUser = container.Resolve<IAccountLogic>();
            //执行
            Employe employe = IUser.Login(account, pwd);
            Mapper.CreateMap<Employe, EmployeModel>(); // 配置
            EmployeModel model = Mapper.Map<Employe, EmployeModel>(employe); // 使用AutoMapper自动映射
            return model;
        }
        #endregion
        #region 角色
        //public bool InsertRole([FromBody]RoleModel model)
        //{

        //}
        #endregion
        #region 用户
        
        #endregion
    }
}
