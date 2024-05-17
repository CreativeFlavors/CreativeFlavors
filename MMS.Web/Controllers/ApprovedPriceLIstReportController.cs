using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class ApprovedPriceLIstReportController : Controller
    {
        //
        // GET: /ApprovedPriceLIstReport/

        public ActionResult Index()
        {
            return View("~/Views/ApprovedPriceLIstReport/ApprovedPriceLIstReport.cshtml");
        }
      
        [HttpPost]
        public ActionResult RedirectToAspx(string supplierName, string store)
        {
           
            string SupplierName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(supplierName.Trim()));
            string StoreName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(store.Trim()));
            var result = new {  Supplier = SupplierName, Store = StoreName };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
