using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Report
{
    public class AllStoresGRNReportController : Controller
    {
        //
        // GET: /AllStoresGRNReport/

        public ActionResult Index()
        {
            return View("AllStoresGRNReport");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string store,string supplier,string group,string from , string to,string category,string GRNOption)
        {
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string StoreName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(store.Trim()));
            string SupplierName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(supplier.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim())); 
            string From = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string To = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            string GRNoption_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(GRNOption.Trim()));
            var result = new {Supplier = SupplierName,Group =GroupName,Store =StoreName,FromDate =  From, ToDate = To,Category = CategoryName, GRNoption = GRNoption_ };
            return Json (result, JsonRequestBehavior.AllowGet);
        }

    }
}
