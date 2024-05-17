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

namespace MMS.Web.Controllers.JobWork
{
    public class Job_BOMMaterialListController : Controller
    {
        //

        [CustomFilter]
        [HttpGet]
        public ActionResult Job_BOMMaterialListMaster()
        {
            BillOfMaterialModel Pm = new BillOfMaterialModel();
            return View("~/Views/Jobwork/Job_Master/BOM/BOM_Master.cshtml", Pm);
        }
        // GET: /Job_BOMMaterialList/
        #region Helper Method
        public ActionResult Job_BOMMaterialListGrid()
        {
            BillOfMaterialManager BillOfMaterialManager = new BillOfMaterialManager();
            List<BillOfMaterialGrid> bomGrid = new List<BillOfMaterialGrid>();
            List<Bom> bOMMaterialList = new List<Bom>();
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
            BillOfMaterialModel model = new BillOfMaterialModel();
            model.BOMMaterialLists = bomGrid;
            return View("~/Views/Jobwork/Job_Master/BOM/Partial/BOMGrid.cshtml", model);

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

            Bom arg = new Bom();
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
                model.WastageQtyUOM = arg.WastageQtyUOM != null ? arg.WastageQtyUOM.Value : 0;
                model.TotalNorms = arg.TotalNorms;
                model.TotalNormsUOM = arg.TotalNormsUOM != null ? arg.TotalNormsUOM.Value : 0;
                model.NoOfSets = arg.NoOfSets != null ? arg.NoOfSets.Value : 0;
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
                    model_.NoOfSets1 = model.NoOfSets;
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
            return View("~/Views/Jobwork/Job_Master/BOM/Partial/Bom_Detail.cshtml", model);
        }
        public ActionResult BillOfMaterialDetails(int? BOMID)
        {
            BillOfMaterialModel model = new BillOfMaterialModel();
            List<BOMMaterial> listBomMaterialList = new List<BOMMaterial>();
            List<BOMAmendmentMaterial> listBOMAmendmentMaterialList = new List<BOMAmendmentMaterial>();
            List<BOMMaterilGrid> materilGridList = new List<BOMMaterilGrid>();
            //int BOMID = Convert.ToInt32(Request.QueryString["BOMID"]);
            BillOfMaterialManager BillOfMaterialManager = new BillOfMaterialManager();
            Bom arg = new Bom();
            BOMMaterialListManager materialListManager = new BOMMaterialListManager();
            List<BOMMaterialList> materialList_ = new List<BOMMaterialList>();
            List<BomGridModel> bomGridModel = new List<BomGridModel>();
            List<bomgriddetail> bomGridList = new List<bomgriddetail>();
            Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
            List<Product_BuyerStyleMaster> product_BuyerStyleMasterList = new List<Product_BuyerStyleMaster>();
            bomGridList = BillOfMaterialManager.GetBomDetails(BOMID.GetValueOrDefault());
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
                arg = BillOfMaterialManager.GetbomId(BOMID.GetValueOrDefault());
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
                listBOMAmendmentMaterialList = BillOfMaterialManager.GetBomAmendmentMaterialBOMID(BOMID.GetValueOrDefault());
                listBomMaterialList = BillOfMaterialManager.GetBomMaterialBOMID(BOMID.GetValueOrDefault());
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
                    if (item.MaterialName == 4751)
                    {
                        string message = "";
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
                    model_.SupplierMasterId = Convert.ToInt32(item.SupplierMasterID);
                    model_.UomMasterId = model.UomMasterId;
                    model_.SizeRangeMasterId = model.SizeRangeMasterId;
                    model_.NoOfSets1 = model.NoOfSets;
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
                model.bomMaterialGridList = materilGridList.OrderBy(x => Convert.ToInt32(x.SNO)).ToList();
                model.amendmentmaterialList = listBOMAmendmentMaterialList;
            }
            return View("~/Views/Jobwork/Job_Master/BOM/Partial/Bom_Detail.cshtml", model);
        }

        #endregion
        [HttpPost]
        public ActionResult BillOfMaterialDetails(BillOfMaterialModel model, string Grid, string BOMMaterialID_, string CheckBoxsize, string CheckBoxIschecked, string Date_)
        {
            string Message = "";
            int bomID = 0;
            int id = Convert.ToInt32(Request.QueryString["id"]);
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            List<bomgriddetail> listofBOMGrid = new List<bomgriddetail>();
            bomgriddetail bomGrid_ = new bomgriddetail();
            GridModel bomGrid = new GridModel();
            Bom billOfMaterial = new Bom();
            BomGridDetailsModel gridModel = new BomGridDetailsModel();
            Bom billOfMaterial_ = new Bom();
            BOMAmendmentMaterial bomAmendmentMaterial_ = new BOMAmendmentMaterial();
            Bom billOfMaterialIsExist = new Bom();
            BOMAmendmentMaterial bomamendmentIsExist = new BOMAmendmentMaterial();
            BOMMaterial bomMaterial = new BOMMaterial();
            BOMAmendmentMaterial bomAmendmentMaterial = new BOMAmendmentMaterial();
            BOMMaterial bomMaterials = new BOMMaterial();
            MaterialManager materialManager = new MaterialManager();
            MaterialMaster materialMaster = new MaterialMaster();
            materialgroupmaster groupMaster = new materialgroupmaster();
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
            tbl_materialnamemaster materialNameMaste = new tbl_materialnamemaster();
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
            if (model.Date != null)
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
            groupMaster = GroupManager.GetmaterialgroupmasterId(billOfMaterial.MaterialGroupMasterId);
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
                    bomgriddetail GridDetails = new bomgriddetail();
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
                        bomgriddetail bomGrids = new bomgriddetail();
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

    }
}
