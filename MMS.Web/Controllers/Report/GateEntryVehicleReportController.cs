using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Repository.Managers;
namespace MMS.Web.Controllers.Report
{
    public class GateEntryVehicleReportController : Controller
    {
        //
        // GET: /GrnRegister/

        public ActionResult Index()
        {
            return View("~/Views/GateEntryReport/GateEntryVehicleReport/GateEntryVehicleReport.cshtml");
        }
        [HttpPost]
        public ActionResult RedirectToAspx(string from, string to, string gateEntryNo, string vehicleId, string purpose, string driverName)
        {
            string from_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(from.Trim()));
            string to_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(to.Trim()));
            string gateEntryNo_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(gateEntryNo.Trim()));
            string vehicleId_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(vehicleId.Trim()));
            string purpose_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(purpose.Trim()));
            string driverName_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(driverName.Trim()));


            var result = new { From = from_, To = to_, GateEntryNo = gateEntryNo_, VehicleId = vehicleId_, Purpose = purpose_, DriverName = driverName_ };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}
