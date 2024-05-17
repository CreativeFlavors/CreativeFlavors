using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Report
{
    public class CuttingSlipReportController : Controller
    {
        //
        // GET: /CuttingSlip/

        public ActionResult Index()
        {
            return View("CuttingSlipReport");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string buyer, string season, string group, string  category, string lotNo,string IoNo)
        {
            string IONo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(IoNo.Trim()));
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string BuyerName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(buyer.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            string BuyerSeason = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(season.Trim()));
            string LotNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(lotNo.Trim()));
            var result = new { Buyer = BuyerName, Group = GroupName, Category = CategoryName, Season = BuyerSeason, LotNo = LotNo ,IoNo = IONo};
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
