using InvoicingSystemWeb.Filters;
using System;
using System.Collections.Generic;
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
            return View();
        }
        public ActionResult MainPage()
        {
            return View();
        }
    }
}