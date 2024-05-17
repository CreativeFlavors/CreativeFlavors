using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Report
{
    public class ConsumptionStatementController : Controller
    {
        //
        // GET: /ConsumptionStatement/

        public ActionResult Index()
        {
            return View("ConsumptionStatement");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string storeNo, string group, string from, string to, string category, string buyer, string ioNo, string lotNo, string conveyor, string season, string materialType ,string style)
        {
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string StoreNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(storeNo.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            string From = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string To = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));

            string BuyerName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(buyer.Trim()));
            string IONO = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(ioNo.Trim()));
            string ConveyorName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(conveyor.Trim()));
            string SeasonName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(season.Trim()));
            string Material = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(materialType.Trim()));

            string Style = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(style.Trim()));
            string LotNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(lotNo.Trim()));

            var result = new { Group = GroupName, StoreNo = StoreNo, FromDate = From, ToDate = To ,Category = CategoryName,
            Buyer = BuyerName, IoNo =IONO,Conveyor = ConveyorName,Season =SeasonName,MaterialType=Material,Style =Style,LotNo = LotNo};
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
