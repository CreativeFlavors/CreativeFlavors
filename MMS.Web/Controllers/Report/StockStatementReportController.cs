using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Repository.Managers;
using MMS.Core.Entities;
using MMS.Repository.Managers.StockManager;

namespace MMS.Web.Controllers.Report
{
    public class StockStatementReportController : Controller
    {
        //
        // GET: /StockStatement/

        public ActionResult Index()
        {
            return View("StockStatement");
        }
        #region Stock Adjustment
        public ActionResult StockAdjustementReport()
        {
            return View();
        }
        public ActionResult StockReportwithPCs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StockReportwithPCsRedirectToAspx(string storeNo, string group, string from, string to, string category, string materialType, string material)
        {
            string MaterialName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(material.Trim()));
            string MaterialTypes = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(materialType.Trim()));
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string StoreNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(storeNo.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            string From = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string To = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            var result = new { Group = GroupName, StoreNo = StoreNo, FromDate = From, ToDate = To, Category = CategoryName, MaterialType = MaterialTypes, Material = MaterialName };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #region StockwithSizeRange

        [HttpPost]
        public ActionResult RedirectStockAdjustementReportToAspx(string storeNo, string group, string from, string to, string category, string materialType, string material,string StockNo)
        {
            string MaterialName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(material.Trim()));
            string MaterialTypes = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(materialType.Trim()));
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string StoreNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(storeNo.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            string StockAdjustmentNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(StockNo.Trim()));
            var result = new { Group = GroupName, StoreNo = StoreNo, Category = CategoryName, MaterialType = MaterialTypes, Material = MaterialName, StockAdjustmentNo= StockAdjustmentNo };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        [HttpPost]
        public ActionResult RedirectToAspx(string storeNo, string group, string from, string to,string category,string materialType,string material)
        {
            string MaterialName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(material.Trim()));
            string MaterialTypes = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(materialType.Trim()));
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string StoreNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(storeNo.Trim())); 
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            string From = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string To = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            var result = new {  Group = GroupName, StoreNo = StoreNo, FromDate = From, ToDate = To,Category = CategoryName,MaterialType = MaterialTypes, Material = MaterialName };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #region StockwithSizeRange
        public ActionResult StockReportSizeRange()
        {
            return View("StockReportSizeRange");
        }
        [HttpPost]
        public ActionResult RedirectToStockReportSizeRangeAspx(string storeNo, string group, string from, string to, string category, string materialType, string material)
        {
            string MaterialName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(!string.IsNullOrEmpty(material)?material.Trim():""));
            string MaterialTypes = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(!string.IsNullOrEmpty(materialType)? materialType:""));
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(!string.IsNullOrEmpty(category)?category:""));
            string StoreNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(storeNo.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(!string.IsNullOrEmpty(group)?group:""));
            string From = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string To = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            var result = new { Group = GroupName, StoreNo = StoreNo, FromDate = From, ToDate = To, Category = CategoryName, MaterialType = MaterialTypes, Material = MaterialName };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SeasonwiseCosting
        public ActionResult SeasonwiseCosting()
        {
            return View("SeasonwiseCosting");
        }
        //public ActionResult RedirectSeasonwiseCostingToAspx(string storeNo, string group, string from, string to, string category, string materialType, string material, string StockNo)
        //{
        //    string MaterialName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(material.Trim()));
        //    string MaterialTypes = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(materialType.Trim()));
        //    string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
        //    string StoreNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(storeNo.Trim()));
        //    string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
        //    string StockAdjustmentNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(StockNo.Trim()));
        //    var result = new { Group = GroupName, StoreNo = StoreNo, Category = CategoryName, MaterialType = MaterialTypes, Material = MaterialName, StockAdjustmentNo = StockAdjustmentNo };
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        #endregion

        public ActionResult GetMaterial(string MaterialId)
        {
             
            if (MaterialId != null)
            {
                MaterialNameManager manager = new MaterialNameManager();
                var  MaterialList = manager.Get().Where(x => x.MaterialGroupMasterId == Convert.ToInt32(MaterialId)).ToList();
                return Json(MaterialList, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMaterialMasterID(string MaterialId)
        {
            if (MaterialId != null)
            {
                BillOfMaterialManager billofMaterialManager = new BillOfMaterialManager();
                List<MMS.Data.StoredProcedureModel.MaterialDropDownmodel> list = new List<Data.StoredProcedureModel.MaterialDropDownmodel>();
                var list_ = billofMaterialManager.GetMaterialList();
                list_ = list_.Where(x => x.GroupID ==Convert.ToInt32(MaterialId)).ToList();
                var distinctList = list_.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
                distinctList = distinctList.ToList();
                List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                      item => new System.Web.Mvc.SelectListItem()
                                                      {
                                                          Text = item.MaterialDescription,
                                                          Value = item.MaterialMasterId.ToString()
                                                      }).ToList();
                var ShotName = new System.Web.Mvc.SelectListItem()
                {
                    Value = "",
                    Text = "Please Select"
                };
                items_.Insert(0, ShotName);
                return Json(items_, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

    }
}
#endregion