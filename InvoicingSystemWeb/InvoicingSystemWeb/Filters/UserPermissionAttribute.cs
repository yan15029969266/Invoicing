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
                string requestUrl = HttpContext.Current.Request.Url.AbsolutePath + HttpContext.Current.Request.Url.Query;
                string Apiurl = string.Format("{0}/Component/GetMenuListByUrl?url={1}", ConfigurationManager.AppSettings["APIAddress"],requestUrl);
                Sys_MenuModel menu = HttpClientHelpClass.GetResponse<Sys_MenuModel>(Apiurl, ConfigurationManager.AppSettings["APIToken"]);
                //Sys_MenuModel menu = list.Where(t => t.enable == true && t.menuUrl == requestUrl).FirstOrDefault();
                if (menu != null)
                {
                    Apiurl = string.Format("{0}/Component/GetButtonByRoleAndUrl?roleID={1}&menuID={2}", ConfigurationManager.AppSettings["APIAddress"], model.fk_roleID,menu.menuID);
                    List<Sys_ButtonModel> btnList = HttpClientHelpClass.GetResponse<List<Sys_ButtonModel>>(Apiurl, ConfigurationManager.AppSettings["APIToken"]);
                    string btnToolBarHTML = "";
                    foreach (var btn in btnList)
                    {
                        //btnToolBarHTML = string.Format("<div class=\"btn-toolbar\">{0}</div>", btnToolBarHTML);
                        btnToolBarHTML += string.Format("<button class=\"btn btn-primary margin\" onclick=\"{0}\">{1}</button>", btn.func, btn.btnName);
                    }
                    btnToolBarHTML = string.Format("<div class=\"btn-toolbar\">{0}</div>", btnToolBarHTML);
                    ((ViewResult)filterContext.Result).ViewBag.btnToolBar = MvcHtmlString.Create(btnToolBarHTML);
                }
                //CommonMethod.setCookieForMIn("UserName", UserName, 30);//用于全局，加载用户信息
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("/Account/Login");//否则跳转至登陆页
            }
        }
        public void CreateBreadcrumbNavigation(ResultExecutingContext filterContext)
        {
            string requestUrl = HttpContext.Current.Request.Url.AbsolutePath + HttpContext.Current.Request.Url.Query;
            string Apiurl = string.Format("{0}/Component/GetMenuListByUrl?url={1}", ConfigurationManager.AppSettings["APIAddress"], requestUrl);
            Sys_MenuModel menu = HttpClientHelpClass.GetResponse<Sys_MenuModel>(Apiurl, ConfigurationManager.AppSettings["APIToken"]);
            if(menu!=null)
            {
                Apiurl = string.Format("{0}/Component/GetMenuList", ConfigurationManager.AppSettings["APIAddress"]);
                List<Sys_MenuModel> list = HttpClientHelpClass.GetResponse<List<Sys_MenuModel>>(Apiurl, ConfigurationManager.AppSettings["APIToken"]);
                var result = new
                {
                    name = menu.menuName,
                    pname = list.FirstOrDefault(o => o.menuID == menu.parentID).menuName,
                    purl = list.FirstOrDefault(o => o.menuID == menu.parentID).menuUrl
                };
                //var result = list.Select(
                //    t => new
                //    {
                //        name = t.menuName,
                //        pname = bll.GetMenuList().FirstOrDefault(o => o.menuID == t.parentID).menuName,
                //        purl = bll.GetMenuList().FirstOrDefault(o => o.menuID == t.parentID).menuUrl
                //    }).FirstOrDefault();
                string breadcrumbStr = string.Format("<li><a href=\"{0}\">{1}</a></li><li class=\"active\">{2}</li>", result.purl, result.pname, result.name);
                breadcrumbStr = string.Format("<ol class=\"breadcrumb\"><li><a href=\"#\"><i class=\"fa fa-dashboard\"></i> 主页</a></li>{0}</ol>", breadcrumbStr);
                ((ViewResult)filterContext.Result).ViewBag.PageTitle = MvcHtmlString.Create("<h1>" + result.name + "<small> " + result.pname + "</small></h1>");
                ((ViewResult)filterContext.Result).ViewBag.Breadcrumb = MvcHtmlString.Create(breadcrumbStr);
            }
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