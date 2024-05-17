using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Repository.Managers;
namespace MMS.Web.Controllers.Report
{
    public class GrnRegisterReportController : Controller
    {
        //
        // GET: /GrnRegister/

        public ActionResult Index()
        {
            return View("GrnRegisterReport");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string storeNo, string group, string from, string to, string category, string materialType, string material,string supplier,string GRNOption,string ReportOption)
        {
            string SupplierName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(supplier.Trim()));
            string MaterialName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(material.Trim()));
            string MaterialTypes = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(materialType.Trim()));
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string StoreNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(storeNo.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            string From = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string To = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            string GRNoption_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(GRNOption.Trim()));
            string ReportOption_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(ReportOption.Trim()));
            var result = new { Group = GroupName, StoreNo = StoreNo, FromDate = From, ToDate = To, Category = CategoryName, MaterialType = MaterialTypes, Material = MaterialName, Supplier = SupplierName, GRNoption= GRNoption_, ReportOption= ReportOption_ };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetMaterial(string MaterialId)
        {
            MaterialNameManager manager = new MaterialNameManager();
            var MaterialList = manager.Get().Where(x => x.MaterialGroupMasterId == Convert.ToInt32(MaterialId)).ToList();
            return Json(MaterialList, JsonRequestBehavior.AllowGet);
        }

    }
}
