using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Core.Entities.Stock;
using MMS.Web.Models;
using MMS.Core.Entities;
namespace MMS.Web.Controllers.Report
{
    public class GRNReportController : Controller
    {
        #region GRNReport
        public ActionResult Index()
        {
            return View("GRNReport");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string grnNO, string group, string category, string store, string supplier)
        {
            string SupplierName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(supplier.Trim()));
            string StoreName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(store.Trim()));
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            string GrnNO = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(grnNO.Trim()));
            var Result = new { Grn = GrnNO, Category = CategoryName, Group = GroupName, Store = StoreName, Supplier = SupplierName };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetData(int grnNO)
        {
            GrnManager mangager = new GrnManager();
            StoreMasterManager storeManager = new StoreMasterManager();
            MaterialCategoryManager MaterialCategoryManager = new MaterialCategoryManager();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            SupplierMasterManager supplierManager = new SupplierMasterManager();
            List<MaterialCategoryMaster> listCategoryMaster = new List<MaterialCategoryMaster>();
            var Result = mangager.Get().Where(x => x.GrnNo == grnNO).ToList();
            var ResultDataum = mangager.Get().Where(x => x.GrnNo == grnNO).FirstOrDefault();


            var categoryList = (from X in Result
                                join Y in MaterialCategoryManager.Get()
                                on X.CategoryId equals Y.MaterialCategoryMasterId
                                orderby Y.CategoryName
                                select new
                                {
                                    Text = Y.CategoryName,
                                    Value = Y.MaterialCategoryMasterId
                                }).ToList();
            categoryList = categoryList.GroupBy(x => x.Text).Select(x => x.First()).ToList();

            var StoreData = storeManager.Get();

            var storeList = (from X in Result
                             join Y in StoreData
                             on X.Stores equals Y.StoreMasterId
                             orderby Y.StoreName
                             select new
                             {
                                 Text = Y.StoreName,
                                 Value = Y.StoreMasterId
                             }).ToList();
            storeList = storeList.GroupBy(x => x.Text).Select(x => x.First()).ToList();



            var SupplierList = (from X in Result
                                join Y in supplierManager.Get()
                                on X.SupplierNameId equals Y.SupplierMasterId
                                orderby Y.SupplierName
                                select new
                                {
                                    Text = Y.SupplierName,
                                    Value = Y.SupplierMasterId
                                }).ToList();

            var groupList = (from X in Result
                             join Y in materialGroupManager.Get()
                             on X.GroupID equals Y.MaterialGroupMasterId
                             orderby Y.GroupName
                             select new
                             {
                                 Text = Y.GroupName,
                                 Value = Y.MaterialGroupMasterId
                             }).ToList();
            groupList = groupList.GroupBy(x => x.Text).Select(x => x.First()).ToList();
            SupplierList = SupplierList.GroupBy(x => x.Text).Select(x => x.First()).ToList();

            var ResultData = new { Store = storeList, Category = categoryList, Group = groupList, Supplier = SupplierList };
            return Json(ResultData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSupplier(int grnNO)
        {
            GrnManager mangager = new GrnManager();
            StoreMasterManager storeManager = new StoreMasterManager();
            MaterialCategoryManager MaterialCategoryManager = new MaterialCategoryManager();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            List<MaterialCategoryMaster> listCategoryMaster = new List<MaterialCategoryMaster>();
            var Result = mangager.Get().Where(x => x.GrnNo == grnNO).ToList();
            var ResultDataum = mangager.Get().Where(x => x.GrnNo == grnNO).FirstOrDefault();

            var groupList = (from X in Result
                             join Y in materialGroupManager.Get()
                             on X.GroupID equals Y.MaterialGroupMasterId
                             orderby Y.GroupName
                             select new
                             {
                                 Text = Y.GroupName,
                                 Value = Y.MaterialGroupMasterId
                             }).ToList();
            groupList = groupList.GroupBy(x => x.Text).Select(x => x.First()).ToList();

            var ResultData = new { Store = ResultDataum.Stores, Category = ResultDataum.CategoryId, Group = groupList };
            return Json(ResultData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCategory(int stores)
        {
            GrnManager mangager = new GrnManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            var Result = mangager.Get().Where(x => x.Stores == stores).ToList();
            var categoryList = (from X in Result
                                join Y in materialCategoryManager.Get()
                                on X.CategoryId equals Y.MaterialCategoryMasterId
                                orderby Y.CategoryName
                                select new
                                {
                                    Text = Y.CategoryName,
                                    Value = Y.MaterialCategoryMasterId
                                }).ToList();
            categoryList = categoryList.GroupBy(x => x.Text).Select(x => x.First()).ToList();

            return Json(categoryList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGroup(int category)
        {
            GrnManager mangager = new GrnManager();
            MaterialGroupManager groupManager = new MaterialGroupManager();
            var Result = mangager.Get().Where(x => x.CategoryId == category).ToList();
            var groupList = (from X in Result
                             join Y in groupManager.Get()
                             on X.GroupID equals Y.MaterialGroupMasterId
                             orderby Y.GroupName
                             select new
                             {
                                 Text = Y.GroupName,
                                 Value = Y.MaterialGroupMasterId
                             }).ToList();
            groupList = groupList.GroupBy(x => x.Text).Select(x => x.First()).ToList();
            return Json(groupList, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region GST Report      
        public ActionResult GSTReport()
        {
            return View("GSTReport");
        }

        [HttpPost]
        public ActionResult GSTRedirectToAspx(string storeNo, string group, string from, string to, string category, string materialType, string material,string GRNOption)
        {
            string MaterialName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(material.Trim()));
            string MaterialTypes = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(materialType.Trim()));
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string StoreNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(storeNo.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            string From = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string To = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            string grnOption = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(GRNOption.Trim()));
            var result = new { Group = GroupName, StoreNo = StoreNo, FromDate = From, ToDate = To, Category = CategoryName, MaterialType = MaterialTypes, Material = MaterialName, GrnOption= grnOption };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        #endregion

    }
}
