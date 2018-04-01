using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Unity;

namespace InvoicingSystemAPI.Controllers
{
    public class BaseController : ApiController
    {
        protected UnityContainer container = new UnityContainer();
        public BaseController()
        {
            UnityConfigurationSection config = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
            //加载到容器
            config.Configure(container, "MyContainer");
        }

    }
}
