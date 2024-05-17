using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Repository.Managers;
namespace MMS.Web.Controllers.Report
{
    public class GateEntryOutwardDocumentReportController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/GateEntryReport/GateEntryOutwardDocumentReport/GateEntryOutwardDocumentReport.cshtml");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string from, string to, string company, string addressToWhom)
        {
            string from_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string to_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            string company_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(company.Trim()));
            string addressToWhom_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(addressToWhom.Trim()));

            var result = new { From = from_, To = to_, Company = company_, AddressToWhom = addressToWhom_ };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
