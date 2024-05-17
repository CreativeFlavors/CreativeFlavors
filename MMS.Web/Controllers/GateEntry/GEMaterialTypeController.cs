using MMS.Core.Entities.GateEntry;
using MMS.Repository.Managers.GateEntryManager;
using MMS.Web.Models.GateEntryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MMS.Web.Controllers.GateEntry
{
    public class GEMaterialTypeController : Controller
    { 
        [HttpGet]
        public ActionResult GEMaterialType()
        {
            GEMaterialType model = new GEMaterialType();
            return View("~/Views/GateEntry/GateEntryMaterialType/GateEntryMaterialType.cshtml", model);
        }
        public ActionResult GEMaterialTypeGrid()
        {
            GEMaterialType model = new GEMaterialType();
            GEMaterialTypeManger gateEntryDocumentManager = new GEMaterialTypeManger();
            model.GEMaterialTypeList = gateEntryDocumentManager.Get();
            return PartialView("~/Views/GateEntry/GateEntryMaterialType/Partial/GateEntryMaterialTypeGrid.cshtml", model);
        }

        [HttpPost]
        public ActionResult GEMaterialTypeDetails(int GEMaterialTypeId)
        {
            GEMaterialType viewmodel = new GEMaterialType();
            if (GEMaterialTypeId != 0)
            {
                GEMaterialTypeMaster model = new GEMaterialTypeMaster();
                GEMaterialTypeManger materialTypeManager = new GEMaterialTypeManger();
                model = materialTypeManager.GetGEMaterialTypeById(GEMaterialTypeId);
                if (model != null)
                {
                    viewmodel.GEMaterialTypeId = model.GEMaterialTypeId;
                    viewmodel.MaterialType = model.MaterialType;
                }
            }
            else
            {
                return PartialView("~/Views/GateEntry/GateEntryMaterialType/Partial/GateEntryMaterialTypeDetails.cshtml", viewmodel);
            }
            return PartialView("~/Views/GateEntry/GateEntryMaterialType/Partial/GateEntryMaterialTypeDetails.cshtml", viewmodel);
        }

        [HttpPost]
        public ActionResult GEMaterialType(GEMaterialType viewmodel)
        {
            GEMaterialTypeMaster model = new GEMaterialTypeMaster();
            GEMaterialTypeManger materialTypeManager = new GEMaterialTypeManger();
            if (ModelState.IsValid)            {
                /// update coding
                if (viewmodel.GEMaterialTypeId != 0)
                {
                    model = materialTypeManager.GetGEMaterialTypeById(viewmodel.GEMaterialTypeId);
                    model.UpdatedDate = DateTime.Now;
                }
                model.GEMaterialTypeId = viewmodel.GEMaterialTypeId;

                model.MaterialType = viewmodel.MaterialType;
                materialTypeManager.Post(model);
            }
            else
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json(model, JsonRequestBehavior.AllowGet);          
        }

        public ActionResult Delete(int GEMaterialTypeId)
        {
            GEMaterialTypeMaster model = new GEMaterialTypeMaster();
            GEMaterialTypeManger materialTypeManager = new GEMaterialTypeManger();
            string status = "";          
            model = materialTypeManager.GetGEMaterialTypeById(GEMaterialTypeId);
            if (model != null)
            {
                status = "Success";
                materialTypeManager.Delete(GEMaterialTypeId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult Search(string filter)
        {
            List <GEMaterialTypeMaster> model = new List<GEMaterialTypeMaster>();
            GEMaterialTypeManger materialTypeManager = new GEMaterialTypeManger();

            GEMaterialType vm = new GEMaterialType();
            model = materialTypeManager.Get();
            model = model.Where(x => x.MaterialType.Contains(filter.Trim())).ToList();
            vm.GEMaterialTypeList = model;

            return PartialView("~/Views/GateEntry/GateEntryMaterialType/Partial/GateEntryMaterialTypeGrid.cshtml", vm);
        }
    }
}
