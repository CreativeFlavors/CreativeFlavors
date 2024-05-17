using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Repository.Managers;
namespace MMS.Web.Controllers.Report
{
    public class GateEntryOutwardReportController : Controller
    {
        //
        // GET: /GrnRegister/

        public ActionResult Index()
        {
            return View("~/Views/GateEntryReport/GateEntryOutwardReport/GateEntryOutwardReport.cshtml");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string from, string to, string purpose, string returnType, string supplierId, string materialNameId, string storesName)
        {
            string from_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string to_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            string purpose_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(purpose.Trim()));
            string returnType_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(returnType.Trim()));
            string supplierId_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(supplierId.Trim()));
            string materialNameId_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(materialNameId.Trim()));
            string storesName_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(storesName.Trim()));

            var result = new { From = from_, To = to_, Purpose = purpose_, ReturnType = returnType_, SupplierId = supplierId_,
                MaterialNameId = materialNameId_, StoresName = storesName_ };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStore(string StoresName)
        {
            MaterialManager mmanager = new MaterialManager();
            StoreMasterManager manager = new StoreMasterManager();
            int StoresID = Convert.ToInt32(StoresName);
            var StoreList = (from x in mmanager.Get()
                             join y in manager.Get() on x.StoreMasterId equals y.StoreMasterId
                             where x.MaterialMasterId == StoresID
                             select new
                             {
                                 StoreMasterId = y.StoreMasterId,
                                 StoreName = y.StoreName
                             });

            return Json(StoreList, JsonRequestBehavior.AllowGet);
        }


    }
}
