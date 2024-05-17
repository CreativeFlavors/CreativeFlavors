using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Repository.Managers;
namespace MMS.Web.Controllers.Report
{
    public class GateEntryInwardDocumentReportController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/GateEntryReport/GateEntryInwardDocumentReport/GateEntryInwardDocumentReport.cshtml");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string from, string to, string fromWhere, string addressToWhom)
        {
            string from_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string to_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            string fromWhere_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(fromWhere.Trim()));
            string addressToWhom_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(addressToWhom.Trim()));

            var result = new { From = from_, To = to_,FromWhere = fromWhere_,AddressToWhom = addressToWhom_};
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
