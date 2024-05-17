using MMS.Core.Entities.GateEntry;
using MMS.Repository.Managers;
using MMS.Repository.Managers.GateEntryManager;
using MMS.Web.Models.GateEntryModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class GateEntryVehicleController : Controller
    {
        public ActionResult GateEntryVehicleGrid()
        {
            GateEntryVehicle model = new GateEntryVehicle();
            GateEntryVehicleManager vehicleManager = new GateEntryVehicleManager();
            model.GateEntryVehiclelist = vehicleManager.Get();
            return View(model);
        }
        public ActionResult GateEntryVehicleEdit(int VehicleEntryId)
        {
            GateEntryVehicle viewmodel = new GateEntryVehicle();

            if (VehicleEntryId != 0)
            {
                GateEntryVehicleMaster model = new GateEntryVehicleMaster();
                GateEntryVehicleManager vehicleManager = new GateEntryVehicleManager();
                model = vehicleManager.GetGateEntryVehicleById(VehicleEntryId);
                if (model != null)
                {
                    viewmodel.VehicleEntryId = model.VehicleEntryId;
                    viewmodel.GateEntryNo = model.GateEntryNo;
                    viewmodel.EntryDateTime = model.EntryDateTime;
                    viewmodel.VehicleId = model.VehicleId;
                    viewmodel.Destination = model.Destination;
                    viewmodel.Purpose = model.Purpose;
                    viewmodel.DriverName = model.DriverName;
                    viewmodel.Passengers = model.Passengers;
                    viewmodel.selectedempid = StringList(viewmodel.Passengers);
                    viewmodel.MaterialTaken = model.MaterialTaken;
                    viewmodel.DCNo = model.DCNo;
                    viewmodel.selectedid = StringList(viewmodel.DCNo);
                    viewmodel.InwardDcNo = model.InwardDcNo;
                    viewmodel.selectedInwardDcid = StringList(viewmodel.InwardDcNo);
                    viewmodel.InwardDcDate = model.InwardDcDate;
                    viewmodel.InwardMaterial = model.InwardMaterial;
                    viewmodel.StartingKm = model.StartingKm;
                    viewmodel.ClosingKm = model.ClosingKm;
                    viewmodel.KmUsed = model.KmUsed;
                    viewmodel.PurposeofTravel = model.PurposeofTravel;
                    viewmodel.ReturnDateTime = model.ReturnDateTime;
                    viewmodel.Remarks = model.Remarks;
                }
            }
            else
            {
                string year = DateTime.Now.Year.ToString();
                viewmodel.GateEntryNo = GetVehicleEntryID();
                viewmodel.GateEntryNo = "GEVehicle" + viewmodel.GateEntryNo + "/" + year;
            }
            return View(viewmodel);
        }

        public static IEnumerable<int> StringToIntList(string str)
        {
            if (String.IsNullOrEmpty(str))
                yield break;

            foreach (var s in str.Split(','))
            {
                int num;
                if (int.TryParse(s, out num))
                    yield return num;
            }
        }
        public static IEnumerable<string> StringList(string str)
        {
            if (String.IsNullOrEmpty(str))
                yield break;

            foreach (var s in str.Split(','))
            {
                yield return s;
            }
        }

        public ActionResult GateEntryVehicleDetails()
        {
            GateEntryVehicle viewmodel = new GateEntryVehicle();

            string year = DateTime.Now.Year.ToString();
            viewmodel.GateEntryNo = GetVehicleEntryID();
            viewmodel.GateEntryNo = "GEVehicle" + viewmodel.GateEntryNo + "/" + year;
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult GateEntryVehicle(GateEntryVehicle viewmodel, string EntryDateTime, string ReturnDateTime)
        {
            GateEntryVehicleMaster model = new GateEntryVehicleMaster();
            GateEntryVehicleManager vehicleManager = new GateEntryVehicleManager();
            string[] dateFormats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                         "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss","dd/MM/yyyy HH:mm:ss","dd/MM/yyyy hh:mm:ss",
                         "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                         "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                         "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm",
                         "MM/d/yyyy HH:mm:ss.ffffff" };
            if (viewmodel.VehicleEntryId != 0)
            {
                model = vehicleManager.GetGateEntryVehicleById(viewmodel.VehicleEntryId);
                model.UpdatedDate = DateTime.Now;
            }
            if (viewmodel.EntryDate == null)
            {
                DateTime? returnDateValue;
                string EntryDate = string.IsNullOrEmpty(EntryDateTime.ToString()) ? null : EntryDateTime;
                DateTime? date = Convert.ToDateTime(Convert.ToDateTime(EntryDate).ToString("dd/MM/yyyy HH:mm:ss"));
                if (!string.IsNullOrEmpty(EntryDate) && viewmodel.VehicleEntryId != 0)
                {
                    model.EntryDateTime = date;
                }
                string returnDate = !string.IsNullOrEmpty(ReturnDateTime) ? ReturnDateTime : viewmodel.ReturnDate;
                returnDateValue = Convert.ToDateTime(Convert.ToDateTime(returnDate).ToString("dd/MM/yyyy HH:mm:ss"));
                //returnDateValue = DateTime.ParseExact(returnDate, dateFormats,
                //                                 new CultureInfo("en-US"),
                //                                 DateTimeStyles.None);

                model.ReturnDateTime = returnDateValue;
            }
            else if (viewmodel.ReturnDate == null && viewmodel.EntryDate != null)
            {
                //CultureInfo culture = new CultureInfo("en-US");
                //string[] ETDString = viewmodel.EntryDate.ToString().Split('-');
                //DateTime? ETDString_date = Convert.ToDateTime(ETDString[1] + "/" + ETDString[0] + "/" + ETDString[2], culture);


                string[] formats = { "dd/MM/yyyy HH:mm:ss" };
                //var dateTime = DateTime.ParseExact(viewmodel.EntryDate, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                var dateTime = Convert.ToDateTime(Convert.ToDateTime(viewmodel.EntryDate).ToString("dd/MM/yyyy HH:mm:ss"));
                model.EntryDateTime = dateTime;
                model.ReturnDateTime = null;
            }
            if (viewmodel.ReturnDate != null)
            {
                string[] formats = { "dd/MM/yyyy HH:mm:ss" };
                // var dateTime2 = DateTime.ParseExact(viewmodel.ReturnDate, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                var dateTime2 = Convert.ToDateTime(Convert.ToDateTime(viewmodel.ReturnDate).ToString("dd/MM/yyyy HH:mm:ss"));
                model.ReturnDateTime = dateTime2;
            }

            model.VehicleEntryId = viewmodel.VehicleEntryId;
            model.GateEntryNo = viewmodel.GateEntryNo;
            model.VehicleId = viewmodel.VehicleId;
            model.Destination = viewmodel.Destination;
            model.Purpose = viewmodel.Purpose;
            model.DriverName = viewmodel.DriverName;
            model.Passengers = viewmodel.Passengers;
            model.MaterialTaken = viewmodel.MaterialTaken;
            model.DCNo = viewmodel.DCNo;
            model.StartingKm = viewmodel.StartingKm;
            model.ClosingKm = viewmodel.ClosingKm;
            model.KmUsed = viewmodel.KmUsed;
            model.PurposeofTravel = viewmodel.PurposeofTravel;
            model.Remarks = viewmodel.Remarks;
            model.InwardDcNo = viewmodel.InwardDcNo;
            model.InwardDcDate = viewmodel.InwardDcDate;
            model.InwardMaterial = viewmodel.InwardMaterial;
            vehicleManager.Post(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public string GetVehicleEntryID()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateGEVehicleID();
            ID++;
            return ID.ToString();
        }

        public ActionResult Delete(int VehicleEntryId)
        {
            GateEntryVehicleMaster model = new GateEntryVehicleMaster();
            GateEntryVehicleManager vehicleManager = new GateEntryVehicleManager();
            string status = "";
            model = vehicleManager.GetGateEntryVehicleById(VehicleEntryId);
            if (model != null)
            {
                status = "Success";
                vehicleManager.Delete(VehicleEntryId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string filter)
        {
            GateEntryVehicleManager vehicleManager = new GateEntryVehicleManager();
            List<GateEntryVehicleMaster> model = new List<GateEntryVehicleMaster>();
            GateEntryVehicle vm = new GateEntryVehicle();
            model = vehicleManager.Get();
            model = model.Where(x => x.GateEntryNo.Contains(filter.Trim())).ToList();
            vm.GateEntryVehiclelist = model;
            return View("GateEntryVehicleGrid", vm);
        }

        public ActionResult GetVehicleNo(string VehicleName)
        {
            if (VehicleName != null)
            {
                VehicleManager manager = new VehicleManager();
                var MaterialList = (from x in manager.Get()

                                    where x.VehicleName == VehicleName
                                    select new
                                    {
                                        VehicleNo = x.VehicleNo,
                                        VehicleId = x.VehicleId
                                    });
                return Json(MaterialList, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public static SelectList GEMaterialName()
        {
            MaterialNameManager Manager = new MaterialNameManager();
            MaterialManager mmananger = new MaterialManager();
            var result = (from x in Manager.Get()
                          join y in mmananger.Get() on x.MaterialMasterID equals y.MaterialName
                          select new
                          {
                              x.MaterialDescription,
                              y.MaterialMasterId
                          });
            List<System.Web.Mvc.SelectListItem> items = result.Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.MaterialDescription,
                                                     Value = item.MaterialMasterId.ToString()
                                                 }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.RemoveAll(c => c.Text == "" || c.Text == null);
            items.Insert(0, ShotName);
            return new SelectList(items, "Value", "Text");
        }
        public ActionResult GetGateEntryInwardDetails(string InwardDcNo)
        {
            if (InwardDcNo != null)
            {
                string[] DCNoAray = InwardDcNo.Split(',');
                GateEntryInwardMaterialsManager Inwardmanager = new GateEntryInwardMaterialsManager();
                MaterialManager mmananger = new MaterialManager();
                MaterialNameManager Manager = new MaterialNameManager();
                var InwardListDetails = new List<GateEntryInwardList>();
                foreach (var s in DCNoAray)
                {
                    var InwardList = (from x in Inwardmanager.Get()
                                      join y in mmananger.Get() on x.MaterialNameId equals y.MaterialMasterId
                                      join z in Manager.Get() on y.MaterialName equals z.MaterialMasterID
                                      where x.DcNo == s.ToString()
                                      select new GateEntryInwardList
                                      {
                                          GateEntryInwardId = x.GateEntryInwardId,
                                          MaterialName = y.MaterialName,
                                          DcDate = x.DcDate,
                                          MaterialDescription = z.MaterialDescription
                                      }).ToList();
                    InwardListDetails.AddRange(InwardList);
                }
                return Json(InwardListDetails, JsonRequestBehavior.AllowGet);
            }

            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGateEntryOutwardwardDetails(string DCNo)
        {
            if (DCNo != null)
            {
                string[] DCNoAray = DCNo.Split(',');
                GateEntryOutwardManager Inwardmanager = new GateEntryOutwardManager();
                MaterialManager mmananger = new MaterialManager();
                MaterialNameManager Manager = new MaterialNameManager();
                var OutwardListDetails = new List<GateEntryOutwardList>();
                foreach (var s in DCNoAray)
                {
                    var OutwardList = (from x in Inwardmanager.Get()
                                       join y in mmananger.Get() on x.MaterialNameId equals y.MaterialMasterId
                                       join z in Manager.Get() on y.MaterialName equals z.MaterialMasterID
                                       where x.DcNo == s.ToString()
                                       select new GateEntryOutwardList
                                       {
                                           GateEntryOutwardId = x.GateEntryOutwardId,
                                           MaterialName = y.MaterialName,
                                           MaterialDescription = z.MaterialDescription
                                       }).ToList();
                    OutwardListDetails.AddRange(OutwardList);
                }
                return Json(OutwardListDetails, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
}
