using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Common;
using DataModel.Account;
using System.Security.Principal;
using System.Web.Security;
using Newtonsoft.Json;

namespace InvoicingSystemWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly object LOCK = new object();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.SystemName = ConfigurationManager.AppSettings["SystemName"];
            return View();
        }
        [HttpPost]
        public bool Login(string account, string pwd)
        {
            //EmployeModel model = HttpClientHelpClass.GetResponse<EmployeModel>("http://localhost:4157/api/account/login?account=1234&pwd=1234", "123456");
            try
            {
                lock (LOCK)
                {
                    string url = string.Format("{0}/account/login?account={1}&pwd={2}", ConfigurationManager.AppSettings["APIAddress"], account, pwd);
                    EmployeModel model = HttpClientHelpClass.GetResponse<EmployeModel>(url, ConfigurationManager.AppSettings["APIToken"]);
                    if (model != null)
                    {
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                               1,
                               model.employeID.ToString(),
                               DateTime.Now,
                               DateTime.Now.AddMinutes(30),
                               false,
                                JsonConvert.SerializeObject(model),
                               "/"
                               );
                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        System.Web.HttpCookie authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ActionResult Logout()
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie cookies = System.Web.HttpContext.Current.Request.Cookies[cookieName];
            if (cookies != null)
            {
                cookies.Expires = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cookies.Add(cookies);
                System.Web.HttpContext.Current.Request.Cookies.Remove(cookieName);
            }
            //return RedirectToAction("Login");
            return Redirect("/Account/Login");
        }
    }
}