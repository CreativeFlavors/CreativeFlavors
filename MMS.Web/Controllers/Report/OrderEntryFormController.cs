using MMS.Core.Entities.Stock;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Report
{
    public class OrderEntryFormController : Controller
    {
        //
        // GET: /OrderEntryForm/

        public ActionResult Index()
        {
            return View("OrderEntryFormReport");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string buyer, string lotNo, string orderEntryId, string poNo)
        {
            string BuyerName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(buyer.Trim()));
            string Lot = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(lotNo.Trim()));
            string OrderEntry = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(orderEntryId.Trim()));
            string Po = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(poNo.Trim()));
            var result = new { Buyer = BuyerName, LotNo = Lot, OrderEntryId = OrderEntry, PoNo = Po };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region OrderEntry  Report View
        public ActionResult OrderEntryReport()
        {
            return View("OrderEntryReport");
        }
        [HttpPost]
        public ActionResult RedirectToOrderEntryAspx(string orderType, string buyerName, string season, string lot, string order)
        //string seasonName, string style, string sizeRangeName, string sizeRange)
        {
            
            string OrderType = orderType.Trim();
            string BuyerName = buyerName.Trim();
            string Season = season.Trim();
            string Lot = lot.Trim();
            string Order = order.Trim();

            var result = new { OrderType = OrderType, BuyerName = BuyerName, season = Season, Lot = Lot, Order = Order };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

       

    }

}

