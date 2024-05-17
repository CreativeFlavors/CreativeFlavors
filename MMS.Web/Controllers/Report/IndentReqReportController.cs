using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Report
{
    public class IndentReqReportController : Controller
    {
        //
        // GET: /IndentReqReport/

        public ActionResult Index()
        {
            return View("IndentReq");
        }
        public ActionResult RedirectToAspx(string indentNo, string indentType, string buyerId,string category,string group,string store)
        {

            string StoreName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(store.Trim()));
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            string IndentNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(indentNo.Trim()));
            string IndentTypes = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(indentType.Trim()));
            string BuyerId = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(buyerId.Trim()));
            var Result = new { Indent = IndentNo, IndentType = IndentTypes, Buyer = BuyerId,Category = CategoryName,Group = GroupName,Store = StoreName};
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


    }
}
