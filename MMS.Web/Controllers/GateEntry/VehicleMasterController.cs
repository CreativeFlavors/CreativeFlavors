using MMS.Core.Entities.GateEntry;
using MMS.Repository.Managers.GateEntryManager;
using MMS.Web.Models.GateEntryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.GateEntry
{
    public class VehicleMasterController : Controller
    {
        [HttpGet]
        public ActionResult VehicleMaster()
        {
            VehicleMaster model = new VehicleMaster();

            return View("~/Views/GateEntry/VehicleMaster/VehicleMaster.cshtml", model);
        }

        public ActionResult VehicleMasterGrid()
        {
            GEVehicleMaster model = new GEVehicleMaster();
            VehicleManager gateEntryDocumentManager = new VehicleManager();
            model.vehicleList = gateEntryDocumentManager.Get();
            return PartialView("~/Views/GateEntry/VehicleMaster/Partial/VehicleMasterGrid.cshtml", model);
        }

        [HttpPost]
        public ActionResult VehicleMasterDetails(int VehicleId)
        {
            GEVehicleMaster viewmodel = new GEVehicleMaster();
            if (VehicleId != 0)
            {
                VehicleMaster model = new VehicleMaster();
                VehicleManager gateEntryDocumentManager = new VehicleManager();
                model = gateEntryDocumentManager.GetVehicleById(VehicleId);
                if (model != null)
                {
                    viewmodel.VehicleId = model.VehicleId;
                    viewmodel.VehicleName = model.VehicleName;
                    viewmodel.VehicleNo = model.VehicleNo;
                }
            }
            else
            {
                return PartialView("~/Views/GateEntry/VehicleMaster/Partial/VehicleMasterDetails.cshtml", viewmodel);
            }
            return PartialView("~/Views/GateEntry/VehicleMaster/Partial/VehicleMasterDetails.cshtml", viewmodel);
        }

        [HttpPost]
        public ActionResult VehicleMaster(VehicleMaster viewmodel)
        {
            VehicleMaster model = new VehicleMaster();
            VehicleManager gateEntryDocumentManager = new VehicleManager();
            if (ModelState.IsValid)
            {
                /// update coding
                if (viewmodel.VehicleId != 0)
                {
                    model = gateEntryDocumentManager.GetVehicleById(viewmodel.VehicleId);
                    model.UpdatedDate = DateTime.Now;
                }
                model.VehicleId = viewmodel.VehicleId;
                model.VehicleName = viewmodel.VehicleName;
                model.VehicleNo = viewmodel.VehicleNo;
                gateEntryDocumentManager.Post(model);
            }
            else
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int VehicleId)
        {
            VehicleMaster model = new VehicleMaster();
            VehicleManager gateEntryDocumentManager = new VehicleManager();
            string status = "";
            model = gateEntryDocumentManager.GetVehicleById(VehicleId);
            if (model != null)
            {
                status = "Success";
                gateEntryDocumentManager.Delete(VehicleId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Search(string filter)
        {
            List<VehicleMaster> model = new List<VehicleMaster>();
            VehicleManager gateEntryDocumentManager = new VehicleManager();
            GEVehicleMaster vm = new GEVehicleMaster();
            model = gateEntryDocumentManager.Get();
            model = model.Where(x => x.VehicleName.Contains(filter.Trim())).ToList();
            vm.vehicleList = model;

            return PartialView("~/Views/GateEntry/VehicleMaster/Partial/VehicleMasterGrid.cshtml", vm);
        }

    }
}
