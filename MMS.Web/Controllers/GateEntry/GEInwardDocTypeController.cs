using MMS.Core.Entities.GateEntry;
using MMS.Repository.Managers.GateEntryManager;
using MMS.Web.Models.GateEntryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MMS.Web.Controllers.GateEntry
{
    public class GEInwardDocTypeController : Controller
    {
        [HttpGet]
        public ActionResult GEInwardDocType()
        {
            GEInwardDocType model = new GEInwardDocType();
            return View("~/Views/GateEntry/GateEntryDocType/GateEntryDocType.cshtml", model);
        }

        public ActionResult GEInwardDocTypeGrid()
        {
            GEInwardDocType model = new GEInwardDocType();
            GEInwardDocTypeManger gateEntryDocumentManager = new GEInwardDocTypeManger();
            model.GEDocTypeList = gateEntryDocumentManager.Get();
            return PartialView("~/Views/GateEntry/GateEntryDocType/Partial/GateEntryDocTypeGrid.cshtml", model);
        }

        [HttpPost]
        public ActionResult GEInwardDocTypeDetails(int InwardDocumentTypeId)
        {
            GEInwardDocType viewmodel = new GEInwardDocType();
            if (InwardDocumentTypeId != 0)
            {
                GateEntryDocTypeMaster model = new GateEntryDocTypeMaster();
                GEInwardDocTypeManger gateEntryDocumentManager = new GEInwardDocTypeManger();
                model = gateEntryDocumentManager.GetGEDocTypeById(InwardDocumentTypeId);
                if (model != null)
                {
                    viewmodel.InwardDocumentTypeId = model.InwardDocumentTypeId;
                    viewmodel.DocumentType = model.DocumentType;
                }
            }
            else
            {
                return PartialView("~/Views/GateEntry/GateEntryDocType/Partial/GateEntryDocTypeDetails.cshtml", viewmodel);
            }
            return PartialView("~/Views/GateEntry/GateEntryDocType/Partial/GateEntryDocTypeDetails.cshtml", viewmodel);
        }

        [HttpPost]
        public ActionResult GEInwardDocType(GEInwardDocType viewmodel)
        {
            GateEntryDocTypeMaster model = new GateEntryDocTypeMaster();
            GEInwardDocTypeManger gateEntryDocumentManager = new GEInwardDocTypeManger();
            if (ModelState.IsValid)            {
                /// update coding
                if (viewmodel.InwardDocumentTypeId != 0)
                {
                    model = gateEntryDocumentManager.GetGEDocTypeById(viewmodel.InwardDocumentTypeId);
                    model.UpdatedDate = DateTime.Now;
                }
                model.InwardDocumentTypeId = viewmodel.InwardDocumentTypeId;

                model.DocumentType = viewmodel.DocumentType;
                gateEntryDocumentManager.Post(model);
            }
            else
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json(model, JsonRequestBehavior.AllowGet);          
        }

        public ActionResult Delete(int InwardDocumentTypeId)
        {
            GateEntryDocTypeMaster model = new GateEntryDocTypeMaster();
            GEInwardDocTypeManger gateEntryDocumentManager = new GEInwardDocTypeManger();
            string status = "";          
            model = gateEntryDocumentManager.GetGEDocTypeById(InwardDocumentTypeId);
            if (model != null)
            {
                status = "Success";
                gateEntryDocumentManager.Delete(InwardDocumentTypeId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult Search(string filter)
        {
            List <GateEntryDocTypeMaster> model = new List<GateEntryDocTypeMaster>();
            GEInwardDocTypeManger gateEntryDocumentManager = new GEInwardDocTypeManger();
            GEInwardDocType vm = new GEInwardDocType();
            model = gateEntryDocumentManager.Get();
            model = model.Where(x => x.DocumentType.Contains(filter.Trim())).ToList();
            vm.GEDocTypeList = model;

            return PartialView("~/Views/GateEntry/GateEntryDocType/Partial/GateEntryDocTypeGrid.cshtml", vm);
        }
    }
}
