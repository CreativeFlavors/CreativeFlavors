using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Report
{
    public class ProductBuyerStyleReportController : Controller
    {
        //
        // GET: /ProductBuyerStyle/

        public ActionResult Index()
        {
            return View("ProductBuyerStyleReport");
        } 
        [HttpPost]
        public ActionResult RedirectToAspx(string buyer, string buyerModel, string season)
        {
            string SeasonName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(season.Trim()));
            string BuyerMod = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(buyerModel.Trim()));
            string BuyerName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(buyer.Trim()));
            var result = new { Buyer = BuyerName, BuyerModel = BuyerMod, Season = SeasonName };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
