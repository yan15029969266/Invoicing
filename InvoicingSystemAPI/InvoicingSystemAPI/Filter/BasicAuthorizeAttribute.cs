using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace InvoicingSystemAPI.Filter
{
    public class BasicAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Contains("AuthToken"))
            {
                var key = actionContext.Request.Headers.GetValues("AuthToken").FirstOrDefault();
                try
                {
                    var appKey = ConfigurationManager.AppSettings["AuthToken"];
                    if (!appKey.Equals(key))
                    {
                        throw new UnauthorizedAccessException("Invalid key, please check.");
                    }
                }
                catch (Exception e)
                {
                    throw new UnauthorizedAccessException("Invalid key, please check.");
                }
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid key, please check.");
            }
        }
    }
}