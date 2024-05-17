using MMS.Core.Entities.GateEntry;
using MMS.Repository.Managers.GateEntryManager;
using MMS.Web.Models.GateEntryModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.GateEntry
{
    public class GateEntryInwardDocumentController : Controller
    {
        [HttpGet]
        public ActionResult GateEntryInwardDocument()
        {
            GateEntryInwardDocument model = new GateEntryInwardDocument();
            return View("~/Views/GateEntry/GateEntryInwardDocument/GateEntryInwardDocument.cshtml", model);
        }

        public ActionResult GateEntryInwardDocumentGrid()
        {
            GateEntryInwardDocument model = new GateEntryInwardDocument();
            GateEntryInwardDocumentManager gateEntryDocumentManager = new GateEntryInwardDocumentManager();
            model.GateEntryDocumentlist = gateEntryDocumentManager.Get();
            return PartialView("~/Views/GateEntry/GateEntryInwardDocument/Partial/GateEntryInwardDocumentGrid.cshtml", model);
        }


        [HttpPost]
        public ActionResult GateEntryInwardDocumentDetails(int GateEntryDocumentId)
        {
            GateEntryInwardDocument viewmodel = new GateEntryInwardDocument();
            if (GateEntryDocumentId != 0)
            {
                GateEntryInwardDocumentMaster model = new GateEntryInwardDocumentMaster();
                GateEntryInwardDocumentManager gateEntryDocumentManager = new GateEntryInwardDocumentManager();
                model = gateEntryDocumentManager.GetGateEntryDocumentById(GateEntryDocumentId);

                if (model != null)
                {
                    viewmodel.GateEntryDocumentId = model.GateEntryDocumentId;
                    viewmodel.InwardDocTypeId = model.InwardDocTypeId;
                    viewmodel.GateEntryNo = model.GateEntryNo;
                    viewmodel.EntryDateTime = model.EntryDateTime;
                    viewmodel.Mode = model.Mode;
                    viewmodel.Company =  model.Company;
                    viewmodel.FromWhere = model.FromWhere;
                    viewmodel.Unit = model.Unit;
                    viewmodel.PersonName = model.PersonName;
                    viewmodel.ModeofTransport = model.ModeofTransport;
                    viewmodel.VehicleNo = model.VehicleNo;
                    viewmodel.AddressToWhom = model.AddressToWhom;
                    viewmodel.HandOverTo = model.HandOverTo;
                    viewmodel.ReceivedBy = model.ReceivedBy;
                    viewmodel.Remarks = model.Remarks;
                    viewmodel.TagName = model.TagName;
                    viewmodel.GateEntryInwardDocumentUploadlist = gateEntryDocumentManager.GetGateEntryDocumentuploadById(model.GateEntryDocumentId);
                }
            }
            else
            {
              string year =   DateTime.Now.Year.ToString();
                viewmodel.GateEntryNo = GetDocumentEntryID();
                viewmodel.GateEntryNo = "GEINDOC"+ viewmodel.GateEntryNo + "/"+ year;
            }
            return PartialView("~/Views/GateEntry/GateEntryInwardDocument/Partial/GateEntryInwardDocumentDetails.cshtml", viewmodel);
        }

        [HttpPost]
        public JsonResult GateEntryInwardDocumenttest()
        {
            GateEntryInwardDocument viewmodel = new Models.GateEntryModel.GateEntryInwardDocument();
            HttpPostedFileBase file = null;
            var listOfAttachment = new List<string>();
           viewmodel.GateEntryDocumentId= Convert.ToInt32(Request.Form["GateEntryDocumentId"].ToString());
            viewmodel.InwardDocTypeId = Convert.ToInt32(Request.Form["InwardDocTypeId"].ToString());
            viewmodel.GateEntryNo = Request.Form["GateEntryNo"].ToString();
            if (viewmodel.GateEntryDocumentId == 0)
            {
                viewmodel.EntryDate = Request.Form["EntryDate"].ToString();
            }
            if (viewmodel.GateEntryDocumentId !=0)
            {
                viewmodel.EntryDateTime = Convert.ToDateTime(Convert.ToDateTime(Request.Form["EntryDateTime"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"));
                //var dateTime = Convert.ToDateTime(Convert.ToDateTime(viewmodel.EntryDate).ToString("dd/MM/yyyy HH:mm:ss"));
            }
            viewmodel.Mode= Request.Form["Mode"].ToString();
            viewmodel.Company= Request.Form["Company"].ToString();
            viewmodel.FromWhere= Request.Form["FromWhere"].ToString();
            viewmodel.Unit= Request.Form["Unit"].ToString();
            viewmodel.PersonName= Request.Form["PersonName"].ToString();
            viewmodel.ModeofTransport = Request.Form["ModeofTransport"].ToString();
            viewmodel.VehicleNo= Request.Form["VehicleNo"].ToString();
            viewmodel.AddressToWhom= Request.Form["AddressToWhom"].ToString();
            viewmodel.HandOverTo= Request.Form["HandOverTo"].ToString();
            viewmodel.ReceivedBy= Request.Form["ReceivedBy"].ToString();
            viewmodel.Remarks= Request.Form["Remarks"].ToString();
            viewmodel.TagName= Request.Form["TagName"].ToString();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                file = Request.Files[i];
                if (file != null && file.ContentLength > 0)
                {
                    int val = RandomNumber(99, 9999);
                    string fileName = Path.GetFileName(val + file.FileName);
                    string fileExtension = Path.GetExtension(file.FileName);
                   var filePath = Path.Combine(Server.MapPath("~/DateDocumentupload"), fileName);
                    file.SaveAs(filePath);
                    listOfAttachment.Add(fileName);
                }
            }


            GateEntryInwardDocumentMaster model = new GateEntryInwardDocumentMaster();
            GateEntryInwardDocumentManager gateEntryDocumentManager = new GateEntryInwardDocumentManager();
            string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                         "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss","dd/MM/yyyy HH:mm:ss",
                         "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                         "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                         "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm",
                         "MM/d/yyyy HH:mm:ss.ffffff" };
            DateTime? EntrydateValue = null;
            if (viewmodel.GateEntryDocumentId != 0)
            {       
                string EntryDate = string.IsNullOrEmpty(viewmodel.EntryDateTime.ToString()) ? null : viewmodel.EntryDateTime.ToString();
                DateTime? date = Convert.ToDateTime(Convert.ToDateTime(EntryDate).ToString("dd/MM/yyyy HH:mm:ss"));
                if (!string.IsNullOrEmpty(EntryDate) && viewmodel.GateEntryDocumentId != 0)
                {
                    model.EntryDateTime = date;
                }
                model.GateEntryDocumentId = viewmodel.GateEntryDocumentId;
            }
            else
            {
                if (!string.IsNullOrEmpty(viewmodel.EntryDate))
                {
                    CultureInfo culture = new CultureInfo("en-US");
                   // string[] LastAmendmentDateString = viewmodel.EntryDate.ToString().Split('-');
                   // EntrydateValue = Convert.ToDateTime(Convert.ToDateTime(viewmodel.EntryDate).ToString("dd/MM/yyyy HH:mm:ss"));
                  //  EntrydateValue = Convert.ToDateTime(LastAmendmentDateString[1] + "/" + LastAmendmentDateString[0] + "/" + LastAmendmentDateString[2], culture);
                   EntrydateValue = DateTime.ParseExact(viewmodel.EntryDate, formats,
                                                 new CultureInfo("en-US"),
                                                  DateTimeStyles.None);                   
                    model.EntryDateTime = EntrydateValue;
                }
            }
                model.GateEntryNo = viewmodel.GateEntryNo;
                model.InwardDocTypeId = viewmodel.InwardDocTypeId;
                model.Mode = viewmodel.Mode;
                model.Company = viewmodel.Company;
                model.FromWhere = viewmodel.FromWhere;
                model.Unit = viewmodel.Unit;
                model.PersonName = viewmodel.PersonName;
                model.ModeofTransport = viewmodel.ModeofTransport;
                model.VehicleNo = viewmodel.VehicleNo;
                model.AddressToWhom = viewmodel.AddressToWhom;
                model.HandOverTo = viewmodel.HandOverTo;
                model.ReceivedBy = viewmodel.ReceivedBy;
                model.Remarks = viewmodel.Remarks;
            model.TagName = viewmodel.TagName;
                gateEntryDocumentManager.Post(model, listOfAttachment);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int GateEntryDocumentId)
        {
           GateEntryInwardDocumentMaster model = new GateEntryInwardDocumentMaster();
            string status = "";
            GateEntryInwardDocumentManager gateEntryDocumentManager = new GateEntryInwardDocumentManager();
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
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateGEDocID();
            ID++;
            return ID.ToString();
        }

        public ActionResult Search(string filter)
        {           
           GateEntryInwardDocumentManager gateEntryManager = new GateEntryInwardDocumentManager();
            List<GateEntryInwardDocumentMaster> model = new List<GateEntryInwardDocumentMaster>();
            GateEntryInwardDocument vm = new GateEntryInwardDocument();
            //model = gateEntryManager.Get().Where(x=>x.GateEntryNo!=null&&x.AddressToWhom!=null).ToList();
            model = gateEntryManager.Get().Where(x => x.GateEntryNo != null).ToList();
            // model = model.Where(x => x.GateEntryNo.Contains(filter.Trim()) ||  x.AddressToWhom.Contains(filter.Trim())).ToList();
            model = model.Where(x => x.GateEntryNo.Contains(filter.Trim()) || x.PersonName.ToLower().Contains(filter.ToLower().Trim()) || x.FromWhere.ToLower().Contains(filter.ToLower().Trim())).ToList();
            //if (model.Count==0)
            //{
            //    model = gateEntryManager.Get().Where(x => x.GateEntryNo != null && x.AddressToWhom != null).ToList();
            //    model = model.Where(x => x.PersonName.ToLower().Contains(filter.ToLower().Trim()) || x.FromWhere.ToLower().Contains(filter.ToLower().Trim())).ToList();
            //}
             
            //if(model.Count==0)
            //{
            //    model = gateEntryManager.Get().Where(x => x.GateEntryNo != null && x.AddressToWhom != null).ToList();
            //    model = model.Where(x => x.FromWhere.ToLower().Contains(filter.ToLower().Trim())).ToList();
            //}
            
               var modelVehicleNo = gateEntryManager.Get().Where(x => x.GateEntryNo != null && x.VehicleNo !=null).ToList();
              var  listVehicleNo = modelVehicleNo.Where(x => x.VehicleNo.ToLower().Contains(filter.ToLower().Trim())).ToList();

                foreach(var list in listVehicleNo)
                {
                    model.Add(list);
                }





            //var modelTagName = gateEntryManager.Get().Where(x => x.GateEntryNo != null && x.TagName != null).ToList();
            //var listTagName = modelTagName.Where(x => x.TagName.ToLower().Contains(filter.ToLower().Trim())).ToList();
            //foreach (var list in listTagName)
            //{
            //    model.Add(list);
            //}




            vm.GateEntryDocumentlist = model.Distinct().ToList();
            
            return PartialView("~/Views/GateEntry/GateEntryInwardDocument/Partial/GateEntryInwardDocumentGrid.cshtml", vm);
        }
        [HttpPost]
        public ActionResult TagNameSearch(string tagFilter)
        {
            GateEntryInwardDocumentManager gateEntryManager = new GateEntryInwardDocumentManager();
            List<GateEntryInwardDocumentMaster> model = new List<GateEntryInwardDocumentMaster>();
            GateEntryInwardDocument vm = new GateEntryInwardDocument();

            model = gateEntryManager.Get().Where(x => x.GateEntryNo != null && x.TagName != null).ToList();
           model = model.Where(x => x.TagName.ToLower().Contains(tagFilter.ToLower().Trim())).ToList();
            vm.GateEntryDocumentlist = model;

            return PartialView("~/Views/GateEntry/GateEntryInwardDocument/Partial/GateEntryInwardDocumentGrid.cshtml", vm);
        }












        private readonly Random _random = new Random();

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
