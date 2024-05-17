using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Report
{
    public class IssueSlipDetailsRegisterController : Controller
    {
         
        public ActionResult Index()
        {
            return View("IssueSlipDetailsRegister");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string storeNo, string group, string from, string to, string category, string materialType, string material, string customer,string conveyor,string issue,string ionumber)
        {
            string IssueFor = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(issue.Trim()));
            string ConveyorName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(conveyor.Trim()));
            string CustomerName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(customer.Trim()));
            string MaterialName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(material.Trim()));
            string MaterialTypes = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(materialType.Trim()));
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string StoreNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(storeNo.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(group.Trim()));
            string From = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string To = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            string IONumber = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(ionumber.Trim()));
            var result = new { Group = GroupName, StoreNo = StoreNo, FromDate = From, ToDate = To, Category = CategoryName, MaterialType = MaterialTypes, Material = MaterialName, Customer = CustomerName, Conveyor = ConveyorName, Issue = IssueFor,IONumber=IONumber};
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult IssueSizewise()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IssueSizewise(string storeNo, string group, string from, string to, string category, string materialType, string material, string customer, string conveyor, string issue, string ionumber,string IssueSlipNumber)
        {
            string IssueFor = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(issue.Trim()));
            string ConveyorName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(conveyor.Trim()));
            string CustomerName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(customer.Trim()));
            string MaterialName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(material.Trim()));
            string MaterialTypes = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(materialType.Trim()));
            string CategoryName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(category.Trim()));
            string StoreNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(storeNo.Trim()));
            string GroupName = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(!string.IsNullOrEmpty(group)?group.Trim():""));
            //string From = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            //string To = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            string IssueSlipNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(IssueSlipNumber.Trim()));
            string IONumber = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(!string.IsNullOrEmpty(ionumber) ? ionumber.Trim() : ""));
            var result = new { Group = GroupName, StoreNo = StoreNo, Category = CategoryName, MaterialType = MaterialTypes, Material = MaterialName, Customer = CustomerName, Conveyor = ConveyorName, Issue = IssueFor, IONumber = IONumber, IssueSlipNo= IssueSlipNo };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
