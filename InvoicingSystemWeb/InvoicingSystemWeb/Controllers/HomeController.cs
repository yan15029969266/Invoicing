using Common;
using DataModel;
using DataModel.Component;
using InvoicingSystemWeb.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoicingSystemWeb.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        [Authentication]
        public ActionResult Index()
        {
            CommonModel model = new CommonModel();
            model.employ = GetEmployInCookie();
            string url = string.Format("{0}/Component/GetMenuListByRole?roleID={1}", ConfigurationManager.AppSettings["APIAddress"], model.employ.fk_roleID);
            List<Sys_MenuModel> list = HttpClientHelpClass.GetResponse<List<Sys_MenuModel>>(url, ConfigurationManager.AppSettings["APIToken"]);
            model.MenuList = list;
            return View(model);
        }
        public ActionResult MainPage()
        {
            return View();
        }
    }
}