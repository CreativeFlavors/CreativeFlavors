using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Repository.Managers;
namespace MMS.Web.Controllers.Report
{
    public class GateEntryVisitorReportController : Controller
    {
        //
        // GET: /GrnRegister/

        public ActionResult Index()
        {
            return View("~/Views/GateEntryReport/GateEntryVisitorReport/GateEntryVisitorReport.cshtml");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string from, string to, string gateEntryNo, string visitorName, string visitorType)
        {
            string from_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string to_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            string gateEntryNo_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(gateEntryNo.Trim()));
            string visitorName_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(visitorName.Trim()));
            string visitorType_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(visitorType.Trim()));

            var result = new { From = from_, To = to_, GateEntryNo = gateEntryNo_, VisitorName = visitorName_, VisitorType = visitorType_ };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}
