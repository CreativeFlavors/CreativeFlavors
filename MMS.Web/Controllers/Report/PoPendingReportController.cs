using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Report
{
    public class PoPendingReportController : Controller
    {
        #region Po Pending Report
        public ActionResult Index()
        {
            return View("PoPendingReport");
        }

        [HttpPost]
        public ActionResult RedirectToAspx(string PoNo, string from, string to, string season, string supplierName, string store)
        {
            string SeasonName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(season.Trim()));
            string FromDate = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string ToDate = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            string Pono = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(PoNo.Trim()));

            string SupplierName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(supplierName.Trim()));
            string StoreName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(store.Trim()));
            var result = new { From = FromDate, To = ToDate, PoNo = Pono, Season = SeasonName, Supplier = SupplierName, Store = StoreName };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Indent Pending Report
        public ActionResult IndentPendingReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RedirectToIndentPendingAspx(string indentNo, string from, string to, string season, string supplierId)
        {
            string SeasonName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(season.Trim()));
            string FromDate = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string ToDate = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            string Indent = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(indentNo.Trim()));
            string supplierid = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(supplierId.Trim()));
            var result = new { From = FromDate, To = ToDate, IndentNo = Indent, Season = SeasonName, SupplierID = supplierid };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

}
