using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Common;
using System.Security.Principal;
using System.Web.Security;
using Newtonsoft.Json;
using DataModel;
using InvoicingSystemWeb.Filters;
using InvoicingSystemWeb.Extension;

namespace InvoicingSystemWeb.Controllers
{
    public class AccountController : BaseController
    {
        private readonly object LOCK = new object();
        // GET: Account
        //public ActionResult Index()
        //{
        //    return View();
        //}

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

                    string url = string.Format("{0}/account/login?account={1}&pwd={2}", ConfigurationManager.AppSettings["APIAddress"], account, MD5HelpClass.CreateMD5Hash(pwd));
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

          #region Employer
        [Authentication]
        [UserPermission]
        public ActionResult Employer()
        {
            return View();
        }
        public ActionResult EmployerList()
        {
            int pageSize = 20;
            int pageIndex = (GetPageIndex() / pageSize) + 1;
            string url = string.Format("{0}/Account/GetEmployeList?pageIndex={1}&pageSize={2}", ConfigurationManager.AppSettings["APIAddress"], pageIndex, pageSize);
            List<EmployeModel> resultList = HttpClientHelpClass.GetResponse<List<EmployeModel>>(url, ConfigurationManager.AppSettings["APIToken"]);
            return Json(new
            {
                iDisplayStart = pageSize,
                iTotalRecords = resultList.Count,
                iTotalDisplayRecords = resultList.Count,
                aaData = resultList
            }
            , JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authentication]
        public ActionResult AddEmployer()
        {
            string statusCode = "";
            EmployeModel model = new EmployeModel { employeID = Guid.NewGuid() };
            string url = string.Format("{0}/Account/GetNewEmployeNo", ConfigurationManager.AppSettings["APIAddress"]);
            model.employeNo = HttpClientHelpClass.GetResponse(url, ConfigurationManager.AppSettings["APIToken"], out statusCode);
            return PartialView("EmployerForm", model);
        }
        [HttpPost]
        [Authentication]
        public ActionResult AddEmployer(EmployeModel model)
        {

            InsertBaseData(model);
            string url = string.Format("{0}/Account/InsertEmploye", ConfigurationManager.AppSettings["APIAddress"]);
            string statusCode = string.Empty;
            bool isSuccess = Convert.ToBoolean(HttpClientHelpClass.PostResponse<EmployeModel>(url, model, ConfigurationManager.AppSettings["APIToken"], out statusCode));
            if (isSuccess)
            {
                return Json(new OperationResult(OperationResultType.Success, "添加成功！"));
            }
            else
            {
                return Json(new OperationResult(OperationResultType.Warning, "添加失败！"));
            }

        }
        [HttpGet]
        [Authentication]
        public ActionResult ModifyEmployer(Guid id)
        {
            string url = string.Format("{0}/Account/GetEmploye?id={1}", ConfigurationManager.AppSettings["APIAddress"], id);
            EmployeModel model = HttpClientHelpClass.GetResponse<EmployeModel>(url, ConfigurationManager.AppSettings["APIToken"]);
            return PartialView("EmployerForm", model);
        }
        [HttpPost]
        [Authentication]
        public ActionResult ModifyEmployer(EmployeModel model)
        {
            UpdateBaseData(model);
            try
            {
                string url = string.Format("{0}/Account/UpdateEmploye", ConfigurationManager.AppSettings["APIAddress"]);
                string statusCode = string.Empty;
                bool isSuccess = Convert.ToBoolean(HttpClientHelpClass.PostResponse<EmployeModel>(url, model, ConfigurationManager.AppSettings["APIToken"], out statusCode));
                if (isSuccess)
                {
                    return Json(new OperationResult(OperationResultType.Success, "修改成功！"));
                }
                else
                {
                    return Json(new OperationResult(OperationResultType.Warning, "修改失败！"));
                }
            }
            catch (Exception e)
            {
                return Json(new OperationResult(OperationResultType.Warning, "添加失败！", e.Message));
            }
        }
        [Authentication]
        public ActionResult DeleteEmployer(Guid id)
        {
            try
            {
                string statusCode = "";
                string url = string.Format("{0}/Account/DeleteEmploye?id={1}", ConfigurationManager.AppSettings["APIAddress"], id);
                bool isSuccess = Convert.ToBoolean(HttpClientHelpClass.GetResponse(url, ConfigurationManager.AppSettings["APIToken"], out statusCode));
                if (isSuccess)
                {
                    return Json(new OperationResult(OperationResultType.Success, "删除成功！"));
                }
                else
                {
                    return Json(new OperationResult(OperationResultType.Warning, "删除失败！"));
                }
            }
            catch (Exception e)
            {
                return Json(new OperationResult(OperationResultType.Warning, "删除失败！", e.Message));
            }
        }
        #endregion
        #region Role
        [Authentication]
        [UserPermission]
        public ActionResult Role()
        {
            return View();
        }
        public ActionResult RoleList()
        {
            int pageSize = 20;
            int pageIndex = (GetPageIndex() / pageSize) + 1;

            //List<Sys_ButtonModel> list = new List<Sys_ButtonModel>();
            string url = string.Format("{0}/Account/GetRoleList?pageIndex={1}&pageSize={2}", ConfigurationManager.AppSettings["APIAddress"], pageIndex, pageSize);
            List<RoleModel> list = HttpClientHelpClass.GetResponse<List<RoleModel>>(url, ConfigurationManager.AppSettings["APIToken"]);
            return Json(new
            {
                iDisplayStart = pageSize,
                iTotalRecords = list.Count,
                iTotalDisplayRecords = list.Count,
                aaData = list
            }
            , JsonRequestBehavior.AllowGet);
        }
        [Authentication]
        [HttpGet]
        public ActionResult RoleAuth(Guid roleID)
        {
            string url = string.Format("{0}/Account/GetRoleAuth?roleID={1}", ConfigurationManager.AppSettings["APIAddress"], roleID);
            AuthModel model = HttpClientHelpClass.GetResponse<AuthModel>(url, ConfigurationManager.AppSettings["APIToken"]);
            //AuthModel model=new AuthModel();

            return PartialView("RoleAuthForm", model);
        }
        [HttpPost]
        public ActionResult RoleAuth(string jasonData)
        {
            try
            {
                AuthModel model = JsonConvert.DeserializeObject<AuthModel>(jasonData);
                string url = string.Format("{0}/Account/SetRoleAuth", ConfigurationManager.AppSettings["APIAddress"]);
                string statusCode = string.Empty;
                bool isSuccess = Convert.ToBoolean(HttpClientHelpClass.PostResponse<AuthModel>(url, model, ConfigurationManager.AppSettings["APIToken"], out statusCode));
                //bool isSuccess = false;
                if (isSuccess)
                {
                    return Json(new OperationResult(OperationResultType.Success, "修改成功！"));
                }
                else
                {
                    return Json(new OperationResult(OperationResultType.Warning, "修改失败！"));
                }
            }
            catch (Exception e)
            {
                return Json(new OperationResult(OperationResultType.Error, "修改失败！", e.Message));
            }
        }
        [HttpGet]
        [Authentication]
        public ActionResult AddRole()
        {
            RoleModel model = new RoleModel { roleID = Guid.NewGuid() };
            return PartialView("RoleForm", model);
        }
        [HttpPost]
        [Authentication]
        public ActionResult AddRole(RoleModel model)
        {
            InsertBaseData(model);
            try
            {
                string url = string.Format("{0}/Account/InsertRole", ConfigurationManager.AppSettings["APIAddress"]);
                string statusCode = string.Empty;
                bool isSuccess = Convert.ToBoolean(HttpClientHelpClass.PostResponse<RoleModel>(url, model, ConfigurationManager.AppSettings["APIToken"], out statusCode));
                if (isSuccess)
                {
                    return Json(new OperationResult(OperationResultType.Success, "添加成功！"));
                }
                else
                {
                    return Json(new OperationResult(OperationResultType.Warning, "添加失败！"));
                }
            }
            catch (Exception e)
            {
                return Json(new OperationResult(OperationResultType.Warning, "添加失败！", e.Message));
            }
        }

        [HttpGet]
        [Authentication]
        public ActionResult ModifyRole(Guid Id)
        {
            string url = string.Format("{0}/Account/GetRole?id={1}", ConfigurationManager.AppSettings["APIAddress"], Id);
            Sys_MenuModel model = HttpClientHelpClass.GetResponse<Sys_MenuModel>(url, ConfigurationManager.AppSettings["APIToken"]);
            return PartialView("RoleForm", model);
        }
        [HttpGet]
        [Authentication]
        public ActionResult ModifyRole(RoleModel model)
        {
            UpdateBaseData(model);
            try
            {
                string url = string.Format("{0}/Account/UpdateRole", ConfigurationManager.AppSettings["APIAddress"]);
                string statusCode = string.Empty;
                bool isSuccess = Convert.ToBoolean(HttpClientHelpClass.PostResponse<RoleModel>(url, model, ConfigurationManager.AppSettings["APIToken"], out statusCode));
                if (isSuccess)
                {
                    return Json(new OperationResult(OperationResultType.Success, "修改成功！"));
                }
                else
                {
                    return Json(new OperationResult(OperationResultType.Warning, "修改失败！"));
                }
            }
            catch (Exception e)
            {
                return Json(new OperationResult(OperationResultType.Warning, "添加失败！", e.Message));
            }
        }
        [Authentication]
        public ActionResult DeleteRole(Guid id)
        {
            try
            {
                string statusCode = "";
                string url = string.Format("{0}/Account/DeleteRole?id={1}", ConfigurationManager.AppSettings["APIAddress"], id);
                bool isSuccess = Convert.ToBoolean(HttpClientHelpClass.GetResponse(url, ConfigurationManager.AppSettings["APIToken"],out statusCode));
                if (isSuccess)
                {
                    return Json(new OperationResult(OperationResultType.Success, "删除成功！"));
                }
                else
                {
                    return Json(new OperationResult(OperationResultType.Warning, "删除失败！"));
                }
            }
            catch (Exception e)
            {
                return Json(new OperationResult(OperationResultType.Warning, "删除失败！", e.Message));
            }
        }
        #endregion

    }
}