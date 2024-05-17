using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Report
{
    public class MRPRequirementController : Controller
    {
        //
        // GET: /MRPRequirement/

        //
        // GET: /IndentReqReport/

        public ActionResult Index()
        {
            return View("MrpReqReport");
        }
        public ActionResult RedirectToAspx(string mrpNo, string indentType, string buyerId, string category, string group, string store)
        {

            string StoreName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(store.Trim()));
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            string MrpNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(mrpNo.Trim()));
            string IndentTypes = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(indentType.Trim()));
            string BuyerId = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(buyerId.Trim()));
            var Result = new { MRPNO = MrpNo, IndentType = IndentTypes, Buyer = BuyerId, Category = CategoryName, Group = GroupName, Store = StoreName };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


    }
}
