using MMS.Core.Entities.GateEntry;
using MMS.Repository.Managers.GateEntryManager;
using MMS.Web.Models.GateEntryModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class GateEntryVisitorController : Controller
    {
        [HttpGet]
        public ActionResult GateEntryVisitor()
        {
            GateEntryVisitor model = new GateEntryVisitor();
            return View("~/Views/GateEntry/GateEntryVisitor/GateEntryVisitor.cshtml", model);
        }

        public ActionResult GateEntryVisitorGrid()
        {
            GateEntryVisitor model = new GateEntryVisitor();
            GateEntryVisitorManager visitorManager = new GateEntryVisitorManager();
            model.GateEntryVisitorlist = visitorManager.Get();
            return PartialView("~/Views/GateEntry/GateEntryVisitor/Partial/GateEntryVisitorGrid.cshtml", model);
        }

        public ActionResult GateEntryVisitorDetails(int GateEntryVisitorId)
        {
            GateEntryVisitor viewmodel = new GateEntryVisitor();
            if (GateEntryVisitorId != 0)
            {
                GateEntryVisitorMaster model = new GateEntryVisitorMaster();
                GateEntryVisitorManager visitorManager = new GateEntryVisitorManager();
                model = visitorManager.GetGateEntryVisitorById(GateEntryVisitorId);
                if (model != null)
                {
                    viewmodel.GateEntryVisitorId = model.GateEntryVisitorId;
                    viewmodel.GateEntryNo = model.GateEntryNo;
                    viewmodel.EntryDateTime = model.EntryDateTime;
                    viewmodel.EntryType = model.EntryType;
                    viewmodel.Designation = model.Designation;
                    viewmodel.VisitorType = model.VisitorType;
                    viewmodel.Purpose = model.Purpose;
                    viewmodel.VisitorName = model.VisitorName;
                    viewmodel.CompanyName = model.CompanyName;
                    viewmodel.VehicleNo = model.VehicleNo;
                    viewmodel.VisitorIdNo = model.VisitorIdNo;
                    viewmodel.VisitorId = model.VisitorId;
                    viewmodel.ComeBy = model.ComeBy;
                    viewmodel.ToMeet = model.ToMeet;
                    viewmodel.MobileNo = model.MobileNo;
                    viewmodel.ReasonForVisit = model.ReasonForVisit;
                    viewmodel.HandCarried = model.HandCarried;
                    if (model.ReturnDateTime == null)
                    {
                        viewmodel.ReturnDateTime = DateTime.Now;
                    }
                    else
                    {
                        viewmodel.ReturnDateTime = model.ReturnDateTime.Value;
                    }
                    viewmodel.OtherInfo = model.OtherInfo;
                    viewmodel.Remarks = model.Remarks;
                }
            }
            else
            {
                string year = DateTime.Now.Year.ToString();
                viewmodel.GateEntryNo = GetVisitorEntryID();
                viewmodel.GateEntryNo = "GEVisitor" + viewmodel.GateEntryNo + "/" + year;
            }
            return PartialView("~/Views/GateEntry/GateEntryVisitor/Partial/GateEntryVisitorDetails.cshtml", viewmodel);
        }

        [HttpPost]
        public ActionResult GateEntryVisitor(GateEntryVisitor viewmodel, string EntryDateTime, string ReturnDateTime)
        {
            GateEntryVisitorMaster visitormaster = new GateEntryVisitorMaster();
            GateEntryVisitorManager visitorManager = new GateEntryVisitorManager();
            string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                         "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss","dd/MM/yyyy HH:mm:ss",
                         "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                         "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                         "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm",
                         "MM/d/yyyy HH:mm:ss.ffffff" };

            if (viewmodel.GateEntryVisitorId != 0)
            {
                string[] EntrydateStrings = { EntryDateTime };
                string[] ReturnDatetimeStrings = { ReturnDateTime };
                DateTime? EntrydateValue;
                DateTime? ReturnDatetimeValue;
                foreach (string dateString in ReturnDatetimeStrings)
                {
                    EntrydateValue = DateTime.ParseExact(dateString, formats,
                                                    new CultureInfo("en-US"),
                                                    DateTimeStyles.None);
                    visitormaster.EntryDateTime = EntrydateValue;
                }
                foreach (string daString in ReturnDatetimeStrings)
                {
                    ReturnDatetimeValue = DateTime.ParseExact(daString, formats,
                                                    new CultureInfo("en-US"),
                                                    DateTimeStyles.None);
                    visitormaster.ReturnDateTime = ReturnDatetimeValue;
                }               
                visitormaster.GateEntryVisitorId = viewmodel.GateEntryVisitorId;
            }
            else
            {

                string[] dateStrings = { viewmodel.EntryDate };
                string[] ReturnDateStrings = { viewmodel.ReturnDate };
                DateTime? dateValue;
                DateTime? ReturnDateValue;
                if (!string.IsNullOrEmpty(viewmodel.EntryDate))
                {
                    foreach (string dateString in dateStrings)
                    {
                        dateValue = DateTime.ParseExact(dateString, formats,
                                                        new CultureInfo("en-US"),
                                                        DateTimeStyles.None);
                        visitormaster.EntryDateTime = dateValue;
                    }
                }
                if (!string.IsNullOrEmpty(viewmodel.ReturnDate))
                {
                    foreach (string dateString in ReturnDateStrings)
                    {
                        ReturnDateValue = DateTime.ParseExact(dateString, formats,
                                                        new CultureInfo("en-US"),
                                                        DateTimeStyles.None);
                        visitormaster.ReturnDateTime = ReturnDateValue;
                    }
                }
            }

            visitormaster.GateEntryNo = viewmodel.GateEntryNo;
            visitormaster.EntryType = viewmodel.EntryType;
            visitormaster.Designation = viewmodel.Designation;
            visitormaster.VisitorType = viewmodel.VisitorType;
            visitormaster.Purpose = viewmodel.Purpose;
            visitormaster.VisitorName = viewmodel.VisitorName;
            visitormaster.CompanyName = viewmodel.CompanyName;
            visitormaster.VehicleNo = viewmodel.VehicleNo;
            visitormaster.VisitorIdNo = viewmodel.VisitorIdNo;
            visitormaster.VisitorId = viewmodel.VisitorId;
            visitormaster.ComeBy = viewmodel.ComeBy;
            visitormaster.ToMeet = viewmodel.ToMeet;
            visitormaster.MobileNo = viewmodel.MobileNo;
            visitormaster.ReasonForVisit = viewmodel.ReasonForVisit;
            visitormaster.HandCarried = viewmodel.HandCarried;
            visitormaster.OtherInfo = viewmodel.OtherInfo;
            visitormaster.Remarks = viewmodel.Remarks;
            visitorManager.Post(visitormaster);


            return Json(visitormaster, JsonRequestBehavior.AllowGet);
        }
        public string GetVisitorEntryID()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateGEVisitorID();
            ID++;
            return ID.ToString();
        }

        public ActionResult Delete(int GateEntryVisitorId)
        {
            GateEntryVisitorMaster model = new GateEntryVisitorMaster();
            GateEntryVisitorManager visitorManager = new GateEntryVisitorManager();
            string status = "";
            model = visitorManager.GetGateEntryVisitorById(GateEntryVisitorId);
            if (model != null)
            {
                status = "Success";
                visitorManager.Delete(GateEntryVisitorId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string filter)
        {

            GateEntryVisitorManager visitorManager = new GateEntryVisitorManager();
            GEVisitReasonManager reasonManager = new GEVisitReasonManager();
            List<GateEntryVisitorMaster> model = new List<GateEntryVisitorMaster>();
            GateEntryVisitor vm = new GateEntryVisitor();
            model = visitorManager.Get();
            model = model.Where(x => x.GateEntryNo.Contains(filter.Trim()) || x.CompanyName.Contains(filter.Trim()) || x.VisitorName.Contains(filter.Trim())).ToList();
            vm.GateEntryVisitorlist = model;

            return PartialView("~/Views/GateEntry/GateEntryVisitor/Partial/GateEntryVisitorGrid.cshtml", vm);
        }
    }
}
