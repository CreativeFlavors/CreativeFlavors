using MMS.Core.Entities.GateEntry;
using MMS.Repository.Managers.GateEntryManager;
using MMS.Web.Models.GateEntryModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace MMS.Web.Controllers.GateEntry
{
    public class GateEntryOutwardDocumentController : Controller
    {
        [HttpGet]
        public ActionResult GateEntryOutwardDocument()
        {
            GateEntryOutwardDocument model = new GateEntryOutwardDocument();
            return View("~/Views/GateEntry/GateEntryOutwardDocument/GateEntryOutwardDocument.cshtml", model);
        }

        public ActionResult GateEntryOutwardDocumentGrid()
        {
            GateEntryOutwardDocument model = new GateEntryOutwardDocument();
            GateEntryOutwardDocumentManager gateEntryDocumentManager = new GateEntryOutwardDocumentManager();
            model.GateEntryDocumentlist = gateEntryDocumentManager.Get();
            return PartialView("~/Views/GateEntry/GateEntryOutwardDocument/Partial/GateEntryOutwardDocumentGrid.cshtml", model);
        }


        [HttpPost]
        public ActionResult GateEntryOutwardDocumentDetails(int GateEntryDocumentId)
        {
            GateEntryOutwardDocument viewmodel = new GateEntryOutwardDocument();
            if (GateEntryDocumentId != 0)
            {
                GateEntryOutwardDocumentMaster model = new GateEntryOutwardDocumentMaster();
                GateEntryOutwardDocumentManager gateEntryDocumentManager = new GateEntryOutwardDocumentManager();
                model = gateEntryDocumentManager.GetGateEntryDocumentById(GateEntryDocumentId);
                if (model != null)
                {
                    viewmodel.GateEntryOutwardDocumentId = model.GateEntryOutwardDocumentId;
                    viewmodel.OutwardDocTypeId = model.OutwardDocTypeId;
                    viewmodel.GateEntryNo = model.GateEntryNo;
                    viewmodel.EntryDateTime = model.EntryDateTime;
                    viewmodel.Mode = model.Mode;
                    viewmodel.Company =  model.Company;
                    viewmodel.CourierNo = model.CourierNo;
                    viewmodel.Unit = model.Unit;
                    viewmodel.PersonName = model.PersonName;
                    viewmodel.ModeofTransport = model.ModeofTransport;
                    viewmodel.VehicleNo = model.VehicleNo;
                    viewmodel.AddressToWhom = model.AddressToWhom;
                    viewmodel.HandOverTo = model.HandOverTo;
                    viewmodel.ReceivedBy = model.ReceivedBy;
                    viewmodel.Remarks = model.Remarks;
                }
            }
            else
            {
              string year =   DateTime.Now.Year.ToString();
                viewmodel.GateEntryNo = GetDocumentEntryID();
                viewmodel.GateEntryNo = "GEONDOC"+ viewmodel.GateEntryNo + "/"+ year;
            }
            return PartialView("~/Views/GateEntry/GateEntryOutwardDocument/Partial/GateEntryOutwardDocumentDetails.cshtml", viewmodel);
        }

        [HttpPost]
        public ActionResult GateEntryOutwardDocument(GateEntryOutwardDocument viewmodel)
        {
            GateEntryOutwardDocumentMaster model = new GateEntryOutwardDocumentMaster();
            GateEntryOutwardDocumentManager gateEntryDocumentManager = new GateEntryOutwardDocumentManager();
            string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                         "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss","dd/MM/yyyy HH:mm:ss",
                         "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                         "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                         "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm",
                         "MM/d/yyyy HH:mm:ss.ffffff" };
            DateTime? EntrydateValue = null;
            if (viewmodel.GateEntryOutwardDocumentId != 0)
            {
                string EntryDate = string.IsNullOrEmpty(viewmodel.EntryDateTime.ToString()) ? null : viewmodel.EntryDateTime.ToString();
                DateTime? date = Convert.ToDateTime(Convert.ToDateTime(EntryDate).ToString("dd/MM/yyyy HH:mm:ss"));
                if (!string.IsNullOrEmpty(EntryDate) && viewmodel.GateEntryOutwardDocumentId != 0)
                {
                    model.EntryDateTime = date;
                }
                model.GateEntryOutwardDocumentId = viewmodel.GateEntryOutwardDocumentId;
            }
            else
            {
                if (!string.IsNullOrEmpty(viewmodel.EntryDate))
                {
                    EntrydateValue = DateTime.ParseExact(viewmodel.EntryDate.ToString(), formats,
                                               new CultureInfo("en-US"),
                                               DateTimeStyles.None);
                    model.EntryDateTime = EntrydateValue;
                }
            }
                model.GateEntryNo = viewmodel.GateEntryNo;
                model.OutwardDocTypeId = viewmodel.OutwardDocTypeId;
                model.Mode = viewmodel.Mode;
                model.Company = viewmodel.Company;
                model.CourierNo = viewmodel.CourierNo;
                model.Unit = viewmodel.Unit;
                model.PersonName = viewmodel.PersonName;
                model.ModeofTransport = viewmodel.ModeofTransport;
                model.VehicleNo = viewmodel.VehicleNo;
                model.AddressToWhom = viewmodel.AddressToWhom;
                model.HandOverTo = viewmodel.HandOverTo;
                model.ReceivedBy = viewmodel.ReceivedBy;
                model.Remarks = viewmodel.Remarks;
                gateEntryDocumentManager.Post(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int GateEntryDocumentId)
        {
            GateEntryOutwardDocumentMaster model = new GateEntryOutwardDocumentMaster();
            string status = "";
            GateEntryOutwardDocumentManager gateEntryDocumentManager = new GateEntryOutwardDocumentManager();
            model = gateEntryDocumentManager.GetGateEntryDocumentById(GateEntryDocumentId);
            if (model != null)
            {
                status = "Success";
                gateEntryDocumentManager.Delete(GateEntryDocumentId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public string GetDocumentEntryID()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateGEOutwardDocID();
            ID++;
            return ID.ToString();
        }

        public ActionResult Search(string filter)
        {
            GateEntryOutwardDocumentManager gateEntryManager = new GateEntryOutwardDocumentManager();
            List<GateEntryOutwardDocumentMaster> model = new List<GateEntryOutwardDocumentMaster>();
            GateEntryOutwardDocument vm = new GateEntryOutwardDocument();
            model = gateEntryManager.Get();
            model = model.Where(x => x.GateEntryNo.Contains(filter.Trim()) || x.AddressToWhom.Contains(filter.Trim())).ToList();
            vm.GateEntryDocumentlist = model;
            
            return PartialView("~/Views/GateEntry/GateEntryOutwardDocument/Partial/GateEntryOutwardDocumentGrid.cshtml", vm);
        }


    }
}
