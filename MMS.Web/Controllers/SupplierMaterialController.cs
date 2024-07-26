using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class SupplierMaterialController : Controller
    {
        public ActionResult SupplierMaster()
        {
            return View();
        } 
        public ActionResult SupplierGrid()
        {
            return PartialView("~/Views/SupplierMaterial/Partial/SupplierGrid.cshtml");
        }
        public ActionResult SupplierMaterial_Details()
        {
            return PartialView("~/Views/SupplierMaterial/Partial/SupplierMaterial_Details.cshtml");
        }

    }
}
