using DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InvoicingSystemWeb.Filters
{
    // 登录认证特性
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
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
                filterContext.HttpContext.Response.Write("<script>location.href='/Account/Login';</script>");
                //filterContext.HttpContext.Response.Redirect("/Account/Login");//否则跳转至登陆页
                return;
            }
            if (authTicket != null && filterContext.HttpContext.User.Identity.IsAuthenticated)//如果Cookies不为Null 也通过验证
            {
                string UserName = authTicket.Name;
                EmployeModel model=JsonConvert.DeserializeObject<EmployeModel>(authTicket.UserData);
                //CommonMethod.setCookieForMIn("UserName", UserName, 30);//用于全局，加载用户信息
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.HttpContext.Response.Write("<script>location.href='/Account/Login';</script>");
                //filterContext.HttpContext.Response.Redirect("/Account/Login");//否则跳转至登陆页
            }
        }
    }
}