using Common;
using DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InvoicingSystemWeb.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            ViewBag.SystemName = ConfigurationManager.AppSettings["SystemName"];
        }
        protected EmployeModel GetEmployInCookie()
        {
            string cookieName = FormsAuthentication.FormsCookieName;//读取登录授权Cookies的名称
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[cookieName];//接收这个Cookies
            //System.Web.HttpContext.Current.Request.Cookies.Remove("");
            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);//我们知道MVC登录授权的Cookies是加密的，所以我们在此需要解密
            }
            catch (Exception ex)
            {
                HttpContext.Response.Redirect("/Account/Login");//否则跳转至登陆页
                return null;
            }
            if (authTicket != null && HttpContext.User.Identity.IsAuthenticated)//如果Cookies不为Null 也通过验证
            {
                string UserID = authTicket.Name;
                EmployeModel model = JsonConvert.DeserializeObject<EmployeModel>(authTicket.UserData);
                //CommonMethod.setCookieForMIn("UserName", UserName, 30);//用于全局，加载用户信息
                return model;
            }
            else
            {
                HttpContext.Response.Redirect("/Account/Login");//否则跳转至登陆页
                return null;
            }
        }
        protected int GetPageIndex()
        {
            return TypeConverter.StrToInt(Request["iDisplayStart"]);
        }
        protected void InsertBaseData<T>(T model) where T : BaseModel
        {
            EmployeModel e = GetEmployInCookie();
            if (e!=null)
            {
                model.cid = e.employeID;
                model.upid = e.employeID;
                model.ctime = DateTime.Now;
                model.uptime = DateTime.Now;
            }
        }
        protected void UpdateBaseData<T>(T model) where T : BaseModel
        {
            EmployeModel e = GetEmployInCookie();
            if (e != null)
            {
                //model.cid = e.employeID;
                model.upid = e.employeID;
                //model.ctime = DateTime.Now;
                model.uptime = DateTime.Now;
            }
        }
    }
}