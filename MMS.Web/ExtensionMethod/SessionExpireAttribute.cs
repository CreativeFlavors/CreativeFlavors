using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;

namespace MMS.Web.ExtensionMethod
{

    public class SessionExpireAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            // check  sessions here
            if (HttpContext.Current.Session["UserName"] == null)
            {
                //filterContext.Result = new RedirectResult("Accounts/login");
                filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    {"controller", "Accounts"},
                    {"action", "Login" }
               });

                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}