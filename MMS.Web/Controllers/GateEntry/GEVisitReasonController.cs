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
    public class GEVisitReasonController : Controller
    {
        [HttpGet]
        public ActionResult GEVisitReason()
        {
            GEVisitReason model = new GEVisitReason();
            return View("~/Views/GateEntry/GateEntryVisitReason/GateEntryVisitReason.cshtml", model);
        }

        public ActionResult GEVisitReasonGrid()
        {
            GEVisitReason model = new GEVisitReason();
            GEVisitReasonManager gateEntryDocumentManager = new GEVisitReasonManager();
            model.VisitReasonList = gateEntryDocumentManager.Get();
            return PartialView("~/Views/GateEntry/GateEntryVisitReason/Partial/GateEntryVisitReasonGrid.cshtml", model);
       }

    [HttpPost]
    public ActionResult GEVisitReasonDetails(int GEVisitReasonId)
    {
        GEVisitReason viewmodel = new GEVisitReason();
        if (GEVisitReasonId != 0)
        {
            GateEntryVisitReasonMaster model = new GateEntryVisitReasonMaster();
            GEVisitReasonManager gateEntryDocumentManager = new GEVisitReasonManager();
            model = gateEntryDocumentManager.GetGEVisitReasonById(GEVisitReasonId);
            if (model != null)
            {
                viewmodel.GEVisitReasonId = model.GEVisitReasonId;
                viewmodel.Reason = model.Reason;
            }
        }
        else
        {
            return PartialView("~/Views/GateEntry/GateEntryVisitReason/Partial/GateEntryVisitReasonDetails.cshtml", viewmodel);
        }
        return PartialView("~/Views/GateEntry/GateEntryVisitReason/Partial/GateEntryVisitReasonDetails.cshtml", viewmodel);
    }

    [HttpPost]
    public ActionResult GEVisitReason(GEVisitReason viewmodel)
    {
        GateEntryVisitReasonMaster model = new GateEntryVisitReasonMaster();
        GEVisitReasonManager gateEntryDocumentManager = new GEVisitReasonManager();
        if (ModelState.IsValid)
        {
            /// update coding
            if (viewmodel.GEVisitReasonId != 0)
            {
                model = gateEntryDocumentManager.GetGEVisitReasonById(viewmodel.GEVisitReasonId);
                model.UpdatedDate = DateTime.Now;
            }
            model.GEVisitReasonId = viewmodel.GEVisitReasonId;
            model.Reason = viewmodel.Reason;
            gateEntryDocumentManager.Post(model);
        }
        else
        {
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        return Json(model, JsonRequestBehavior.AllowGet);
    }

    public ActionResult Delete(int GEVisitReasonId)
    {
        GateEntryVisitReasonMaster model = new GateEntryVisitReasonMaster();
        GEVisitReasonManager gateEntryDocumentManager = new GEVisitReasonManager();
        string status = "";
        model = gateEntryDocumentManager.GetGEVisitReasonById(GEVisitReasonId);
        if (model != null)
        {
            status = "Success";
            gateEntryDocumentManager.Delete(GEVisitReasonId);
        }
        return Json(status, JsonRequestBehavior.AllowGet);
    }


    public ActionResult Search(string filter)
    {

        List<GateEntryVisitReasonMaster> model = new List<GateEntryVisitReasonMaster>();
        GEVisitReasonManager gateEntryDocumentManager = new GEVisitReasonManager();
        GEVisitReason vm = new GEVisitReason();
        model = gateEntryDocumentManager.Get();
        model = model.Where(x => x.Reason.Contains(filter.Trim())).ToList();
        vm.VisitReasonList = model;

        return PartialView("~/Views/GateEntry/GateEntryVisitReason/Partial/GateEntryVisitReasonGrid.cshtml", vm);
    }

}
}
