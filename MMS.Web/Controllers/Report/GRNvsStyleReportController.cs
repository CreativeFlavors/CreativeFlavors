using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Report
{
    public class GRNvsStyleReportController : Controller
    {
        //
        // GET: /GRNvsStyleReport/

        public ActionResult Index()
        {
            return View("GrnVsStyleReport");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string  style)
        {
            string Style = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(style.Trim()));            
            return Json(Style, JsonRequestBehavior.AllowGet);
        } 
    }
}
