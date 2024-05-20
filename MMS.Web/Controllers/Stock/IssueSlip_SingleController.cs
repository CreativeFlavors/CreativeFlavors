using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Repository.Managers.StockManager;
using MMS.Core.Entities.Stock;
using MMS.Web.Models.StockModel;
using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;

namespace MMS.Web.Controllers.Stock
{
    [CustomFilter]
    public class IssueSlip_SingleController : Controller
    {
        #region MultipleIssue 
        public ActionResult MuplitpleIssue()
        {
            return View("~/Views/Stock/MultipleIssue/MuplitpleIssue.cshtml");
        }
        public ActionResult MultipleIssueGrid()
        {
            IssueSlip_SingleManager manager = new IssueSlip_SingleManager();
            List<singleissueslip> issueSlipList = new List<singleissueslip>();
            issueSlipList = manager.GetMultipleIssue();
            var query = issueSlipList.GroupBy(x => x.InternalOderID).ToList();

            IssueSlip_SingleModel model = new IssueSlip_SingleModel();

            model.IssueSlip_SingleModelList = issueSlipList;
            return PartialView("~/Views/Stock/MultipleIssue/Partial/MultipleIssueGrid.cshtml", model);
        }
        #endregion

        public ActionResult SingleIssueSlip()
        {
            return View("~/Views/Stock/IssueSlips/SingleIssueSlip.cshtml");
        }
        public ActionResult SingleIssueSlipGrid()
        {
            IssueSlip_SingleManager manager = new IssueSlip_SingleManager();
            List<singleissueslip> issueSlipList = new List<singleissueslip>();
            issueSlipList = manager.GetMultipleIssue();
            var query = issueSlipList.GroupBy(x => x.InternalOderID).ToList();

            IssueSlip_SingleModel model = new IssueSlip_SingleModel();

            model.IssueSlip_SingleModelList = issueSlipList;
            return PartialView("~/Views/Stock/IssueSlips/Partial/SingleIssueSlipGrid.cshtml", model);
        }
        public ActionResult IssueSlipSingle()
        {
            string SearchFilter = Request.QueryString["SearchFilter"];
            IssueSlip_SingleModel Model = new IssueSlip_SingleModel();
            IssueSlip_SingleManager ObjManager = new IssueSlip_SingleManager();
            if (SearchFilter == null)
            {
                Model.IssueSlip_SingleModelList = ObjManager.GetMultipleIssue();
            }
            else
            {
                Model.IssueSlip_SingleModelList = ObjManager.GetMultipleIssue().Where(x => x.IssueSlipNo.Contains(SearchFilter)).ToList();
            }
            return View(Model);
        }
        [HttpPost]
        public ActionResult SaveDetails(IssueSlip_SingleModel Model)
        {
            singleissueslip EntModel = new singleissueslip();
            IssueSlip_SingleManager ObjManager = new IssueSlip_SingleManager();
            //bool Result = false;

            EntModel.IssueSlipID = Model.IssueSlipID;
            EntModel.IssueSlipNo = Model.IssueSlipNo;
            EntModel.InternalOderID = Model.InternalOderID;
            EntModel.StyleNo = Model.Style;   
            EntModel.ConveyorID = Model.ConveyorID;
            EntModel.MachineName = Model.MachineName;
            EntModel.SubtanceID = Model.SubtanceID;
            EntModel.CurrentStock = Model.CurrentStock;
            EntModel.CurrentStockType = Model.CurrentStockType;
            EntModel.FreeStock = Model.FreeStock;
            EntModel.FreeStockType = Model.FreeStockType;
            EntModel.ReserverQty = Model.ReserverQty;
            EntModel.ReserverQtyType = Model.ReserverQtyType;
            EntModel.ClosingStockinAllStores = Model.ClosingStockinAllStores;
            EntModel.ClosingStockinAllStoredType = Model.ClosingStockinAllStoredType;
            EntModel.InTransitValue = Model.InTransitValue;
            EntModel.InTransitType = Model.InTransitType;
            string username = Session["UserName"].ToString();
            EntModel.CreatedBy = username;
            singleissueslip issueSlip = new singleissueslip();
            singleissueslip issueSlip_ = new singleissueslip();
            issueSlip = ObjManager.GetorderNo(Model.InternalOderID);
            if (issueSlip != null)
            {
                EntModel.IssueSlipID = issueSlip.IssueSlipID;
            }
            ObjManager.Post(EntModel);
            tbl_issueslipdetails issueSlip_MaterialDetails = new tbl_issueslipdetails();
            if (issueSlip != null)
            {
                IssueSlipManager issueSlipManager = new IssueSlipManager();
                tbl_issueslipdetails issueSlipMaterial = new tbl_issueslipdetails();
                tbl_issueslipdetails issueSlipMaterials = new tbl_issueslipdetails();
                issueSlipMaterials = issueSlipManager.GetGRNSelectedRow(Model.IssueSlipMaterialId);
                if (issueSlipMaterials != null)
                {
                    issueSlipMaterials.CurrentIssue = Model.CurrentIssue;
                    issueSlipMaterials.IsChecked = true;
                    issueSlipMaterials.IssueSlipFK = issueSlip.IssueSlipID;
                    issueSlip_MaterialDetails = issueSlipManager.Post(issueSlipMaterials);
                }

            }
            return Json(issueSlip_MaterialDetails, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetIssueDetails(int IssueSlipId)
        {
            IssueSlip_SingleModel model = new IssueSlip_SingleModel();
            singleissueslip arg = new singleissueslip();
            IssueSlip_SingleManager Manager = new IssueSlip_SingleManager();
            List<tbl_issueslipdetails> issueSlipMaterials = new List<tbl_issueslipdetails>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            if (IssueSlipId != 0)
            {
                arg = Manager.GetGRNSelectedRow(IssueSlipId);
                issueSlipMaterials = issueSlipManager.GetOrderNo(arg.InternalOderID);
                OrderEntry orderEntryForm = new OrderEntry();
                orderEntryForm = buyerOrderEntryManager.GetBuyerOderSlNo(arg.InternalOderID);
                model.OrderEntryId = orderEntryForm != null ? orderEntryForm.OrderEntryId : 0;
                model.issueSlip_MaterialDetails = issueSlipMaterials;
                SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();
                List<SizeRangeQtyRate> sizeRangeQtyRateList = new List<SizeRangeQtyRate>();
                SizeRangeMasterManager sizeRangeMasterManager = new SizeRangeMasterManager();
                List<SizeRangeMaster> sizeRangeMasterList = new List<SizeRangeMaster>();
                if (orderEntryForm != null)
                {
                    model.sizeRangeQtyRateList = sizeRangeQtyRateManager.GetSizeRangeByOrderEntryId(orderEntryForm.OrderEntryId);
                }
                else if (orderEntryForm == null)
                {
                    
                
                model.IssueSlipID = arg.IssueSlipID;
                model.IssueSlipNo = arg.IssueSlipNo;
                model.InternalOderID = arg.InternalOderID;
                //model.NoOfSets = orderEntryForm.TotalAmount.Value;
               /// model.OrderQty = orderEntryForm.TotalAmount;
                model.Style = arg.StyleNo;
                model.ConveyorID = arg.ConveyorID;
                model.MachineName = arg.MachineName;
                model.SubtanceID = arg.SubtanceID;
                model.CurrentStock = arg.CurrentStock;
                model.CurrentStockType = arg.CurrentStockType;
                model.FreeStock = arg.FreeStock;
                model.FreeStockType = arg.FreeStockType;
                model.ReserverQty = arg.ReserverQty;
                model.ReserverQtyType = arg.ReserverQtyType;
                model.ClosingStockinAllStores = arg.ClosingStockinAllStores;
                model.ClosingStockinAllStoredType = arg.ClosingStockinAllStoredType;
                model.InTransitValue = arg.InTransitValue;
                model.InTransitType = arg.InTransitType;
                }
            }
            return PartialView("~/Views/Stock/IssueSlips/Partial/SingleIssueSlipeDetails.cshtml", model);
        }
        public JsonResult DeleteIssueDetails(int IssueSlipId)
        {
            singleissueslip EntModel = new singleissueslip();
            IssueSlip_SingleManager ObjManager = new IssueSlip_SingleManager();
            string status = "";
            bool result = false;
            result = ObjManager.Delete(IssueSlipId);
            if (result == true)
            {
                status = "Success";
            }


            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetInternalBuyeSlNoWithMaterialName(string InternalOderID, string IssueType, string StoreName, string CategoryName)
        {
            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            MaterialManager materialManager = new MaterialManager();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            Bom billOfMaterial = new Bom();
            OrderEntry internalOrderEntryForm = new OrderEntry();
            Product_BuyerStyleManager productBuyerStyleManager = new Product_BuyerStyleManager();
            Product_BuyerStyleMaster productBuyerStyleMaster = new Product_BuyerStyleMaster();
            List<BOMMaterial> bomMaterialList = new List<BOMMaterial>();
            IssueSlip_SingleManager issueSlip_SingleManager = new IssueSlip_SingleManager();
            internalOrderEntryForm = buyerOrderEntryManager.GetBuyerOderSlNo(InternalOderID);
            internalOrderEntryForm.BomNo = internalOrderEntryForm.OurStyle;
            productBuyerStyleMaster = productBuyerStyleManager.GetProductOrBuyerStyleId(Convert.ToInt32(internalOrderEntryForm.OurStyle));
            billOfMaterial = billOfMaterialManager.GetBomNO(productBuyerStyleMaster.BomNo);
            List<MMS.Web.Models.SPBomMaterialList> spBOMMaterialList = new List<Models.SPBomMaterialList>();
            spBOMMaterialList = issueSlip_SingleManager.GetBOMMaterialforIssueSlip(billOfMaterial.BomId).ToList();
            SizeRangeQtyRateManager sizerangeQtyRateManager = new SizeRangeQtyRateManager();
            List<SizeRangeQtyRate> sizeRangeQtyRate = new List<SizeRangeQtyRate>();
            if (internalOrderEntryForm != null)
            {
                sizeRangeQtyRate = sizerangeQtyRateManager.GetSizeRangeByOrderEntryId(internalOrderEntryForm.OrderEntryId);
            }
            if (StoreName != "")
            {
                spBOMMaterialList = spBOMMaterialList.Where(x => x.StoreName == StoreName).ToList();
            }
            if (CategoryName != "")
            {
                spBOMMaterialList = spBOMMaterialList.Where(x => x.CategoryName == CategoryName).ToList();
            }

            List<tbl_issueslipdetails> issueSlipMaterialList = new List<tbl_issueslipdetails>();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            foreach (var item in spBOMMaterialList)
            {
                tbl_issueslipdetails issueSlip_MaterialDetails = new tbl_issueslipdetails();

                tbl_issueslipdetails issueSlipMaterial = new tbl_issueslipdetails();
                tbl_issueslipdetails issueSlipMaterial_ = new tbl_issueslipdetails();
                issueSlipMaterial.IssueSlipFK = 0;
                issueSlipMaterial.OrderNo = internalOrderEntryForm.BuyerOrderSlNo;
                issueSlipMaterial.Style = string.Empty;

                issueSlipMaterial.IssueType = IssueType;
                issueSlipMaterial.NoOfSets = item.NoOfSets;
                issueSlipMaterial.StoreName = item.StoreName;
                issueSlipMaterial.MaterialName = item.MaterialDescription;
                issueSlipMaterial.ColourId = item.Color;
                issueSlipMaterial.CategoryId = item.CategoryName;
                issueSlipMaterial.GroupId = item.GroupName;
                issueSlipMaterial.RequiredQty = item.RequiredQty;
                issueSlipMaterial.AlredayIssued = 0;
                issueSlipMaterial.IsChecked = false;
                issueSlipMaterial.CurrentIssue = item.RequiredQty;
                issueSlipMaterial.Rate = item.Rate;
                issueSlipMaterial.Piecies = item.Piecies == null ? 0 : item.Piecies;
                issueSlipMaterial.CurrentStock = item.CurrentStock == null ? 0 : item.CurrentStock;
                issueSlipMaterial_ = issueSlipManager.GetIssueSlipMaterial(internalOrderEntryForm.BuyerOrderSlNo, item.MaterialDescription);
                if (issueSlipMaterial_ == null)
                {
                    issueSlip_MaterialDetails = issueSlipManager.Post(issueSlipMaterial);
                }
            }
            issueSlipMaterialList = issueSlipManager.GetOrderNo(InternalOderID);
            if (StoreName != "")
            {
                issueSlipMaterialList = issueSlipMaterialList.Where(x => x.StoreName == StoreName).ToList();
            }
            if (CategoryName != "")
            {
                issueSlipMaterialList = issueSlipMaterialList.Where(x => x.CategoryId == CategoryName).ToList();
            }
            return Json(new { Material = issueSlipMaterialList, TotalAmount = internalOrderEntryForm.TotalAmount, SizeRange = sizeRangeQtyRate }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategoryIDWithMaterialName(int CategoryID, int StoresID)
        {
            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            MaterialManager materialManager = new MaterialManager();
            var internalMaterialList = (from x in buyerOrderEntryManager.Get()
                                        join z in materialManager.Get()
                                        on x.LeatherDescription equals z.MaterialMasterId.ToString()
                                        join y in materialNameManager.Get()
                                        on z.MaterialName equals y.MaterialMasterID

                                        where x.IsInternal = true && z.MaterialCategoryMasterId == CategoryID && z.StoreMasterId == StoresID
                                       
                                        select new { MaterialDescription = y.MaterialDescription, MaterialMasterID = z.MaterialMasterId });

            var distinctList = internalMaterialList.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            List<System.Web.Mvc.SelectListItem> items = distinctList.Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.MaterialDescription,
                                        Value = item.MaterialMasterID.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStoredIDWithMaterialName(int StoredID)
        {
            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            MaterialManager materialManager = new MaterialManager();
            var internalMaterialList = (from x in buyerOrderEntryManager.Get()
                                        join z in materialManager.Get()
                                        on x.LeatherDescription equals z.MaterialMasterId.ToString()
                                        join y in materialNameManager.Get()
                                        on z.MaterialName equals y.MaterialMasterID

                                        where x.IsInternal = true && z.StoreMasterId == StoredID
                                        select new { MaterialDescription = y.MaterialDescription, MaterialMasterID = z.MaterialMasterId });

            var distinctList = internalMaterialList.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            List<System.Web.Mvc.SelectListItem> items = distinctList.Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.MaterialDescription,
                                        Value = item.MaterialMasterID.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMaterialDetails(int Material, string InternalOrderNo)
        {
            BuyerOrderEntryManager buyerOrderManager = new BuyerOrderEntryManager();
            OrderEntry internalOrderEntryForm = new OrderEntry();
            IssueSlip_SingleManager issueSlip_SingleManager = new IssueSlip_SingleManager();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            BOMMaterial bomMaterial = new BOMMaterial();
            MaterialOpeningStockManager MaterialOpeningStockManager = new MaterialOpeningStockManager();
            MaterialOpeningMaster materialOpeningMaster = new MaterialOpeningMaster();
            internalOrderEntryForm = buyerOrderManager.GetBuyerOrderSlNo(InternalOrderNo);
            decimal AlreadyIssued = issueSlip_SingleManager.GetBuyerOderSlNoWithmaterial(Material);
            bomMaterial = issueSlip_SingleManager.GetBomMaterialDetails(Material);
            materialOpeningMaster = MaterialOpeningStockManager.GetmaterialOpeningMaterialID(Material);
            decimal OpeningBal = 0;
            if (materialOpeningMaster != null)
            {
                OpeningBal = materialOpeningMaster.Qty;
            }
            return Json(new { BomMaterial = bomMaterial, AlreadyIssued = AlreadyIssued, LotNo = internalOrderEntryForm.LotNo, OpeningBalance = OpeningBal }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string filter)
        {
            IssueSlip_SingleModel model = new IssueSlip_SingleModel();
            singleissueslip EntObj = new singleissueslip();
            IssueSlip_SingleManager Manager = new IssueSlip_SingleManager();
            model.IssueSlip_SingleModelList = Manager.GetMultipleIssue();
            if (filter != null)
            {
                model.IssueSlip_SingleModelList = model.IssueSlip_SingleModelList.Where(x => (x.IssueSlipNo.ToString().ToLower().Trim().Contains(filter.ToLower().Trim()))).ToList();
            }
            return PartialView("~/Views/Stock/IssueSlips/Partial/SingleIssueSlipGrid.cshtml", model);
        }
    }
}
