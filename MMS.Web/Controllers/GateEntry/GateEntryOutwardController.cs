using MMS.Core.Entities;
using MMS.Core.Entities.GateEntry;
using MMS.Data.Context;
using MMS.Repository.Managers;
using MMS.Repository.Managers.GateEntryManager;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.GateEntryModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class GateEntryOutwardController : Controller
    {
        [HttpGet]
        public ActionResult GateEntryOutward()
        {
            GateEntryOutward model = new GateEntryOutward();
            return View("~/Views/GateEntry/GateEntryOutward/GateEntryOutward.cshtml", model);
        }

        public ActionResult GateEntryOutwardGrid()
        {
            GateEntryOutward model = new GateEntryOutward();
            List<GateEntryOutwardMaster> geMaterialGridList = new List<GateEntryOutwardMaster>();
            GateEntryOutwardManager outwardManager = new GateEntryOutwardManager();
            model.GateEntryOutwardList = outwardManager.Get();
            geMaterialGridList = outwardManager.Get().ToList();
            var groupList = geMaterialGridList.GroupBy(x => x.GateEntryNo.ToString().Trim()).Select(g => g.First()).ToList();
            model.GateEntryOutwardList = groupList;
            return PartialView("~/Views/GateEntry/GateEntryOutward/Partial/GateEntryOutwardGrid.cshtml", model);
        }

        [HttpPost]
        public ActionResult GateEntryOutwardDetails(int GateEntryOutwardId)
        {
            GateEntryOutwardMaster model = new GateEntryOutwardMaster();
            GateEntryOutward viewmodel = new GateEntryOutward();
            GateEntryOutwardManager outwardMaterialManager = new GateEntryOutwardManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            UOMManager uOMManager = new UOMManager();
            SizeRangeDetailsManager srdManager = new SizeRangeDetailsManager();
            StoreMasterManager stManager = new StoreMasterManager();
            OutwardMaterialSizeQtyRateManager sizeRatemanager = new OutwardMaterialSizeQtyRateManager();
            OutwardMaterialSizeQtyRateMaster outwardMaterialSizeQtyRate = new OutwardMaterialSizeQtyRateMaster();
            List<GEOutwardGrid> geOutwardGridList = new List<GEOutwardGrid>();
            model = outwardMaterialManager.GetGateEntryOutById(GateEntryOutwardId);
            if (model.GateEntryOutwardId != 0)
            {
                viewmodel.GateEntryOutwardId = model.GateEntryOutwardId;
                viewmodel.GateEntryNo = model.GateEntryNo;
                viewmodel.OutwardEntryDateTime = model.OutwardEntryDateTime;
                viewmodel.OutwardMaterialType = model.OutwardMaterialType;
                viewmodel.IsNewOutward = model.IsNewOutward;
                viewmodel.IsDocuments = model.IsDocuments;
                viewmodel.IsJobWork = model.IsJobWork;
                viewmodel.IsSales = model.IsSales;
                viewmodel.GSTAmount = model.GSTAmount;
                viewmodel.MaterialName = model.MaterialName;
                viewmodel.Uom = model.Uom;
                viewmodel.StoresRefNo = model.StoresRefNo;
                viewmodel.StoresRefDate = model.StoresRefDate;
                viewmodel.StoresName = model.StoresName;
                viewmodel.MaterialType = model.MaterialType;
                viewmodel.DcNo = model.DcNo;
                viewmodel.DcDate = model.DcDate.ToString();
                viewmodel.UnitDivision = model.UnitDivision;
                viewmodel.ReturnType = model.ReturnType;
                viewmodel.InvoiceNo = model.InvoiceNo;
                viewmodel.InvoiceDate = model.InvoiceDate;
                viewmodel.ModeofTransport = model.ModeofTransport;
                viewmodel.InvoiceCurrency = model.InvoiceCurrency;
                viewmodel.InvoiceValue = model.InvoiceValue;
                viewmodel.VehicleNo = model.VehicleNo;
                viewmodel.IsEmployee = model.IsEmployee;
                viewmodel.PersonId = model.PersonId;
                viewmodel.PersonName = model.PersonName;
                viewmodel.SupplierId = model.SupplierId;
                viewmodel.PreparedBy = model.PreparedBy;
                viewmodel.ApprovedBy = model.ApprovedBy;
                viewmodel.SupplierAcknowledgedBy = model.SupplierAcknowledgedBy;
                viewmodel.TotalQty = model.TotalQty;
                viewmodel.Remarks = model.Remarks;
                viewmodel.TransportCompany = model.TransportCompany;
                viewmodel.VehicleArrangedBy = model.VehicleArrangedBy;
                viewmodel.Purpose = model.Purpose;
                viewmodel.PendingQuantity = model.PendingQuantity;
                viewmodel.GST = model.GST;
                viewmodel.PlaceOfSupply = model.PlaceOfSupply;
                viewmodel.GrandTotal = model.GrandTotal;
                var GateEntryNo = model.GateEntryNo;
                viewmodel.GateEntryOutwardList = outwardMaterialManager.GEMaterialListGridBasedOnGateEntryNo(GateEntryNo);

                List<GEOutwardGrid> GridList = new List<GEOutwardGrid>();
                foreach (var item in viewmodel.GateEntryOutwardList)
                {
                    GEOutwardGrid geOutwardGrid = new GEOutwardGrid();
                    geOutwardGrid.GateEntryOutwardId = item.GateEntryOutwardId;
                    geOutwardGrid.HSNCode = item.HSNCode;
                    geOutwardGrid.PoNoId = item.PoNoId;
                    geOutwardGrid.OutwardEntryDateTime = item.OutwardEntryDateTime;
                    geOutwardGrid.MaterialNameId = item.MaterialNameId;
                    geOutwardGrid.UomId = item.UomId;
                    geOutwardGrid.SizeRangeId = item.SizeRangeId;

                    geOutwardGrid.PendingQuantity = item.PendingQuantity;
                    geOutwardGrid.TotalQty = item.TotalQty;
                    geOutwardGrid.UnitName = uOMManager.GetUomMasterId(Convert.ToInt32(geOutwardGrid.UomId)).ShortUnitName;
                    geOutwardGrid.SizeRangeDetails = srdManager.GetSizeRangeDetailsId(geOutwardGrid.SizeRangeId).SizeRangeName;
                    var MaterialNameList = materialManager.GetMaterialMasterId(geOutwardGrid.MaterialNameId).MaterialName;
                    geOutwardGrid.MaterialName = materialNameManager.GetMaterialNameMaterial(MaterialNameList.Value).MaterialDescription;
                    GridList.Add(geOutwardGrid);
                }
                viewmodel.GEOutwardGrid = GridList;
            }
            else
            {
                string year = DateTime.Now.Year.ToString();
                viewmodel.GateEntryNo = GetOutwardEntryID();
                viewmodel.GateEntryNo = "GEIM " + viewmodel.GateEntryNo + "/" + year;
                viewmodel.GEOutwardGrid = geOutwardGridList;
                return PartialView("~/Views/GateEntry/GateEntryOutward/Partial/GateEntryOutwardDetails.cshtml", viewmodel);
            }
            return PartialView("~/Views/GateEntry/GateEntryOutward/Partial/GateEntryOutwardDetails.cshtml", viewmodel);
        }


        [HttpPost]
        public ActionResult GateEntryOutwardDetailsEdit(int GateEntryOutwardId)
        {
            GateEntryOutwardMaster model = new GateEntryOutwardMaster();
            GateEntryOutward viewmodel = new GateEntryOutward();
            GateEntryOutwardManager gateEntryOutwardManager = new GateEntryOutwardManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            UOMManager uOMManager = new UOMManager();
            SizeRangeDetailsManager srdManager = new SizeRangeDetailsManager();
            StoreMasterManager stManager = new StoreMasterManager();
            List<OutwardMaterialSizeQtyRateMaster> listSizeItemMaterial = new List<OutwardMaterialSizeQtyRateMaster>();
            OutwardMaterialSizeQtyRateManager sizeqty = new OutwardMaterialSizeQtyRateManager();
            model = gateEntryOutwardManager.GetGateEntryOutById(GateEntryOutwardId);
            if (model != null)
            {
                viewmodel.GateEntryOutwardId = model.GateEntryOutwardId;
                viewmodel.GateEntryNo = model.GateEntryNo;
                viewmodel.PersonId = model.PersonId;
                viewmodel.IsEmployee = model.IsEmployee;
                viewmodel.PersonName = model.PersonName;
                viewmodel.MaterialName = model.MaterialName;
                viewmodel.Uom = model.Uom;
                viewmodel.OutwardEntryDateTime = model.OutwardEntryDateTime;
                viewmodel.OutwardMaterialType = model.OutwardMaterialType;
                viewmodel.IsNewOutward = model.IsNewOutward;
                viewmodel.GSTAmount = model.GSTAmount;
                viewmodel.IsDocuments = model.IsDocuments;
                viewmodel.IsJobWork = model.IsJobWork;
                viewmodel.IsSales = model.IsSales;
                viewmodel.StoresRefNo = model.StoresRefNo;
                viewmodel.StoresRefDate = model.StoresRefDate;
                viewmodel.StoresName = model.StoresName;
                viewmodel.MaterialType = model.MaterialType;
                viewmodel.DcNo = model.DcNo;
                viewmodel.DcDate = model.DcDate.ToString();
                viewmodel.UnitDivision = model.UnitDivision;
                viewmodel.ReturnType = model.ReturnType;
                viewmodel.InvoiceNo = model.InvoiceNo;
                viewmodel.InvoiceDate = model.InvoiceDate;
                viewmodel.ModeofTransport = model.ModeofTransport;
                viewmodel.InvoiceCurrency = model.InvoiceCurrency;
                viewmodel.InvoiceValue = model.InvoiceValue;
                viewmodel.VehicleNo = model.VehicleNo;
                viewmodel.PoNoId = model.PoNoId;
                viewmodel.SupplierId = model.SupplierId;
                viewmodel.HSNCode = model.HSNCode;
                viewmodel.UomId = model.UomId;
                viewmodel.SizeRangeId = model.SizeRangeId;
                viewmodel.MaterialNameId = model.MaterialNameId;
                viewmodel.Pcs = model.Pcs;
                viewmodel.PreparedBy = model.PreparedBy;
                viewmodel.GST = model.GST;
                viewmodel.PlaceOfSupply = model.PlaceOfSupply;
                viewmodel.GrandTotal = model.GrandTotal;
                viewmodel.ApprovedBy = model.ApprovedBy;
                viewmodel.SupplierAcknowledgedBy = model.SupplierAcknowledgedBy;
                viewmodel.Remarks = model.Remarks;
                viewmodel.TotalQty = model.TotalQty;
                viewmodel.Rate = model.Rate;
                viewmodel.Value = model.Value;
                viewmodel.Total = model.Total;
                viewmodel.TransportCompany = model.TransportCompany;
                viewmodel.VehicleArrangedBy = model.VehicleArrangedBy;
                viewmodel.Purpose = model.Purpose;
                viewmodel.PendingQuantity = model.PendingQuantity;
                var GateEntryNo = model.GateEntryNo;
                viewmodel.GateEntryOutwardList = gateEntryOutwardManager.GEMaterialListGridBasedOnGateEntryNo(GateEntryNo);

                List<GEOutwardGrid> geMaterialGridList = new List<GEOutwardGrid>();
                foreach (var item in viewmodel.GateEntryOutwardList)
                {
                    GEOutwardGrid geOutwardGrid = new GEOutwardGrid();
                    geOutwardGrid.HSNCode = item.HSNCode;
                    geOutwardGrid.GateEntryNo = item.GateEntryNo;
                    geOutwardGrid.OutwardMaterialType = item.OutwardMaterialType;
                    geOutwardGrid.IsNewOutward = item.IsNewOutward;
                    geOutwardGrid.IsDocuments = item.IsDocuments;
                    geOutwardGrid.IsJobWork = item.IsJobWork;
                    geOutwardGrid.IsSales = item.IsSales;
                    geOutwardGrid.MaterialName = item.MaterialName;
                    geOutwardGrid.GST = item.GST;

                    geOutwardGrid.PlaceOfSupply = item.PlaceOfSupply;
                    geOutwardGrid.GrandTotal = item.GrandTotal;
                    geOutwardGrid.Uom = item.Uom;
                    geOutwardGrid.StoresRefNo = item.StoresRefNo;
                    geOutwardGrid.SizeRangeId = item.SizeRangeId;
                    geOutwardGrid.SizeRangeDetails = srdManager.GetSizeRangeDetailsId(geOutwardGrid.SizeRangeId).SizeRangeName;
                    geOutwardGrid.StoresName = item.StoresName;
                    geOutwardGrid.MaterialType = item.MaterialType;
                    geOutwardGrid.DcNo = item.DcNo;
                    geOutwardGrid.UnitDivision = item.UnitDivision;
                    geOutwardGrid.ReturnType = item.ReturnType;
                    geOutwardGrid.InvoiceNo = item.InvoiceNo;
                    geOutwardGrid.ModeofTransport = item.ModeofTransport;
                    geOutwardGrid.InvoiceCurrency = item.InvoiceCurrency;
                    geOutwardGrid.InvoiceValue = item.InvoiceValue;
                    geOutwardGrid.VehicleNo = item.VehicleNo;
                    geOutwardGrid.MaterialNameId = item.MaterialNameId;
                    geOutwardGrid.SupplierId = item.SupplierId;
                    geOutwardGrid.Purpose = item.Purpose;
                    geOutwardGrid.PoNoId = item.PoNoId;
                    geOutwardGrid.UomId = item.UomId;
                    geOutwardGrid.PendingQuantity = item.PendingQuantity;
                    geOutwardGrid.TransportCompany = item.TransportCompany;
                    geOutwardGrid.VehicleArrangedBy = item.VehicleArrangedBy;
                    geOutwardGrid.TotalQty = item.TotalQty;
                    geOutwardGrid.UnitName = uOMManager.GetUomMasterId(Convert.ToInt32(geOutwardGrid.UomId)).ShortUnitName;

                    var MaterialNameList = materialManager.GetMaterialMasterId(geOutwardGrid.MaterialNameId).MaterialName;
                    geOutwardGrid.MaterialName = materialNameManager.GetMaterialNameMaterial(MaterialNameList.Value).MaterialDescription;

                    geMaterialGridList.Add(geOutwardGrid);
                }
                viewmodel.GEOutwardGrid = geMaterialGridList;
                int materialid = Convert.ToInt32(GateEntryOutwardId);
                listSizeItemMaterial = sizeqty.GetSizeItemMaterial(materialid);
               
            }
            return Json(new { ViewModel = viewmodel, SizeRange = listSizeItemMaterial, MaxJsonLength = Int32.MaxValue, }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GateEntryOutward(GateEntryOutward viewmodel)
        {
            GateEntryOutwardMaster model = new GateEntryOutwardMaster();
            List<GateEntryOutwardMaster> Outwardmodel = new List<GateEntryOutwardMaster>();
            GateEntryOutwardManager onwardManager = new GateEntryOutwardManager();
            OutwardMaterialSizeQtyRateManager sizeRatemanager = new OutwardMaterialSizeQtyRateManager();
            OutwardMaterialSizeQtyRateMaster outwardMaterialSizeQtyRate = new OutwardMaterialSizeQtyRateMaster();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            UOMManager uOMManager = new UOMManager();
            SizeRangeDetailsManager srdManager = new SizeRangeDetailsManager();
            StoreMasterManager stManager = new StoreMasterManager();
            var MaterialId1 = viewmodel.MaterialNameId;
            /// update coding
            if (viewmodel.GateEntryOutwardId != 0)
            {

                model = onwardManager.GetGateEntryOutById(viewmodel.GateEntryOutwardId);
                model.UpdatedDate = DateTime.Now;

                if (viewmodel.Size != null && viewmodel.Size != "")
                {

                    string[] sizeAray = viewmodel.Size.Split(',');
                    string[] QtyAray = viewmodel.Quantity.Split(',');
                    string[] SizeQtyRateIdAray = viewmodel.OutwardMaterialSizeQtyRateId.Split(',');

                    int count = 0;
                    foreach (var item in sizeAray)
                    {
                        OutwardMaterialSizeQtyRateMaster SizeQtyRateDetails = new OutwardMaterialSizeQtyRateMaster();
                        SizeQtyRateDetails.Size = item;
                        SizeQtyRateDetails.Quantity = QtyAray[count];
                        SizeQtyRateDetails.MaterialId = MaterialId1;
                        SizeQtyRateDetails.GateEntryOutwardId = viewmodel.GateEntryOutwardId;
                        SizeQtyRateDetails.OutwardMaterialSizeQtyRateId = int.Parse(SizeQtyRateIdAray[count]);

                        sizeRatemanager.PostMaterialSizeQtyRate(SizeQtyRateDetails);
                        count++;
                    }
                    sizeRatemanager.Post(outwardMaterialSizeQtyRate);
                }
            }
            string[] dateFormats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                         "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss","dd/MM/yyyy HH:mm:ss",
                         "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                         "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                         "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm",
                         "MM/d/yyyy HH:mm:ss.ffffff" };
            DateTime? EntrydateValue = null;
            DateTime? dcdateValue = null;
            if (viewmodel.GateEntryOutwardId != 0)
            {

                string EntryDate = string.IsNullOrEmpty(viewmodel.OutwardEntryDateTime.ToString()) ? null : viewmodel.OutwardEntryDateTime.ToString();
                DateTime? date = Convert.ToDateTime(Convert.ToDateTime(EntryDate).ToString("dd/MM/yyyy HH:mm:ss"));
                if (!string.IsNullOrEmpty(EntryDate) && viewmodel.GateEntryOutwardId != 0)
                {
                    model.OutwardEntryDateTime = date;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(viewmodel.EntryDate))
                {
                    EntrydateValue = DateTime.ParseExact(viewmodel.EntryDate, dateFormats,
                                                    new CultureInfo("en-US"),
                                                    DateTimeStyles.None);
                    model.OutwardEntryDateTime = EntrydateValue;
                }
            }

            model.GateEntryNo = viewmodel.GateEntryNo;
            model.OutwardMaterialType = viewmodel.OutwardMaterialType;
            model.IsNewOutward = viewmodel.IsNewOutward;
            model.IsDocuments = viewmodel.IsDocuments;
            model.IsJobWork = viewmodel.IsJobWork;
            model.IsSales = viewmodel.IsSales;
            model.MaterialName = viewmodel.MaterialName;
            model.Uom = viewmodel.Uom;
            model.StoresRefNo = viewmodel.StoresRefNo;
            model.GSTAmount = viewmodel.GSTAmount;
            model.GST = viewmodel.GST;
            model.PlaceOfSupply = viewmodel.PlaceOfSupply;
            model.GrandTotal = viewmodel.GrandTotal;
            model.StoresRefDate = viewmodel.StoresRefDate;
            model.StoresName = viewmodel.StoresName;
            model.UnitDivision = viewmodel.UnitDivision;
            model.ReturnType = viewmodel.ReturnType;
            model.MaterialType = viewmodel.MaterialType;
            model.DcNo = viewmodel.DcNo;
            if (!string.IsNullOrEmpty(viewmodel.DcDate))
            {
               
                DateTime DT1;
                string dts = viewmodel.DcDate.ToString();
                DateTime? date = DateTime.ParseExact(viewmodel.DcDate, "dd/MM/yyyy", null);
                model.DcDate = date;
            }
          
            model.InvoiceNo = viewmodel.InvoiceNo;
            model.InvoiceDate = viewmodel.InvoiceDate;
            model.ModeofTransport = viewmodel.ModeofTransport;
            model.InvoiceCurrency = viewmodel.InvoiceCurrency;
            model.InvoiceValue = viewmodel.InvoiceValue;
            model.VehicleNo = viewmodel.VehicleNo;
            model.PoNoId = viewmodel.PoNoId;
            model.SupplierId = viewmodel.SupplierId;
            model.IsEmployee = viewmodel.IsEmployee;
            model.PersonId = viewmodel.PersonId;
            model.PersonName = viewmodel.PersonName;
            model.MaterialNameId = viewmodel.MaterialNameId;
            model.HSNCode = viewmodel.HSNCode;
            model.UomId = viewmodel.UomId;
            model.SizeRangeId = viewmodel.SizeRangeId;
            model.Pcs = viewmodel.Pcs;
            model.PreparedBy = viewmodel.PreparedBy;
            model.ApprovedBy = viewmodel.ApprovedBy;
            model.SupplierAcknowledgedBy = viewmodel.SupplierAcknowledgedBy;
            model.Remarks = viewmodel.Remarks;
            model.TotalQty = viewmodel.TotalQty;
            model.Rate = viewmodel.Rate;
            model.Value = viewmodel.Value;
            model.Total = viewmodel.Total;
            model.TransportCompany = viewmodel.TransportCompany;
            model.VehicleArrangedBy = viewmodel.VehicleArrangedBy;
            model.Purpose = viewmodel.Purpose;
            model.PendingQuantity = viewmodel.PendingQuantity;
            var MaterialOutwardId = onwardManager.Post(model);
           var MaterialId = viewmodel.MaterialNameId;
            if (viewmodel.MaterialNameId != 0 && viewmodel.GateEntryOutwardId == 0)
            {
                
                if (viewmodel.Size != null && viewmodel.Size != "")
                    {
                       
                        string[] sizeAray = viewmodel.Size.Split(',');
                        string[] QtyAray = viewmodel.Quantity.Split(',');
                        int count = 0;
                        foreach (var item in sizeAray)
                        {
                            OutwardMaterialSizeQtyRateMaster SizeQtyRateDetails = new OutwardMaterialSizeQtyRateMaster();
                            SizeQtyRateDetails.Size = item;
                            SizeQtyRateDetails.Quantity = QtyAray[count];
                             SizeQtyRateDetails.MaterialId = MaterialId;
                            SizeQtyRateDetails.GateEntryOutwardId = MaterialOutwardId;
                            SizeQtyRateDetails.CreatedDate = DateTime.Now;
                            sizeRatemanager.PostMaterialSizeQtyRate(SizeQtyRateDetails);
                            count++;
                        }
                    sizeRatemanager.Post(outwardMaterialSizeQtyRate);
                }

               
            }
            var GateEntryNo = viewmodel.GateEntryNo;
            Outwardmodel = onwardManager.GEMaterialListGridBasedOnGateEntryNo(GateEntryNo);
            viewmodel.GateEntryOutwardList = Outwardmodel;
            List<GEOutwardGrid> GridList = new List<GEOutwardGrid>();
            foreach (var item in viewmodel.GateEntryOutwardList)
            {
                GEOutwardGrid geOutwardGrid = new GEOutwardGrid();
                geOutwardGrid.GateEntryOutwardId = item.GateEntryOutwardId;
                geOutwardGrid.HSNCode = item.HSNCode;
                geOutwardGrid.PoNoId = item.PoNoId;
                geOutwardGrid.MaterialNameId = item.MaterialNameId;
                geOutwardGrid.UomId = item.UomId;
                geOutwardGrid.SizeRangeId = item.SizeRangeId;
                geOutwardGrid.PendingQuantity = item.PendingQuantity;
                geOutwardGrid.TotalQty = item.TotalQty;
                geOutwardGrid.GST = item.GST;
                geOutwardGrid.PlaceOfSupply = item.PlaceOfSupply;
                geOutwardGrid.GrandTotal = item.GrandTotal;
                geOutwardGrid.OutwardEntryDateTime = item.OutwardEntryDateTime;
                geOutwardGrid.UnitName = uOMManager.GetUomMasterId(Convert.ToInt32(geOutwardGrid.UomId)).ShortUnitName;
                geOutwardGrid.SizeRangeDetails = srdManager.GetSizeRangeDetailsId(geOutwardGrid.SizeRangeId).SizeRangeName;
                var MaterialNameList = materialManager.GetMaterialMasterId(geOutwardGrid.MaterialNameId).MaterialName;
                geOutwardGrid.MaterialName = materialNameManager.GetMaterialNameMaterial(MaterialNameList.Value).MaterialDescription;
                GridList.Add(geOutwardGrid);
            }
            viewmodel.GEOutwardGrid = GridList;
            return Json(new { Viewmodel = viewmodel }, JsonRequestBehavior.AllowGet);

        }
        public string GetOutwardEntryID()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateGEOutwardID();
            ID++;
            return ID.ToString();
        }

        public ActionResult Delete(int GateEntryOutwardId)
        {
            GateEntryOutwardMaster model = new GateEntryOutwardMaster();
            GateEntryOutwardManager outwardManager = new GateEntryOutwardManager();
            string status = "";

            model = outwardManager.GetGateEntryOutById(GateEntryOutwardId);
            if (model != null)
            {
                status = "Success";
                outwardManager.Delete(GateEntryOutwardId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string filter)
        {
            GateEntryOutwardManager outwardMaterialManager = new GateEntryOutwardManager();
            List<GateEntryOutwardMaster> model = new List<GateEntryOutwardMaster>();
            GateEntryOutward ge = new GateEntryOutward();
            model = outwardMaterialManager.Get();
            model = model.Where(x => x.GateEntryNo.Contains(filter.Trim())).ToList();
            ge.GateEntryOutwardList = model;

            return PartialView("~/Views/GateEntry/GateEntryOutward/Partial/GateEntryOutwardGrid.cshtml", ge);
        }

        [HttpGet]
        public ActionResult GetPoNo(string SupplierId)
        {
            if (SupplierId != null)
            {
                PurchaseOrderManager manager = new PurchaseOrderManager();
                var MaterialList = (from x in manager.Get()

                                    where x.Supplier == Convert.ToInt32(SupplierId)
                                    select new
                                    {
                                        PoNo = x.PoNo,
                                        PorderId = x.PoOrderId
                                    });

                var distinctList = MaterialList.GroupBy(x => x.PoNo).Select(g => g.First()).ToList();
                return Json(distinctList, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMaterialByPoNo(string PoNoId)
        {
            if (PoNoId != null)
            {
                PurchaseOrderManager manager = new PurchaseOrderManager();
                MaterialManager smanager = new MaterialManager();
                MaterialNameManager mnmanager = new MaterialNameManager();
                var MaterialList = (from x in manager.Get()
                                    join y in smanager.Get() on x.Material equals y.MaterialMasterId
                                    join z in mnmanager.Get() on y.MaterialName equals z.MaterialMasterID
                                    where x.PoNo == PoNoId
                                    select new
                                    {
                                        MaterialMasterId = x.Material,
                                        MaterialDescription = z.MaterialDescription,
                                    });
                return Json(MaterialList, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDetailsByMaterialName(string MaterialNameId)
        {
            List<GateEntryOutwardMaterialData> InwardData = new List<GateEntryOutwardMaterialData>();
            List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
            ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();
           
            if (MaterialNameId != null)
            {
                InwardData = GetMaterialData(MaterialNameId);
                listSizeItemMaterial = GetSizeRangeData(MaterialNameId);

                return Json(new { Material = InwardData, SizeRange = listSizeItemMaterial }, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public static List<GateEntryOutwardMaterialData> GetMaterialData(string MaterialNameId)
        {
            List<GateEntryOutwardMaterialData> Materiallist = new List<GateEntryOutwardMaterialData>();

            using (var context = new MMSContext())
            {
                Materiallist = context.Database.SqlQuery<GateEntryOutwardMaterialData>("EXEC spGateEntryOutwardMaterialData @MaterialNameId={0}", MaterialNameId).ToList();
            }
            return Materiallist;
        }
        public static List<SizeItemMaterial> GetSizeRangeData(string MaterialNameId)
        {
            List<SizeItemMaterial> Materiallist = new List<SizeItemMaterial>();

            using (var context = new MMSContext())
            {
                Materiallist = context.Database.SqlQuery<SizeItemMaterial>("EXEC spGateEntrySizeData @MaterialNameId={0}", MaterialNameId).ToList();
            }
            return Materiallist;
        }
    
        #region PDF Generation
        public FileResult PDFGenerate(string GateEntryNo)
        {
            GateEntryOutward model = new GateEntryOutward();
            GateEntryOutwardManager manager = new GateEntryOutwardManager();
            string file = HostingEnvironment.MapPath("~/App_Data/PdfTemplate/index.html");
            string contents = System.IO.File.ReadAllText(file);
            string filename = MMS.Web.PDFGeneration.PdfGeneration.PrintPdfGeneration(GateEntryNo, contents);
          
            if (filename != "")
            {
                var bytes = System.IO.File.ReadAllBytes(filename);
             
                string PDFfileName = Path.GetFileName(filename);
                return File(bytes, "application/pdf", PDFfileName);
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
