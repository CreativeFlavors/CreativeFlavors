using MMS.Core.Entities;
using MMS.Core.Entities.GateEntry;
using MMS.Core.Entities.Stock;
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
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class GateEntryInwardMaterialsController : Controller
    {
        [HttpGet]
        public ActionResult GateEntryInwardMaterials()
        {
            GateEntryInwardMaterials model = new GateEntryInwardMaterials();
            return View("~/Views/GateEntry/GateEntryInwardMaterials/GateEntryInwardMaterials.cshtml", model);
        }

        public ActionResult GateEntryInwardMaterialsGrid()
        {
            GateEntryInwardMaterials model = new GateEntryInwardMaterials();
            List<GateEntryInwardMaterialsMaster> geMaterialGridList = new List<GateEntryInwardMaterialsMaster>();
            GateEntryInwardMaterialsManager manager = new GateEntryInwardMaterialsManager();
            model.GateEntryInwardMaterialList = manager.Get();
            geMaterialGridList = manager.Get().ToList();
            var groupList = geMaterialGridList.GroupBy(x => x.GateEntryNo.ToString().Trim()).Select(g => g.First()).ToList();
            model.GateEntryInwardMaterialList = groupList;

            return PartialView("~/Views/GateEntry/GateEntryInwardMaterials/Partial/GateEntryInwardMaterialsGrid.cshtml", model);
        }
        [HttpGet]
        public ActionResult GateEntryInwardNO(string GateEntryNo)
        {
            GateEntryInwardMaterialsMaster model = new GateEntryInwardMaterialsMaster();
            GateEntryInwardMaterials viewmodel = new GateEntryInwardMaterials();
            GateEntryInwardMaterialsManager inwardMaterialManager = new GateEntryInwardMaterialsManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            UOMManager uOMManager = new UOMManager();
            SizeRangeDetailsManager srdManager = new SizeRangeDetailsManager();
            StoreMasterManager stManager = new StoreMasterManager();
            List<GEInwardMaterialGrid> geMaterialGridList = new List<GEInwardMaterialGrid>();
            viewmodel.GateEntryInwardMaterialList = inwardMaterialManager.GEMaterialListGridBasedOnPoNo(GateEntryNo);
            if (viewmodel.GateEntryInwardMaterialList != null && viewmodel.GateEntryInwardMaterialList.Count > 0)
            {
                model = inwardMaterialManager.GetGateEntryInMaterialById(viewmodel.GateEntryInwardMaterialList.FirstOrDefault().GateEntryInwardId);
            }

            if (model.GateEntryInwardId != 0)
            {
                viewmodel.InwardEntryDateTime = model.InwardEntryDateTime;
                viewmodel.InwardMaterialType = model.InwardMaterialType;
                viewmodel.IsReturned = model.IsReturned;
                viewmodel.IsNewInward = model.IsNewInward;
                viewmodel.IsJobWork = model.IsJobWork;
                viewmodel.DcRefNo = model.DcRefNo;
                viewmodel.DcRefDate = model.DcRefDate;
                viewmodel.Company = model.Company;
                viewmodel.MaterialName = model.MaterialName;
                viewmodel.UnitDivision = model.UnitDivision;
                viewmodel.ReturnType = model.ReturnType;
                viewmodel.MaterialType = model.MaterialType;
                viewmodel.DcNo = model.DcNo;
                viewmodel.DcDate = model.DcDate;
                viewmodel.InvoiceNo = model.InvoiceNo;
                viewmodel.InvoiceDate = model.InvoiceDate;
                viewmodel.ModeofTransport = model.ModeofTransport;
                viewmodel.InvoiceCurrency = model.InvoiceCurrency;
                viewmodel.InvoiceValue = model.InvoiceValue;
                viewmodel.VehicleNo = model.VehicleNo;
                viewmodel.PoNoId = model.PoNoId;
                viewmodel.SupplierId = model.SupplierId;
                viewmodel.Pcs = model.Pcs;
                viewmodel.ReceivedBy = model.ReceivedBy;
                viewmodel.AcknowledgedBy = model.AcknowledgedBy;
                viewmodel.Remarks = model.Remarks;
                viewmodel.GateEntryNo = model.GateEntryNo;
                viewmodel.TotalQty = model.TotalQty;
                viewmodel.Rate = model.Rate;
                viewmodel.Value = model.Value;
                viewmodel.InwardPo = model.InwardPo;
                viewmodel.PoTotal = model.PoTotal;
                viewmodel.PendingQuantity = model.PendingQuantity;
                viewmodel.GateEntryInwardMaterialList = inwardMaterialManager.GEMaterialListGridBasedOnPoNo(GateEntryNo);
                List<GEInwardMaterialGrid> GridList = new List<GEInwardMaterialGrid>();
                foreach (var item in viewmodel.GateEntryInwardMaterialList)
                {
                    GEInwardMaterialGrid geInMaterialGrid = new GEInwardMaterialGrid();
                    /*SUB GRID VALUES*/
                    geInMaterialGrid.GateEntryInwardId = item.GateEntryInwardId;
                    geInMaterialGrid.TotalQty = item.TotalQty;
                    geInMaterialGrid.MaterialNameId = item.MaterialNameId;
                    geInMaterialGrid.HSNCode = item.HSNCode;
                    geInMaterialGrid.SizeRangeId = item.SizeRangeId;
                    geInMaterialGrid.PendingQuantity = item.PendingQuantity;
                    geInMaterialGrid.Piecies = item.Piecies;
                    geInMaterialGrid.SupplierId = item.SupplierId;
                    geInMaterialGrid.StoresId = item.StoresId;
                    geInMaterialGrid.UomId = item.UomId;
                    geInMaterialGrid.UnitName = uOMManager.GetUomMasterId(Convert.ToInt32(geInMaterialGrid.UomId)).ShortUnitName;
                    if (geInMaterialGrid.SizeRangeId != null && geInMaterialGrid.SizeRangeId != 0)
                    {
                        geInMaterialGrid.SizeRangeDetails = srdManager.GetSizeRangeDetailsId(geInMaterialGrid.SizeRangeId).SizeRangeName;
                    }
                    geInMaterialGrid.StoreName = stManager.GetStoreMasterId(geInMaterialGrid.StoresId.Value).StoreName;
                    var MaterialNameList = materialManager.GetMaterialMasterId(geInMaterialGrid.MaterialNameId).MaterialName;
                    geInMaterialGrid.MaterialName = materialNameManager.GetMaterialNameMaterial(MaterialNameList.Value).MaterialDescription;
                    geInMaterialGrid.InwardEntryDateTime = item.InwardEntryDateTime;
                    GridList.Add(geInMaterialGrid);
                }
                viewmodel.GEMaterialsGrid = GridList;
                ViewBag.Name = model.InwardPo;
            }
            else
            {
                string year = DateTime.Now.Year.ToString();
                viewmodel.GateEntryNo = GetInwardEntryID();
                viewmodel.GateEntryNo = "GEIM " + viewmodel.GateEntryNo + "/" + year;
                viewmodel.GEMaterialsGrid = geMaterialGridList;
                return PartialView("~/Views/GateEntry/GateEntryInwardMaterials/Partial/GateEntryInwardMaterialsDetails.cshtml", viewmodel);
            }
            return PartialView("~/Views/GateEntry/GateEntryInwardMaterials/Partial/GateEntryInwardMaterialsDetails.cshtml", viewmodel);
        }
        [HttpPost]
        public ActionResult GateEntryInwardMaterialsDetails(int GateEntryInwardId)
        {
            GateEntryInwardMaterialsMaster model = new GateEntryInwardMaterialsMaster();
            GateEntryInwardMaterials viewmodel = new GateEntryInwardMaterials();
            GateEntryInwardMaterialsManager inwardMaterialManager = new GateEntryInwardMaterialsManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            UOMManager uOMManager = new UOMManager();
            SizeRangeDetailsManager srdManager = new SizeRangeDetailsManager();
            StoreMasterManager stManager = new StoreMasterManager();
            List<GEInwardMaterialGrid> geMaterialGridList = new List<GEInwardMaterialGrid>();
            model = inwardMaterialManager.GetGateEntryInMaterialById(GateEntryInwardId);
            if (model.GateEntryInwardId != 0)
            {
                viewmodel.InwardEntryDateTime = model.InwardEntryDateTime;
                viewmodel.InwardMaterialType = model.InwardMaterialType;
                viewmodel.IsReturned = model.IsReturned;
                viewmodel.IsNewInward = model.IsNewInward;
                viewmodel.IsJobWork = model.IsJobWork;
                viewmodel.DcRefNo = model.DcRefNo;
                viewmodel.DcRefDate = model.DcRefDate;
                viewmodel.Company = model.Company;
                viewmodel.MaterialName = model.MaterialName;
                viewmodel.UnitDivision = model.UnitDivision;
                viewmodel.ReturnType = model.ReturnType;
                viewmodel.MaterialType = model.MaterialType;
                viewmodel.DcNo = model.DcNo;
                viewmodel.DcDate = model.DcDate;
                viewmodel.InvoiceNo = model.InvoiceNo;
                viewmodel.InvoiceDate = model.InvoiceDate;
                viewmodel.ModeofTransport = model.ModeofTransport;
                viewmodel.InvoiceCurrency = model.InvoiceCurrency;
                viewmodel.InvoiceValue = model.InvoiceValue;
                viewmodel.VehicleNo = model.VehicleNo;
                viewmodel.PoNoId = model.PoNoId;
                viewmodel.SupplierId = model.SupplierId;
                viewmodel.Pcs = model.Pcs;
                viewmodel.ReceivedBy = model.ReceivedBy;
                viewmodel.AcknowledgedBy = model.AcknowledgedBy;
                viewmodel.Remarks = model.Remarks;
                viewmodel.GateEntryNo = model.GateEntryNo;
                viewmodel.TotalQty = model.TotalQty;
                viewmodel.Rate = model.Rate;
                viewmodel.Value = model.Value;
                viewmodel.InwardPo = model.InwardPo;
                viewmodel.PoTotal = model.PoTotal;
                viewmodel.PendingQuantity = model.PendingQuantity;
                string GateEntryNo = model.GateEntryNo;
                viewmodel.GateEntryInwardMaterialList = inwardMaterialManager.GEMaterialListGridBasedOnPoNo(GateEntryNo);
                List<GEInwardMaterialGrid> GridList = new List<GEInwardMaterialGrid>();
                foreach (var item in viewmodel.GateEntryInwardMaterialList)
                {
                    GEInwardMaterialGrid geInMaterialGrid = new GEInwardMaterialGrid();
                    /*SUB GRID VALUES*/
                    geInMaterialGrid.GateEntryInwardId = item.GateEntryInwardId;
                    geInMaterialGrid.TotalQty = item.TotalQty;
                    geInMaterialGrid.MaterialNameId = item.MaterialNameId;
                    geInMaterialGrid.HSNCode = item.HSNCode;
                    geInMaterialGrid.SizeRangeId = item.SizeRangeId;
                    geInMaterialGrid.PendingQuantity = item.PendingQuantity;
                    geInMaterialGrid.Piecies = item.Piecies;
                    geInMaterialGrid.SupplierId = item.SupplierId;
                    geInMaterialGrid.StoresId = item.StoresId;
                    geInMaterialGrid.UomId = item.UomId;
                    geInMaterialGrid.UnitName = uOMManager.GetUomMasterId(Convert.ToInt32(geInMaterialGrid.UomId)).ShortUnitName;
                    if (geInMaterialGrid.SizeRangeId != null && geInMaterialGrid.SizeRangeId != 0)
                    {
                        geInMaterialGrid.SizeRangeDetails = srdManager.GetSizeRangeDetailsId(geInMaterialGrid.SizeRangeId).SizeRangeName;
                    }
                    geInMaterialGrid.StoreName = stManager.GetStoreMasterId(geInMaterialGrid.StoresId.Value).StoreName;
                    var MaterialNameList = materialManager.GetMaterialMasterId(geInMaterialGrid.MaterialNameId).MaterialName;
                    geInMaterialGrid.MaterialName = materialNameManager.GetMaterialNameMaterial(MaterialNameList.Value).MaterialDescription;
                    geInMaterialGrid.InwardEntryDateTime = item.InwardEntryDateTime;
                    GridList.Add(geInMaterialGrid);

                }
                viewmodel.GateEntryInwardMaterialsUploadlist = inwardMaterialManager.GetGateEntryInwordMaterialuploaddatalist(GateEntryInwardId);
                viewmodel.GEMaterialsGrid = GridList;
                ViewBag.Name = model.InwardPo;
            }
            else
            {
                string year = DateTime.Now.Year.ToString();
                viewmodel.GateEntryNo = GetInwardEntryID();
                viewmodel.GateEntryNo = "GEIM " + viewmodel.GateEntryNo + "/" + year;
                viewmodel.GEMaterialsGrid = geMaterialGridList;
                return PartialView("~/Views/GateEntry/GateEntryInwardMaterials/Partial/GateEntryInwardMaterialsDetails.cshtml", viewmodel);
            }
            return PartialView("~/Views/GateEntry/GateEntryInwardMaterials/Partial/GateEntryInwardMaterialsDetails.cshtml", viewmodel);
        }

        [HttpPost]
        public ActionResult GateEntryInwardMaterialsDetailsEdit(int GateEntryInwardId)
        {
            GateEntryInwardMaterialsMaster model = new GateEntryInwardMaterialsMaster();
            GateEntryInwardMaterials viewmodel = new GateEntryInwardMaterials();
            GateEntryInwardMaterialsManager inwardMaterialManager = new GateEntryInwardMaterialsManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            UOMManager uOMManager = new UOMManager();
            SizeRangeDetailsManager srdManager = new SizeRangeDetailsManager();
            StoreMasterManager stManager = new StoreMasterManager();
            List<InwardMaterialSizeQtyRateMaster> listSizeItemMaterial = new List<InwardMaterialSizeQtyRateMaster>();
            List<InwardMaterialPendingQuantityMaster> pendingQtyMaterial = new List<InwardMaterialPendingQuantityMaster>();
            InwardMaterialSizeQtyRateManager sizeqty = new InwardMaterialSizeQtyRateManager();
            InwardMaterialPendingQuantityManager pendingqty = new InwardMaterialPendingQuantityManager();

            model = inwardMaterialManager.GetGateEntryInMaterialById(GateEntryInwardId);
            if (model != null)
            {
                viewmodel.GateEntryInwardId = model.GateEntryInwardId;
                viewmodel.GateEntryNo = model.GateEntryNo;
                viewmodel.InwardEntryDateTime = model.InwardEntryDateTime;
                viewmodel.InwardMaterialType = model.InwardMaterialType;
                viewmodel.IsReturned = model.IsReturned;
                viewmodel.IsNewInward = model.IsNewInward;
                viewmodel.IsJobWork = model.IsJobWork;
                viewmodel.DcRefNo = model.DcRefNo;
                viewmodel.MaterialName = model.MaterialName;
                viewmodel.DcRefDate = model.DcRefDate;
                viewmodel.Company = model.Company;
                viewmodel.UnitDivision = model.UnitDivision;
                viewmodel.ReturnType = model.ReturnType;
                viewmodel.MaterialType = model.MaterialType;
                viewmodel.InwardMaterialType = model.InwardMaterialType;
                viewmodel.DcNo = model.DcNo;
                viewmodel.DcDate = model.DcDate;
                viewmodel.InvoiceNo = model.InvoiceNo;
                viewmodel.InvoiceDate = model.InvoiceDate;
                viewmodel.ModeofTransport = model.ModeofTransport;
                viewmodel.InvoiceCurrency = model.InvoiceCurrency;
                viewmodel.InvoiceValue = model.InvoiceValue;
                viewmodel.VehicleNo = model.VehicleNo;
                viewmodel.PoNoId = model.PoNoId;
                viewmodel.SupplierId = model.SupplierId;
                viewmodel.ReceivedBy = model.ReceivedBy;
                viewmodel.AcknowledgedBy = model.AcknowledgedBy;
                viewmodel.Remarks = model.Remarks;
                viewmodel.UomId = model.UomId;
                viewmodel.MaterialNameId = model.MaterialNameId;
                viewmodel.Pcs = model.Pcs;
                viewmodel.SizeRangeId = model.SizeRangeId;
                viewmodel.HSNCode = model.HSNCode;
                viewmodel.StoresId = model.StoresId;
                viewmodel.Quantity = model.Quantity;
                viewmodel.TotalQty = model.TotalQty;
                viewmodel.Rate = model.Rate;
                viewmodel.Value = model.Value;
                viewmodel.DcTotal = model.DcTotal;
                viewmodel.ArrivedTotal = model.ArrivedTotal;
                viewmodel.InwardPo = model.InwardPo;
                viewmodel.PoTotal = model.PoTotal;
                viewmodel.PendingQuantity = model.PendingQuantity;
                string GateEntryNo = model.GateEntryNo;
                viewmodel.GateEntryInwardMaterialList = inwardMaterialManager.GEMaterialListGridBasedOnPoNo(GateEntryNo);
                List<GEInwardMaterialGrid> geMaterialGridList = new List<GEInwardMaterialGrid>();
                foreach (var item in viewmodel.GateEntryInwardMaterialList)
                {
                    GEInwardMaterialGrid geInMaterialGrid = new GEInwardMaterialGrid();
                    geInMaterialGrid.InvoiceDate = item.InvoiceDate;
                    geInMaterialGrid.MaterialNameId = item.MaterialNameId;
                    geInMaterialGrid.DcDate = item.DcDate;
                    geInMaterialGrid.HSNCode = item.HSNCode;
                    geInMaterialGrid.MaterialName = item.MaterialName;
                    geInMaterialGrid.SizeRangeId = item.SizeRangeId;
                    geInMaterialGrid.PendingQuantity = item.PendingQuantity;
                    geInMaterialGrid.TotalQty = item.TotalQty;
                    geInMaterialGrid.Piecies = item.Piecies;
                    geInMaterialGrid.SupplierId = item.SupplierId;
                    geInMaterialGrid.AcknowledgedBy = item.AcknowledgedBy;
                    geInMaterialGrid.ReceivedBy = item.ReceivedBy;
                    geInMaterialGrid.StoresId = item.StoresId;
                    geInMaterialGrid.UomId = item.UomId;
                    geInMaterialGrid.UnitName = uOMManager.GetUomMasterId(Convert.ToInt32(geInMaterialGrid.UomId)).ShortUnitName;
                    if (geInMaterialGrid.SizeRangeId != null && geInMaterialGrid.SizeRangeId != 0)
                    {
                        geInMaterialGrid.SizeRangeDetails = srdManager.GetSizeRangeDetailsId(geInMaterialGrid.SizeRangeId.Value).SizeRangeName;
                    }
                    geInMaterialGrid.StoreName = stManager.GetStoreMasterId(geInMaterialGrid.StoresId.Value).StoreName;
                    var MaterialNameList = materialManager.GetMaterialMasterId(geInMaterialGrid.MaterialNameId).MaterialName;
                    geInMaterialGrid.MaterialName = materialNameManager.GetMaterialNameMaterial(MaterialNameList.Value).MaterialDescription;
                    geMaterialGridList.Add(geInMaterialGrid);
                }
                viewmodel.GEMaterialsGrid = geMaterialGridList;
                int materialid = Convert.ToInt32(GateEntryInwardId);
                listSizeItemMaterial = sizeqty.GetSizeItemMaterial(materialid);
                if (model.PendingQuantity != null && model.PendingQuantity != "" && model.PendingQuantity != "0")
                {
                    pendingQtyMaterial = pendingqty.GetInwardIDBasedMaterial(materialid, model.PoNoId.Value);
                }

            }
            return Json(new { ViewModel = viewmodel, SizeRange = listSizeItemMaterial, PendingQuantity = pendingQtyMaterial }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GateEntryInwardMaterialsfile()
        {
            GateEntryInwardMaterials viewmodel = new Models.GateEntryModel.GateEntryInwardMaterials();

             HttpPostedFileBase file = null;
            var listOfAttachment = new List<string>();
            viewmodel.GateEntryInwardId = Convert.ToInt32(Request.Form["GateEntryInwardId"].ToString());
            viewmodel.GateEntryNo = Request.Form["GateEntryNo"].ToString();
            viewmodel.EntryDate = Request.Form["EntryDate"].ToString();
            viewmodel.Size = Request.Form["Size"].ToString();
            viewmodel.PoQty = Request.Form["PoQty"].ToString();
            viewmodel.DcQty = Request.Form["DcQty"].ToString();
            viewmodel.ArrivedQty = Request.Form["ArrivedQty"].ToString();
            viewmodel.PendingQty = Request.Form["PendingQty"].ToString();
            viewmodel.MaterialName = Request.Form["MaterialName"].ToString();
             var InwardEDateTime = Request.Form["InwardEntryDateTime"].ToString();
            CultureInfo culture = new CultureInfo("en-US");
            string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                         "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss","dd/MM/yyyy HH:mm:ss",
                         "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                         "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                         "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm",
                         "MM/d/yyyy HH:mm:ss.ffffff" };
            if (InwardEDateTime!= "undefined")
            {
                viewmodel.InwardEntryDateTime = DateTime.ParseExact(InwardEDateTime, formats,
                                                     new CultureInfo("en-US"),
                                                      DateTimeStyles.None);
            }
            else
            {
                viewmodel.InwardEntryDateTime = null;

            }
            viewmodel.InwardMaterialType = Convert.ToInt32(Request.Form["InwardMaterialType"].ToString());
            viewmodel.IsReturned = Convert.ToBoolean(Request.Form["IsReturned"].ToString());
            viewmodel.IsNewInward = Convert.ToBoolean (Request.Form["IsNewInward"].ToString());
            viewmodel.IsJobWork = Convert.ToBoolean(Request.Form["IsJobWork"].ToString());
            viewmodel.DcRefNo = Request.Form["DcRefNo"].ToString();
            viewmodel.DcRefDate = Request.Form["DcRefDate"].ToString();
            viewmodel.Company = Request.Form["Company"].ToString();
            viewmodel.UnitDivision = Request.Form["UnitDivision"].ToString();
            viewmodel.ReturnType = Request.Form["ReturnType"].ToString();
            viewmodel.MaterialType = Request.Form["MaterialType"].ToString();
            viewmodel.DcNo = Request.Form["DcNo"].ToString();
            viewmodel.DcDate = Request.Form["DcDate"].ToString();
            viewmodel.InvoiceNo = Request.Form["InvoiceNo"].ToString();
            viewmodel.InvoiceDate = Request.Form["InvoiceDate"].ToString();
            viewmodel.ModeofTransport = Request.Form["ModeofTransport"].ToString();
            viewmodel.InvoiceCurrency =Convert.ToInt32(Request.Form["InvoiceCurrency"].ToString());
            viewmodel.InvoiceValue = Request.Form["InvoiceValue"].ToString();
            viewmodel.VehicleNo = Request.Form["VehicleNo"].ToString();
           var ponoid= Request.Form["PoNoId"].ToString();
            if(ponoid!="")
            {
                viewmodel.PoNoId = Convert.ToInt32(ponoid);
            }
           
            
            viewmodel.SupplierId = Convert.ToInt32(Request.Form["SupplierId"].ToString());
            viewmodel.StoresId = Convert.ToInt32(Request.Form["StoresId"].ToString());
            viewmodel.MaterialNameId = Convert.ToInt32(Request.Form["MaterialNameId"].ToString());
            viewmodel.HSNCode = Request.Form["HSNCode"].ToString();
           var sizerangeid= Request.Form["SizeRangeId"].ToString();

            if(sizerangeid != "undefined" && sizerangeid!="")
            {
                viewmodel.SizeRangeId = Convert.ToInt32(Request.Form["SizeRangeId"].ToString());
            }
            
            viewmodel.UomId = Convert.ToInt32(Request.Form["UomId"].ToString());
            viewmodel.Pcs = Request.Form["Pcs"].ToString();
            viewmodel.ReceivedBy = Request.Form["ReceivedBy"].ToString();
            viewmodel.AcknowledgedBy = Request.Form["AcknowledgedBy"].ToString();
            viewmodel.Remarks = Request.Form["Remarks"].ToString();
          var quality=  Request.Form["Quantity"].ToString();
            if(quality!= "undefined" && quality !="")
            {
                viewmodel.Quantity = Request.Form["Quantity"].ToString();
            }
           
            
            viewmodel.TotalQty =Convert.ToDecimal(Request.Form["TotalQty"].ToString());
            viewmodel.Rate = Convert.ToDecimal(Request.Form["Rate"].ToString());
            viewmodel.Value = Convert.ToDecimal(Request.Form["Value"].ToString());
           var dctotal= Request.Form["DcTotal"].ToString();
            if(dctotal!="")
            {
                viewmodel.DcTotal = Convert.ToDecimal(Request.Form["DcTotal"].ToString());
            }
            var arrivedtotal=Request.Form["ArrivedTotal"].ToString();
            if(arrivedtotal!="")
            {
                viewmodel.ArrivedTotal = Convert.ToInt32(Request.Form["ArrivedTotal"].ToString());
            }
            var inwordpo=Request.Form["InwardPo"].ToString();
            if(inwordpo!="")
            {
                viewmodel.InwardPo = Convert.ToInt32(Request.Form["InwardPo"].ToString());
            }
           var pototal= Request.Form["PoTotal"].ToString();
            if(pototal!="")
            {
                viewmodel.PoTotal = Convert.ToInt32(Request.Form["PoTotal"].ToString());

            }            
            viewmodel.PendingQuantity = Request.Form["PendingQuantity"].ToString();


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



            List<GateEntryInwardMaterialsMaster> Outwardmodel = new List<GateEntryInwardMaterialsMaster>();
            GateEntryInwardMaterialsMaster model = new GateEntryInwardMaterialsMaster();
            GateEntryInwardMaterialsManager inwardMaterialManager = new GateEntryInwardMaterialsManager();
            InwardMaterialSizeQtyRateManager sizeRatemanager = new InwardMaterialSizeQtyRateManager();
            InwardMaterialPendingQuantityManager pendingQtyManager = new InwardMaterialPendingQuantityManager();
            InwardMaterialSizeQtyRateMaster inwardMaterialSizeQtyRate = new InwardMaterialSizeQtyRateMaster();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            UOMManager uOMManager = new UOMManager();
            SizeRangeDetailsManager srdManager = new SizeRangeDetailsManager();
            StoreMasterManager stManager = new StoreMasterManager();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            PurchaseOrderManager purchaseorderManager = new PurchaseOrderManager();
       //     purchaseOrder = purchaseorderManager.GetPoExist(viewmodel.PoNoId.ToString(), viewmodel.MaterialNameId.Value);
            int? MaterialId1 = viewmodel.MaterialNameId;
            if (purchaseOrder != null && purchaseOrder.PoOrderId != 0)
            {
                viewmodel.PoNoId = purchaseOrder.PoOrderId;
            }
            /// update coding           
            if (viewmodel.GateEntryInwardId != 0)
            {
                model = inwardMaterialManager.GetGateEntryInMaterialById(viewmodel.GateEntryInwardId);
                model.UpdatedDate = DateTime.Now;

                if (viewmodel.Size != null && viewmodel.Size != "")
                {
                    string[] SizeQtyRateIdAray = null;
                    string[] PendQtyIdAray = null;
                    string[] sizeAray = viewmodel.Size.Split(',');
                    string[] PoQtyAray = viewmodel.PoQty.Split(',');
                    string[] DcQtyAray = viewmodel.DcQty.Split(',');
                    string[] ArrQtyAray = viewmodel.ArrivedQty.Split(',');
                    string[] PendAray = viewmodel.PendingQty.Split(',');
                    if (viewmodel.InwardMaterialSizeQtyRateId != null)
                    {
                        SizeQtyRateIdAray = viewmodel.InwardMaterialSizeQtyRateId.Split(',');
                    }
                    if (viewmodel.InwardMaterialSizeQtyRateId != null)
                    {
                        PendQtyIdAray = viewmodel.PendingQuantityId.Split(',');
                    }

                    int count = 0;
                    foreach (var item in sizeAray)
                    {
                        InwardMaterialSizeQtyRateMaster SizeQtyRateDetails = new InwardMaterialSizeQtyRateMaster();
                        SizeQtyRateDetails.Size = item;
                        SizeQtyRateDetails.PoQty = PoQtyAray[count];
                        SizeQtyRateDetails.DcQty = DcQtyAray[count];
                        SizeQtyRateDetails.ArrivedQty = ArrQtyAray[count];
                        SizeQtyRateDetails.MaterialId = MaterialId1;
                        SizeQtyRateDetails.GateEntryInwardId = viewmodel.GateEntryInwardId;
                        if (SizeQtyRateIdAray[count] != "")
                        {
                            SizeQtyRateDetails.InwardMaterialSizeQtyRateId = int.Parse(SizeQtyRateIdAray[count]);
                        }

                        sizeRatemanager.PostMaterialSizeQtyRate(SizeQtyRateDetails);
                        count++;
                    }
                    int count1 = 0;
                    if (model.PendingQuantity != null && model.PendingQuantity != "" && model.PendingQuantity != "0")
                    {
                        foreach (var item in sizeAray)
                        {
                            InwardMaterialPendingQuantityMaster pendingQtyDetails = new InwardMaterialPendingQuantityMaster();
                            pendingQtyDetails.Size = item;
                            pendingQtyDetails.DcQty = DcQtyAray[count1];
                            pendingQtyDetails.ArrivedQty = ArrQtyAray[count1];
                            pendingQtyDetails.PendingQty = PendAray[count1];
                            pendingQtyDetails.PoQuantity = PendAray[count1];
                            pendingQtyDetails.MaterialId = MaterialId1;
                            pendingQtyDetails.PoOrderID = viewmodel.PoNoId;
                            pendingQtyDetails.GateEntryInwardId = viewmodel.GateEntryInwardId;
                            pendingQtyDetails.CreatedDate = DateTime.Now;
                            if (PendQtyIdAray[count1] != "")
                            {
                                pendingQtyDetails.PendingQuantityId = int.Parse(PendQtyIdAray[count1]);
                            }
                            pendingQtyManager.PostMaterialSizeQtyRate(pendingQtyDetails);
                            count1++;
                        }
                    }
                }

            }
            string[] dateFormats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                         "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss","dd/MM/yyyy HH:mm:ss","dd/MM/yyyy","MM/dd/yyyy",
                         "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                         "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                         "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm",
                         "MM/d/yyyy HH:mm:ss.ffffff" };
            DateTime? EntrydateValue = null;
            DateTime? DcdateValue = null;
            DateTime? DcRefdateValue = null;
            DateTime? InvoicedateValue = null;

            if (viewmodel.GateEntryInwardId != 0)
            {

                string EntryDate = string.IsNullOrEmpty(viewmodel.InwardEntryDateTime.ToString()) ? null : viewmodel.InwardEntryDateTime.ToString();
                DateTime? date = Convert.ToDateTime(Convert.ToDateTime(EntryDate).ToString("dd/MM/yyyy HH:mm:ss"));

                if (!string.IsNullOrEmpty(viewmodel.DcDate))
                {
                    DcdateValue = DateTime.ParseExact(viewmodel.DcDate, dateFormats,
                                                    new CultureInfo("en-US"),
                                                    DateTimeStyles.None);
                    model.DcDate = DcdateValue.ToString();
                }
                if (!string.IsNullOrEmpty(viewmodel.DcRefDate.ToString()))
                {
                    DcRefdateValue = DateTime.ParseExact(viewmodel.DcRefDate, dateFormats,
                                                    new CultureInfo("en-US"),
                                                    DateTimeStyles.None);
                    model.DcRefDate = DcRefdateValue.ToString();
                }
                if (!string.IsNullOrEmpty(viewmodel.InvoiceDate.ToString()))
                {
                    InvoicedateValue = DateTime.ParseExact(viewmodel.InvoiceDate, dateFormats,
                                                    new CultureInfo("en-US"),
                                                    DateTimeStyles.None);
                    model.InvoiceDate = InvoicedateValue.ToString();
                }
                if (!string.IsNullOrEmpty(EntryDate))
                {
                    model.InwardEntryDateTime = date;
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(viewmodel.EntryDate))
                {
                  // var entrydate= viewmodel.EntryDate;
                  //var entrydate2=  Convert.ToDateTime(Convert.ToDateTime(viewmodel.EntryDate).ToString("dd/MM/yyyy HH:mm:ss"));
                    //EntrydateValue = DateTime.ParseExact(viewmodel.EntryDate, dateFormats,
                    //                                new CultureInfo("en-US"),
                    //                                DateTimeStyles.None);
                    model.InwardEntryDateTime = System.DateTime.Now;
                }
                if (!string.IsNullOrEmpty(viewmodel.DcDate))
                {
                    DcdateValue = DateTime.ParseExact(viewmodel.DcDate, dateFormats,
                                                    new CultureInfo("en-US"),
                                                    DateTimeStyles.None);
                    model.DcDate = DcdateValue.ToString();
                }
                if (!string.IsNullOrEmpty(viewmodel.DcRefDate.ToString()))
                {
                    DcRefdateValue = DateTime.ParseExact(viewmodel.DcRefDate, dateFormats,
                                                    new CultureInfo("en-US"),
                                                    DateTimeStyles.None);
                    model.DcRefDate = DcRefdateValue.ToString();
                }
                if (!string.IsNullOrEmpty(viewmodel.InvoiceDate.ToString()))
                {
                    InvoicedateValue = DateTime.ParseExact(viewmodel.InvoiceDate, dateFormats,
                                                    new CultureInfo("en-US"),
                                                    DateTimeStyles.None);
                    model.InvoiceDate = InvoicedateValue.ToString();
                }
            }
            model.InwardEntryDateTime = System.DateTime.Now;
            model.IsReturned = viewmodel.IsReturned;
            model.IsNewInward = viewmodel.IsNewInward;
            model.IsJobWork = viewmodel.IsJobWork;
            model.GateEntryNo = viewmodel.GateEntryNo;
            model.InwardMaterialType = viewmodel.InwardMaterialType;
            model.DcRefNo = viewmodel.DcRefNo;
            model.DcRefDate = viewmodel.DcRefDate;
            model.Company = viewmodel.Company;
            model.MaterialName = viewmodel.MaterialName;
            model.UnitDivision = viewmodel.UnitDivision;
            model.ReturnType = viewmodel.ReturnType;
            model.MaterialType = viewmodel.MaterialType;
            model.DcNo = viewmodel.DcNo;
            model.DcDate = viewmodel.DcDate;
            model.InvoiceNo = viewmodel.InvoiceNo;
            model.InvoiceDate = viewmodel.InvoiceDate;
            model.ModeofTransport = viewmodel.ModeofTransport;
            model.InvoiceCurrency = viewmodel.InvoiceCurrency;
            model.InvoiceValue = viewmodel.InvoiceValue;
            model.VehicleNo = viewmodel.VehicleNo;
            model.PoNoId = viewmodel.PoNoId;
            model.SupplierId = viewmodel.SupplierId;
            model.StoresId = viewmodel.StoresId;
            model.MaterialNameId = viewmodel.MaterialNameId;
            model.HSNCode = viewmodel.HSNCode;
            model.SizeRangeId = viewmodel.SizeRangeId;
            model.UomId = viewmodel.UomId;
            model.Pcs = viewmodel.Pcs;
            model.ReceivedBy = viewmodel.ReceivedBy;
            model.AcknowledgedBy = viewmodel.AcknowledgedBy;
            model.Remarks = viewmodel.Remarks;
            model.Quantity = viewmodel.Quantity;
            model.TotalQty = viewmodel.TotalQty;
            model.Rate = viewmodel.Rate;
            model.Value = viewmodel.Value;
            model.DcTotal = viewmodel.DcTotal;
            model.ArrivedTotal = viewmodel.ArrivedTotal;
            model.InwardPo = viewmodel.InwardPo;
            model.PoTotal = viewmodel.PoTotal;
            model.PendingQuantity = viewmodel.PendingQuantity;

            model.jobsheet_pair_Code_id = viewmodel.jobsheet_pair_Code_id;
            model.Process_Name = viewmodel.Process_Name;
            model.Manual_DcNo = viewmodel.Manual_DcNo;
            var MaterialInwardId = inwardMaterialManager.PostUpload(model, listOfAttachment);
            var MaterialId = viewmodel.MaterialNameId;
            if (viewmodel.MaterialNameId != 0 && viewmodel.GateEntryInwardId == 0)
            {
                if (viewmodel.Size != null && viewmodel.Size != "")
                {
                    string[] sizeAray = viewmodel.Size.Split(',');
                    string[] PoAray = viewmodel.PoQty.Split(',');
                    string[] DcAray = viewmodel.DcQty.Split(',');
                    string[] ArrAray = viewmodel.ArrivedQty.Split(',');
                    string[] PendAray = viewmodel.PendingQty.Split(',');
                    int count = 0;
                    foreach (var item in sizeAray)
                    {
                        InwardMaterialSizeQtyRateMaster SizeQtyRateDetails = new InwardMaterialSizeQtyRateMaster();
                        SizeQtyRateDetails.Size = item;
                        SizeQtyRateDetails.PoQty = PoAray[count];
                        SizeQtyRateDetails.DcQty = DcAray[count];
                        SizeQtyRateDetails.ArrivedQty = ArrAray[count];
                        SizeQtyRateDetails.PendingQty = PendAray[count];
                        SizeQtyRateDetails.MaterialId = MaterialId;
                        SizeQtyRateDetails.GateEntryInwardId = MaterialInwardId;
                        SizeQtyRateDetails.CreatedDate = DateTime.Now;
                        sizeRatemanager.PostMaterialSizeQtyRate(SizeQtyRateDetails);
                        count++;
                    }
                    int count1 = 0;
                    if (model.PendingQuantity != null && model.PendingQuantity != "" && model.PendingQuantity != "0")
                    {
                        foreach (var item in sizeAray)
                        {
                            InwardMaterialPendingQuantityMaster pendingQtyDetails = new InwardMaterialPendingQuantityMaster();
                            pendingQtyDetails.Size = item;
                            pendingQtyDetails.DcQty = DcAray[count1];
                            pendingQtyDetails.ArrivedQty = ArrAray[count1];
                            pendingQtyDetails.PendingQty = PendAray[count1];
                            pendingQtyDetails.PoQuantity = PoAray[count1];
                            pendingQtyDetails.MaterialId = MaterialId;
                            pendingQtyDetails.PoOrderID = viewmodel.PoNoId;
                            pendingQtyDetails.GateEntryInwardId = MaterialInwardId;
                            pendingQtyDetails.CreatedDate = DateTime.Now;
                            pendingQtyManager.PostMaterialSizeQtyRate(pendingQtyDetails);
                            count1++;
                        }
                    }
                }
            }

            string GateEntryNo = viewmodel.GateEntryNo;
            Outwardmodel = inwardMaterialManager.GEMaterialListGridBasedOnPoNo(GateEntryNo);
            viewmodel.GateEntryInwardMaterialList = Outwardmodel;
            List<GEInwardMaterialGrid> GridList = new List<GEInwardMaterialGrid>();
            foreach (var item in viewmodel.GateEntryInwardMaterialList)
            {
                GEInwardMaterialGrid geInMaterialGrid = new GEInwardMaterialGrid();
                /*SUB GRID VALUES*/
                geInMaterialGrid.GateEntryInwardId = item.GateEntryInwardId;
                geInMaterialGrid.TotalQty = item.TotalQty;
                geInMaterialGrid.MaterialNameId = item.MaterialNameId;
                geInMaterialGrid.HSNCode = item.HSNCode;
                geInMaterialGrid.SizeRangeId = item.SizeRangeId;
                geInMaterialGrid.PendingQuantity = item.PendingQuantity;
                geInMaterialGrid.Piecies = item.Piecies;
                geInMaterialGrid.SupplierId = item.SupplierId;
                geInMaterialGrid.StoresId = item.StoresId;
                geInMaterialGrid.UomId = item.UomId;
                geInMaterialGrid.UnitName = uOMManager.GetUomMasterId(Convert.ToInt32(geInMaterialGrid.UomId)).ShortUnitName;
                if (geInMaterialGrid.SizeRangeId != null && geInMaterialGrid.SizeRangeId != 0)
                {
                    geInMaterialGrid.SizeRangeDetails = srdManager.GetSizeRangeDetailsId(geInMaterialGrid.SizeRangeId).SizeRangeName;
                }
                geInMaterialGrid.StoreName = stManager.GetStoreMasterId(geInMaterialGrid.StoresId.Value).StoreName;
                var MaterialNameList = materialManager.GetMaterialMasterId(geInMaterialGrid.MaterialNameId).MaterialName;
                geInMaterialGrid.MaterialName = materialNameManager.GetMaterialNameMaterial(MaterialNameList.Value).MaterialDescription;
                geInMaterialGrid.InwardEntryDateTime = item.InwardEntryDateTime;
                GridList.Add(geInMaterialGrid);
            }
            viewmodel.GEMaterialsGrid = GridList;

            return Json(new { Viewmodel = viewmodel }, JsonRequestBehavior.AllowGet);
        }
        public string GetInwardEntryID()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateGEInwardID();
            ID++;
            return ID.ToString();
        }

        public ActionResult Delete(int GateEntryInwardId)
        {
            GateEntryInwardMaterialsMaster model = new GateEntryInwardMaterialsMaster();
            GateEntryInwardMaterialsManager inwardMaterialManager = new GateEntryInwardMaterialsManager();
            string status = "";

            model = inwardMaterialManager.GetGateEntryInMaterialById(GateEntryInwardId);
            if (model != null)
            {
                status = "Success";
                inwardMaterialManager.Delete(GateEntryInwardId);
            }
            return Json(new { status = status, GateEntryNo = model.GateEntryNo }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string filter)
        {

            GateEntryInwardMaterialsManager inwardMaterialManager = new GateEntryInwardMaterialsManager();
            List<GateEntryInwardMaterialsMaster> model = new List<GateEntryInwardMaterialsMaster>();
            GateEntryInwardMaterials vm = new GateEntryInwardMaterials();
            model = inwardMaterialManager.Get();
            var groupList = model.GroupBy(x => x.GateEntryNo.ToString().Trim()).Select(g => g.First()).ToList();
            //var group = groupList.Where(x => x.GateEntryNo.Contains(filter.Trim())).ToList();
            model = groupList.Where(x => x.GateEntryNo.Contains(filter.Trim())).ToList();
           // model = groupList.Where(x => x.GateEntryNo.Contains(filter.Trim()) || x.Company.Contains(filter.Trim())).ToList();
            vm.GateEntryInwardMaterialList = model;

            return PartialView("~/Views/GateEntry/GateEntryInwardMaterials/Partial/GateEntryInwardMaterialsGrid.cshtml", vm);
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
        public ActionResult GetSupplierBasedMaterial(string SupplierId)
        {
            List<GateEntryInward> appList = new List<GateEntryInward>();
            if (SupplierId != null)
            {
                appList = GetMaterial(SupplierId);
                return Json(appList, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public static List<GateEntryInward> GetMaterial(string SupplierId)
        {
            List<GateEntryInward> Materiallist = new List<GateEntryInward>();

            using (var context = new MMSContext())
            {
                Materiallist = context.Database.SqlQuery<GateEntryInward>("EXEC GetSupplierBasedMaterial @SupplierId={0}", SupplierId).ToList();
            }
            return Materiallist;
        }

        [HttpGet]
        public ActionResult GetMaterialByPoNo(string PoNoId)
        {
            if (PoNoId != null && PoNoId != "")
            {
                PurchaseOrderManager manager = new PurchaseOrderManager();
                MaterialManager smanager = new MaterialManager();
                MaterialNameManager mnmanager = new MaterialNameManager();
                ColorManager colorManager = new ColorManager();
                var MaterialList = (from x in manager.Get()
                                    join y in smanager.Get() on x.Material equals y.MaterialMasterId
                                    join z in mnmanager.Get() on y.MaterialName equals z.MaterialMasterID
                                    join z1 in colorManager.Get() on y.ColorMasterId equals z1.ColorMasterId
                                    where x.PoNo == PoNoId
                                    select new
                                    {
                                        MaterialMasterId = x.Material,
                                        MaterialDescription = z.MaterialDescription + " " + z1.Color,
                                    });
                return Json(MaterialList, JsonRequestBehavior.AllowGet);
            }
            else
            {

            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult GetDetailsByMaterialName(string MaterialNameId, string SupplierId, string PoNo)
        {
            List<GateEntryInwardMaterialData> InwardData = new List<GateEntryInwardMaterialData>();
            List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
            List<PurchaseOrderSizeRangeQuantity> listQty = new List<PurchaseOrderSizeRangeQuantity>();
            List<InwardMaterialPendingQuantityMaster> listPendingQty = new List<InwardMaterialPendingQuantityMaster>();
            InwardMaterialPendingQuantityManager pendingQtyManager = new InwardMaterialPendingQuantityManager();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            GateEntryInwardMaterialsManager gateEntryInwardMaterialsManager = new GateEntryInwardMaterialsManager();
            GateEntryInwardMaterialsMaster gateEntryInwardMaterialsMaster = new GateEntryInwardMaterialsMaster();
            string Message = "";
            GateEntryInwardMaterialsManager inwardMaterialManager = new GateEntryInwardMaterialsManager();
            decimal? Pending = 0;
            string ErrorMessage = string.Empty;
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            purchaseOrder = purchaseOrderManager.GetPoExist(PoNo, Convert.ToInt32(MaterialNameId));
            GateEntryInwardMaterialsMaster gateEntryInwardMaterialsMasterList = new GateEntryInwardMaterialsMaster();
            if (purchaseOrder != null && purchaseOrder.PoOrderId != 0)
            {
                gateEntryInwardMaterialsMasterList = inwardMaterialManager.GetGateEntryPendinglist(Convert.ToInt32(MaterialNameId), purchaseOrder.PoOrderId);
            }

            if (gateEntryInwardMaterialsMasterList != null)
            {
                Pending = Convert.ToDecimal(gateEntryInwardMaterialsMasterList.PendingQuantity);
            }

            if (gateEntryInwardMaterialsMasterList != null && gateEntryInwardMaterialsMasterList.PendingQuantity == "0")
            {
                Message = "Already Exists";
                return Json(Message, JsonRequestBehavior.AllowGet);
            }
            if (MaterialNameId != null)
            {
                InwardData = GetMaterialData(MaterialNameId, SupplierId, PoNo);
                if (InwardData.Count > 0)
                {
                    listSizeItemMaterial = GetSizeRangeData(MaterialNameId);
                    int materialID = Convert.ToInt32(MaterialNameId);
                    if (gateEntryInwardMaterialsMasterList != null && Convert.ToDecimal(gateEntryInwardMaterialsMasterList.PendingQuantity) != 0)
                    {
                        listPendingQty = pendingQtyManager.GetPendingMaterial(materialID);
                        if (listPendingQty.Sum(x => Convert.ToDecimal(x.PendingQty)) > 0)
                        {
                            listPendingQty = GetPendingQuantityData(MaterialNameId);
                        }
                    }

                    if (!string.IsNullOrEmpty(PoNo))
                    {
                        purchaseOrder = null;
                        purchaseOrder = purchaseOrderManager.GetPoExist(PoNo, Convert.ToInt32(MaterialNameId));
                        if (purchaseOrder != null && purchaseOrder.PoOrderId != 0)
                        {
                            listQty = GetQuantiySizeData(purchaseOrder.PoOrderId.ToString());
                        }
                        if (listQty == null || listQty.Count == 0)
                        {
                            gateEntryInwardMaterialsMaster = gateEntryInwardMaterialsManager.GetGateEntryPendingMaterial(Convert.ToInt32(MaterialNameId), purchaseOrder.PoOrderId);
                            if (gateEntryInwardMaterialsMaster != null)
                            {
                                purchaseOrder.PoQty = Convert.ToDecimal(gateEntryInwardMaterialsMaster.PendingQuantity);
                            }
                        }

                    }
                    else
                    {
                        listSizeItemMaterial = GetSizeRangeData(MaterialNameId);
                    }




                    return Json(new { Material = InwardData, SizeRange = listSizeItemMaterial, PoQuantity = listQty, PendingQuantity = listPendingQty, Message = Message, purchaseOrder = purchaseOrder, withPoTotal = listQty.Sum(x => x.Quantity) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Message = "There is no data";
                    return Json(Message, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public static List<GateEntryInwardMaterialData> GetMaterialData(string MaterialNameId, string SupplierId, string PoNo)
        {
            List<GateEntryInwardMaterialData> Materiallist = new List<GateEntryInwardMaterialData>();

            using (var context = new MMSContext())
            {
                Materiallist = context.Database.SqlQuery<GateEntryInwardMaterialData>("EXEC spGateEntryInwardMaterialData @MaterialNameId={0},@SupplierId={1},@PoNo={2}", MaterialNameId, SupplierId, PoNo).ToList();
            }
            return Materiallist;
        }
        public static List<SizeItemMaterial> GetSizeRangeData(string MaterialNameId)
        {
            List<SizeItemMaterial> Materiallist = new List<SizeItemMaterial>();

            using (var context = new MMSContext())
            {
                Materiallist = context.Database.SqlQuery<SizeItemMaterial>("EXEC spGateEntryInwardSizeData @MaterialNameId={0}", MaterialNameId).ToList();
            }
            return Materiallist;
        }
        public static List<PurchaseOrderSizeRangeQuantity> GetQuantiySizeData(string PoNo)
        {
            List<PurchaseOrderSizeRangeQuantity> Materiallist = new List<PurchaseOrderSizeRangeQuantity>();

            using (var context = new MMSContext())
            {
                Materiallist = context.Database.SqlQuery<PurchaseOrderSizeRangeQuantity>("EXEC spGateEntryInwardQtyData @PoNo={0}", PoNo).ToList();
            }
            return Materiallist;
        }
        public static List<InwardMaterialPendingQuantityMaster> GetPendingQuantityData(string MaterialNameId)
        {
            List<InwardMaterialPendingQuantityMaster> Materiallist = new List<InwardMaterialPendingQuantityMaster>();

            using (var context = new MMSContext())
            {
                Materiallist = context.Database.SqlQuery<InwardMaterialPendingQuantityMaster>("EXEC spGateEntryInwardPendingQtyData @MaterialNameId={0}", MaterialNameId).ToList();
            }
            return Materiallist;
        }

        private readonly Random _random = new Random();

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

    }
}
