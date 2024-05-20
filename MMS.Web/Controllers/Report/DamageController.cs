using MMS.Core.Entities.Stock;
using MMS.Data.Context;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Report
{
    public class DamageController : Controller
    {


        #region Damager Report
        public ActionResult Index()
        {
            OrderEntryModel model = new OrderEntryModel();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<OrderEntry> _items = new List<OrderEntry>();
            _items = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true).OrderBy(x => x.BuyerOrderSlNo).ToList();
            model.OrderEntryList = _items;
            return View("Damage", model);
        }
        public ActionResult LotBasedOrder(string lotNo, string Buyer, string SeasonName)
        {
            MMSContext context = new MMSContext();
            OrderEntryModel model = new OrderEntryModel();
            if (lotNo != null)
            {
                var ResultList = context.Database.SqlQuery<OrderEntryModel>("Execute spOrderEntryReport  @OrderType= {0},@BuyerName={1},@Season={2},@Lot={3},@Order={4}", 2, Buyer, SeasonName, lotNo, "").Distinct().ToList();

                var items = ResultList.OrderBy(x => x.OrderEntryId).Select(x => new SelectListItem { Text = x.BuyerOrderSlNo, Value = x.OrderEntryId.ToString() }).Distinct().ToList();
                model.OrderStyleList = items.Distinct().ToList();
                return Json(model.OrderEntryList, JsonRequestBehavior.AllowGet);
            }
            return View("Damage", model);
        }

        public ActionResult FillSeason(int Buyer)
        {
            List<OrderEntry> OrderEntryList_ = new List<OrderEntry>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            SeasonManager seasonmanager = new SeasonManager();
            var items = (from x in buyerOrderEntryManager.Get()
                         join y in seasonmanager.Get()
                         on x.BuyerSeason equals y.SeasonMasterId
                         where x.BuyerName == Buyer
                         select new { season = y.SeasonName, order = y.SeasonMasterId });

            var distinctList = items.GroupBy(x => x.season).Select(g => g.First()).ToList();

            return Json(distinctList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult FillLotNo(string Season)
        {
            List<OrderEntry> OrderEntryList_ = new List<OrderEntry>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            SeasonManager seasonmanager = new SeasonManager();

            var items = (from x in buyerOrderEntryManager.Get().Where(x => x.IsInternal == true)
                         join y in seasonmanager.Get()
                         on x.BuyerSeason equals y.SeasonMasterId
                         where y.SeasonMasterId ==Convert.ToInt32(Season)
                         select new { Lot = x.LotNo, order = x.OrderEntryId });

            var distinctList = items.GroupBy(x => x.Lot).Select(g => g.First()).ToList();

            return Json(distinctList, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult RedirectToAspx(string From, string To, string Order, string Buyer, string group, string store, string category, string lotNo, string season, string material)
        {
            string Orders = "";
            if (Order != "null")
            {
                string[] internalOrder = Order.Split(',');
                var SizeDetails = JsonConvert.DeserializeObject<List<string>>(Order);

                foreach (var item in SizeDetails)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        Orders += item + "" + ",";
                    }
                }

                Orders = Orders.TrimEnd(',');

            }
            else
                Order = "";
            string from = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(From.Trim()));
            string to = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(To.Trim()));
            string Orderno = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(Orders.Trim()));
            string BuyerName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(Buyer.Trim()));
            string groups = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            string stores = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(store.Trim()));
            string categorys = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string lotNos = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(lotNo.Trim()));
            string seasons = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(season.Trim()));
            string materials = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(material.Trim()));
            var result = new { From = from, To = to, Buyer = BuyerName, LotNo = lotNos, Ordersno = Orderno, GroupName = groups, StoreName = stores, CategoryName = categorys, SeasonName = seasons, MaterialType = materials };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
