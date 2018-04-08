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
using System.Text;
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
        [Route("api/Account/Login")]
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
        public List<RoleModel> GetRoleList(int pageIndex, int pageSize)
        {
            IAccountLogic IComponent = container.Resolve<IAccountLogic>();
            List<Role> list = IComponent.GetRoleList(pageIndex,pageSize);
            Mapper.CreateMap<Role, RoleModel>(); // 配置
            List<RoleModel> resultList = Mapper.Map<List<Role>, List<RoleModel>>(list); // 使用AutoMapper自动映射
            return resultList;
        }
        [Route("api/Account/InsertRole")]
        [HttpPost]
        public bool InsertRole([FromBody]RoleModel model)
        {
            Mapper.CreateMap<RoleModel, Role>(); // 配置
            Role role = Mapper.Map<RoleModel, Role>(model); // 使用AutoMapper自动映射
            IAccountLogic IUser = container.Resolve<IAccountLogic>();
            //执行
            return IUser.InsertRole(role);
        }
        [Route("api/Account/UpdateRole")]
        [HttpPost]
        public bool UpdateRole([FromBody]RoleModel model)
        {
            Mapper.CreateMap<RoleModel, Role>(); // 配置
            Role role = Mapper.Map<RoleModel, Role>(model); // 使用AutoMapper自动映射
            IAccountLogic IUser = container.Resolve<IAccountLogic>();
            //执行
            return IUser.UpdateRole(role);
        }
        [Route("api/Account/DeleteRole")]
        public bool DeleteRole(Guid id)
        {
            IAccountLogic IUser = container.Resolve<IAccountLogic>();
            //执行
            return IUser.DeleteRole(id);
        }
        [HttpGet]
        [Route("api/Account/GetRoleAuth")]
        public AuthModel GetRoleAuth(Guid roleID)
        {
            IAccountLogic IUser = container.Resolve<IAccountLogic>();
            return IUser.GetRoleAuth(roleID);
        }
        #endregion
        #region Employe

        [Route("api/Account/GetEmployeList")]
        [HttpGet]
        public List<EmployeModel> GetEmployeList(int pageIndex, int pageSize)
        {
            IAccountLogic IComponent = container.Resolve<IAccountLogic>();
            List<Employe> list = IComponent.GetEmployeList(pageIndex,pageSize);
            Mapper.CreateMap<Employe, EmployeModel>(); // 配置
            List<EmployeModel> resultList = Mapper.Map<List<Employe>, List<EmployeModel>>(list);
            return resultList;
        }
        [Route("api/Account/InsertEmploye")]
        [HttpPost]
        public bool InsertEmploye([FromBody]EmployeModel model)
        {
            Mapper.CreateMap<EmployeModel, Employe>(); // 配置
            Employe e = Mapper.Map<EmployeModel, Employe>(model); // 使用AutoMapper自动映射
            IAccountLogic IUser = container.Resolve<IAccountLogic>();
            //执行
            return IUser.InsertEmploye(e);
        }
        [Route("api/Account/UpdateRole")]
        [HttpPost]
        public bool UpdateRole([FromBody]EmployeModel model)
        {
            Mapper.CreateMap<EmployeModel, Employe>(); // 配置
            Employe e = Mapper.Map<EmployeModel, Employe>(model); // 使用AutoMapper自动映射
            IAccountLogic IUser = container.Resolve<IAccountLogic>();
            //执行
            return IUser.UpdateEmploye(e);
        }
        [HttpGet]
        [Route("api/Account/DeleteEmploye")]
        public bool DeleteEmploye(Guid id)
        {
            IAccountLogic IUser = container.Resolve<IAccountLogic>();
            //执行
            return IUser.DeleteEmploye(id);
        }
        [HttpGet]
        [Route("api/Account/GetNewEmployeNo")]
        public HttpResponseMessage GetNewEmployeNo()
        {
            IAccountLogic IUser = container.Resolve<IAccountLogic>();
            //执行
            HttpResponseMessage responseMessage = new HttpResponseMessage { Content = new StringContent(IUser.GetNewEmployeNo(), Encoding.GetEncoding("UTF-8"), "text/plain") };
            return responseMessage;
            //return IUser.GetNewEmployeNo();
        }
        #endregion
    }
}
