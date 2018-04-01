using DataModel.Account;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InvoicingSystemWeb.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class UserPermissionAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //生成Module操作
            CreatePermission(filterContext);
            //生成面包屑&&Title
            CreateBreadcrumbNavigation(filterContext);
        }
        public void CreatePermission(ResultExecutingContext filterContext)
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
                filterContext.HttpContext.Response.Redirect("/Account/Login");//否则跳转至登陆页
                return;
            }
            if (authTicket != null && filterContext.HttpContext.User.Identity.IsAuthenticated)//如果Cookies不为Null 也通过验证
            {
                string UserID = authTicket.Name;
                EmployeModel model = JsonConvert.DeserializeObject<EmployeModel>(authTicket.UserData);
                //CommonMethod.setCookieForMIn("UserName", UserName, 30);//用于全局，加载用户信息
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("/Account/Login");//否则跳转至登陆页
            }
            //if (!string.IsNullOrEmpty(CookieHelper.GetCookieValue("adminID")))
            //{
            //    AuthenticationBll auth_bll = new AuthenticationBll();
            //    AccountServiceBll account_bll = new AccountServiceBll();
            //    Guid userId = new Guid(CookieHelper.GetCookieValue("adminID"));
            //    AdminInfoModel admin = new AdminInfoModel();
            //    admin = (AdminInfoModel)SessionHelper.GetObj("adminInfo");
            //    if (admin == null)
            //    {
            //        Guid userID = new Guid(CookieHelper.GetCookieValue("adminID"));
            //        Sys_Admin entity = account_bll.GetAdminByID(userID);
            //        Mapper.CreateMap<Sys_Admin, AdminInfoModel>(); // 配置
            //        admin = Mapper.Map<Sys_Admin, AdminInfoModel>(entity); // 使用AutoMapper自动映射

            //    }
            //    if (admin != null)
            //    {
            //        string requestUrl = HttpContext.Current.Request.Url.AbsolutePath + HttpContext.Current.Request.Url.Query;

            //        List<Sys_MenuInfo> list = auth_bll.GetMenuList().Where(t => t.enable == true && t.menuUrl == requestUrl).ToList();
            //        if (list.Count > 0)
            //        {
            //            List<Sys_Button> btnList = auth_bll.GetButtonByID(admin.adminId, list.FirstOrDefault().menuID);
            //            string btnToolBarHTML = "";
            //            foreach (var btn in btnList)
            //            {
            //                //btnToolBarHTML = string.Format("<div class=\"btn-toolbar\">{0}</div>", btnToolBarHTML);
            //                btnToolBarHTML += string.Format("<button class=\"btn btn-primary margin\" onclick=\"{0}\">{1}</button>", btn.func, btn.btnName);
            //            }
            //            btnToolBarHTML = string.Format("<div class=\"btn-toolbar\">{0}</div>", btnToolBarHTML);
            //            ((ViewResult)filterContext.Result).ViewBag.btnToolBar = MvcHtmlString.Create(btnToolBarHTML);
            //        }
            //    }
            //}
        }
        public void CreateBreadcrumbNavigation(ResultExecutingContext filterContext)
        {
            string requestUrl = HttpContext.Current.Request.Url.AbsolutePath + HttpContext.Current.Request.Url.Query; ;
            //AuthenticationBll bll = new AuthenticationBll();
            //List<Sys_MenuInfo> list = bll.GetMenuList().Where(t => t.enable == true && t.menuUrl == requestUrl).ToList();
            //if (list.Count > 0)
            //{
            //    var result = list.Select(
            //        t => new
            //        {
            //            name = t.menuName,
            //            pname = bll.GetMenuList().FirstOrDefault(o => o.menuID == t.parentID).menuName,
            //            purl = bll.GetMenuList().FirstOrDefault(o => o.menuID == t.parentID).menuUrl
            //        }).FirstOrDefault();
            //    string breadcrumbStr = string.Format("<li><a href=\"{0}\">{1}</a></li><li class=\"active\">{2}</li>", result.purl, result.pname, result.name);
            //    breadcrumbStr = string.Format("<ol class=\"breadcrumb\"><li><a href=\"#\"><i class=\"fa fa-dashboard\"></i> 主页</a></li>{0}</ol>", breadcrumbStr);
            //    ((ViewResult)filterContext.Result).ViewBag.PageTitle = MvcHtmlString.Create("<h1>" + result.name + "<small> " + result.pname + "</small></h1>");
            //    ((ViewResult)filterContext.Result).ViewBag.Breadcrumb = MvcHtmlString.Create(breadcrumbStr);
            //}
        }
    }
}