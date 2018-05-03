using DBDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InvoicingSystemAPI.Controllers
{
    public class TestController : BaseController
    {
        [Route("api/Test/TestMethod")]
        [HttpGet]
        public List<Role> Test()
        {
            return new List<Role> { new Role { roleName = "test1" } };
        }
    }
}
