using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoicingSystemWeb.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            ViewBag.SystemName = ConfigurationManager.AppSettings["SystemName"];
        }
    }
}