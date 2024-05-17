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

namespace MMS.Web.Controllers.ReportController
{
    public class POReportController : Controller
    {
        //
        // GET: /POReport/

        public ActionResult Index()
        {
            return View("PurchaseOrder");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string poNo,string store ,string group, string category)
        {

            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string StoreName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(store.Trim()));          
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            string Po = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(poNo.Trim()));
            var result = new { Group = GroupName, Store = StoreName,  Category = CategoryName ,PoNo = Po};           
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetData(string poNo)
        {
            PurchaseOrderManager mangager = new PurchaseOrderManager();
            StoreMasterManager storeManager = new StoreMasterManager();
            MaterialManager materialmanager = new MaterialManager();
            MaterialCategoryManager MaterialCategoryManager = new MaterialCategoryManager();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            List<MaterialCategoryMaster> listCategoryMaster = new List<MaterialCategoryMaster>();
            var Result = mangager.Get().Where(x => x.PoNo == poNo.ToString()).ToList();
            var ResultDataum = mangager.Get().Where(x => x.PoNo == poNo.ToString()).FirstOrDefault();

            var store = materialmanager.Get().Where(x => x.MaterialMasterId == ResultDataum.Material).FirstOrDefault();  
            var groupList = (from X in Result
                             join Y in materialGroupManager.Get()
                             on X.Groupname equals Y.MaterialGroupMasterId
                             where (X.Category == ResultDataum.Category)
                             orderby Y.GroupName
                             select new
                             {
                                 Text = Y.GroupName,
                                 Value = Y.MaterialGroupMasterId
                             }).ToList();
            groupList = groupList.GroupBy(x => x.Text).Select(x => x.First()).ToList();

            var categoryList = (from X in Result
                                join Y in MaterialCategoryManager.Get()
                                on X.Category equals Y.MaterialCategoryMasterId
                                orderby Y.CategoryName
                                select new
                                {
                                    Text = Y.CategoryName,
                                    Value = Y.MaterialCategoryMasterId
                                }).ToList();
            categoryList = categoryList.GroupBy(x => x.Text).Select(x => x.First()).ToList();

            var storeList = (from X in Result
                             join Y in materialGroupManager.Get()
                             on X.Groupname equals Y.MaterialGroupMasterId
                             join Z in storeManager.Get()
                             on Y.StoreName equals Z.StoreMasterId
                             orderby Y.StoreName
                             select new
                             {
                                 Text = Z.StoreName,
                                 Value = Z.StoreMasterId
                             }).ToList();
            storeList = storeList.GroupBy(x => x.Text).Select(x => x.First()).ToList(); 

            groupList = groupList.GroupBy(x => x.Text).Select(x => x.First()).ToList(); 
            var ResultData = new { Store = storeList, Category = categoryList, Group = groupList };
            return Json(ResultData, JsonRequestBehavior.AllowGet);
        }

    }
}
