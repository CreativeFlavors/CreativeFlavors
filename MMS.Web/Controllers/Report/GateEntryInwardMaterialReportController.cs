using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Repository.Managers;
namespace MMS.Web.Controllers.Report
{
    public class GateEntryInwardMaterialReportController : Controller
    {
        //
        // GET: /GrnRegister/

        public ActionResult Index()
        {
            return View("~/Views/GateEntryReport/GateEntryInwardMaterialReport/GateEntryInwardMaterialReport.cshtml");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string from, string to, string returnType, string supplierId, string poNoId, string materialNameId)
        {
            string from_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string to_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            string returnType_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(returnType.Trim()));
            string supplierId_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(supplierId.Trim()));
            string poNoId_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(poNoId.Trim()));
            string materialNameId_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(materialNameId.Trim()));


            var result = new { From = from_, To = to_, ReturnType = returnType_, SupplierId = supplierId_, PoNoId = poNoId_, MaterialNameId = materialNameId_ };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}
