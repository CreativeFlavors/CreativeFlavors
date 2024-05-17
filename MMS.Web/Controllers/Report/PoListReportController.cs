using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Report
{
    public class PoListReportController : Controller
    {
        //
        // GET: /PoListReport/

        public ActionResult Index()
        {
            return View("PoListReport");
        }
        public ActionResult IndexPo()
        {
            return View("PoListNewReport");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string indentNo, string from, string to, string season,string supplierName, string store)
        {
            string SeasonName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(season.Trim()));
            string FromDate = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string ToDate = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            string Indent = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(indentNo.Trim()));

            string SupplierName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(supplierName.Trim()));
            string StoreName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(store.Trim()));
            var result = new { From = FromDate, To = ToDate, IndentNo = Indent, Season = SeasonName, Supplier= SupplierName, Store= StoreName };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
