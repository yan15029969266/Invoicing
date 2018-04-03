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
using System.Threading.Tasks;
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
        #region role
        [HttpGet]
        [Route("api/Account/GetRoleList")]
        public List<RoleModel> GetRoleList()
        {
            IAccountLogic IComponent = container.Resolve<IAccountLogic>();
            List<Role> list = IComponent.GetRoleList();
            Mapper.CreateMap<Role, RoleModel>(); // 配置
            List<RoleModel> resultList = Mapper.Map<List<Role>, List<RoleModel>>(list); // 使用AutoMapper自动映射
            return resultList;
        }
        [HttpPost]
        public bool InsertRole([FromBody]RoleModel model)
        {
            Mapper.CreateMap<RoleModel, Role>(); // 配置
            Role role = Mapper.Map<RoleModel, Role>(model); // 使用AutoMapper自动映射
            IAccountLogic IUser = container.Resolve<IAccountLogic>();
            //执行
            return IUser.InsertRole(role);
        }
        [HttpPost]
        public bool UpdateRole([FromBody]RoleModel model)
        {
            Mapper.CreateMap<RoleModel, Role>(); // 配置
            Role role = Mapper.Map<RoleModel, Role>(model); // 使用AutoMapper自动映射
            IAccountLogic IUser = container.Resolve<IAccountLogic>();
            //执行
            return IUser.UpdateRole(role);
        }
        public bool DeleteRole(Guid id)
        {
            IAccountLogic IUser = container.Resolve<IAccountLogic>();
            //执行
            return IUser.DeleteRole(id);
        }
        #endregion
        #region Employe

        [Route("api/Account/GetEmployeList")]
        [HttpGet]
        public List<EmployeModel> GetEmployeList()
        {
            IAccountLogic IComponent = container.Resolve<IAccountLogic>();
            List<Employe> list = IComponent.GetEmployeList();
            Mapper.CreateMap<Employe, EmployeModel>(); // 配置
            List<EmployeModel> resultList = Mapper.Map<List<Employe>, List<EmployeModel>>(list);
            return resultList;
        }
        [HttpPost]
        public bool InsertEmploye([FromBody]EmployeModel model)
        {
            Mapper.CreateMap<EmployeModel, Employe>(); // 配置
            Employe e = Mapper.Map<EmployeModel, Employe>(model); // 使用AutoMapper自动映射
            IAccountLogic IUser = container.Resolve<IAccountLogic>();
            //执行
            return IUser.InsertEmploye(e);
        }
        [HttpPost]
        public bool UpdateRole([FromBody]EmployeModel model)
        {
            Mapper.CreateMap<EmployeModel, Employe>(); // 配置
            Employe e = Mapper.Map<EmployeModel, Employe>(model); // 使用AutoMapper自动映射
            IAccountLogic IUser = container.Resolve<IAccountLogic>();
            //执行
            return IUser.UpdateEmploye(e);
        }
        public bool DeleteEmploye(Guid id)
        {
            IAccountLogic IUser = container.Resolve<IAccountLogic>();
            //执行
            return IUser.DeleteEmploye(id);
        }
        #endregion
    }
}
