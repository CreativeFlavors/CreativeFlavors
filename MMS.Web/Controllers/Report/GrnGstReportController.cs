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
using MMS.Core.Entities.JobWork;
using MMS.Repository.Managers.JobWork;

namespace MMS.Web.Controllers.Report
{
    public class GrnGstReportController : Controller
    {
        //
        public ActionResult Index()
        {
            return View("GrnGstReport");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string grnNO ,string group, string category,string store,string supplier)
        {
            string SupplierName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(supplier.Trim()));
            string StoreName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(store.Trim()));
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            string GrnNO = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(grnNO.Trim()));             
            var Result = new { Grn = GrnNO, Category = CategoryName, Group = GroupName, Store = StoreName,Supplier = SupplierName };
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public class SupplierList
        {
            public string Text { get; set; }
            public int Value { get; set; }
        }
        public ActionResult GetData(int grnNO)
        {
            GrnManagerNew mangager = new GrnManagerNew();
            StoreMasterManager storeManager = new StoreMasterManager();
            MaterialCategoryManager MaterialCategoryManager = new MaterialCategoryManager();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            SupplierMasterManager supplierManager = new SupplierMasterManager();
            List<MaterialCategoryMaster> listCategoryMaster = new List<MaterialCategoryMaster>();      
            var Result = mangager.Get().Where(x => x.GrnNo == grnNO).ToList();
            var ResultDataum = mangager.Get().Where(x => x.GrnNo == grnNO).FirstOrDefault();
            GrnTypeMaster GrnTypeMaster = new GrnTypeMaster();
            GRNTypeManager GRNTypeManager = new GRNTypeManager();
            JobworkManager JobworkManager = new JobworkManager();
            JobworkMaster JobworkMaster = new JobworkMaster();

            GrnTypeMaster = GRNTypeManager.Get().Where(x=>x.GrnTypeMasterId== Result[0].GrnType).FirstOrDefault();
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
           List<SupplierList> SupplierList = new List<SupplierList>();
            if (GrnTypeMaster.IsJobWork == true)
            {
                SupplierList = (from X in Result
                                    join Y in JobworkManager.Get()
                                    on X.SupplierNameId equals Y.JW_Id
                                    orderby Y.JW_Id
                                    select new SupplierList
                                    {
                                        Text = Y.JW_Name,
                                        Value = Y.JW_Id
                                    }).ToList();
            }
            else
            {
                SupplierList = (from X in Result
                                    join Y in supplierManager.Get()
                                    on X.SupplierNameId equals Y.SupplierMasterId
                                    orderby Y.SupplierName
                                    select new SupplierList
                                    {
                                        Text = Y.SupplierName,
                                        Value = Y.SupplierMasterId
                                    }).ToList();
            }
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
            SupplierList = SupplierList.GroupBy(x =>x.Text).Select(x => x.First()).ToList();

            var ResultData = new { Store = storeList, Category = categoryList, Group = groupList,Supplier = SupplierList };      
            return Json(ResultData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSupplier(int grnNO)
        {
            GrnManagerNew mangager = new GrnManagerNew();
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
            GrnManagerNew mangager = new GrnManagerNew();
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
            GrnManagerNew mangager = new GrnManagerNew();
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

    }
}
