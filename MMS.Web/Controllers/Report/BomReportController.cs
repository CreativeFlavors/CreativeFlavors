using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Core.Entities.Stock;
using MMS.Web.Models.StockModel;
using MMS.Repository.Managers.StockManager;
using MMS.Repository.Managers;
using MMS.Core.Entities;
using MMS.Web.Controllers.Report.StoredProcedureModel;
using MMS.Data.Context;
namespace MMS.Web.Controllers.Report
{
    public class BomReportController : Controller
    {
        #region BOM Report
        public ActionResult Index(string lotNo, string SeasonName, string Buyer)
        {
            MMSContext context = new MMSContext();
            BillOfMaterialModel model = new BillOfMaterialModel();
            if (lotNo != null)
            {
                var ResultList = context.Database.SqlQuery<BomModel>("Execute GetBOMNo  @LotNo= {0},@BuyerName={1},@SeasonName={2}", lotNo, Buyer, SeasonName).Distinct().ToList();
                var items = ResultList.OrderBy(x => x.BomId).Select(x => new SelectListItem { Text = x.BomNo, Value = x.BomId.ToString() }).Distinct().ToList();
                model.bomStyleList = items.Distinct().ToList();
                return Json(new { ResultList = model.bomStyleList }, JsonRequestBehavior.AllowGet);
            }
            return View("BomReport", model);
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string Buyer, string Bom, string store, string group, string category, string lotNo, string season, string material)
        {
            string BomNo = string.Empty;
            string Lot = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(lotNo.Trim()));
            string SeasonName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(season.Trim()));
            string MaterialName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(material.Trim()));
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string StoreName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(store.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            BomNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(Bom.Trim()));
            string BuyerName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(Buyer.Trim()));
            HttpCookie cName = new HttpCookie("Name");
            cName.Value = BomNo;
            cName.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cName);
            var result = new { Buyer = BuyerName, Bom = "", Group = GroupName, Store = StoreName, Category = CategoryName, LotNo = Lot, Season = SeasonName, Material = MaterialName };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSeasonToLotno(int seasonIds)
        {
            BuyerOrderEntryManager manager = new BuyerOrderEntryManager();
            var lotNo = manager.Get().Where(x => x.BuyerSeason == seasonIds && x.IsDeleted == false && x.IsInternal == true).Select(x => new { x.LotNo }).Distinct().ToList();

            var Result = new
            {
                LotNo = lotNo
            };
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetBuyerToSeason(int Buyer)
        {
            BuyerOrderEntryManager manager = new BuyerOrderEntryManager();
            var Season = manager.Get().Where(x => x.BuyerName == Buyer).Select(x => new { x.BuyerSeason }).Distinct().ToList();
            SeasonManager seasonManager = new SeasonManager();
            List<SeasonMaster> listSeasonMaster = new List<SeasonMaster>();
            var SeasonIds = Season.GroupBy(x => x.BuyerSeason).ToList();
            if (SeasonIds != null && SeasonIds.Count > 0)
            {
                foreach (var item in SeasonIds)
                {
                    SeasonMaster seasonMaster = new SeasonMaster();
                    seasonMaster = seasonManager.GetSeasonMasterId(Convert.ToInt32(item.Key));
                    listSeasonMaster.Add(seasonMaster);
                }
                var listSeasonMasters = listSeasonMaster.Select(x => new { x.SeasonMasterId, x.SeasonName }).Distinct().ToList();
                return Json(listSeasonMasters, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region BOM Cost report
        public ActionResult BomCostingReport(string lotNo, string SeasonName, string Buyer)
        {
            MMSContext context = new MMSContext();
            BillOfMaterialModel model = new BillOfMaterialModel();
            if (lotNo != null)
            {
                var ResultList = context.Database.SqlQuery<BomModel>("Execute spBom  @LotNo= {0},@BuyerName={1},@SeasonName={2}", lotNo, Buyer, SeasonName).Distinct().ToList();

                var items = ResultList.OrderBy(x => x.BomId).Select(x => new SelectListItem { Text = x.BomNo, Value = x.BomId.ToString() }).Distinct().ToList();
                model.bomStyleList = items.Distinct().ToList();
                return Json(model.bomStyleList, JsonRequestBehavior.AllowGet);
            }
            return View("BomCostingReport", model);
        }
        [HttpPost]
        public ActionResult BOMCostToAspx(string Buyer, string Bom, string store, string group, string category, string lotNo, string season, string material)
        {
            string Lot = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(lotNo.Trim()));
            string SeasonName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(season.Trim()));
            string MaterialName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(material.Trim()));
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string StoreName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(store.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            string BomNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(Bom.Trim()));
            string BuyerName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(Buyer.Trim()));


            var result = new { Buyer = BuyerName, Bom = BomNo, Group = GroupName, Store = StoreName, Category = CategoryName, LotNo = Lot, Season = SeasonName, Material = MaterialName };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
