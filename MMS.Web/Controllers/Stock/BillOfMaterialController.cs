using Excel;
using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Stock
{
    [CustomFilter]
    public class BillOfMaterialController : Controller
    {
        //
        // GET: /BillOfMaterial/

        #region Helper Method
        public ActionResult BOMMaterialListGrid()
        {
                BillOfMaterialModel model = new BillOfMaterialModel();           
                BillOfMaterialManager BillOfMaterialManager = new BillOfMaterialManager();
                List<BillOfMaterialGrid> bomGrid = new List<BillOfMaterialGrid>();
                List<BillOfMaterial> bOMMaterialList = new List<BillOfMaterial>();
                bOMMaterialList = BillOfMaterialManager.Get().Where(x => x.IsDeleted == false).ToList();
                foreach (var items in bOMMaterialList)
                {
                    BillOfMaterialGrid grid = new BillOfMaterialGrid();
                    grid.BomId = items.BomId;
                    grid.BomNo = items.BomNo;
                    grid.BuyerMasterId = items.BuyerMasterId;
                    grid.ParentBomNo = items.ParentBomNo;
                    grid.MaterialMasterId = items.MaterialMasterId != null ? items.MaterialMasterId.Value : 0;
                    grid.Date = items.Date;
                    grid.MaterialCategoryMasterId = items.MaterialCategoryMasterId != null ? items.MaterialCategoryMasterId.Value : 0;
                    bomGrid.Add(grid);
                }
               
                model.BOMMaterialLists = bomGrid;
           
            return View("~/Views/Stock/BillOfMaterial/BOMMaterialListGrid.cshtml", model);

        }
        public ActionResult GetBOMMaterilsList(int BOMMaterialID, int BOMID, int BuyerMasterId)
        {

            List<BomGrid> listBOMGrid = new List<BomGrid>();
            BOMMaterial bOMMaterialList = new BOMMaterial();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
            List<DisplaySizeMaterial> listDisplaySizeMaterial = new List<DisplaySizeMaterial>();
            listDisplaySizeMaterial = bomMaterialListManager.DisplaySizeMaterialGet(BOMMaterialID);
            listDisplaySizeMaterial = listDisplaySizeMaterial.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();
            BillOfMaterial billOfMaterial = new BillOfMaterial();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            MaterialGroupMaster_ materialGroupMaster = new MaterialGroupMaster_();
            NormsManager normsManager = new NormsManager();
            Norms norms = new Norms();
            BuyerManager buyerManager = new BuyerManager();
            BuyerMaster buyermaster = new BuyerMaster();
            MaterialCategoryManager mateialCategoryManager = new MaterialCategoryManager();
            MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
            bOMMaterialList = billOfMaterialManager.getBomMaterialID(BOMMaterialID);
            listBOMGrid = billOfMaterialManager.getBomIDHistoryList(BOMID, BOMMaterialID);
            billOfMaterial = billOfMaterialManager.GetbomId(BOMID);
            buyermaster = buyerManager.GetBuyerMasterId(BuyerMasterId);
            materialCategoryMaster = mateialCategoryManager.GetMaterialCategoryMaster(bOMMaterialList.MaterialCategoryMasterId);
            materialGroupMaster = materialGroupManager.GetMaterialGroupMaster_Id(bOMMaterialList.MaterialGroupMasterId);
            norms = normsManager.GetGroupIDWithBuyername(bOMMaterialList.MaterialGroupMasterId, BuyerMasterId);
            return Json(new { BOM = billOfMaterial, Grid = listBOMGrid, Material = bOMMaterialList, MaterialGroup = materialGroupMaster, Norms = norms, DisplaySize = listDisplaySizeMaterial, buyermaster = buyermaster, materialCategoryMaster = materialCategoryMaster }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchBOMMaterial(string Materialname, string BomNo, int? BOMMaterialID)
        {

            List<BomGrid> listBOMGrid = new List<BomGrid>();
            BOMMaterial bOMMaterialList = new BOMMaterial();
            List<MMS.Web.Models.SPBomMaterialList> bomSearchList = new List<Models.SPBomMaterialList>();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();          
            BillOfMaterial billOfMaterial = new BillOfMaterial();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            MaterialGroupMaster_ materialGroupMaster = new MaterialGroupMaster_();
            NormsManager normsManager = new NormsManager();
            Norms norms = new Norms();
            BuyerManager buyerManager = new BuyerManager();
            BuyerMaster buyermaster = new BuyerMaster();
            MaterialCategoryManager mateialCategoryManager = new MaterialCategoryManager();
            MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
            bomSearchList = billOfMaterialManager.BOMMaterialSearch(Materialname,BomNo);      
            
            materialCategoryMaster = mateialCategoryManager.GetMaterialCategoryMaster(bOMMaterialList.MaterialCategoryMasterId);
            materialGroupMaster = materialGroupManager.GetMaterialGroupMaster_Id(bOMMaterialList.MaterialGroupMasterId);
 
            return Json(new { BOM = billOfMaterial, Grid = listBOMGrid, Material = bomSearchList, MaterialGroup = materialGroupMaster, Norms = norms, buyermaster = buyermaster, materialCategoryMaster = materialCategoryMaster }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditBillOfMaterialDetails(int BOMID)
        {
            BillOfMaterialModel model = new BillOfMaterialModel();


            BillOfMaterialManager BillOfMaterialManager = new BillOfMaterialManager();
            BOMMaterialListManager materialListManager = new BOMMaterialListManager();
            List<BOMMaterialList> materialList_ = new List<BOMMaterialList>();
            List<BOMMaterial> listBomMaterialList = new List<BOMMaterial>();
            List<BOMAmendmentMaterial> listBomMaterialList_ = new List<BOMAmendmentMaterial>();
            List<BOMMaterilGrid> materilGridList = new List<BOMMaterilGrid>();
            materialList_ = materialListManager.Get();

            BillOfMaterial arg = new BillOfMaterial();
            if (BOMID == 0)
            {
                model.BillOfMaterialList = null;
            }
            if (BOMID != 0)
            {
                arg = BillOfMaterialManager.GetbomId(BOMID);
                model.BomId = arg.BomId;
                model.BomNo = arg.BomNo;
                model.Description = arg.Description;
                model.BuyerMasterId = arg.BuyerMasterId;
                model.BuyerModel = arg.BuyerModel;
                model.MeanSize = arg.MeanSize;
                model.Date = arg.Date.ToString();
                model.ParentBomNo = arg.ParentBomNo;
                model.LastBomNoEntered = arg.LastBomNoEntered;
                model.LinkBomNo = arg.LinkBomNo;
                model.Ammendment = arg.Ammendment;
                model.CommonBomNo = arg.CommonBomNo;
                model.PreparedBy = arg.PreparedBy;
                model.VerifiedBy = arg.VerifiedBy;
                model.CheckedBy = arg.CheckedBy;
                model.MaterialName = arg.MaterialMasterId.Value;
                model.CommnBOM1 = arg.CommnBOM1;
                model.CommnBOM2 = arg.CommnBOM2;
                model.CommnBOM3 = arg.CommnBOM3;
                model.CommnBOM4 = arg.CommnBOM4;
                model.CommnBOM5 = arg.CommnBOM5;
                model.MaterialCategoryMasterId = arg.MaterialCategoryMasterId.Value;
                model.MaterialGroupMasterId = arg.MaterialGroupMasterId.Value;
                model.ColorMasterId = arg.ColorMasterId.Value;
                model.SubstanceMasterId = arg.SubstanceMasterId.Value;
                model.SampleNorms = arg.SampleNorms;
                model.Wastage = arg.Wastage;
                model.ComponentName = arg.ComponentName.Value;
                model.SupplierMasterId = arg.SupplierMasterId.Value;
                model.UomMasterId = arg.UomMasterId.Value;
                model.SizeRangeMasterId = arg.SizeRangeMasterId.Value;
                model.SizeScheduleMasterId = arg.SizeScheduleMasterId;
                model.WastageQty = arg.WastageQty;
                model.WastageQtyUOM = arg.WastageQtyUOM!=null? arg.WastageQtyUOM.Value:0;
                model.TotalNorms = arg.TotalNorms;
                model.TotalNormsUOM = arg.TotalNormsUOM!=null? arg.TotalNormsUOM.Value:0;
                model.NoOfSets = arg.NoOfSets!=null? arg.NoOfSets.Value:0;
                model.BuyerNorms = arg.BuyerNorms;
                model.OurNorms = arg.OurNorms;
                model.OurNormsPercent = arg.OurNormsPercent;
                model.PurchaseNorms =arg.PurchaseNorms!=null? arg.PurchaseNorms.Value:0;
                model.PurchaseNormsPercent = arg.PurchaseNormsPercent;
                model.IssueNorms = arg.IssueNorms!=null?arg.IssueNorms.Value:0;
                model.IssueNormsPercent = arg.IssueNormsPercent;
                model.CostingNorms = arg.CostingNorms!=null? arg.CostingNorms.Value:0;
                model.CostingNormsPercent = arg.CostingNormsPercent;
                model.CreatedDate = arg.CreatedDate;
                model.UpdatedDate = arg.UpdatedDate;
                model.CreatedBy = arg.CreatedBy;
                model.UpdatedBy = arg.UpdatedBy;
                model.Edit = true;
                listBomMaterialList_ = BillOfMaterialManager.GetBomAmendmentMaterialBOMID(BOMID);
                listBomMaterialList = listBomMaterialList.Where(x => x.IsDeleted == false).ToList();
                foreach (var item in listBomMaterialList)
                {
                    BOMMaterilGrid model_ = new BOMMaterilGrid();
                    model_.BOMMaterialID = item.BOMMaterialID;
                    model_.BomId = item.BOMID;
                    model_.MaterialCategoryMasterId = item.MaterialCategoryMasterId;
                    model_.MaterialGroupMasterId = item.MaterialGroupMasterId;
                    model_.SubstanceMasterId = item.SubstanceMasterId;
                    model_.MaterialMasterId = item.MaterialName;
                    model_.ColorMasterId = item.ColorMasterId;
                    model_.SampleNorms = item.SampleNorms;
                    model_.Wastage = item.Wastage;
                    model_.WastageQty = item.WastageQty;
                    model_.BomNo = model.BomNo;
                    model_.Description = model.Description;
                    model_.BuyerMasterId = model.BuyerMasterId;
                    model_.BuyerModel = model.BuyerModel;
                    model_.MeanSize = model.MeanSize;
                    model_.Date = model.Date;
                    model_.ParentBomNo = model.ParentBomNo;
                    model_.LastBomNoEntered = model.LastBomNoEntered;
                    model_.LinkBomNo = model.LinkBomNo;
                    model_.Ammendment = model.Ammendment;
                    model_.CommonBomNo = model.CommonBomNo;
                    model_.PreparedBy = model.PreparedBy;
                    model_.VerifiedBy = model.VerifiedBy;
                    model_.CheckedBy = model.CheckedBy;
                    model_.GroupID = model.GroupID;
                    model_.SupplierMasterId = model.SupplierMasterId;
                    model_.UomMasterId = model.UomMasterId;
                    model_.SizeRangeMasterId = model.SizeRangeMasterId;
                    model_.NoOfSets = model.NoOfSets;
                    model_.BuyerNorms = model.BuyerNorms;
                    model_.ComponentName = model.ComponentName;
                    model_.OurNorms = model.OurNorms;
                    model_.OurNormsPercent = model.OurNormsPercent;
                    model_.PurchaseNorms = model.PurchaseNorms;
                    model_.PurchaseNormsPercent = model.PurchaseNormsPercent;
                    model_.IssueNorms = model.IssueNorms;
                    model_.IssueNormsPercent = model.IssueNormsPercent;
                    model_.CostingNorms = model.CostingNorms;
                    model_.CostingNormsPercent = model.CostingNormsPercent;
                    model_.CommnBOM1 = model.CommnBOM1;
                    model_.CommnBOM2 = model.CommnBOM2;
                    model_.CommnBOM3 = model.CommnBOM3;
                    model_.CommnBOM4 = model.CommnBOM4;
                    model_.CommnBOM5 = model.CommnBOM5;
                    model_.NoOfSet = model.NoOfSet;
                    model_.MaterialSuppliedBY = model.MaterialSuppliedBY;
                    model_.SubtanceMasterID = model.SubtanceMasterID;
                    materilGridList.Add(model_);
                }
                model.bomMaterialGridList = materilGridList;
                model.amendmentmaterialList = listBomMaterialList_;            
              
            }
            return View("~/Views/Stock/BillOfMaterial/BillOfMaterialDetails.cshtml", model);
        }
        public ActionResult BillOfMaterialDetails()

        {
            BillOfMaterialModel model = new BillOfMaterialModel();
            List<BOMMaterial> listBomMaterialList = new List<BOMMaterial>();
            List<BOMAmendmentMaterial> listBOMAmendmentMaterialList = new List<BOMAmendmentMaterial>();
            List<BOMMaterilGrid> materilGridList = new List<BOMMaterilGrid>();
            int BOMID = Convert.ToInt32(Request.QueryString["BOMID"]);
            BillOfMaterialManager BillOfMaterialManager = new BillOfMaterialManager();
            BillOfMaterial arg = new BillOfMaterial();
            BOMMaterialListManager materialListManager = new BOMMaterialListManager();
            List<BOMMaterialList> materialList_ = new List<BOMMaterialList>();
            List<BomGridModel> bomGridModel = new List<BomGridModel>();
            List<BomGrid> bomGridList = new List<BomGrid>();
            Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
            List<Product_BuyerStyleMaster> product_BuyerStyleMasterList = new List<Product_BuyerStyleMaster>();
            bomGridList = BillOfMaterialManager.GetBomDetails(BOMID);
            materialList_ = materialListManager.Get();
            List<BomHistory> bomHistory = new List<BomHistory>();
            bomHistory = BillOfMaterialManager.getBomHistoryList();
            model.bomHistoryList = bomHistory;
            string LastBOmNO = string.Empty;
            product_BuyerStyleMasterList = product_BuyerStyleManager.Get();
            LastBOmNO = BillOfMaterialManager.GetLastbomNumber();
            model.bomStyleList = (from item in product_BuyerStyleMasterList
                                  select new SelectListItem
                                  {
                                      Text = item.OurStyle,
                                      Value = item.ProductOrBuyerStyleId.ToString()
                                  }).ToList();
            if (LastBOmNO == null || LastBOmNO == "")
            {
                LastBOmNO = "NotApplicable";
            }
            if (BOMID == 0)
            {
                model.BillOfMaterialList = null;
                model.materialList = null;
                model.bomGridList = null;
                model.LastBomNoEntered = LastBOmNO;
            }
            if (BOMID != 0)
            {
                arg = BillOfMaterialManager.GetbomId(BOMID);
                model.BomId = arg.BomId;
                model.BomNo = arg.BomNo;
                model.bomGridList = bomGridList;
                model.Description = arg.Description;
                model.BuyerMasterId = arg.BuyerMasterId;
                model.BuyerModel = arg.BuyerModel;
                model.MeanSize = arg.MeanSize;
                model.ParentBomNo = arg.ParentBomNo;
                model.Date = arg.Date.ToString();
                model.CommnBOM1 = arg.CommnBOM1;
                model.CommnBOM2 = arg.CommnBOM2;
                model.CommnBOM3 = arg.CommnBOM3;
                model.CommnBOM4 = arg.CommnBOM4;
                model.CommnBOM5 = arg.CommnBOM5;
                model.ParentBomNo = arg.ParentBomNo;
                model.LastBomNoEntered = arg.LastBomNoEntered;
                model.LinkBomNo = arg.LinkBomNo;
                model.Ammendment = arg.Ammendment;
                model.CommonBomNo = arg.CommonBomNo;
                model.PreparedBy = arg.PreparedBy;
                model.VerifiedBy = arg.VerifiedBy;
                model.CheckedBy = arg.CheckedBy;
                model.Date = Convert.ToDateTime(arg.Date).Date.ToString("dd/MM/yyyy");
                model.NoOfSets = arg.NoOfSets.Value;
                model.BuyerNorms = arg.BuyerNorms;
                model.OurNorms = arg.OurNorms;
                model.OurNormsPercent = arg.OurNormsPercent;
                model.PurchaseNorms = arg.PurchaseNorms != null ? arg.PurchaseNorms.Value : 0;
                model.PurchaseNormsPercent = arg.PurchaseNormsPercent;
                model.IssueNorms = arg.IssueNorms != null ? arg.IssueNorms.Value : 0;
                model.IssueNormsPercent = arg.IssueNormsPercent;
                model.CostingNorms = arg.CostingNorms != null ? arg.CostingNorms.Value : 0;
                model.CostingNormsPercent = arg.CostingNormsPercent;
                model.CreatedDate = arg.CreatedDate;
                model.UpdatedDate = arg.UpdatedDate;
                model.CreatedBy = arg.CreatedBy;
                model.UpdatedBy = arg.UpdatedBy;
                model.Edit = true;
                materialList_ = materialList_.Where(x => x.BomID == arg.BomId).ToList();
                model.materialList = materialList_;
                listBOMAmendmentMaterialList = BillOfMaterialManager.GetBomAmendmentMaterialBOMID(BOMID);
                listBomMaterialList = BillOfMaterialManager.GetBomMaterialBOMID(BOMID);
                foreach (var item in listBomMaterialList)
                {
                    BOMMaterilGrid model_ = new BOMMaterilGrid();
                    UOMManager uomManager = new UOMManager();
                    UomMaster uomMaster = new UomMaster();
                    uomMaster = uomManager.GetUomMasterId(item.WastageQtyUOM);
                    if (uomMaster != null)
                    {
                        model_.WastageqtyUOM = uomMaster.LongUnitName;
                    }
                    if(item.MaterialName== 4751)
                    {
                        string message= "";
                    }
                    model_.BOMMaterialID = item.BOMMaterialID;
                    model_.BomId = item.BOMID;
                    model_.MaterialCategoryMasterId = item.MaterialCategoryMasterId;
                    model_.MaterialGroupMasterId = item.MaterialGroupMasterId;
                    model_.SubstanceMasterId = item.SubstanceMasterId;
                    model_.MaterialMasterId = item.MaterialName;
                    model_.ColorMasterId = item.ColorMasterId;
                    model_.SampleNorms = item.SampleNorms;
                    model_.Wastage = item.Wastage;
                    model_.TotalNorms = item.TotalNorms;
                    model_.Conversion = item.Conversion;
                    model_.WastageQty = item.WastageQty;
                    model_.BomNo = model.BomNo;
                    model_.Description = model.Description;
                    model_.BuyerMasterId = model.BuyerMasterId;
                    model_.BuyerModel = model.BuyerModel;
                    model_.MeanSize = model.MeanSize;
                    model_.Date = model.Date;
                    model_.ParentBomNo = model.ParentBomNo;
                    model_.LastBomNoEntered = model.LastBomNoEntered;
                    model_.LinkBomNo = model.LinkBomNo;
                    model_.Ammendment = model.Ammendment;
                    model_.CommonBomNo = model.CommonBomNo;
                    model_.PreparedBy = model.PreparedBy;
                    model_.VerifiedBy = model.VerifiedBy;
                    model_.CheckedBy = model.CheckedBy;
                    model_.GroupID = model.GroupID;
                    model_.SupplierMasterId = Convert.ToInt32( item.SupplierMasterID);
                    model_.UomMasterId = model.UomMasterId;
                    model_.SizeRangeMasterId = model.SizeRangeMasterId;
                    model_.NoOfSets = model.NoOfSets;
                    model_.BuyerNorms = model.BuyerNorms;
                    model_.ComponentName = model.ComponentName;
                    model_.OurNorms = model.OurNorms;
                    model_.OurNormsPercent = model.OurNormsPercent;
                    model_.PurchaseNorms = model.PurchaseNorms;
                    model_.PurchaseNormsPercent = model.PurchaseNormsPercent;
                    model_.IssueNorms = model.IssueNorms;
                    model_.IssueNormsPercent = model.IssueNormsPercent;
                    model_.CostingNorms = model.CostingNorms;
                    model_.CostingNormsPercent = model.CostingNormsPercent;
                    model_.CommnBOM1 = model.CommnBOM1;
                    model_.CommnBOM2 = model.CommnBOM2;
                    model_.CommnBOM3 = model.CommnBOM3;
                    model_.SNO = item.SNO;
                    model_.CommnBOM4 = model.CommnBOM4;
                    model_.CommnBOM5 = model.CommnBOM5;
                    model_.NoOfSet = model.NoOfSet;
                    model_.MaterialSuppliedBY = model.MaterialSuppliedBY;
                    model_.SubtanceMasterID = model.SubtanceMasterID;
                    materilGridList.Add(model_);
                }
                model.bomMaterialGridList = materilGridList.OrderBy(x=>Convert.ToInt32(x.SNO)).ToList();
 model.amendmentmaterialList = listBOMAmendmentMaterialList;
            }
            return View("~/Views/Stock/BillOfMaterial/BillOfMaterialDetails.cshtml", model);
        }
        public ActionResult ParentMaterialList(BillOfMaterialModel models)
        {

            BOMMaterial bommat = new BOMMaterial();
            BillOfMaterialModel model = new BillOfMaterialModel();           
            int BOMID = models.BomId;
            BillOfMaterialManager BillOfMaterialManager = new BillOfMaterialManager();
            BillOfMaterial arg = new BillOfMaterial();
            BillOfMaterial returnBillOfMaterial = new BillOfMaterial();
            BillOfMaterial billOfMaterial = new BillOfMaterial();
            BOMMaterialListManager materialListManager = new BOMMaterialListManager();
            List<BOMMaterialList> materialList_ = new List<BOMMaterialList>();
            materialList_ = materialListManager.Get();
            BillOfMaterial bilOfMaterialBomno = new BillOfMaterial();
            bilOfMaterialBomno = BillOfMaterialManager.GetBomNO(models.BomNo);
            if (bilOfMaterialBomno != null && bilOfMaterialBomno.BomId != 0)
            {
                List<BOMMaterial> listOfBomMaterial = new List<BOMMaterial>();
                int ParentBOMId = 0;
                if (!string.IsNullOrEmpty(bilOfMaterialBomno.CommnBOM1))
                {
                    ParentBOMId = Convert.ToInt32(bilOfMaterialBomno.CommnBOM1);
                    listOfBomMaterial = BillOfMaterialManager.GetCOmmonBomMaterialBOMID(ParentBOMId, bilOfMaterialBomno.BomId);
                }
                // BillOfMaterialManager.BomMaterialDelete_new( ParentBOMId, bilOfMaterialBomno.BomId);
                foreach (var item in listOfBomMaterial)
                {
                    BillOfMaterialManager.BomMaterialDelete_new(item.BOMMaterialID);
                }
            }
            if (BOMID == 0 && bilOfMaterialBomno.CommnBOM1 == null)
            {
                model.BillOfMaterialList = null;
                model.materialList = null;
            }


            if (BOMID != 0)
            {
                arg = BillOfMaterialManager.GetbomId(BOMID);
            }
            else
            {
                arg = BillOfMaterialManager.GetbomId(bilOfMaterialBomno.BomId);
            }
            model.BomId = arg.BomId;
            billOfMaterial.BomNo = models.BomNo;
            billOfMaterial.Description = models.Description;
            billOfMaterial.BuyerMasterId = models.BuyerMasterId;
            billOfMaterial.BuyerModel = models.BuyerModel;
            billOfMaterial.MeanSize = models.MeanSize;
            string[] formats = { "dd/MM/yyyy" };
            var dateTime = DateTime.ParseExact(models.Date, formats, new CultureInfo("en-US"), DateTimeStyles.None);
            billOfMaterial.Date = dateTime;
            billOfMaterial.ParentBomNo = model.ParentBomNo;
            billOfMaterial.NoOfSets = models.NoOfSets;
            billOfMaterial.LastBomNoEntered = models.LastBomNoEntered;
            billOfMaterial.LinkBomNo = models.LinkBomNo;
            billOfMaterial.Ammendment = models.Ammendment;
            billOfMaterial.CommnBOM1 = models.CommnBOM1;
            billOfMaterial.CommnBOM2 = models.CommnBOM2;
            billOfMaterial.CommnBOM3 = models.CommnBOM3;
            billOfMaterial.CommnBOM4 = models.CommnBOM4;
            billOfMaterial.CommnBOM5 = models.CommnBOM5;
            billOfMaterial.CommonBomNo = models.CommonBomNo;
            billOfMaterial.PreparedBy = models.PreparedBy;
            billOfMaterial.VerifiedBy = models.VerifiedBy;
            billOfMaterial.CheckedBy = models.CheckedBy;
            billOfMaterial.MaterialMasterId = models.MaterialName;
            billOfMaterial.MaterialCategoryMasterId = models.MaterialCategoryMasterId;
            billOfMaterial.MaterialGroupMasterId = models.MaterialGroupMasterId;
            billOfMaterial.ColorMasterId = models.ColorMasterId;
            billOfMaterial.SubstanceMasterId = models.SubstanceMasterId;
            billOfMaterial.SampleNorms = models.SampleNorms;
            billOfMaterial.Wastage = models.Wastage;
            billOfMaterial.ComponentName = models.ComponentName;
            billOfMaterial.SupplierMasterId = models.SupplierMasterId;
            billOfMaterial.SizeScheduleMasterId = models.SizeScheduleMasterId;
            billOfMaterial.UomMasterId = models.UomMasterId;
            billOfMaterial.SizeRangeMasterId = models.SizeRangeMasterId;
            billOfMaterial.WastageQty = models.WastageQty;
            billOfMaterial.WastageQtyUOM = models.WastageQtyUOM;
            billOfMaterial.TotalNorms = models.TotalNorms;
            billOfMaterial.TotalNormsUOM = models.TotalNormsUOM;
            billOfMaterial.NoOfSets = models.NoOfSets;
            billOfMaterial.BuyerNorms = models.BuyerNorms;         
            billOfMaterial.CreatedDate = DateTime.Now;
            billOfMaterial.UpdatedDate = null;
            billOfMaterial.BomId = 0;
            if (bilOfMaterialBomno != null && bilOfMaterialBomno.BomId != 0)
            {
                billOfMaterial.BomId = bilOfMaterialBomno.BomId;
            }
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            returnBillOfMaterial = billOfMaterialManager.Post(billOfMaterial);
            model.Edit = true;
            model.Status = true;
            materialList_ = materialList_.Where(x => x.BomID == arg.BomId).ToList();
            model.materialList = materialList_;
            List<BOMMaterial> listBomMaterialList = new List<BOMMaterial>();
            List<BOMMaterial> listBomMaterialList_ = new List<BOMMaterial>();
            List<BOMMaterilGrid> materilGridList = new List<BOMMaterilGrid>();

            bommat = BillOfMaterialManager.Chek_toInsertbom(returnBillOfMaterial.BomId, BOMID);
            if (bommat == null)
            {
                //insert data
                BillOfMaterialManager.Insert_materialdetail_common(returnBillOfMaterial.BomId, BOMID);
            }
            else
            {
                return Json(new { data = "Data is alredy saved" }, JsonRequestBehavior.AllowGet);

            }
            listBomMaterialList = BillOfMaterialManager.GetBomMaterialBOMID(BOMID);
            //Entry for Display size range     
            int count = !string.IsNullOrEmpty(models.SNO.ToString())? Convert.ToInt32(models.SNO):1;
            //foreach (var iteration in listBomMaterialList)
            //{
            //    BOMMaterial bomMaterials = new BOMMaterial();
            //    bomMaterials.BOMID = returnBillOfMaterial.BomId;
            //    bomMaterials.ParentCommonBOMID = BOMID;
            //    bomMaterials.ParentBOMID = iteration.ParentBOMID;
            //    bomMaterials.MaterialCategoryMasterId = iteration.MaterialCategoryMasterId;
            //    bomMaterials.MaterialGroupMasterId = iteration.MaterialGroupMasterId;
            //    bomMaterials.SubstanceMasterId = iteration.SubstanceMasterId;
            //    bomMaterials.MaterialName = iteration.MaterialName;
            //    bomMaterials.ColorMasterId = iteration.ColorMasterId;
            //    bomMaterials.SupplierMasterID = iteration.SupplierMasterID;
            //    bomMaterials.SampleNorms = (iteration.SampleNorms);
            //    bomMaterials.Wastage = iteration.Wastage;
            //    bomMaterials.Conversion = iteration.Conversion;
            //    bomMaterials.SNO = count.ToString(); ;
            //    bomMaterials.WastageQty = iteration.WastageQty;
            //    bomMaterials.WastageQtyUOM = iteration.WastageQtyUOM;
            //    bomMaterials.SizeRangeMasterID = iteration.SizeRangeMasterID;
            //    bomMaterials.NoofSets = iteration.NoofSets;
            //    bomMaterials.BuyerNorms = iteration.BuyerNorms;
            //    bomMaterials.TotalNorms = iteration.TotalNorms;
            //    bomMaterials.TotalNormsUOM = iteration.TotalNormsUOM;
            //    bomMaterials.SizeScheduleMasterId = iteration.SizeScheduleMasterId;
            //    if (arg != null && arg.BomId != 0 && arg.IssueNormsPercent != null)
            //    {
            //        bomMaterials.OurNorms = arg.OurNorms;
            //        bomMaterials.OurNormsPercent = arg.OurNormsPercent;
            //        bomMaterials.PurchaseNorms = arg.PurchaseNorms;
            //        bomMaterials.PurchaseNormsPercent = arg.PurchaseNormsPercent;
            //        bomMaterials.IssueNorms = arg.IssueNorms;
            //        bomMaterials.IssueNormsPercent = arg.IssueNormsPercent;
            //        bomMaterials.CostingNorms = arg.CostingNorms;
            //        bomMaterials.CostingNormsPercent = arg.CostingNormsPercent;
            //    }
            //    else
            //    {
            //        bomMaterials.OurNorms = iteration.OurNorms;
            //        bomMaterials.OurNormsPercent = iteration.OurNormsPercent;
            //        bomMaterials.PurchaseNorms = iteration.PurchaseNorms;
            //        bomMaterials.PurchaseNormsPercent = iteration.PurchaseNormsPercent;
            //        bomMaterials.IssueNorms = iteration.IssueNorms;
            //        bomMaterials.IssueNormsPercent = iteration.IssueNormsPercent;
            //        bomMaterials.CostingNorms = iteration.CostingNorms;
            //        bomMaterials.CostingNormsPercent = iteration.CostingNormsPercent;
            //    }

            //    bomMaterials.Rate = Convert.ToDecimal(iteration.Rate);
            //    BOMMaterial returnBOMMaterial = new BOMMaterial();
            //    returnBOMMaterial = billOfMaterialManager.BOMMaterialPost(bomMaterials);
            //    List<DisplaySizeMaterial> listDisplaySizeMaterial = new List<DisplaySizeMaterial>();
            //    BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
            //    listDisplaySizeMaterial = bomMaterialListManager.DisplaySizeMaterialGet(iteration.BOMMaterialID);
            //    //Entry for Display size range                   
            //    foreach (var item in listDisplaySizeMaterial)
            //    {
            //        DisplaySizeMaterial displaySizeMaterial = new DisplaySizeMaterial();
            //        displaySizeMaterial.SizeRange = item.SizeRange;
            //        displaySizeMaterial.SizeIsChecked = item.SizeIsChecked;
            //        displaySizeMaterial.BOMmaterialID = returnBOMMaterial.BOMMaterialID;
            //        displaySizeMaterial.CreatedDate = DateTime.Now;
            //        bomMaterialListManager.DisplaySizePost(displaySizeMaterial);
            //    }
            //    count++;
            //}
            listBomMaterialList_ = BillOfMaterialManager.GetBomMaterialBOMID(returnBillOfMaterial.BomId);

            ////Material add to list
            foreach (var item in listBomMaterialList_)
            {
                BOMMaterilGrid model_ = new BOMMaterilGrid();
                BuyerManager buyerManager = new BuyerManager();
                BuyerMaster buyerMaster = new BuyerMaster();
                MaterialGroupManager groupManager = new MaterialGroupManager();
                MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
                MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
                MaterialGroupMaster_ materialGroupMaster = new MaterialGroupMaster_();
                MaterialManager materialManager = new MaterialManager();
                MaterialMaster materialMaster = new MaterialMaster();
                ColorManager colorManager = new ColorManager();
                ColorMaster colorMaster = new ColorMaster();
                SubstanceMasterManager substanceMasterManager = new SubstanceMasterManager();
                SubstanceMaster substanceMaster = new SubstanceMaster();
                materialCategoryMaster = materialCategoryManager.GetMaterialCategoryMaster(item.MaterialCategoryMasterId);
                materialGroupMaster = groupManager.GetMaterialGroupMaster_Id(item.MaterialGroupMasterId);
                materialMaster = materialManager.GetMaterialMasterId(item.MaterialName);
                MaterialNameManager nameManager = new MaterialNameManager();
                MaterialNameMaster nameMaster = new MaterialNameMaster();
                if (materialMaster != null)
                {
                    nameMaster = nameManager.GetMaterialNameMasterId(materialMaster.MaterialName);
                }
                colorMaster = colorManager.GetColorMasterID(item.ColorMasterId);
                substanceMaster = substanceMasterManager.GetsubstanceMasterId(item.SubstanceMasterId);
                if (materialCategoryMaster != null)
                {
                    model_.CategoryName = materialCategoryMaster.CategoryName;
                }
                if (materialGroupMaster != null)
                {
                    model_.GroupName = materialGroupMaster.GroupName;
                }
                if (colorMaster != null)
                {
                    model_.ColorName = colorMaster.Color;
                }
                if (nameMaster != null)
                {
                    model_.MaterialName = nameMaster.MaterialDescription;
                }
                if (substanceMaster != null)
                {
                    model_.SubtranceRangeName = substanceMaster.SubstanceRange;
                }
                model_.BOMMaterialID = item.BOMMaterialID;
                model_.BomId = item.BOMID;
                model_.MaterialCategoryMasterId = item.MaterialCategoryMasterId;
                model_.MaterialGroupMasterId = item.MaterialGroupMasterId;
                model_.SubstanceMasterId = item.SubstanceMasterId;
                model_.MaterialMasterId = item.MaterialName;
                model_.ColorMasterId = item.ColorMasterId;
                model_.SampleNorms = item.SampleNorms;
                model_.Wastage = item.Wastage;
                model_.WastageQty = item.WastageQty;
                model_.BomNo = models.BomNo;
                model_.Description = models.Description;
                model_.BuyerMasterId = models.BuyerMasterId;
                model_.BuyerModel = models.BuyerModel;
                model_.MeanSize = models.MeanSize;
                model_.Date = models.Date;
                model_.ParentBomNo = models.ParentBomNo;
                model_.LastBomNoEntered = models.LastBomNoEntered;
                model_.LinkBomNo = models.LinkBomNo;
                model_.Ammendment = models.Ammendment;
                model_.CommonBomNo = models.CommonBomNo;
                UOMManager uomManager = new UOMManager();
                UomMaster uomMaster = new UomMaster();
                uomMaster = uomManager.GetUomMasterId(item.WastageQtyUOM);
                model_.WastageqtyUOM = uomMaster.LongUnitName;
                model_.PreparedBy = models.PreparedBy;
                model_.VerifiedBy = models.VerifiedBy;
                model_.CheckedBy = models.CheckedBy;
                model_.GroupID = models.GroupID;
                model_.SupplierMasterId = models.SupplierMasterId;
                model_.UomMasterId = models.UomMasterId;
                model_.SizeRangeMasterId = models.SizeRangeMasterId;
                model_.NoOfSets = models.NoOfSets;
                model_.BuyerNorms = models.BuyerNorms;
                model_.ComponentName = models.ComponentName;
                model_.OurNorms = item.OurNorms;
                model_.TotalNorms = item.TotalNorms;
                model_.TotalNormsUOM = item.TotalNormsUOM;
                model_.OurNormsPercent = item.OurNormsPercent;
                model_.PurchaseNorms = item.PurchaseNorms;
                model_.PurchaseNormsPercent = item.PurchaseNormsPercent;
                model_.IssueNorms = item.IssueNorms;
                model_.IssueNormsPercent = item.IssueNormsPercent;
                model_.CostingNorms = item.CostingNorms;
                model_.CostingNormsPercent = item.CostingNormsPercent;
                model_.CommnBOM1 = models.CommnBOM1;
                model_.CommnBOM2 = models.CommnBOM2;
                model_.CommnBOM3 = models.CommnBOM3;
                model_.CommnBOM4 = models.CommnBOM4;
                model_.CommnBOM5 = models.CommnBOM5;
                model_.WastageQtyUOM = item.WastageQtyUOM;
                model_.SNO = item.SNO;
                model_.NoOfSet = models.NoOfSet;
                model_.MaterialSuppliedBY = models.MaterialSuppliedBY;
                model_.SubtanceMasterID = models.SubtanceMasterID;
                materilGridList.Add(model_);
            }
            model.bomMaterialGridList = materilGridList;
            return Json(new { Model = models, Material = model.bomMaterialGridList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ParentBOMMaterialList(BillOfMaterialModel models)
        {

            BOMMaterial bommat = new BOMMaterial();
            BillOfMaterialModel model = new BillOfMaterialModel();
            BillOfMaterial BillOfMaterialModel_ = new BillOfMaterial();
            int BOMID = models.BomId;
            BillOfMaterialManager BillOfMaterialManager = new BillOfMaterialManager();
            BillOfMaterial arg = new BillOfMaterial();
            BillOfMaterial returnBillOfMaterial = new BillOfMaterial();
            BillOfMaterial billOfMaterial = new BillOfMaterial();
            BOMMaterialListManager materialListManager = new BOMMaterialListManager();
            List<BOMMaterialList> materialList_ = new List<BOMMaterialList>();
            materialList_ = materialListManager.Get();
            BillOfMaterial bilOfMaterialBomno = new BillOfMaterial();
            bilOfMaterialBomno = BillOfMaterialManager.GetBomNO(models.BomNo);
            if (bilOfMaterialBomno != null && bilOfMaterialBomno.BomId != 0)
            {
                List<BOMMaterial> listOfBomMaterial = new List<BOMMaterial>();
                int ParentBOMId = 0;
                if (!string.IsNullOrEmpty(bilOfMaterialBomno.ParentBomNo))
                {
                    ParentBOMId = Convert.ToInt32(bilOfMaterialBomno.ParentBomNo);
                    listOfBomMaterial = BillOfMaterialManager.GetCOmmonBomMaterialBOMID(ParentBOMId, bilOfMaterialBomno.BomId);
                }
                foreach (var item in listOfBomMaterial)
                {
                    BillOfMaterialManager.BomMaterialDelete_new(item.BOMMaterialID);
                }
            }
            if (BOMID == 0 && bilOfMaterialBomno.CommnBOM1 == null)
            {
                model.BillOfMaterialList = null;
                model.materialList = null;
            }
            if (BOMID != 0)
            {
                arg = BillOfMaterialManager.GetbomId(BOMID);
            }
            else
            {
                arg = BillOfMaterialManager.GetbomId(bilOfMaterialBomno.BomId);
            }
            model.BomId = arg.BomId;
            billOfMaterial.BomNo = models.BomNo;
            billOfMaterial.Description = models.Description;
            billOfMaterial.BuyerMasterId = models.BuyerMasterId;
            billOfMaterial.BuyerModel = models.BuyerModel;
            billOfMaterial.MeanSize = models.MeanSize;
            string[] formats = { "dd/MM/yyyy" };
            var dateTime = DateTime.ParseExact(models.Date, formats, new CultureInfo("en-US"), DateTimeStyles.None);
            billOfMaterial.Date = dateTime;
            billOfMaterial.ParentBomNo = model.ParentBomNo;
            billOfMaterial.NoOfSets = models.NoOfSets;
            billOfMaterial.LastBomNoEntered = models.LastBomNoEntered;
            billOfMaterial.LinkBomNo = models.LinkBomNo;
            billOfMaterial.Ammendment = models.Ammendment;
            billOfMaterial.CommnBOM1 = models.CommnBOM1;
            billOfMaterial.CommnBOM2 = models.CommnBOM2;
            billOfMaterial.CommnBOM3 = models.CommnBOM3;
            billOfMaterial.CommnBOM4 = models.CommnBOM4;
            billOfMaterial.CommnBOM5 = models.CommnBOM5;
            billOfMaterial.CommonBomNo = models.CommonBomNo;
            billOfMaterial.PreparedBy = models.PreparedBy;
            billOfMaterial.VerifiedBy = models.VerifiedBy;
            billOfMaterial.CheckedBy = models.CheckedBy;
            billOfMaterial.MaterialMasterId = models.MaterialName;
            billOfMaterial.MaterialCategoryMasterId = models.MaterialCategoryMasterId;
            billOfMaterial.MaterialGroupMasterId = models.MaterialGroupMasterId;
            billOfMaterial.ColorMasterId = models.ColorMasterId;
            billOfMaterial.SubstanceMasterId = models.SubstanceMasterId;
            billOfMaterial.SampleNorms = models.SampleNorms;
            billOfMaterial.Wastage = models.Wastage;
            billOfMaterial.ComponentName = models.ComponentName;
            billOfMaterial.SupplierMasterId = models.SupplierMasterId;
            billOfMaterial.SizeScheduleMasterId = models.SizeScheduleMasterId;
            billOfMaterial.UomMasterId = models.UomMasterId;
            billOfMaterial.SizeRangeMasterId = models.SizeRangeMasterId;
            billOfMaterial.WastageQty = models.WastageQty;
            billOfMaterial.WastageQtyUOM = models.WastageQtyUOM;
            billOfMaterial.TotalNorms = models.TotalNorms;
            billOfMaterial.TotalNormsUOM = models.TotalNormsUOM;
            billOfMaterial.NoOfSets = models.NoOfSets;
            billOfMaterial.BuyerNorms = models.BuyerNorms;  
            billOfMaterial.CreatedDate = DateTime.Now;
            billOfMaterial.UpdatedDate = null;
            billOfMaterial.BomId = 0;
            if (bilOfMaterialBomno != null && bilOfMaterialBomno.BomId != 0)
            {
                billOfMaterial.BomId = bilOfMaterialBomno.BomId;
            }
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            returnBillOfMaterial = billOfMaterialManager.Post(billOfMaterial);
            model.Edit = true;
            model.Status = true;
            materialList_ = materialList_.Where(x => x.BomID == arg.BomId).ToList();
            model.materialList = materialList_;
            List<BOMMaterial> listBomMaterialList = new List<BOMMaterial>();
            List<BOMMaterial> listBomMaterialList_ = new List<BOMMaterial>();
            List<BOMMaterilGrid> materilGridList = new List<BOMMaterilGrid>();

            bommat = BillOfMaterialManager.Chek_toInsertbom(returnBillOfMaterial.BomId, BOMID);
            if (bommat == null)
            {
                //insert data
                BillOfMaterialManager.Insert_materialdetail(returnBillOfMaterial.BomId, BOMID);
            }
            else
            {
                return Json(new { data = "Data is alredy saved" }, JsonRequestBehavior.AllowGet);

            }
            listBomMaterialList = BillOfMaterialManager.GetBomMaterialBOMID(BOMID);
            //Entry for Display size range     
            int count = 1;
            NormsManager normsManager = new NormsManager();
            Norms norms = new Norms();
            norms = normsManager.GetGroupIDWithBuyername(model.MaterialGroupMasterId, model.BuyerMasterId);
            //foreach (var iteration in listBomMaterialList)
            //{
            //    BOMMaterial bomMaterials = new BOMMaterial();

            //    bomMaterials.BOMID = returnBillOfMaterial.BomId;
            //    bomMaterials.ParentCommonBOMID = BOMID;
            //    bomMaterials.ParentBOMID = iteration.ParentBOMID;
            //    bomMaterials.MaterialCategoryMasterId = iteration.MaterialCategoryMasterId;
            //    bomMaterials.MaterialGroupMasterId = iteration.MaterialGroupMasterId;
            //    bomMaterials.SubstanceMasterId = iteration.SubstanceMasterId;
            //    bomMaterials.MaterialName = iteration.MaterialName;
            //    bomMaterials.ColorMasterId = iteration.ColorMasterId;
            //    bomMaterials.SupplierMasterID = iteration.SupplierMasterID;
            //    bomMaterials.SampleNorms = (iteration.SampleNorms);
            //    bomMaterials.Wastage = iteration.Wastage;
            //    bomMaterials.Conversion = iteration.Conversion;
            //    bomMaterials.WastageQty = iteration.WastageQty;
            //    bomMaterials.WastageQtyUOM = iteration.WastageQtyUOM;
            //    bomMaterials.SizeRangeMasterID = iteration.SizeRangeMasterID;
            //    bomMaterials.NoofSets = iteration.NoofSets;
            //    bomMaterials.SNO = count.ToString(); ;
            //    bomMaterials.BuyerNorms = iteration.BuyerNorms;
            //    bomMaterials.TotalNorms = iteration.TotalNorms;
            //    bomMaterials.TotalNormsUOM = iteration.TotalNormsUOM;
            //    bomMaterials.SizeScheduleMasterId = iteration.SizeScheduleMasterId;

            //    if (arg != null && arg.BomId != 0 && arg.IssueNormsPercent != null)
            //    {
            //        bomMaterials.OurNorms = arg.OurNorms;
            //        bomMaterials.OurNormsPercent = arg.OurNormsPercent;
            //        bomMaterials.PurchaseNorms = arg.PurchaseNorms;
            //        bomMaterials.PurchaseNormsPercent = arg.PurchaseNormsPercent;
            //        bomMaterials.IssueNorms = arg.IssueNorms;
            //        bomMaterials.IssueNormsPercent = arg.IssueNormsPercent;
            //        bomMaterials.CostingNorms = arg.CostingNorms;
            //        bomMaterials.CostingNormsPercent = arg.CostingNormsPercent;
            //    }
            //    else
            //    {
            //        bomMaterials.OurNorms = iteration.OurNorms;
            //        bomMaterials.OurNormsPercent = iteration.OurNormsPercent;
            //        bomMaterials.PurchaseNorms = iteration.PurchaseNorms;
            //        bomMaterials.PurchaseNormsPercent = iteration.PurchaseNormsPercent;
            //        bomMaterials.IssueNorms = iteration.IssueNorms;
            //        bomMaterials.IssueNormsPercent = iteration.IssueNormsPercent;
            //        bomMaterials.CostingNorms = iteration.CostingNorms;
            //        bomMaterials.CostingNormsPercent = iteration.CostingNormsPercent;
            //    }

            //    bomMaterials.Rate = Convert.ToDecimal(iteration.Rate);

            //    BOMMaterial returnBOMMaterial = new BOMMaterial();
            //    returnBOMMaterial = billOfMaterialManager.BOMMaterialPost(bomMaterials);
            //    List<DisplaySizeMaterial> listDisplaySizeMaterial = new List<DisplaySizeMaterial>();
            //    BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
            //    listDisplaySizeMaterial = bomMaterialListManager.DisplaySizeMaterialGet(iteration.BOMMaterialID);
            //    //Entry for Display size range                   
            //    foreach (var item in listDisplaySizeMaterial)
            //    {
            //        DisplaySizeMaterial displaySizeMaterial = new DisplaySizeMaterial();
            //        displaySizeMaterial.SizeRange = item.SizeRange;
            //        displaySizeMaterial.SizeIsChecked = item.SizeIsChecked;
            //        displaySizeMaterial.BOMmaterialID = returnBOMMaterial.BOMMaterialID;
            //        displaySizeMaterial.CreatedDate = DateTime.Now;
            //        bomMaterialListManager.DisplaySizePost(displaySizeMaterial);
            //    }
            //    count++;
            //}
            listBomMaterialList_ = BillOfMaterialManager.GetBomMaterialBOMID(returnBillOfMaterial.BomId);
            //Material add to list
            foreach (var item in listBomMaterialList_)
            {
                BOMMaterilGrid model_ = new BOMMaterilGrid();
                BuyerManager buyerManager = new BuyerManager();
                BuyerMaster buyerMaster = new BuyerMaster();
                MaterialGroupManager groupManager = new MaterialGroupManager();
                MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
                MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
                MaterialGroupMaster_ materialGroupMaster = new MaterialGroupMaster_();
                MaterialManager materialManager = new MaterialManager();
                MaterialMaster materialMaster = new MaterialMaster();
                ColorManager colorManager = new ColorManager();
                ColorMaster colorMaster = new ColorMaster();
                SubstanceMasterManager substanceMasterManager = new SubstanceMasterManager();
                SubstanceMaster substanceMaster = new SubstanceMaster();
                materialCategoryMaster = materialCategoryManager.GetMaterialCategoryMaster(item.MaterialCategoryMasterId);
                materialGroupMaster = groupManager.GetMaterialGroupMaster_Id(item.MaterialGroupMasterId);
                materialMaster = materialManager.GetMaterialMasterId(item.MaterialName);
                MaterialNameManager nameManager = new MaterialNameManager();
                MaterialNameMaster nameMaster = new MaterialNameMaster();
                if (materialMaster != null)
                {
                    nameMaster = nameManager.GetMaterialNameMasterId(materialMaster.MaterialName);
                }
                colorMaster = colorManager.GetColorMasterID(item.ColorMasterId);
                substanceMaster = substanceMasterManager.GetsubstanceMasterId(item.SubstanceMasterId);
                if (materialCategoryMaster != null)
                {
                    model_.CategoryName = materialCategoryMaster.CategoryName;
                }
                if (materialGroupMaster != null)
                {
                    model_.GroupName = materialGroupMaster.GroupName;
                }
                if (colorMaster != null)
                {
                    model_.ColorName = colorMaster.Color;
                }
                if (nameMaster != null)
                {
                    model_.MaterialName = nameMaster.MaterialDescription;
                }
                if (substanceMaster != null)
                {
                    model_.SubtranceRangeName = substanceMaster.SubstanceRange;
                }
                model_.BOMMaterialID = item.BOMMaterialID;
                model_.BomId = item.BOMID;
                model_.MaterialCategoryMasterId = item.MaterialCategoryMasterId;
                model_.MaterialGroupMasterId = item.MaterialGroupMasterId;
                model_.SubstanceMasterId = item.SubstanceMasterId;
                model_.MaterialMasterId = item.MaterialName;
                model_.ColorMasterId = item.ColorMasterId;
                model_.SampleNorms = item.SampleNorms;
                model_.Wastage = item.Wastage;
                model_.WastageQty = item.WastageQty;
                model_.BomNo = models.BomNo;
                model_.Description = models.Description;
                model_.BuyerMasterId = models.BuyerMasterId;
                model_.BuyerModel = models.BuyerModel;
                model_.MeanSize = models.MeanSize;
                model_.Date = models.Date;
                model_.ParentBomNo = models.ParentBomNo;
                model_.LastBomNoEntered = models.LastBomNoEntered;
                model_.LinkBomNo = models.LinkBomNo;
                model_.Ammendment = models.Ammendment;
                model_.CommonBomNo = models.CommonBomNo;
                UOMManager uomManager = new UOMManager();
                UomMaster uomMaster = new UomMaster();
                uomMaster = uomManager.GetUomMasterId(item.WastageQtyUOM);
                model_.WastageqtyUOM = uomMaster.LongUnitName;
                model_.PreparedBy = models.PreparedBy;
                model_.VerifiedBy = models.VerifiedBy;
                model_.CheckedBy = models.CheckedBy;
                model_.GroupID = models.GroupID;
                model_.SupplierMasterId = models.SupplierMasterId;
                model_.UomMasterId = models.UomMasterId;
                model_.SizeRangeMasterId = models.SizeRangeMasterId;
                model_.NoOfSets = models.NoOfSets;
                model_.BuyerNorms = models.BuyerNorms;
                model_.ComponentName = models.ComponentName;
                model_.OurNorms = item.OurNorms;
                model_.TotalNorms = item.TotalNorms;
                model_.TotalNormsUOM = item.TotalNormsUOM;
                model_.OurNormsPercent = item.OurNormsPercent;
                model_.PurchaseNorms = item.PurchaseNorms;
                model_.PurchaseNormsPercent = item.PurchaseNormsPercent;
                model_.IssueNorms = item.IssueNorms;
                model_.IssueNormsPercent = item.IssueNormsPercent;
                model_.CostingNorms = item.CostingNorms;
                model_.CostingNormsPercent = item.CostingNormsPercent;
                model_.CommnBOM1 = models.CommnBOM1;
                model_.CommnBOM2 = models.CommnBOM2;
                model_.CommnBOM3 = models.CommnBOM3;
                model_.CommnBOM4 = models.CommnBOM4;
                model_.SNO = item.SNO;
                model_.CommnBOM5 = models.CommnBOM5;
                model_.WastageQtyUOM = item.WastageQtyUOM;
                model_.NoOfSet = models.NoOfSet;
                model_.MaterialSuppliedBY = models.MaterialSuppliedBY;
                model_.SubtanceMasterID = models.SubtanceMasterID;
                materilGridList.Add(model_);
            }
            model.bomMaterialGridList = materilGridList;
            return Json(new { Model = models, Material = model.bomMaterialGridList }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BillOfMaterialDetailsView(int BomId = 0)
        {

            return RedirectToAction("~/Views/Stock/BillOfMaterial/BillOfMaterialDetails.cshtml", new { id = BomId });

        }

        public ActionResult SelectedBOMMaterial(int MaterialBomID)
        {
            BOMMaterialListManager materialListManager = new BOMMaterialListManager();
            BillOfMaterialModel model = new BillOfMaterialModel();
            List<BOMMaterialList> materialList_ = new List<BOMMaterialList>();
            BOMMaterialList result = new BOMMaterialList();

            materialList_ = materialListManager.Get();
            result = materialList_.Where(x => x.MaterilBomID == MaterialBomID).FirstOrDefault();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string filter)
        {
            List<BillOfMaterialGrid> bomGrid = new List<BillOfMaterialGrid>();
            List<BillOfMaterial> billOfMateriallist = new List<BillOfMaterial>();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();

            billOfMateriallist = billOfMaterialManager.GetBomList(filter.Trim());


            foreach (var items in billOfMateriallist)
            {
                BillOfMaterialGrid grid = new BillOfMaterialGrid();
                grid.BomId = items.BomId;
                grid.BomNo = items.BomNo;
                grid.BuyerMasterId = items.BuyerMasterId;
                grid.ParentBomNo = items.ParentBomNo;
                grid.MaterialMasterId = items.MaterialMasterId.Value;
                grid.Date = items.Date;
                grid.MaterialCategoryMasterId = items.MaterialCategoryMasterId.Value;
                bomGrid.Add(grid);
            }
            BillOfMaterialModel model = new BillOfMaterialModel();
            model.BOMMaterialLists = bomGrid;
            return PartialView("~/Views/Stock/BillOfMaterial/BOMMaterialListGrid.cshtml", model);
        }

        public ActionResult CommonBomNoDetail(string CommnBOM)
        {
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            BillOfMaterial billOfMaterial = new BillOfMaterial();
            billOfMaterial = billOfMaterialManager.getBomNumber(CommnBOM);
            return Json(new { MaterialCategoryMasterId = billOfMaterial.MaterialCategoryMasterId, MaterialGroupMasterId = billOfMaterial.MaterialGroupMasterId, SubstanceMasterId = billOfMaterial.SubstanceMasterId, MaterialName = billOfMaterial.MaterialMasterId, ColorMasterId = billOfMaterial.ColorMasterId, SampleNorms = billOfMaterial.SampleNorms, Wastage = billOfMaterial.Wastage, WastageQty = billOfMaterial.WastageQty, WastageQtyUOM = billOfMaterial.WastageQtyUOM, TotalNorms = billOfMaterial.TotalNorms, TotalNormsUOM = billOfMaterial.TotalNormsUOM }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult FillMaterialName(int MaterialGroupMasterId)
        {
            List<MaterialNameMaster> materialNameMasterList = new List<MaterialNameMaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            var items = (from x in materialNameManager.Get().Where(x => x.MaterialGroupMasterId == MaterialGroupMasterId)
                         select new { MaterialDescription = x.MaterialDescription, MaterialMasterID = x.MaterialMasterID });

            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillColorName(int MaterialmasterId)
        {
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            ColorManager colorManager = new ColorManager();
            var items = (from x in materialNameManager.Get()
                         join y in materialManager.Get()
                          on x.MaterialMasterID equals y.MaterialName
                         join z in colorManager.Get()
                         on y.ColorMasterId equals z.ColorMasterId
                         where y.MaterialMasterId == MaterialmasterId
                         select new { MaterialDescription = string.Format("{0} {1} {2} {3}", x.MaterialDescription, y.OptionMaterialValue, y.MaterialCode, z.Color), y.MaterialCode, y.MaterialMasterId, z.ColorMasterId, y.SizeRangeMasterId });

            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();

            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult MaterialListDetails(BOMMaterialListModel Model)
        {
            BOMMaterialListManager billOfMaterialManager = new BOMMaterialListManager();
            BOMMaterilaListGrid bomGrid = new BOMMaterilaListGrid();
            BOMMaterialList bOMMaterialList = new BOMMaterialList();
            BOMMaterialListModel gridModel = new BOMMaterialListModel();
            bOMMaterialList.BomID = Model.BomID;
            bOMMaterialList.BomNo = Model.BomNo;
            bOMMaterialList.Date = Model.Date;
            bOMMaterialList.ParentBomNo = Model.ParentBomNo;
            bOMMaterialList.CommnBOM1 = Model.CommnBOM1;
            bOMMaterialList.CommnBOM2 = Model.CommnBOM2;
            bOMMaterialList.CommnBOM3 = Model.CommnBOM3;
            bOMMaterialList.CommnBOM4 = Model.CommnBOM4;
            bOMMaterialList.CommnBOM5 = Model.CommnBOM5;
            bOMMaterialList.MaterialMasterId = Model.MaterialMasterId;
            bOMMaterialList.MaterialCategoryMasterId = Model.MaterialCategoryMasterId;
            bOMMaterialList.SampleNorms = Model.SampleNorms;
            bOMMaterialList.MaterialGroupMasterId = Model.MaterialGroupMasterId;
            bOMMaterialList.ColorMasterId = Model.ColorMasterId;
            bOMMaterialList.Wastage = Model.Wastage;
            bOMMaterialList.WastageQty = Model.WastageQty;
            bOMMaterialList.WastageQtyUOM = Model.WastageQtyUOM;
            bOMMaterialList.TotalNorms = Model.TotalNorms;
            bOMMaterialList.TotalNormsUOM = Model.TotalNormsUOM;
            bOMMaterialList.NoOfSet = Model.NoOfSet;
            bOMMaterialList.MaterialSuppliedBY = Model.MaterialSuppliedBY;
            bOMMaterialList.SubstanceMasterId = Model.SubstanceMasterId;
            bOMMaterialList.SizeScheduleMasterId = Model.SizeScheduleMasterId;
            bOMMaterialList = billOfMaterialManager.Post(bOMMaterialList);
            return Json(bOMMaterialList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckBomNo(string BomNo)
        {
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            BillOfMaterial billOfMaterial = new BillOfMaterial();
            billOfMaterial = billOfMaterialManager.GetBomNO(BomNo.Trim());
            Product_BuyerStyleManager productStyleManager = new Product_BuyerStyleManager();
            Product_BuyerStyleMaster productStyleMaster = new Product_BuyerStyleMaster();
            productStyleMaster = productStyleManager.GetOurStyle(BomNo.Trim());
            string Message = "";
            if (billOfMaterial != null)
            {
                Message = "Already Existed";
            }
            else
            {
                Message = "Success";
            }
            return Json(new { productStyleMaster = productStyleMaster, Message = Message }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult BillOfMaterialDetails(BillOfMaterialModel model, string Grid, string BOMMaterialID_, string CheckBoxsize, string CheckBoxIschecked,string Date_)
        {
            string Message = "";
            int bomID = 0;
            int id = Convert.ToInt32(Request.QueryString["id"]);
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            List<BomGrid> listofBOMGrid = new List<BomGrid>();
            BomGrid bomGrid_ = new BomGrid();
            GridModel bomGrid = new GridModel();
            BillOfMaterial billOfMaterial = new BillOfMaterial();
            BomGridDetailsModel gridModel = new BomGridDetailsModel();
            BillOfMaterial billOfMaterial_ = new BillOfMaterial();
            BOMAmendmentMaterial bomAmendmentMaterial_ = new BOMAmendmentMaterial();
            BillOfMaterial billOfMaterialIsExist = new BillOfMaterial();
            BOMAmendmentMaterial bomamendmentIsExist = new BOMAmendmentMaterial();
            BOMMaterial bomMaterial = new BOMMaterial();
            BOMAmendmentMaterial bomAmendmentMaterial = new BOMAmendmentMaterial();
            BOMMaterial bomMaterials = new BOMMaterial();
            MaterialManager materialManager = new MaterialManager();
            MaterialMaster materialMaster = new MaterialMaster();
            MaterialGroupMaster_ groupMaster = new MaterialGroupMaster_();
            MaterialCategoryMaster categoryMaster = new MaterialCategoryMaster();
            ColorMaster colorMaster = new ColorMaster();
            ColorManager colorManager = new ColorManager();
            UomMaster uomMaster = new UomMaster();
            UOMManager uomManager = new UOMManager();
            SubstanceMasterManager substanceManager = new SubstanceMasterManager();
            SubstanceMaster substanceMaster = new SubstanceMaster();
            MaterialCategoryManager categoryManager = new MaterialCategoryManager();
            MaterialGroupManager GroupManager = new MaterialGroupManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialNameMaster materialNameMaste = new MaterialNameMaster();
            string LastBOmNO = string.Empty;
            string CategoryName = string.Empty;
            string GroupName = string.Empty;
            string MaterialName = string.Empty;
            string ColorName = string.Empty;
            string SubtanceName = string.Empty;
            string WastageUOM = string.Empty;
            string TotalNormUOM = string.Empty;
            LastBOmNO = billOfMaterialManager.GetLastbomNumber();
            if (LastBOmNO == null)
            {
                LastBOmNO = "NotApplicable";
            }
            billOfMaterial.BomId = model.BomId;
            billOfMaterial.BomNo = model.BomNo;
            billOfMaterial.CommnBOM1 = model.CommnBOM1;
            billOfMaterial.CommnBOM2 = model.CommnBOM2;
            billOfMaterial.CommnBOM3 = model.CommnBOM3;
            billOfMaterial.CommnBOM4 = model.CommnBOM4;
            billOfMaterial.CommnBOM5 = model.CommnBOM5;
            billOfMaterial.Description = model.Description;
            billOfMaterial.BuyerMasterId = model.BuyerMasterId;
            billOfMaterial.BuyerModel = model.BuyerModel;
            billOfMaterial.MeanSize = model.MeanSize;
            DateTime date = new DateTime();
            string[] formats = { "dd/MM/yyyy" };
            if(model.Date!=null)
            {
                var dateTime = DateTime.ParseExact(model.Date, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                materialMaster = materialManager.GetMaterialMasterId(model.MaterialName);
                billOfMaterial.Date = dateTime;
            }
           
            if (!string.IsNullOrEmpty(model.HiddenParentBOMID.ToString()))
            {
                billOfMaterial.ParentBomNo = model.HiddenParentBOMID.ToString();
            }
            else
            {
                billOfMaterial.ParentBomNo = model.ParentBomNo;
            }
            billOfMaterial.LastBomNoEntered = LastBOmNO;
            billOfMaterial.LinkBomNo = model.LinkBomNo;
            billOfMaterial.Ammendment = model.Ammendment;
            billOfMaterial.CommonBomNo = model.CommonBomNo;
            billOfMaterial.PreparedBy = model.PreparedBy;
            billOfMaterial.VerifiedBy = model.VerifiedBy;
            billOfMaterial.CheckedBy = model.CheckedBy;
            billOfMaterial.MaterialMasterId = model.MaterialName;
            billOfMaterial.SizeScheduleMasterId = model.SizeScheduleMasterId;
            billOfMaterial.MaterialCategoryMasterId = model.MaterialCategoryMasterId;
            billOfMaterial.MaterialGroupMasterId = model.MaterialGroupMasterId;
            billOfMaterial.ColorMasterId = materialMaster.ColorMasterId;
            billOfMaterial.SubstanceMasterId = model.SubstanceMasterId;
            billOfMaterial.SampleNorms = model.SampleNorms;
            billOfMaterial.Wastage = model.Wastage;
            billOfMaterial.SupplierMasterId = model.SupplierMasterId;
            billOfMaterial.UomMasterId = model.UomMasterId;
            billOfMaterial.SizeRangeMasterId = model.SizeRangeMasterId;
            billOfMaterial.WastageQty = model.WastageQty;
            billOfMaterial.WastageQtyUOM = model.WastageQtyUOM;
            billOfMaterial.TotalNorms = model.TotalNorms;
            billOfMaterial.TotalNormsUOM = model.TotalNormsUOM;
            billOfMaterial.ComponentName = model.ComponentName;
            billOfMaterial.NoOfSets = model.NoOfSets;
            billOfMaterial.BuyerNorms = model.BuyerNorms;
            billOfMaterial.CreatedDate = DateTime.Now;
            string AlertMessage = "";
            if (model.BomNo == null && model.Description != "")
            {
                model.BomNo = model.Description;
            }
            billOfMaterialIsExist = billOfMaterialManager.GetBomNO(model.BomNo);
            if (billOfMaterialIsExist == null && model.BomId == 0)
            {
                billOfMaterial_ = billOfMaterialManager.Post(billOfMaterial);
                AlertMessage = "Saved Successfully";
            }
            else if (billOfMaterialIsExist != null && billOfMaterialIsExist.BomId == 0)
            {
                AlertMessage = "Already Existed";
                return Json(AlertMessage, JsonRequestBehavior.AllowGet);
            }
            else if (billOfMaterialIsExist.BomId != 0 && billOfMaterialIsExist != null)
            {
                billOfMaterial.BomId = billOfMaterialIsExist.BomId;
                billOfMaterial_ = billOfMaterialManager.Post(billOfMaterial);
                AlertMessage = "Updated Successfully";
            }
            groupMaster = GroupManager.GetMaterialGroupMaster_Id(billOfMaterial.MaterialGroupMasterId);
            categoryMaster = categoryManager.GetMaterialCategoryMaster(billOfMaterial.MaterialCategoryMasterId);
            substanceMaster = substanceManager.GetsubstanceMasterId(billOfMaterial.SubstanceMasterId);
            colorMaster = colorManager.GetColorMasterID(materialMaster.ColorMasterId);
            uomMaster = uomManager.GetUomMasterId(model.WastageQtyUOM);
            materialNameMaste = materialNameManager.GetMaterialNameMasterId(materialMaster.MaterialName);
            GroupName = groupMaster.GroupName;
            CategoryName = categoryMaster.CategoryName;
            SubtanceName = substanceMaster.SubstanceRange;
            ColorName = colorMaster.Color;
            MaterialName = materialNameMaste.MaterialDescription;
            WastageUOM = uomMaster.LongUnitName;
            uomMaster = uomManager.GetUomMasterId(model.TotalNormsUOM);
            TotalNormUOM = uomMaster.LongUnitName;
            //Insert data into BOMMaterial table
            BOMMaterial ischeckBomMaterial = new BOMMaterial();
            BOMMaterial bomMaterial_ = new BOMMaterial();
            materialMaster = materialManager.GetMaterialMasterId(model.MaterialName);
            ischeckBomMaterial = billOfMaterialManager.IsExistMaterial(model.MaterialCategoryMasterId, model.MaterialGroupMasterId, model.MaterialName, materialMaster.ColorMasterId.Value, billOfMaterial_.BomId);
            string MaterialMessage = "";
            if (ischeckBomMaterial != null && ischeckBomMaterial.BOMMaterialID != 0 && BOMMaterialID_ == "")
            {
                MaterialMessage = "Material Existed";
                return Json(MaterialMessage, JsonRequestBehavior.AllowGet);
            }
            if ((ischeckBomMaterial == null || ischeckBomMaterial.BOMMaterialID == 0) || ischeckBomMaterial != null)
            {
                bomMaterials.BOMID = billOfMaterial_.BomId;
                bomMaterials.MaterialCategoryMasterId = model.MaterialCategoryMasterId;
                bomMaterials.MaterialGroupMasterId = model.MaterialGroupMasterId;
                bomMaterials.SubstanceMasterId = model.SubstanceMasterId;
                bomMaterials.MaterialName = model.MaterialName;
                bomMaterials.ColorMasterId = materialMaster.ColorMasterId.Value;
                bomMaterials.SupplierMasterID = model.SupplierMasterId;
                bomMaterials.SampleNorms = (model.SampleNorms);
                bomMaterials.Wastage = model.Wastage;
                bomMaterials.WastageQty = model.WastageQty;
                bomMaterials.SNO = model.SNO;
                bomMaterials.WastageQtyUOM = model.WastageQtyUOM;
                bomMaterials.SizeRangeMasterID = model.SizeRangeMasterId;
                bomMaterials.NoofSets = model.NoOfSets;
                bomMaterials.BuyerNorms = model.BuyerNorms;
                bomMaterials.TotalNorms = model.TotalNorms;
                bomMaterials.Conversion = (model.Conversion);
                bomMaterials.TotalNormsUOM = model.TotalNormsUOM;
                bomMaterials.SizeScheduleMasterId = model.SizeScheduleMasterId;
                bomMaterials.OurNorms = model.OurNorms;
                bomMaterials.OurNormsPercent = model.OurNormsPercent;
                bomMaterials.PurchaseNorms = model.PurchaseNorms;
                bomMaterials.PurchaseNormsPercent = model.PurchaseNormsPercent;
                bomMaterials.IssueNorms = model.IssueNorms;
                bomMaterials.IssueNormsPercent = model.IssueNormsPercent;
                bomMaterials.CostingNorms = model.CostingNorms;
                bomMaterials.CostingNormsPercent = model.CostingNormsPercent;
                bomMaterials.Rate = Convert.ToDecimal(materialMaster.Price);
                if (BOMMaterialID_ != null && BOMMaterialID_ != "")
                {
                    bomMaterials.BOMMaterialID = Convert.ToInt32(BOMMaterialID_);
                    bomMaterial_ = billOfMaterialManager.BOMMaterialPost(bomMaterials);
                }
                else
                {
                    bomMaterial_ = billOfMaterialManager.BOMMaterialPost(bomMaterials);
                }

                if (model.CheckBoxsize != null && CheckBoxsize != "")
                {
                    string[] sizeAray = model.CheckBoxsize.Split(',');
                    string[] IscheckedAray = model.CheckBoxIschecked.Split(',');
                    int counts_ = 0;
                    BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
                    List<DisplaySizeMaterial> listDispalySizeMaterial = new List<DisplaySizeMaterial>();
                    if (BOMMaterialID_ != null && BOMMaterialID_ != "")
                    {
                        listDispalySizeMaterial = bomMaterialListManager.DisplaySizeMaterialGet(Convert.ToInt32(BOMMaterialID_));
                    }
                    foreach (var item in listDispalySizeMaterial)
                    {
                        bomMaterialListManager.DisplaySizeMaterialDelete(item.DisplaySizeMaterialD);
                    }
                    foreach (var item in sizeAray)
                    {
                        DisplaySizeMaterial displaySizeMaterial = new DisplaySizeMaterial();
                        displaySizeMaterial.SizeRange = sizeAray[counts_];
                        displaySizeMaterial.SizeIsChecked = Convert.ToBoolean(IscheckedAray[counts_]);
                        if (BOMMaterialID_ != null && BOMMaterialID_ != "")
                        {
                            displaySizeMaterial.BOMmaterialID = Convert.ToInt32(BOMMaterialID_);
                        }
                        else
                        {
                            displaySizeMaterial.BOMmaterialID = bomMaterial_.BOMMaterialID;
                        }

                        displaySizeMaterial.CreatedDate = DateTime.Now;
                        bomMaterialListManager.DisplaySizePost(displaySizeMaterial);
                        counts_++;
                    }
                }
                else if (BOMMaterialID_ != null && BOMMaterialID_ != "")
                {

                }

                BOMMaterialListManager billOfMaterialListManager = new BOMMaterialListManager();
                List<BOMMaterialList> materialList = new List<BOMMaterialList>();
                BomHistory bomHistory = new BomHistory();
                if (model.CommnBOM1 != null)
                {
                    materialList = billOfMaterialListManager.Get();
                    materialList = materialList.Where(x => x.BomID == Convert.ToInt32(model.CommnBOM1)).ToList();
                    foreach (var item in materialList)
                    {
                        BOMMaterialList bOMMaterialList = new BOMMaterialList();
                        bOMMaterialList.BomID = bomID;
                        bOMMaterialList.BomNo = item.BomNo;
                        bOMMaterialList.Date = item.Date;
                        bOMMaterialList.ParentBomNo = item.ParentBomNo;
                        bOMMaterialList.CommnBOM1 = item.CommnBOM1;
                        bOMMaterialList.CommnBOM2 = item.CommnBOM2;
                        bOMMaterialList.CommnBOM3 = item.CommnBOM3;
                        bOMMaterialList.CommnBOM4 = item.CommnBOM4;
                        bOMMaterialList.CommnBOM5 = item.CommnBOM5;
                        bOMMaterialList.MaterialMasterId = item.MaterialMasterId;
                        bOMMaterialList.MaterialCategoryMasterId = item.MaterialCategoryMasterId;
                        bOMMaterialList.SampleNorms = item.SampleNorms;
                        bOMMaterialList.MaterialGroupMasterId = item.MaterialGroupMasterId;
                        bOMMaterialList.ColorMasterId = item.ColorMasterId;
                        bOMMaterialList.Wastage = item.Wastage;
                        bOMMaterialList.WastageQty = item.WastageQty;
                        bOMMaterialList.WastageQtyUOM = item.WastageQtyUOM;
                        bOMMaterialList.TotalNorms = item.TotalNorms;
                        bOMMaterialList.TotalNormsUOM = item.TotalNormsUOM;
                        bOMMaterialList.NoOfSet = item.NoOfSet;
                        bOMMaterialList.MaterialSuppliedBY = item.MaterialSuppliedBY;
                        bOMMaterialList.SubstanceMasterId = item.SubstanceMasterId;
                        bOMMaterialList = billOfMaterialListManager.Post(bOMMaterialList);
                    }
                }

                if (model.CommnBOM2 != null)
                {
                    materialList = billOfMaterialListManager.Get();
                    materialList = materialList.Where(x => x.BomID == Convert.ToInt32(model.CommnBOM2)).ToList();
                    foreach (var item in materialList)
                    {
                        BOMMaterialList bOMMaterialList = new BOMMaterialList();
                        bOMMaterialList.BomID = bomID;
                        bOMMaterialList.BomNo = item.BomNo;
                        bOMMaterialList.Date = item.Date;
                        bOMMaterialList.ParentBomNo = item.ParentBomNo;
                        bOMMaterialList.CommnBOM1 = item.CommnBOM1;
                        bOMMaterialList.CommnBOM2 = item.CommnBOM2;
                        bOMMaterialList.CommnBOM3 = item.CommnBOM3;
                        bOMMaterialList.CommnBOM4 = item.CommnBOM4;
                        bOMMaterialList.CommnBOM5 = item.CommnBOM5;
                        bOMMaterialList.MaterialMasterId = item.MaterialMasterId;
                        bOMMaterialList.MaterialCategoryMasterId = item.MaterialCategoryMasterId;
                        bOMMaterialList.SampleNorms = item.SampleNorms;
                        bOMMaterialList.MaterialGroupMasterId = item.MaterialGroupMasterId;
                        bOMMaterialList.ColorMasterId = item.ColorMasterId;
                        bOMMaterialList.Wastage = item.Wastage;
                        bOMMaterialList.WastageQty = item.WastageQty;
                        bOMMaterialList.WastageQtyUOM = item.WastageQtyUOM;
                        bOMMaterialList.TotalNorms = item.TotalNorms;
                        bOMMaterialList.TotalNormsUOM = item.TotalNormsUOM;
                        bOMMaterialList.NoOfSet = item.NoOfSet;
                        bOMMaterialList.MaterialSuppliedBY = item.MaterialSuppliedBY;
                        bOMMaterialList.SubstanceMasterId = item.SubstanceMasterId;
                        bOMMaterialList = billOfMaterialListManager.Post(bOMMaterialList);
                    }
                }

                if (model.CommnBOM3 != null)
                {
                    materialList = billOfMaterialListManager.Get();
                    materialList = materialList.Where(x => x.BomID == Convert.ToInt32(model.CommnBOM3)).ToList();
                    foreach (var item in materialList)
                    {
                        BOMMaterialList bOMMaterialList = new BOMMaterialList();
                        bOMMaterialList.BomID = bomID;
                        bOMMaterialList.BomNo = item.BomNo;
                        bOMMaterialList.Date = item.Date;
                        bOMMaterialList.ParentBomNo = item.ParentBomNo;
                        bOMMaterialList.CommnBOM1 = item.CommnBOM1;
                        bOMMaterialList.CommnBOM2 = item.CommnBOM2;
                        bOMMaterialList.CommnBOM3 = item.CommnBOM3;
                        bOMMaterialList.CommnBOM4 = item.CommnBOM4;
                        bOMMaterialList.CommnBOM5 = item.CommnBOM5;
                        bOMMaterialList.MaterialMasterId = item.MaterialMasterId;
                        bOMMaterialList.MaterialCategoryMasterId = item.MaterialCategoryMasterId;
                        bOMMaterialList.SampleNorms = item.SampleNorms;
                        bOMMaterialList.MaterialGroupMasterId = item.MaterialGroupMasterId;
                        bOMMaterialList.ColorMasterId = item.ColorMasterId;
                        bOMMaterialList.Wastage = item.Wastage;
                        bOMMaterialList.WastageQty = item.WastageQty;
                        bOMMaterialList.WastageQtyUOM = item.WastageQtyUOM;
                        bOMMaterialList.TotalNorms = item.TotalNorms;
                        bOMMaterialList.TotalNormsUOM = item.TotalNormsUOM;
                        bOMMaterialList.NoOfSet = item.NoOfSet;
                        bOMMaterialList.MaterialSuppliedBY = item.MaterialSuppliedBY;
                        bOMMaterialList.SubstanceMasterId = item.SubstanceMasterId;
                        bOMMaterialList = billOfMaterialListManager.Post(bOMMaterialList);
                    }
                }

                if (model.CommnBOM4 != null)
                {
                    materialList = billOfMaterialListManager.Get();
                    materialList = materialList.Where(x => x.BomID == Convert.ToInt32(model.CommnBOM4)).ToList();
                    foreach (var item in materialList)
                    {
                        BOMMaterialList bOMMaterialList = new BOMMaterialList();
                        bOMMaterialList.BomID = bomID;
                        bOMMaterialList.BomNo = item.BomNo;
                        bOMMaterialList.Date = item.Date;
                        bOMMaterialList.ParentBomNo = item.ParentBomNo;
                        bOMMaterialList.CommnBOM1 = item.CommnBOM1;
                        bOMMaterialList.CommnBOM2 = item.CommnBOM2;
                        bOMMaterialList.CommnBOM3 = item.CommnBOM3;
                        bOMMaterialList.CommnBOM4 = item.CommnBOM4;
                        bOMMaterialList.CommnBOM5 = item.CommnBOM5;
                        bOMMaterialList.MaterialMasterId = item.MaterialMasterId;
                        bOMMaterialList.MaterialCategoryMasterId = item.MaterialCategoryMasterId;
                        bOMMaterialList.SampleNorms = item.SampleNorms;
                        bOMMaterialList.MaterialGroupMasterId = item.MaterialGroupMasterId;
                        bOMMaterialList.ColorMasterId = item.ColorMasterId;
                        bOMMaterialList.Wastage = item.Wastage;
                        bOMMaterialList.WastageQty = item.WastageQty;
                        bOMMaterialList.WastageQtyUOM = item.WastageQtyUOM;
                        bOMMaterialList.TotalNorms = item.TotalNorms;
                        bOMMaterialList.TotalNormsUOM = item.TotalNormsUOM;
                        bOMMaterialList.NoOfSet = item.NoOfSet;
                        bOMMaterialList.MaterialSuppliedBY = item.MaterialSuppliedBY;
                        bOMMaterialList.SubstanceMasterId = item.SubstanceMasterId;
                        bOMMaterialList = billOfMaterialListManager.Post(bOMMaterialList);
                    }
                }

                if (model.CommnBOM5 != null)
                {
                    materialList = billOfMaterialListManager.Get();
                    materialList = materialList.Where(x => x.BomID == Convert.ToInt32(model.CommnBOM5)).ToList();
                    foreach (var item in materialList)
                    {
                        BOMMaterialList bOMMaterialList = new BOMMaterialList();
                        bOMMaterialList.BomID = bomID;
                        bOMMaterialList.BomNo = item.BomNo;
                        bOMMaterialList.Date = item.Date;
                        bOMMaterialList.ParentBomNo = item.ParentBomNo;
                        bOMMaterialList.CommnBOM1 = item.CommnBOM1;
                        bOMMaterialList.CommnBOM2 = item.CommnBOM2;
                        bOMMaterialList.CommnBOM3 = item.CommnBOM3;
                        bOMMaterialList.CommnBOM4 = item.CommnBOM4;
                        bOMMaterialList.CommnBOM5 = item.CommnBOM5;
                        bOMMaterialList.MaterialMasterId = item.MaterialMasterId;
                        bOMMaterialList.MaterialCategoryMasterId = item.MaterialCategoryMasterId;
                        bOMMaterialList.SampleNorms = item.SampleNorms;
                        bOMMaterialList.MaterialGroupMasterId = item.MaterialGroupMasterId;
                        bOMMaterialList.ColorMasterId = item.ColorMasterId;
                        bOMMaterialList.Wastage = item.Wastage;
                        bOMMaterialList.WastageQty = item.WastageQty;
                        bOMMaterialList.WastageQtyUOM = item.WastageQtyUOM;
                        bOMMaterialList.TotalNorms = item.TotalNorms;
                        bOMMaterialList.TotalNormsUOM = item.TotalNormsUOM;
                        bOMMaterialList.NoOfSet = item.NoOfSet;
                        bOMMaterialList.MaterialSuppliedBY = item.MaterialSuppliedBY;
                        bOMMaterialList.SubstanceMasterId = item.SubstanceMasterId;
                        bOMMaterialList = billOfMaterialListManager.Post(bOMMaterialList);
                    }
                }

                List<GridModel> bomGridList = new List<GridModel>();
                model.BomGridlst = billOfMaterialManager.GridList().Where(x => x.BomId == billOfMaterial.BomId).Select(x => x.BomGridId).ToList();
                string GridID = "";
                foreach (var bom in model.BomGridlst)
                {
                    GridID += bom + ",";
                }
                string[] strArray = Grid.Trim().Split('#');
                strArray = strArray.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                int count = 0;
                int Snocount = 1;
                foreach (string obj in strArray)
                {
                    BomGrid GridDetails = new BomGrid();
                    string[] strRow = obj.Split(',');
                    string[] BOMGridID = GridID.Split(',');
                    if (strRow[0].ToString() != "")
                    {

                        if (model.BomGridlst.Count > 0 && BOMGridID[count] != "" && BOMMaterialID_ != "")
                        {
                            bomGrid.BomGridId = Convert.ToInt16(BOMGridID[count].ToString());
                            count++;
                        }
                        else
                        {
                            bomGrid.BomGridId = gridModel.BomGridId;
                        }
                        bomGrid.BomId = billOfMaterial.BomId;
                        bomGrid.Sno = Snocount;
                        bomGrid.BOMMaterialID = bomMaterial_.BOMMaterialID;
                        bomGrid.Component = strRow[0].ToString();
                        bomGrid.Length = strRow[1].ToString();
                        bomGrid.Width = strRow[2].ToString();
                        bomGrid.Nos = Convert.ToInt32(strRow[3].ToString());
                        bomGrid.SampleNorms = Convert.ToDecimal(strRow[4].ToString());
                        bomGrid.WastagePercent = Convert.ToInt32(strRow[5].ToString());
                        bomGrid.WastageQtyGrid = Convert.ToDecimal(strRow[6].ToString());
                        bomGrid.TotalNormsQty = Convert.ToDecimal(strRow[7].ToString());
                        BomGrid bomGrids = new BomGrid();
                        bomGrids.BomGridId = bomGrid.BomGridId;
                        bomGrids.BomId = bomGrid.BomId;
                        bomGrids.Sno = bomGrid.Sno;
                        bomGrids.Component = bomGrid.Component;
                        bomGrids.Length = bomGrid.Length;
                        bomGrids.Width = bomGrid.Width;
                        bomGrids.Nos = bomGrid.Nos;
                        bomGrids.SampleNorms = bomGrid.SampleNorms;
                        bomGrids.WastagePercent = bomGrid.WastagePercent;
                        bomGrids.WastageQtyGrid = bomGrid.WastageQtyGrid;
                        bomGrids.TotalNormsQty = bomGrid.TotalNormsQty;
                        bomGrids.BOMMaterialID = bomGrid.BOMMaterialID;
                        GridDetails = billOfMaterialManager.Post(bomGrids);
                        Message = "Saved Successfully";
                        Snocount++;
                    }
                }


                listofBOMGrid = billOfMaterialManager.getBomIDHistoryList(bomMaterial_.BOMID, bomMaterial_.BOMMaterialID);
                bool result = false;
                if (model.BomId != 0)
                {
                    result = true;
                }
            }

            return Json(new { BOM = billOfMaterial_, Material = bomMaterial_, Grid = listofBOMGrid, Group = GroupName, Category = CategoryName, Subtance = SubtanceName, Color = ColorName, Material_ = MaterialName, WasteUOm = WastageUOM, TotalUOM = TotalNormUOM, AlertMessage = AlertMessage }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int BomId)
        {
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            string status = "";
            BillOfMaterial billOfMaterial = new BillOfMaterial();
            billOfMaterial = billOfMaterialManager.GetbomId(BomId);
            if (billOfMaterial != null)
            {
                status = "Success";
                billOfMaterialManager.Delete(billOfMaterial.BomId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BOMGridDelete(int BomGridId)
        {
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            string status = "";
            BomGrid bomGrid = new BomGrid();
            bomGrid = billOfMaterialManager.GetById(BomGridId);
            if (bomGrid != null)
            {
                status = "Success";
                billOfMaterialManager.BomGridDelete(bomGrid.BomGridId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BOMMaterialDelete(int BOMMaterialID)
        {
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            string status = "";
            BOMMaterial bOMMaterial = new BOMMaterial();
            bOMMaterial = billOfMaterialManager.getBomMaterialID(BOMMaterialID);
            if (bOMMaterial != null)
            {
                status = "Success";
                billOfMaterialManager.BomMaterialDelete_new(bOMMaterial.BOMMaterialID);
            }
            return Json(new { status = status, BOMID = bOMMaterial.BOMID }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Excel Upload
        public ActionResult RemoveSession()
        {
            @Session["BOMerror_"] = null;
            return View("~/Views/Stock/BuyerOrderEntryForm/BuyerOrderEntryForm.cshtml");
        }
        [HttpPost]

        public ActionResult UploadData(HttpPostedFileBase upload)
        {
            BillOfMaterialModel model = new BillOfMaterialModel();
            string BOMError = "";
            if (ModelState.IsValid)
            {
                DataSet result = null;
                if (upload != null && upload.ContentLength > 0)
                {
                    Stream stream = upload.InputStream;
                    
                    IExcelDataReader reader = null;
                    if (upload.FileName.ToLower().EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (upload.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        TempData["successBody"] = "This file format is not supported";
                        return RedirectToAction("BOMMaterialListGrid");
                    }
                    reader.IsFirstRowAsColumnNames = true;
                    result = reader.AsDataSet();
                    reader.Close();
                }
                else
                {
                    TempData["successBody"] = "Please Upload Your file";
                    return RedirectToAction("BOMMaterialListGrid");
                }
                int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoOrderEntryID();
                string consString = ConfigurationManager.ConnectionStrings["MMSConnectionString"].ConnectionString;
                var table = new DataTable();
                table = result.Tables[0];
                ID = ID++;
                DataTable table_ = new DataTable();
                table_.Columns.Add("BomId", typeof(int));
                table_.Columns.Add("BomNo", typeof(string));
                table_.Columns.Add("Description", typeof(string));
                table_.Columns.Add("BuyerMasterId", typeof(int));
                table_.Columns.Add("BuyerModel", typeof(string));
                table_.Columns.Add("MeanSize", typeof(int));
                table_.Columns.Add("Date", typeof(DateTime));
                table_.Columns.Add("ParentBomNo", typeof(string));
                table_.Columns.Add("LastBomNoEntered", typeof(string));
                table_.Columns.Add("LinkBomNo", typeof(string));
                table_.Columns.Add("Ammendment", typeof(string));
                table_.Columns.Add("CommonBomNo", typeof(string));
                table_.Columns.Add("CommnBOM1", typeof(string));
                table_.Columns.Add("CommnBOM2", typeof(string));
                table_.Columns.Add("CommnBOM3", typeof(string));
                table_.Columns.Add("CommnBOM4", typeof(string));
                table_.Columns.Add("CommnBOM5", typeof(string));
                table_.Columns.Add("PreparedBy", typeof(string));
                table_.Columns.Add("VerifiedBy", typeof(string));
                table_.Columns.Add("CheckedBy", typeof(string));
                table_.Columns.Add("CreatedDate", typeof(DateTime));
                table_.Columns.Add("UpdatedDate", typeof(DateTime));
                table_.Columns.Add("CreatedBy", typeof(string));
                table_.Columns.Add("UpdatedBy", typeof(string));
                table_.Columns.Add("MaterialMasterId", typeof(int));
                table_.Columns.Add("MaterialCategoryMasterId", typeof(int));
                table_.Columns.Add("MaterialGroupMasterId", typeof(int));
                table_.Columns.Add("ColorMasterId", typeof(int));
                table_.Columns.Add("SubstanceMasterId", typeof(int));
                table_.Columns.Add("SampleNorms", typeof(string));
                table_.Columns.Add("Wastage", typeof(decimal));
                table_.Columns.Add("SupplierMasterId", typeof(int));
                table_.Columns.Add("UomMasterId", typeof(int));
                table_.Columns.Add("SizeRangeMasterId", typeof(int));
                table_.Columns.Add("SizeScheduleMasterId", typeof(int));
                table_.Columns.Add("WastageQty", typeof(int));
                table_.Columns.Add("WastageQtyUOM", typeof(int));
                table_.Columns.Add("TotalNorms", typeof(decimal));
                table_.Columns.Add("TotalNormsUOM", typeof(int));
                table_.Columns.Add("ComponentName", typeof(int));
                table_.Columns.Add("NoOfSets", typeof(int));
                table_.Columns.Add("BuyerNorms", typeof(decimal));
                table_.Columns.Add("OurNormsPercent", typeof(decimal));
                table_.Columns.Add("PurchaseNorms", typeof(int));
                table_.Columns.Add("PurchaseNormsPercent", typeof(decimal));
                table_.Columns.Add("IssueNorms", typeof(int));
                table_.Columns.Add("IssueNormsPercent", typeof(decimal));
                table.Columns.Add("CostingNorms", typeof(int));
                table.Columns.Add("CostingNormsPercent", typeof(decimal));
                table_.Columns.Add("OurNorms", typeof(int));
                table_.Columns.Add("DeletedBy", typeof(string));
                table_.Columns.Add("IsDeleted", typeof(bool));
                table_.Columns.Add("DeletedOn", typeof(DateTime));
                table_.Columns.Add("RequirementQuantity", typeof(decimal));
                table_.Columns.Add("IsMRP", typeof(bool));
                List<BillOfMaterial> listBillOfMaterial = new List<BillOfMaterial>();
                List<BOMMaterial> listBomMaterial = new List<BOMMaterial>();
                int lotCount = 1;
                int? bomid = null;
                string bomno = "";
                //Check error start
                foreach (DataRow dr in table.Rows)
                {
                    Session["SuccessOrder"] = "Excel data imported Successfully";
                    List<InternalOrderEntryForm> listOrderEntryFormList = new List<InternalOrderEntryForm>();
                    BillOfMaterial billOfMaterial_ = new BillOfMaterial();
                    Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
                    Product_BuyerStyleMaster productStyleMaster = new Product_BuyerStyleMaster();
                    MaterialManager materialManager = new MaterialManager();
                    MaterialMaster materialMaster = new MaterialMaster();
                    MaterialNameManager materialNameManager = new MaterialNameManager();
                    MaterialNameMaster materialNameMaster = new MaterialNameMaster();
                    BillOfMaterial bomDetails = new BillOfMaterial();
                    BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();

                    if (lotCount == 1)
                    {
                        productStyleMaster = product_BuyerStyleManager.GetOurStyle(dr[1].ToString());
                        bomno = dr[1].ToString();
                        if (productStyleMaster != null && productStyleMaster.ProductOrBuyerStyleId != 0)
                        {
                            BillOfMaterial isExistBOM = new BillOfMaterial();
                            isExistBOM = billOfMaterialManager.GetBomNO(dr[1].ToString());
                            if (isExistBOM == null || isExistBOM.BomId == 0)
                            {
                                ID++;
                                if (lotCount == 1)
                                {
                                    billOfMaterial_.BomNo = dr[1].ToString();
                                    billOfMaterial_.Description = dr[1].ToString();

                                    billOfMaterial_.BuyerMasterId = productStyleMaster.BuyerName_ProductGroup;
                                    billOfMaterial_.BuyerModel = productStyleMaster.BuyerModel_ProductType.ToString();
                                    billOfMaterial_.MeanSize = 121;
                                    billOfMaterial_.Date = DateTime.Now;
                                    billOfMaterial_.ParentBomNo = null;
                                    string LastBOmNO = billOfMaterialManager.GetLastbomNumber();
                                    billOfMaterial_.LastBomNoEntered = LastBOmNO;
                                    billOfMaterial_.LinkBomNo = productStyleMaster.ProductOrBuyerStyleId.ToString();
                                    billOfMaterial_.Ammendment = string.Empty;
                                    billOfMaterial_.CommonBomNo = string.Empty;
                                    billOfMaterial_.CommnBOM1 = string.Empty;
                                    billOfMaterial_.CommnBOM2 = string.Empty;
                                    billOfMaterial_.CommnBOM3 = string.Empty;
                                    billOfMaterial_.CommnBOM4 = string.Empty;
                                    billOfMaterial_.CommnBOM5 = string.Empty;
                                    billOfMaterial_.PreparedBy = string.Empty;
                                    billOfMaterial_.VerifiedBy = string.Empty;
                                    billOfMaterial_.CheckedBy = string.Empty;
                                    billOfMaterial_.CreatedDate = DateTime.Now;
                                    billOfMaterial_.UpdatedDate = null;
                                    billOfMaterial_.CreatedBy = string.Empty;
                                    billOfMaterial_.UpdatedBy = string.Empty;
                                    billOfMaterial_.MaterialMasterId = null;
                                    billOfMaterial_.MaterialCategoryMasterId = null;
                                    billOfMaterial_.MaterialGroupMasterId = null;
                                    billOfMaterial_.ColorMasterId = null;
                                    billOfMaterial_.SubstanceMasterId = null;
                                    billOfMaterial_.SampleNorms = null;
                                    billOfMaterial_.Wastage = null;
                                    billOfMaterial_.SupplierMasterId = null;
                                    billOfMaterial_.UomMasterId = null;
                                    billOfMaterial_.SizeRangeMasterId = null;
                                    billOfMaterial_.SizeScheduleMasterId = null;
                                    billOfMaterial_.WastageQty = null;
                                    billOfMaterial_.WastageQtyUOM = null;
                                    billOfMaterial_.TotalNorms = null;
                                    billOfMaterial_.TotalNormsUOM = null;
                                    billOfMaterial_.ComponentName = null;
                                    billOfMaterial_.NoOfSets = null;
                                    billOfMaterial_.BuyerNorms = null;
                                    billOfMaterial_.OurNormsPercent = null;
                                    billOfMaterial_.PurchaseNorms = null;
                                    billOfMaterial_.PurchaseNormsPercent = null;
                                    billOfMaterial_.IssueNorms = null;
                                    billOfMaterial_.IssueNormsPercent = null;
                                    billOfMaterial_.CostingNorms = null;
                                    billOfMaterial_.CostingNormsPercent = null;
                                    billOfMaterial_.OurNorms = null;
                                    billOfMaterial_.DeletedBy = null;
                                    billOfMaterial_.IsDeleted = false;
                                    billOfMaterial_.DeletedOn = null;
                                    billOfMaterial_.RequirementQuantity = null;
                                    billOfMaterial_.IsMRP = false;
                                    bomid = bomDetails.BomId;
                                    listBillOfMaterial.Add(billOfMaterial_);
                                }
                            }
                            else
                            {
                                BOMError = "BOM is not Updated:" + bomno;
                            }
                        }
                        else
                        {
                            BOMError = "Insert Product style:" + bomno;
                        }
                        if (!string.IsNullOrEmpty(BOMError))
                        {
                            Session["BOMerror"] = null;
                            Session["BOMerror"] = BOMError.TrimEnd(',');
                            return RedirectToAction("BOMMaterialListGrid");
                        }
                        lotCount++;
                    }
                    if (bomDetails != null)
                    {
                        BOMMaterial bomMaterial = new BOMMaterial();
                        materialNameMaster = materialNameManager.GetMaterialName(dr[2].ToString());
                        if (materialNameMaster != null && materialNameMaster.MaterialMasterID != 0)
                        {
                            materialMaster = materialManager.GetMaterialName_(materialNameMaster.MaterialMasterID);
                        }
                        if (materialMaster != null && materialMaster.MaterialMasterId != 0)
                        {
                            bomMaterial.BOMID = bomid.Value;
                            bomMaterial.MaterialName = materialMaster.MaterialMasterId;
                            bomMaterial.MaterialCategoryMasterId = materialMaster.MaterialCategoryMasterId;
                            bomMaterial.MaterialGroupMasterId = materialMaster.MaterialGroupMasterId;
                            bomMaterial.NoofSets = 1;
                            bomMaterial.ColorMasterId = materialMaster.ColorMasterId.Value;
                            bomMaterial.RequiredQty = Convert.ToDecimal(dr[4].ToString());
                            bomMaterial.SubstanceMasterId = materialMaster.SubstanceMasterId.Value;
                            bomMaterial.SupplierMasterID = null;
                            bomMaterial.WastageQtyUOM = materialMaster.Uom;
                            bomMaterial.TotalNormsUOM = materialMaster.UomUnit;
                            bomMaterial.CreatedDate = DateTime.Now;
                            bomMaterial.UpdatedDate = null;
                            bomMaterial.Deletedon = null;
                            bomMaterial.IsDeleted = false;
                            bomMaterial.IsMRP = false;
                            bomMaterial.DeletedBy = string.Empty;
                            bomMaterial.Rate = Convert.ToDecimal(materialMaster.Price);
                            BOMMaterial isCheckBOMMaterial = new BOMMaterial();
                            listBomMaterial.Add(bomMaterial);
                            if (isCheckBOMMaterial == null)
                            {
                                BOMError = "BOM Material is not saved:" + dr[1].ToString() + ":" + materialNameMaster.MaterialDescription;
                            }
                        }
                        else if (materialNameMaster == null || materialNameMaster.MaterialMasterID == 0)
                        {
                            BOMError += "Material Name Mater not added:" + dr[2].ToString();
                        }
                        else if (materialMaster == null || materialMaster.MaterialMasterId == 0)
                        {
                            BOMError += "Material  Mater not added:" + dr[2].ToString();
                        }
                        if (!string.IsNullOrEmpty(BOMError))
                        {
                            Session["BOMerror"] = null;
                            Session["BOMerror"] = BOMError.TrimEnd(',');
                            return RedirectToAction("BOMMaterialListGrid");
                        }
                        else
                        {
                            listBomMaterial.Add(bomMaterial);
                        }
                    }
                    else if (bomDetails == null)
                    {
                        BOMError = "BOM is not saved:" + dr[1].ToString();
                    }

                    else if (productStyleMaster == null)
                    {
                        BOMError = "Product Style is not Created:" + dr[1].ToString();
                    }
                    if (!string.IsNullOrEmpty(BOMError))
                    {
                        Session["BOMerror"] = null;
                        Session["BOMerror"] = BOMError.TrimEnd(',');
                        return RedirectToAction("BOMMaterialListGrid");
                    }

                }
                //Check error end

                //Insert record to bom and bomMaterial table start
                DataTable dataTable = new DataTable();
                dataTable = result.Tables[0];
                int iterationCount = 1;
                foreach (DataRow dr in dataTable.Rows)
                {
                    Session["SuccessOrder"] = "Excel data imported Successfully";
                    List<InternalOrderEntryForm> listOrderEntryFormList = new List<InternalOrderEntryForm>();
                    BillOfMaterial billOfMaterial_ = new BillOfMaterial();
                    Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
                    Product_BuyerStyleMaster productStyleMaster = new Product_BuyerStyleMaster();
                    MaterialManager materialManager = new MaterialManager();
                    MaterialMaster materialMaster = new MaterialMaster();
                    MaterialNameManager materialNameManager = new MaterialNameManager();
                    MaterialNameMaster materialNameMaster = new MaterialNameMaster();
                    BillOfMaterial bomDetails = new BillOfMaterial();
                    BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
                    if (iterationCount == 1)
                    {
                        productStyleMaster = product_BuyerStyleManager.GetOurStyle(dr[1].ToString());
                        if (productStyleMaster != null && productStyleMaster.ProductOrBuyerStyleId != 0)
                        {
                            BillOfMaterial isExistBOM = new BillOfMaterial();
                            isExistBOM = billOfMaterialManager.GetBomNO(dr[1].ToString());
                            if (isExistBOM == null || isExistBOM.BomId == 0)
                            {
                                ID++;
                                if (iterationCount == 1)
                                {
                                    billOfMaterial_.BomNo = dr[1].ToString();
                                    billOfMaterial_.Description = dr[1].ToString();

                                    billOfMaterial_.BuyerMasterId = productStyleMaster.BuyerName_ProductGroup;
                                    billOfMaterial_.BuyerModel = productStyleMaster.BuyerModel_ProductType.ToString();
                                    billOfMaterial_.MeanSize = 121;
                                    billOfMaterial_.Date = DateTime.Now;
                                    billOfMaterial_.ParentBomNo = null;
                                    string LastBOmNO = billOfMaterialManager.GetLastbomNumber();
                                    billOfMaterial_.LastBomNoEntered = LastBOmNO;
                                    billOfMaterial_.LinkBomNo = productStyleMaster.ProductOrBuyerStyleId.ToString();
                                    billOfMaterial_.Ammendment = string.Empty;
                                    billOfMaterial_.CommonBomNo = string.Empty;
                                    billOfMaterial_.CommnBOM1 = string.Empty;
                                    billOfMaterial_.CommnBOM2 = string.Empty;
                                    billOfMaterial_.CommnBOM3 = string.Empty;
                                    billOfMaterial_.CommnBOM4 = string.Empty;
                                    billOfMaterial_.CommnBOM5 = string.Empty;
                                    billOfMaterial_.PreparedBy = string.Empty;
                                    billOfMaterial_.VerifiedBy = string.Empty;
                                    billOfMaterial_.CheckedBy = string.Empty;
                                    billOfMaterial_.CreatedDate = DateTime.Now;
                                    billOfMaterial_.UpdatedDate = null;
                                    billOfMaterial_.CreatedBy = string.Empty;
                                    billOfMaterial_.UpdatedBy = string.Empty;
                                    billOfMaterial_.MaterialMasterId = null;
                                    billOfMaterial_.MaterialCategoryMasterId = null;
                                    billOfMaterial_.MaterialGroupMasterId = null;
                                    billOfMaterial_.ColorMasterId = null;
                                    billOfMaterial_.SubstanceMasterId = null;
                                    billOfMaterial_.SampleNorms = null;
                                    billOfMaterial_.Wastage = null;
                                    billOfMaterial_.SupplierMasterId = null;
                                    billOfMaterial_.UomMasterId = null;
                                    billOfMaterial_.SizeRangeMasterId = null;
                                    billOfMaterial_.SizeScheduleMasterId = null;
                                    billOfMaterial_.WastageQty = null;
                                    billOfMaterial_.WastageQtyUOM = null;
                                    billOfMaterial_.TotalNorms = null;
                                    billOfMaterial_.TotalNormsUOM = null;
                                    billOfMaterial_.ComponentName = null;
                                    billOfMaterial_.NoOfSets = null;
                                    billOfMaterial_.BuyerNorms = null;
                                    billOfMaterial_.OurNormsPercent = null;
                                    billOfMaterial_.PurchaseNorms = null;
                                    billOfMaterial_.PurchaseNormsPercent = null;
                                    billOfMaterial_.IssueNorms = null;
                                    billOfMaterial_.IssueNormsPercent = null;
                                    billOfMaterial_.CostingNorms = null;
                                    billOfMaterial_.CostingNormsPercent = null;
                                    billOfMaterial_.OurNorms = null;
                                    billOfMaterial_.DeletedBy = null;
                                    billOfMaterial_.IsDeleted = false;
                                    billOfMaterial_.DeletedOn = null;
                                    billOfMaterial_.RequirementQuantity = null;
                                    billOfMaterial_.IsMRP = false;
                                    bomDetails = billOfMaterialManager.Post(billOfMaterial_);
                                    bomid = bomDetails.BomId;
                                }
                            }

                            else
                            {
                                BOMError = "BOM is not Updated:" + dr[1].ToString();
                            }
                        }
                        else
                        {
                            BOMError = "BOM is not Updated:" + dr[1].ToString();
                        }
                        iterationCount++;
                    }
                    if (bomDetails != null)
                    {
                        BOMMaterial bomMaterial = new BOMMaterial();
                        materialNameMaster = materialNameManager.GetMaterialName(dr[2].ToString());
                        if (materialNameMaster != null && materialNameMaster.MaterialMasterID != 0)
                        {
                            materialMaster = materialManager.GetMaterialName_(materialNameMaster.MaterialMasterID);
                        }
                        if (materialMaster != null && materialMaster.MaterialMasterId != 0)
                        {
                            bomMaterial.BOMID = bomid.Value;
                            bomMaterial.MaterialName = materialMaster.MaterialMasterId;
                            bomMaterial.MaterialCategoryMasterId = materialMaster.MaterialCategoryMasterId;
                            bomMaterial.MaterialGroupMasterId = materialMaster.MaterialGroupMasterId;
                            bomMaterial.NoofSets = 1;
                            bomMaterial.ColorMasterId = materialMaster.ColorMasterId.Value;
                            bomMaterial.RequiredQty = Convert.ToDecimal(dr[4].ToString());
                            bomMaterial.SubstanceMasterId = materialMaster.SubstanceMasterId.Value;
                            bomMaterial.SupplierMasterID = null;
                            bomMaterial.WastageQtyUOM = materialMaster.Uom;
                            bomMaterial.TotalNormsUOM = materialMaster.UomUnit;
                            bomMaterial.CreatedDate = DateTime.Now;
                            bomMaterial.UpdatedDate = null;
                            bomMaterial.Deletedon = null;
                            bomMaterial.IsDeleted = false;
                            bomMaterial.IsMRP = false;
                            bomMaterial.DeletedBy = string.Empty;
                            bomMaterial.Rate = Convert.ToDecimal(materialMaster.Price);
                            BOMMaterial isCheckBOMMaterial = new BOMMaterial();
                            isCheckBOMMaterial = billOfMaterialManager.BOMMaterialPost(bomMaterial);
                            if (isCheckBOMMaterial == null)
                            {
                                BOMError = "BOM Material is not saved:" + dr[1].ToString() + ":" + materialNameMaster.MaterialDescription;
                            }
                        }
                        else if (materialNameMaster == null || materialNameMaster.MaterialMasterID == 0)
                        {
                            BOMError += "Material Name Mater not added:" + dr[2].ToString();
                        }
                        else if (materialMaster == null || materialMaster.MaterialMasterId == 0)
                        {
                            BOMError += "Material Mater not added:" + dr[2].ToString();
                        }
                    }
                    else if (bomDetails == null)
                    {
                        BOMError = "BOM is not saved:" + dr[1].ToString();
                    }
                    else if (productStyleMaster == null)
                    {
                        BOMError = "Product Style is not Created:" + dr[1].ToString();
                    }
                    if (!string.IsNullOrEmpty(BOMError))
                    {
                        Session["BOMerror_"] = null;
                        Session["BOMerror_"] = BOMError.TrimEnd(',');
                        return RedirectToAction("BOMMaterialListGrid");
                    }
                }
            }

            else
            {
                ModelState.AddModelError("File", "Please Upload Your file");
            }
            if (BOMError != "")
            {
                Session["BOMerror"] = null;
                Session["BOMerror"] = BOMError.TrimEnd(',');
            }
            return RedirectToAction("BOMMaterialListGrid");

        }
        protected string GetBulkCopyColumnException(Exception ex, SqlBulkCopy bulkcopy)

        {
            string message = string.Empty;
            if (ex.Message.Contains("Received an invalid column length from the bcp client for colid"))

            {
                string pattern = @"\d+";
                Match match = Regex.Match(ex.Message.ToString(), pattern);
                var index = Convert.ToInt32(match.Value) - 1;

                FieldInfo fi = typeof(SqlBulkCopy).GetField("_sortedColumnMappings", BindingFlags.NonPublic | BindingFlags.Instance);
                var sortedColumns = fi.GetValue(bulkcopy);
                var items = (Object[])sortedColumns.GetType().GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(sortedColumns);

                FieldInfo itemdata = items[index].GetType().GetField("_metadata", BindingFlags.NonPublic | BindingFlags.Instance);
                var metadata = itemdata.GetValue(items[index]);
                var column = metadata.GetType().GetField("column", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                var length = metadata.GetType().GetField("length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                message = (String.Format("Column: {0} contains data with a length greater than: {1}", column, length));
            }
            return message;
        }

        #endregion
    }

}
