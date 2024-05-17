using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MMS.Common
{
    public class CustomFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            if (actionName != "Login")
            {
                string UserName = Convert.ToString(HttpContext.Current.Session["UserName"]);
                if (UserName == "")
                {
                    filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Accounts" },
                    { "action", "Login" }
                });
                }
                HttpContext.Current.Session["tagname"] = controllerName;
            }
            //else
            //{
            //    HttpContext.Current.Session["tagname"] = controllerName;
            //}
        }
    }
}
