using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models;
using MMS.Web.Models.StockModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Stock
{
    [CustomFilter]
    public class MultipleIssueController : Controller
    {

        #region MultipleIssue 
        public ActionResult MuplitpleIssue(int? page)
        {
            List<Data.StoredProcedureModel.IssueSlip_SingleModel> contactList = new List<Data.StoredProcedureModel.IssueSlip_SingleModel>();
            IssueSlip_SingleManager manager = new IssueSlip_SingleManager();
            Data.StoredProcedureModel.IssueSlip_SingleModel model = new Data.StoredProcedureModel.IssueSlip_SingleModel();
            contactList = manager.GetMultipleIssueGrid("");
            List<MultipleIssueSlip> issueSlipList = new List<MultipleIssueSlip>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<OrderEntry> _items = new List<OrderEntry>();
            _items = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true).OrderBy(x => x.BuyerOrderSlNo).ToList();
            var pager = new Data.StoredProcedureModel.Pager(contactList.Count(), page);
            var viewModel = new Data.StoredProcedureModel.IssueSlip_SingleModel
            {
                IssueSlip_ModelList = contactList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager,
                orderList = _items
            };
            return View("~/Views/Stock/MultipleIssue/MuplitpleIssue.cshtml", viewModel);
        }
        public ActionResult MultipleIssueGrid(int? page)
        {

            List<Data.StoredProcedureModel.IssueSlip_SingleModel> contactList = new List<Data.StoredProcedureModel.IssueSlip_SingleModel>();
            IssueSlip_SingleManager manager = new IssueSlip_SingleManager();
            Data.StoredProcedureModel.IssueSlip_SingleModel model = new Data.StoredProcedureModel.IssueSlip_SingleModel();
            contactList = manager.GetMultipleIssueGrid("");
            List<MultipleIssueSlip> issueSlipList = new List<MultipleIssueSlip>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<OrderEntry> _items = new List<OrderEntry>();
            _items = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true).OrderBy(x => x.BuyerOrderSlNo).ToList();
            var pager = new Data.StoredProcedureModel.Pager(contactList.Count(), page);
            var viewModel = new Data.StoredProcedureModel.IssueSlip_SingleModel
            {
                IssueSlip_ModelList = contactList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager,
                orderList = _items
            };
            return PartialView("~/Views/Stock/MultipleIssue/Partial/MultipleIssueGrid.cshtml", viewModel);
        }

        public ActionResult MultipleIssueEdit()
        {
            int IssueSlipId = Convert.ToInt32(Request.QueryString["id"]);
            IssueSlip_SingleManager manager = new IssueSlip_SingleManager();
            Models.StockModel.IssueSlip_SingleModel model = new Models.StockModel.IssueSlip_SingleModel();
            MultipleIssueSlip arg = new MultipleIssueSlip();
            IssueSlip_SingleManager Manager = new IssueSlip_SingleManager();
            List<tbl_issueslipdetails> issueSlipMaterials = new List<tbl_issueslipdetails>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            tbl_issueslipdetails issueSlip_MaterialDetails = new tbl_issueslipdetails();
            var IssueSlip = Manager.GetMultipleIssueGRNSelectedRow(IssueSlipId);
            List<OrderEntry> _items = new List<OrderEntry>();
            BuyerOrderEntryManager buyerOrderManager = new BuyerOrderEntryManager();
            OrderEntry orderEntryList = new OrderEntry();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            tblautogenissueslipdetails autoGenIssueSlipDetails = new tblautogenissueslipdetails();
            tblautogenissueslipdetails autoGenIssue = new tblautogenissueslipdetails();
            AutoGenIssueSlipDetailsManager autoManager = new AutoGenIssueSlipDetailsManager();
            if (IssueSlipId == 0)
            {

                autoGenIssueSlipDetails = autoManager.Get().LastOrDefault();
                if (autoGenIssueSlipDetails != null)
                {
                    autoGenIssueSlipDetails = autoManager.Get().LastOrDefault();
                    var autoId = Convert.ToInt32(autoGenIssueSlipDetails.IssueSlipDetailsId);
                    autoId++;
                    autoGenIssue.IssueSlipDetailsId = autoId.ToString();
                    autoManager.Post(autoGenIssue);
                }
                else
                {
                    autoGenIssue.IssueSlipDetailsId = "1";
                    autoManager.Post(autoGenIssue);
                }
            }
            autoGenIssueSlipDetails = autoManager.Get().LastOrDefault();
            int ID = Convert.ToInt32(autoGenIssueSlipDetails.IssueSlipDetailsId);
            if (IssueSlipId != 0)
            {
                orderEntryList = buyerOrderManager.GetOrderEntryId(Convert.ToInt32(IssueSlip.LotNo));
                var internalMaterialList_ = (from x in buyerOrderManager.GetInternalIO()
                                             join y in purchaseOrderManager.Get() on x.OrderEntryId equals y.IONO
                                             where x.LotNo == orderEntryList.LotNo
                                             select new { BuyerPoNo = x.BuyerPoNo, OrderEntryId = x.OrderEntryId });
                var distinctList_ = internalMaterialList_.GroupBy(x => x.BuyerPoNo).Select(g => g.First()).ToList();

                foreach (var item in distinctList_)
                {
                    OrderEntry orderform = new OrderEntry();
                    orderform.BuyerPoNo = item.BuyerPoNo;
                    orderform.OrderEntryId = item.OrderEntryId;
                    _items.Add(orderform);
                }
                model.orderList = _items.GroupBy(x => x.BuyerPoNo).Select(g => g.First()).ToList();
            }
            else
            {
                var internalMaterialList = (from x in buyerOrderManager.GetInternalIO()
                                            join y in purchaseOrderManager.Get() on x.OrderEntryId equals y.IONO
                                            select new { BuyerPoNo = x.BuyerPoNo, OrderEntryId = x.OrderEntryId });
                var distinctList = internalMaterialList.GroupBy(x => x.BuyerPoNo).Select(g => g.First()).ToList();

                model.IssueSlipNo = ID.ToString();

                foreach (var item in distinctList)
                {
                    OrderEntry orderform = new OrderEntry();
                    orderform.BuyerPoNo = item.BuyerPoNo;
                    orderform.OrderEntryId = item.OrderEntryId;
                    _items.Add(orderform);
                }
                model.orderList = _items.GroupBy(x => x.BuyerPoNo).Select(g => g.First()).ToList();
            }

            if (IssueSlipId != 0)
            {
                arg = Manager.GetMultipleIssueGRNSelectedRow(IssueSlipId);
                issueSlipMaterials = issueSlipManager.GetMultipleIssueid(arg.MultipleIssueSlipID);
                var grouped = issueSlipMaterials
    .GroupBy(s => s.OrderNo)
    .Select(x => x.Key);
                OrderEntry orderEntryForm = new OrderEntry();
                orderEntryForm = buyerOrderEntryManager.GetBuyerOderSlNo(arg.InternalOderID);
                model.OrderEntryId = orderEntryForm != null ? orderEntryForm.OrderEntryId : 0;
                model.issueSlip_MaterialDetails = issueSlipMaterials;
                string orderNo = "";
                foreach (var itreation in grouped)
                {
                    orderNo += itreation + ",";
                }
                model.SelectedItemId = orderNo.TrimEnd(',');
                SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();
                List<SizeRangeQtyRate> sizeRangeQtyRateList = new List<SizeRangeQtyRate>();
                SizeRangeMasterManager sizeRangeMasterManager = new SizeRangeMasterManager();
                List<SizeRangeMaster> sizeRangeMasterList = new List<SizeRangeMaster>();
                List<SizeItemsIssueSlip> sizeItemsIssueSlipList = new List<SizeItemsIssueSlip>();
                model.MultipleIssueSlipID = arg.MultipleIssueSlipID;
                model.IssueSlipNo = arg.IssueSlipNo;
                model.IssueType = arg.IssueType;
                model.InternalOderID = arg.InternalOderID;
                model.CuttingIssueTypeID = arg.CuttingIssueType;
                model.LotNo = arg.LotNo;
                model.ConveyorID = arg.ConveyorID;
                model.MachineName = arg.MachineName;
                model.SubtanceID = arg.SubtanceID;
                GRNTypeManager grnTypeManager = new GRNTypeManager();
                GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
                grnTypeMaster = grnTypeManager.GetIssueTypeMasterId(arg.IssueType);
                model.IssueTypeName = grnTypeMaster.IssueType;
            }
            return PartialView("~/Views/Stock/MultipleIssue/Partial/MultipleIssueDetails.cshtml", model);
        }

        public ActionResult LoadOrderList()
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<OrderEntry> _items = new List<OrderEntry>();
            _items = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true).OrderBy(x => x.BuyerOrderSlNo).ToList();
            Models.StockModel.IssueSlip_SingleModel model = new Models.StockModel.IssueSlip_SingleModel();
            model.orderList = _items;
            return Json(model.orderList, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public ActionResult GetInternalOrderID(int InternalOrderno)
        //{
        //    InternalOrderForm internalOrderEntryForm = new InternalOrderForm();
        //    BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
        //    internalOrderEntryForm = buyerOrderEntryManager.GetInteranlOrderEntryId(InternalOrderno);
        //    return Json(internalOrderEntryForm, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult IssueSlipSingle()
        {
            string SearchFilter = Request.QueryString["SearchFilter"];
            Models.StockModel.IssueSlip_SingleModel Model = new Models.StockModel.IssueSlip_SingleModel();
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
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult SaveDetails(Models.StockModel.IssueSlip_SingleModel Model)
        {
            MultipleIssueSlip EntModel = new MultipleIssueSlip();
            IssueSlip_SingleManager ObjManager = new IssueSlip_SingleManager();
            EntModel.MultipleIssueSlipID = Model.MultipleIssueSlipID;
            EntModel.IssueSlipNo = Model.IssueSlipNo;
            EntModel.InternalOderID = Model.InternalOderID;
            EntModel.LotNo = Model.LotNo;
            EntModel.ConveyorID = Model.ConveyorID;
            EntModel.MachineName = Model.MachineName;
            EntModel.SubtanceID = Model.SubtanceID;
            EntModel.IssueType = Model.IssueType;
            string username = Session["UserName"].ToString();
            EntModel.CreatedBy = username;
            MultipleIssueSlip issueSlip = new MultipleIssueSlip();
            MultipleIssueSlip issueSlip_ = new MultipleIssueSlip();
            issueSlip = ObjManager.GetMultipleIssueorderNo(Model.InternalOderID);
            if (issueSlip != null)
            {
                EntModel.MultipleIssueSlipID = issueSlip.MultipleIssueSlipID;
            }
            ObjManager.MultipleIssuePost(EntModel);
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
                    issueSlipMaterials.IssueDate = Model.IssueDate.ToString();
                    issueSlipMaterials.MaterialType = Model.MaterialType;
                    issueSlipMaterials.BuyerMasterId = Model.BuyerMasterId;
                    issueSlipMaterials.BomStyle = Model.BomStyle;
                    issueSlipMaterials.Season = Model.Season;
                    issueSlipMaterials.InternalOrderingFor = Model.InternalOrderingFor;
                    issueSlipMaterials.IssueSlipFK = issueSlip.MultipleIssueSlipID;
                    issueSlip_MaterialDetails = issueSlipManager.Post(issueSlipMaterials);
                }
                if (Model.Size != null)
                {
                    string[] sizeAray = Model.Size.Split(',');
                    string[] RateAray = Model.Qty.Split(',');
                    int count = 0;
                    List<SizeItemsIssueSlip> listSizeItemsIssueSlip = new List<SizeItemsIssueSlip>();
                    listSizeItemsIssueSlip = issueSlipManager.GetSizeItemsIssueSlip(issueSlipMaterials.IssueSlipId);
                    foreach (var item in listSizeItemsIssueSlip)
                    {
                        issueSlipManager.SizeItemsIssueSlipDelete(item.SizeMaterialD);
                    }
                    foreach (var item in sizeAray)
                    {
                        SizeItemsIssueSlip sizeItemMaterial = new SizeItemsIssueSlip();
                        sizeItemMaterial.SizeRange = item;
                        sizeItemMaterial.Qty = Convert.ToDecimal(RateAray[count]);
                        sizeItemMaterial.MultipleIssueslipFk = issueSlipMaterials.IssueSlipId;
                        sizeItemMaterial.CreatedDate = DateTime.Now;
                        issueSlipManager.sizeItemsIssueSlipPost(sizeItemMaterial);
                        count++;
                    }
                }

            }
            return Json(issueSlip_MaterialDetails, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveDirectIssue(Models.StockModel.IssueSlip_SingleModel Model)
        {
            MultipleIssueSlip EntModel = new MultipleIssueSlip();
            IssueSlip_SingleManager ObjManager = new IssueSlip_SingleManager();
            List<tbl_issueslipdetails> issueSlipMaterialList = new List<tbl_issueslipdetails>();
            EntModel.MultipleIssueSlipID = Model.MultipleIssueSlipID;
            EntModel.IssueSlipNo = Model.IssueSlipNo;
            EntModel.InternalOderID = Model.InternalOderID;
            EntModel.LotNo = Model.LotNo;
            EntModel.ConveyorID = Model.ConveyorID;
            EntModel.MachineName = Model.MachineName;
            EntModel.IssueType = Model.IssueType;
            EntModel.SubtanceID = Model.SubtanceID;
            if (Model != null && Model.IssueSlipFK != null && Model.IssueSlipFK != 0)
            {
                EntModel.MultipleIssueSlipID = Model.IssueSlipFK.Value;
            }
            string username = Session["UserName"].ToString();
            EntModel.CreatedBy = username;
            MultipleIssueSlip issueSlip = new MultipleIssueSlip();
            MultipleIssueSlip issueSlip_ = new MultipleIssueSlip();
            GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
            tbl_issueslipdetails issueSlip_MaterialDetails = new tbl_issueslipdetails();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            tbl_issueslipdetails issueSlipMaterial = new tbl_issueslipdetails();
            tbl_issueslipdetails issueSlipMaterials = new tbl_issueslipdetails();
            issueSlip_ = ObjManager.MultipleIssuePost(EntModel);

            if (issueSlip_ != null && issueSlip_.MultipleIssueSlipID != 0)
            {
                GRNTypeManager grnTypeManager = new GRNTypeManager();

                StoreMaster storeMaster = new StoreMaster();
                StoreMasterManager storeMasterManager = new StoreMasterManager();
                MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
                MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
                MaterialGroupManager materialGroupManager = new MaterialGroupManager();
                materialgroupmaster materialGroupMaster = new materialgroupmaster();
                MaterialMaster materialMaster = new MaterialMaster();
                MaterialManager materialManager = new MaterialManager();
                tbl_materialnamemaster materialNameMaster = new tbl_materialnamemaster();
                MaterialNameManager materialNameManager = new MaterialNameManager();
                ColorManager colorManager = new ColorManager();
                ColorMaster colorMaster = new ColorMaster();
                SubstanceMaster substanceMaster = new SubstanceMaster();
                SubstanceMasterManager substanceMasterManager = new SubstanceMasterManager();
                storeMaster = storeMasterManager.GetStoreMasterId(Convert.ToInt32(Model.StoreName));
                grnTypeMaster = grnTypeManager.GetIssueTypeMasterId(Model.IssueType);
                materialCategoryMaster = materialCategoryManager.GetMaterialCategoryMaster(Convert.ToInt32(Model.CategoryName));
                materialGroupMaster = materialGroupManager.GetmaterialgroupmasterId(Convert.ToInt32(Model.GroupName));
                materialMaster = materialManager.GetMaterialMasterId(Convert.ToInt32(Model.MaterialDescription));
                if (materialMaster != null && materialMaster.MaterialMasterId != 0)
                {
                    materialNameMaster = materialNameManager.GetMaterialNameMasterId(materialMaster.MaterialName);
                    issueSlipMaterials.MaterialName = materialNameMaster.MaterialDescription;
                    issueSlipMaterials.MaterialMasterId = materialMaster.MaterialMasterId;
                }
                else
                {
                    issueSlipMaterials.MaterialName = "";
                    issueSlipMaterials.MaterialMasterId = Convert.ToInt32(Model.MaterialDescription);
                }
                colorMaster = colorManager.GetColorMasterID(Convert.ToInt32(Model.Color));
                issueSlipMaterials.IssueSlipFK = EntModel.MultipleIssueSlipID;
                issueSlipMaterials.IssueType = grnTypeMaster.GrnTypeMasterId.ToString();
                issueSlipMaterials.Style = Model.Style;
                issueSlipMaterials.NoOfSets = Model.NoOfSets;
                issueSlipMaterials.MachineName = Model.MachineName;
                issueSlipMaterials.SubtanceID = Model.SubtanceID;
                issueSlipMaterials.LotNo = !string.IsNullOrEmpty(Model.LotNo) ? Model.LotNo.ToString() : "";
                issueSlipMaterials.RequiredType = Model.RequiredType;
                issueSlipMaterials.IssueDate = !string.IsNullOrEmpty(Model.IssueDate) ? Model.IssueDate : Model.Date.ToString();
                issueSlipMaterials.AlreadyUsedType = Model.AlreadyUsedType;
                issueSlipMaterials.CurrentIssuesType = Model.CurrentIssuesType;
                issueSlipMaterial.MaterialType = Model.MaterialType;
                issueSlipMaterial.InternalOrderingFor = Model.InternalOrderingFor;
                issueSlipMaterial.BuyerMasterId = Model.BuyerMasterId;
                issueSlipMaterials.PiecesRequiredQTY = Convert.ToDecimal(Model.PiecesRequiredQTY);
                issueSlipMaterials.PiecesRequiredQtyType = Model.PiecesRequiredQtyType;
                issueSlipMaterials.TotalQty = Model.TotalQty;
                issueSlipMaterials.PiecesAlreadyIssue = Convert.ToDecimal(Model.PiecesAlreadyIssue);
                issueSlipMaterials.PiecesAlreadyIssueType = Model.PiecesAlreadyIssueType;

                issueSlipMaterials.PiecesCurrentIssue = Convert.ToDecimal(Model.PiecesCurrentIssue);
                issueSlipMaterials.PiecesCurrentType = Model.PiecesCurrentType;
                issueSlipMaterials.IssueSlipId = Model.IssueSlipID;
                issueSlipMaterials.InternalValue = Model.InternalValue;
                issueSlipMaterials.DirectIssue_Style = Model.DirectIssue_Style;

                issueSlipMaterials.IssueSlipNo = Model.IssueSlipNo;
                issueSlipMaterials.ConveyorID = Model.ConveyorID;
                issueSlipMaterials.Season = Model.Season;
                if (storeMaster != null && storeMaster.StoreMasterId != 0)
                {
                    issueSlipMaterials.StoreName = storeMaster.StoreName;
                    issueSlipMaterials.StoreMasterId = storeMaster.StoreMasterId;
                }
                else
                {
                    issueSlipMaterials.StoreName = "";
                    issueSlipMaterials.StoreMasterId = 0;
                }
                issueSlipMaterials.ColourId = Model.Color;
                if (materialCategoryMaster != null && materialCategoryMaster.MaterialCategoryMasterId != 0)
                {
                    issueSlipMaterials.CategoryId = materialCategoryMaster.CategoryName;
                }
                else
                {
                    issueSlipMaterials.CategoryId = "";
                }
                if (materialGroupMaster != null && materialGroupMaster.MaterialCategoryMasterId != 0)
                {
                    issueSlipMaterials.GroupId = materialGroupMaster.GroupName;
                }
                else
                {
                    issueSlipMaterials.GroupId = "";
                }

                issueSlipMaterials.RequiredQty = Model.RequiredQty;
                issueSlipMaterials.AlredayIssued = Model.AlredayIssued;
                issueSlipMaterials.CurrentIssue = Model.CurrentIssue;
                issueSlipMaterials.Piecies = Model.Piecies;
                issueSlipMaterials.CurrentStock = Model.CurrentStock;
                issueSlipMaterials.Rate = Model.Rate;
                issueSlipMaterials.IssueSlipType = "Multiple";
                issueSlipMaterials.IsChecked = true;
                issueSlipMaterials.MaterialType = Model.MaterialType;
                issueSlipMaterials.BuyerMasterId = Model.BuyerMasterId;
                issueSlipMaterials.InternalOrderingFor = Model.InternalOrderingFor;
                issueSlipMaterials.ColourId = colorMaster.Color;
                issueSlip_MaterialDetails = issueSlipManager.Post(issueSlipMaterials);
                issueSlipMaterialList = issueSlipManager.GetMultipleIssueid(EntModel.MultipleIssueSlipID);
                if (Model.Size != null)
                {
                    string[] sizeAray = Model.Size.Split(',');
                    string[] RateAray = Model.Qty.Split(',');
                    int count = 0;
                    List<SizeItemsIssueSlip> listSizeItemsIssueSlip = new List<SizeItemsIssueSlip>();
                    listSizeItemsIssueSlip = issueSlipManager.GetSizeItemsIssueSlip(issueSlipMaterials.IssueSlipId);
                    foreach (var item in listSizeItemsIssueSlip)
                    {
                        issueSlipManager.SizeItemsIssueSlipDelete(item.SizeMaterialD);
                    }
                    RateAray = RateAray.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    foreach (var item in sizeAray)
                    {
                        SizeItemsIssueSlip sizeItemMaterial = new SizeItemsIssueSlip();
                        sizeItemMaterial.SizeRange = item;
                        sizeItemMaterial.Qty = Convert.ToDecimal(RateAray[count]);
                        sizeItemMaterial.MultipleIssueslipFk = issueSlipMaterials.IssueSlipId;
                        sizeItemMaterial.CreatedDate = DateTime.Now;
                        issueSlipManager.sizeItemsIssueSlipPost(sizeItemMaterial);
                        count++;
                    }
                }
            }
            return Json(new { issue = issueSlip_MaterialDetails, Parent = issueSlip_, grnTypeMaster = grnTypeMaster, issueSlipMaterialList = issueSlipMaterialList }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveAllIssue(string AllIssue, Models.StockModel.IssueSlip_SingleModel Model)
        {
            var SizeQuantityRate = JsonConvert.DeserializeObject<List<MultipleIssueMaterialsModel>>(AllIssue);
            MultipleIssueSlip EntModel = new MultipleIssueSlip();
            IssueSlip_SingleManager ObjManager = new IssueSlip_SingleManager();
            IssueSlipManager IssueSlipManager = new IssueSlipManager();
            string Message = "";
            List<tbl_issueslipdetails> issueSlipMaterialList = new List<tbl_issueslipdetails>();
            EntModel.MultipleIssueSlipID = Model.MultipleIssueSlipID;
            EntModel.IssueSlipNo = Model.IssueSlipNo;
            EntModel.InternalOderID = Model.InternalOderID;
            EntModel.LotNo = Model.LotNo;
            EntModel.ConveyorID = Model.ConveyorID;
            EntModel.MachineName = Model.MachineName;
            EntModel.IssueType = Model.IssueType;
            EntModel.SubtanceID = Model.SubtanceID;
            EntModel.IssueSlipNo = Model.IssueSlipNo;
            EntModel.ConveyorID = Model.ConveyorID;
            EntModel.LotNo = Model.LotNo;
            EntModel.StyleNo = Model.BomStyle.ToString();
            if (Model != null && Model.IssueSlipFK != null && Model.IssueSlipFK != 0)
            {
                EntModel.MultipleIssueSlipID = Model.IssueSlipFK.Value;
            }
            else if (SizeQuantityRate != null && SizeQuantityRate.FirstOrDefault().IssueSlipFK != 0)
            {
                EntModel.MultipleIssueSlipID = SizeQuantityRate.FirstOrDefault().IssueSlipFK.Value;
            }
            string username = Session["UserName"].ToString();
            EntModel.CreatedBy = username;
            MultipleIssueSlip issueSlip = new MultipleIssueSlip();
            GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
            tbl_issueslipdetails issueSlip_MaterialDetails = new tbl_issueslipdetails();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            tbl_issueslipdetails issueSlipMaterial = new tbl_issueslipdetails();

            issueSlip = ObjManager.MultipleIssuePost(EntModel);
            if (issueSlip != null && issueSlip.MultipleIssueSlipID != 0)
            {
                foreach (var item in SizeQuantityRate)
                {
                    tbl_issueslipdetails issueSlipMaterials = new tbl_issueslipdetails();
                    var items = ObjManager.GetMaterialIssue(item.OrderNo, item.MaterialName.ToString()).ToList();
                    MultipleIssueSlip multipleIssueSlip = new MultipleIssueSlip();
                    issueSlipMaterials.OrderNo = item.OrderNo;
                    issueSlipMaterials.Style = item.Style;
                    issueSlipMaterials.IssueType = item.IssueType;
                    issueSlipMaterials.NoOfSets = item.NoOfSets;
                    issueSlipMaterials.StoreName = item.StoreName;
                    issueSlipMaterials.MaterialName = item.MaterialName;
                    issueSlipMaterials.MaterialTypes = item.MaterialTypes;
                    issueSlipMaterials.MaterialMasterId = item.MaterialMasterId.Value;
                    issueSlipMaterials.StoreName = (item.StoreName);
                    issueSlipMaterials.StoreMasterId = Convert.ToInt32(item.StoreMasterId);
                    issueSlipMaterials.ColourId = item.ColourId;
                    issueSlipMaterials.CategoryId = item.CategoryId;
                    issueSlipMaterials.IssueSlipNo = Model.IssueSlipNo;
                    issueSlipMaterials.IssueDate = !string.IsNullOrEmpty(Model.IssueDate) ? Model.IssueDate : Model.Date.ToString();
                    issueSlipMaterials.GroupId = item.GroupId;
                    issueSlipMaterials.AlredayIssued = 0;
                    issueSlipMaterials.LotNo = Model.LotNo.ToString();
                    issueSlipMaterials.IssueType = Model.IssueType.ToString();
                    issueSlipMaterials.ConveyorID = Model.ConveyorID;
                    issueSlipMaterials.BuyerMasterId = Model.BuyerMasterId;
                    issueSlipMaterials.BomStyle = Model.BomStyle;
                    issueSlipMaterials.IssueSlipType = "Multiple";
                    issueSlipMaterials.Piecies = item.Piecies == null ? 0 : item.Piecies;
                    issueSlipMaterials.CurrentStock = item.CurrentStock == null ? 0 : item.CurrentStock;
                    issueSlipMaterials.IsChecked = item.IsChecked;
                    issueSlipMaterials.CurrentIssue = item.CurrentIssue;
                    issueSlipMaterials.RequiredQty = item.RequiredQty;
                    issueSlipMaterials.Rate = item.Rate;
                    issueSlipMaterials.IssueSlipId = item.IssueSlipId;
                    issueSlipMaterials.IssueSlipFK = issueSlip.MultipleIssueSlipID;

                    issueSlip_MaterialDetails = issueSlipManager.Post(issueSlipMaterials);
                    if (issueSlip_MaterialDetails != null)
                    {
                        Message = "Saved Sucessfully";
                    }
                }
            }
            else
            {
                Message = "Something went wrong";
            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteIssueDetails(int IssueSlipId)
        {
            MultipleIssueSlip EntModel = new MultipleIssueSlip();
            IssueSlip_SingleManager ObjManager = new IssueSlip_SingleManager();
            string status = "";
            bool result = false;
            result = ObjManager.MultilpleIssueDelete(IssueSlipId);
            if (result == true)
            {
                status = "Success";
            }


            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IssueslipItemDelete(int IssueSlipId)
        {
            MultipleIssueSlip EntModel = new MultipleIssueSlip();
            IssueSlip_SingleManager ObjManager = new IssueSlip_SingleManager();
            string status = "";
            bool result = false;
            tbl_issueslipdetails issueSlip_MaterialDetails = new tbl_issueslipdetails();
            List<tbl_issueslipdetails> issueSlipMaterialList = new List<tbl_issueslipdetails>();
            issueSlip_MaterialDetails = ObjManager.GetIssueslipID(IssueSlipId);
            result = ObjManager.IssueslipItemDelete(IssueSlipId);
            if (issueSlip_MaterialDetails != null && issueSlip_MaterialDetails.IssueSlipId != 0)
            {
                issueSlipMaterialList = ObjManager.GetIssueslipList(issueSlip_MaterialDetails.IssueSlipNo);
            }
            GRNTypeManager grnTypeManager = new GRNTypeManager();
            GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
            if (!string.IsNullOrEmpty(issueSlip_MaterialDetails.IssueType))
            {
                grnTypeMaster = grnTypeManager.GetIssueTypeMasterId(Convert.ToInt32(issueSlip_MaterialDetails.IssueType));
            }


            if (result == true)
            {
                status = "Success";
            }


            return Json(new { status = status, MaterialList = issueSlipMaterialList, grnTypeMaster = grnTypeMaster }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Helper Methods
        public ActionResult getIssueID(int IssueSlipID)
        {
            Models.StockModel.IssueSlip_SingleModel model = new Models.StockModel.IssueSlip_SingleModel();
            MultipleIssueSlip arg = new MultipleIssueSlip();
            IssueSlip_SingleManager Manager = new IssueSlip_SingleManager();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            OrderEntry internalOrderEntryForm = new OrderEntry();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            tbl_issueslipdetails issueSlipeDetails = new tbl_issueslipdetails();
            GRNTypeManager grnTypeManager = new GRNTypeManager();
            GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
            issueSlipeDetails = issueSlipManager.GetGRNSelectedRow(IssueSlipID);
            grnTypeMaster = grnTypeManager.GetIssueTypeMasterId(Convert.ToInt32(issueSlipeDetails.IssueType));
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            materialgroupmaster materialGroupMaster = new materialgroupmaster();
            MaterialMaster materialMaster = new MaterialMaster();
            MaterialManager materialManager = new MaterialManager();
            tbl_materialnamemaster materialNameMaster = new tbl_materialnamemaster();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            ColorManager colorManager = new ColorManager();
            ColorMaster colorMaster = new ColorMaster();
            SubstanceMaster substanceMaster = new SubstanceMaster();
            SubstanceMasterManager substanceMasterManager = new SubstanceMasterManager();
            IssueSlipManager issueSlip = new IssueSlipManager();
            grnTypeMaster = grnTypeManager.GetIssueTypeMasterId(Convert.ToInt32(issueSlipeDetails.IssueType));
            materialCategoryMaster = materialCategoryManager.GetCategoryName(issueSlipeDetails.CategoryId);
            materialGroupMaster = materialGroupManager.GetGroupName(issueSlipeDetails.GroupId);
            colorMaster = colorManager.GetColor(issueSlipeDetails.ColourId);
            issueSlipeDetails.ColourId = colorMaster.ColorMasterId.ToString();
            issueSlipeDetails.CategoryId = materialCategoryMaster.MaterialCategoryMasterId.ToString();
            issueSlipeDetails.GroupId = materialGroupMaster.MaterialGroupMasterId.ToString();
            internalOrderEntryForm = buyerOrderEntryManager.GetBuyerOderSlNo(issueSlipeDetails.InternalValue);
            if (internalOrderEntryForm != null && internalOrderEntryForm.OrderEntryId != 0)
            {
                issueSlipeDetails.InternalValue = internalOrderEntryForm.OrderEntryId.ToString();
            }
            List<SizeItemsIssueSlip> listSizeItemMaterial = new List<SizeItemsIssueSlip>();
            listSizeItemMaterial = issueSlipManager.GetSizeItemsIssueSlip(issueSlipeDetails.IssueSlipId);
            listSizeItemMaterial = listSizeItemMaterial.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();

            var issueSizewiseStockList = issueSlip.MaterialOpeningSizeRangeIssueStock(issueSlipeDetails.MaterialMasterId);
            issueSizewiseStockList = issueSizewiseStockList.Where(x => x.IssueSize1 != 0).ToList();

          //  var issueSizewiseStockList = issueSlipManager.MaterialOpeningSizeRangeIssueStock(issueSlipeDetails.MaterialMasterId);

            return Json(new { issueSlipeDetails = issueSlipeDetails, grnTypeMaster = grnTypeMaster, SizeRange = listSizeItemMaterial, SizeRange_new= issueSizewiseStockList, materialCategoryMaster = materialCategoryMaster, currentStocklist = issueSizewiseStockList }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetIssueDetails(int IssueSlipId)
        {
            Data.StoredProcedureModel.IssueSlip_SingleModel model = new Data.StoredProcedureModel.IssueSlip_SingleModel();
            MultipleIssueSlip arg = new MultipleIssueSlip();
            IssueSlip_SingleManager Manager = new IssueSlip_SingleManager();
            List<tbl_issueslipdetails> issueSlipMaterials = new List<tbl_issueslipdetails>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            tbl_issueslipdetails issueSlip_MaterialDetails = new tbl_issueslipdetails();
            var IssueSlip = Manager.GetMultipleIssueGRNSelectedRow(IssueSlipId);
            List<OrderEntry> _items = new List<OrderEntry>();
            BuyerOrderEntryManager buyerOrderManager = new BuyerOrderEntryManager();
            OrderEntry orderEntryList = new OrderEntry();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();

            tblautogenissueslipdetails autoGenIssueSlipDetails = new tblautogenissueslipdetails();
            tblautogenissueslipdetails autoGenIssue = new tblautogenissueslipdetails();
            AutoGenIssueSlipDetailsManager autoManager = new AutoGenIssueSlipDetailsManager();

            if (IssueSlipId == 0)
            {

                autoGenIssueSlipDetails = autoManager.Get().LastOrDefault();
                if (autoGenIssueSlipDetails != null)
                {
                    autoGenIssueSlipDetails = autoManager.Get().LastOrDefault();
                    var autoId = Convert.ToInt32(autoGenIssueSlipDetails.IssueSlipDetailsId);
                    autoId++;
                    autoGenIssue.IssueSlipDetailsId = autoId.ToString();
                    autoManager.Post(autoGenIssue);
                }
                else
                {
                    autoGenIssue.IssueSlipDetailsId = "1";
                    autoManager.Post(autoGenIssue);
                }
            }


            _items = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true).OrderBy(x => x.BuyerOrderSlNo).ToList();
            autoGenIssueSlipDetails = autoManager.Get().LastOrDefault();
            int ID = Convert.ToInt32(autoGenIssueSlipDetails.IssueSlipDetailsId);

            if (IssueSlipId != 0)
            {
                if (IssueSlip.LotNo != "" && IssueSlip.LotNo != null)
                {


                    var internalMaterialList_ = (from x in buyerOrderManager.GetInternalIO()
                                                 where x.LotNo == IssueSlip.LotNo.ToString()
                                                 select new { BuyerPoNo = x.BuyerPoNo, OrderEntryId = x.OrderEntryId });
                    var distinctList_ = internalMaterialList_.GroupBy(x => x.BuyerPoNo).Select(g => g.First()).ToList();

                    foreach (var item in distinctList_)
                    {
                        OrderEntry orderform = new OrderEntry();
                        orderform.BuyerPoNo = item.BuyerPoNo;
                        orderform.OrderEntryId = item.OrderEntryId;
                        _items.Add(orderform);
                    }
                    model.orderList = _items.GroupBy(x => x.BuyerPoNo).Select(g => g.First()).ToList();
                }
            }
            else
            {
                var internalMaterialList = (from x in buyerOrderManager.GetInternalIO()
                                            select new { BuyerPoNo = x.BuyerPoNo, OrderEntryId = x.OrderEntryId });
                var distinctList = internalMaterialList.GroupBy(x => x.BuyerPoNo).Select(g => g.First()).ToList();
                var distinctList1 = internalMaterialList.GroupBy(x => x.BuyerPoNo).ToList();
                model.IssueSlipNo = ID.ToString();

                foreach (var item in distinctList)
                {
                    OrderEntry orderform = new OrderEntry();
                    orderform.BuyerPoNo = item.BuyerPoNo;
                    orderform.OrderEntryId = item.OrderEntryId;
                    _items.Add(orderform);
                }
                model.orderList = _items.GroupBy(x => x.BuyerPoNo).Select(g => g.First()).ToList();
            }

            if (IssueSlipId != 0)
            {
                arg = Manager.GetMultipleIssueGRNSelectedRow(IssueSlipId);
                issueSlipMaterials = issueSlipManager.GetMultipleIssueid(arg.MultipleIssueSlipID);
                var grouped = issueSlipMaterials
    .GroupBy(s => s.OrderNo)
    .Select(x => x.Key);
                OrderEntry orderEntryForm = new OrderEntry();
                orderEntryForm = buyerOrderEntryManager.GetBuyerOderSlNo(arg.InternalOderID);
                model.OrderEntryId = orderEntryForm != null ? orderEntryForm.OrderEntryId : 0;
                model.issueSlip_MaterialDetails = issueSlipMaterials;
                string orderNo = "";
                foreach (var itreation in grouped)
                {
                    orderNo += itreation + ",";
                }
                model.SelectedItemId = orderNo.TrimEnd(',');
                SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();
                List<SizeRangeQtyRate> sizeRangeQtyRateList = new List<SizeRangeQtyRate>();
                SizeRangeMasterManager sizeRangeMasterManager = new SizeRangeMasterManager();
                List<SizeRangeMaster> sizeRangeMasterList = new List<SizeRangeMaster>();
                List<SizeItemsIssueSlip> sizeItemsIssueSlipList = new List<SizeItemsIssueSlip>();

                model.MultipleIssueSlipID = arg.MultipleIssueSlipID;
                model.IssueSlipNo = arg.IssueSlipNo;
                model.IssueType = arg.IssueType;
                model.InternalOderID = arg.InternalOderID;
                model.CuttingIssueTypeID = arg.CuttingIssueType;
                model.LotNo = arg.LotNo;
                model.ConveyorID = arg.ConveyorID;
                model.CategoryName = issueSlipMaterials.FirstOrDefault().CategoryId;
                model.GroupName = issueSlipMaterials.FirstOrDefault().GroupId;
                model.StoreName = issueSlipMaterials.FirstOrDefault().StoreName;
                model.CurrentIssuesType = arg.CuttingIssueType;
                model.Style = !string.IsNullOrEmpty(arg.StyleNo) ? Convert.ToInt32(arg.StyleNo) : 0;
                model.MachineName = arg.MachineName;
                model.SubtanceID = arg.SubtanceID;
                GRNTypeManager grnTypeManager = new GRNTypeManager();
                GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
                grnTypeMaster = grnTypeManager.GetIssueTypeMasterId(arg.IssueType);
                model.IssueTypeName = grnTypeMaster.IssueType;
            }
            return PartialView("~/Views/Stock/MultipleIssue/Partial/MultipleIssueDetails.cshtml", model);
        }
        public ActionResult GetInternalBuyeSlNoWithMaterialName(string InternalOderID, string IssueType, string StoreName, string CategoryName, string IssueSlipNo, string LotNo, string Style, string CuttingIssueTypeID)
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
            string[] internalOrder = null;
            if (!string.IsNullOrEmpty(InternalOderID))
            {
                internalOrder = InternalOderID.Split(',');
            }

            List<tbl_issueslipdetails> issueSlipMaterialList = new List<tbl_issueslipdetails>();
            SizeRangeQtyRateManager sizerangeQtyRateManager = new SizeRangeQtyRateManager();
            List<SizeRangeQtyRate> sizeRangeQtyRate = new List<SizeRangeQtyRate>();
            List<tbl_issueslipdetails> issueSlipMaterialList_ = new List<tbl_issueslipdetails>();
            string InternalOrder = "";
            string Message = "";
            string OrderNoArray = "";
            if (internalOrder != null)
            {
                foreach (var iteration in internalOrder)
                {
                    internalOrderEntryForm = buyerOrderEntryManager.GetOrderEntryId(Convert.ToInt32(iteration));
                    if (internalOrderEntryForm != null && internalOrderEntryForm.OrderEntryId != 0)
                    {
                        internalOrderEntryForm.BomNo = internalOrderEntryForm.OurStyle;
                        productBuyerStyleMaster = productBuyerStyleManager.GetProductOrBuyerStyleId(Convert.ToInt32(internalOrderEntryForm.OurStyle));
                        billOfMaterial = billOfMaterialManager.GetBomNO(productBuyerStyleMaster.BomNo);
                        InternalOrder += billOfMaterial.BomId + ",";
                        OrderNoArray += internalOrderEntryForm.BuyerOrderSlNo + ",";
                    }

                }
                InternalOrder = InternalOrder.TrimEnd(',');
                OrderNoArray = OrderNoArray.TrimEnd(',');
            }

            List<MMS.Web.Models.SPBomMaterialList> spBOMMaterialList = new List<Models.SPBomMaterialList>();

            spBOMMaterialList = issueSlip_SingleManager.GetInternalOrderMaterialforIssueSlip(OrderNoArray).ToList();
            if (StoreName != "" && StoreName != "Please Select")
            {
                spBOMMaterialList = spBOMMaterialList.Where(x => x.StoreName == StoreName).ToList();
            }
            if (CategoryName != "" && CategoryName != "Please Select")
            {
                spBOMMaterialList = spBOMMaterialList.Where(x => x.CategoryName == CategoryName).ToList();
            }
            if (CuttingIssueTypeID != "" && CuttingIssueTypeID != "Please Select" && CuttingIssueTypeID != "0")
            {
                spBOMMaterialList = spBOMMaterialList.Where(x => x.CuttingIssueTypeID == Convert.ToInt32(CuttingIssueTypeID)).ToList();
            }
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            MultipleIssueSlip multipleIssue = new MultipleIssueSlip();
            multipleIssue.IssueType = Convert.ToInt32(IssueType);
            multipleIssue.IssueSlipNo = IssueSlipNo;
            multipleIssue.InternalOderID = InternalOderID;
            if (!string.IsNullOrEmpty(LotNo))
            {
                multipleIssue.LotNo = (LotNo);
            }
            if (Style != "")
            {
                multipleIssue.StyleNo = Style;
            }
            multipleIssue.CuttingIssueType = Convert.ToInt32(CuttingIssueTypeID);
            IssueSlip_SingleManager issueSlip_manager = new IssueSlip_SingleManager();
            MultipleIssueSlip multipleIssueList = new MultipleIssueSlip();
            tbl_issueslipdetails checkIsExistIssueSlip_MaterialDetails = new tbl_issueslipdetails();
            int count = 0;
            OrderNoArray = OrderNoArray.TrimEnd();
            string[] OrderArray = null;
            if (!string.IsNullOrEmpty(OrderNoArray))
            {
                OrderArray = OrderNoArray.Split(',');
            }
            MultipleIssueSlip mulitiple = new MultipleIssueSlip();
            if (OrderArray != null)
            {
                foreach (var item in OrderArray)
                {
                    if (item != "")
                    {
                        checkIsExistIssueSlip_MaterialDetails = issueSlip_manager.isExixtorderNo(item);
                        if (checkIsExistIssueSlip_MaterialDetails != null)
                        {
                            if (checkIsExistIssueSlip_MaterialDetails != null && checkIsExistIssueSlip_MaterialDetails.IssueSlipFK != 0 && checkIsExistIssueSlip_MaterialDetails.RequiredQty > checkIsExistIssueSlip_MaterialDetails.CurrentIssue)
                            {

                            }
                            if (item != "" && checkIsExistIssueSlip_MaterialDetails != null && checkIsExistIssueSlip_MaterialDetails.IssueSlipFK != 0 && checkIsExistIssueSlip_MaterialDetails.RequiredQty == checkIsExistIssueSlip_MaterialDetails.CurrentIssue)
                            {
                                count++;
                                Message += checkIsExistIssueSlip_MaterialDetails.OrderNo + ",";
                            }
                        }
                    }

                }
            }


            if (count != 0)
            {
                Message = Message.TrimEnd(',');
                return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
            }
            else if (spBOMMaterialList.Count > 0)
            {

            }
            if (spBOMMaterialList.Count == 0)
            {
                string Nodata = "";
                return Json(new { Nodata = Nodata }, JsonRequestBehavior.AllowGet);
            }
            BuyerOrderEntryManager buyerOrderManager = new BuyerOrderEntryManager();
            BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
            List<DisplaySizeMaterial> listDispalySizeMaterial = new List<DisplaySizeMaterial>();
            SizeRangeQtyRateManager sizeRangeQtyRateMgr = new SizeRangeQtyRateManager();
            listDispalySizeMaterial = bomMaterialListManager.GetDisplaySizeMaterialGet();
            foreach (var item in spBOMMaterialList)
            {
                tbl_issueslipdetails issueSlip_MaterialDetails = new tbl_issueslipdetails();
                tbl_issueslipdetails issueSlipMaterial = new tbl_issueslipdetails();
                tbl_issueslipdetails issueSlipMaterial_ = new tbl_issueslipdetails();

                List<SizeRangeQtyRate> sizeRangeQtyRateList = new List<SizeRangeQtyRate>();
                OrderEntry orderEntry = new OrderEntry();
                orderEntry = buyerOrderManager.GetBuyerOderSlNo(item.BuyerOrderSlNo);
                Bom billOfMaterials = new Bom();
                BOMMaterial bomMaterial = new BOMMaterial();
                billOfMaterials = billOfMaterialManager.getLinkBomNumber(orderEntry.OurStyle);
                bomMaterial = billOfMaterialManager.getBOMIDWithMaterial(billOfMaterials.BomId, item.Materialmasterid.Value);
                listDispalySizeMaterial = listDispalySizeMaterial.Where(x => x.BOMmaterialID == bomMaterial.BOMMaterialID && x.SizeIsChecked == true).ToList();
                var SizeQuantityRt = sizeRangeQtyRateMgr.GetSizeRangeByOrderEntryId(orderEntry.OrderEntryId).OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();
                sizeRangeQtyRateList = SizeQuantityRt.Where(x => x.OrderEntryId == orderEntry.OrderEntryId).ToList();
                sizeRangeQtyRateList = sizeRangeQtyRateList.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();
                if (bomMaterial != null && bomMaterial.BOMMaterialID != 0)
                {
                    decimal? sizeqty = 0;
                    listDispalySizeMaterial = bomMaterialListManager.DisplaySizeMaterialGet(Convert.ToInt32(bomMaterial.BOMMaterialID)).Where(x => x.SizeIsChecked == true).ToList();
                    if (listDispalySizeMaterial != null && listDispalySizeMaterial.Count > 0)
                    {
                        foreach (var iteration in listDispalySizeMaterial)
                        {
                            sizeqty += sizeRangeQtyRateList.Where(x => x.SizeRange == iteration.SizeRange).Sum(x => x.Qty);

                        }
                        issueSlipMaterial.RequiredQty = sizeqty != null ? sizeqty : item.RequiredQty;
                        issueSlipMaterial.CurrentIssue = sizeqty != null ? sizeqty : item.RequiredQty;
                    }
                    else
                    {
                        issueSlipMaterial.RequiredQty = item.RequiredQty;
                        issueSlipMaterial.CurrentIssue = item.CurrentIssueQty != null ? item.CurrentIssueQty : item.RequiredQty;
                    }
                }
                issueSlipMaterial.IssueSlipFK = 0;
                issueSlipMaterial.OrderNo = item.BuyerOrderSlNo;
                issueSlipMaterial.Style = item.Style;
                issueSlipMaterial.IssueType = IssueType;
                issueSlipMaterial.NoOfSets = item.NoOfSets;
                issueSlipMaterial.StoreName = item.StoreName;
                issueSlipMaterial.MaterialName = item.MaterialDescription;
                issueSlipMaterial.MaterialMasterId = item.Materialmasterid.Value;
                issueSlipMaterial.StoreMasterId = item.StoreMasterId.Value;
                issueSlipMaterial.ColourId = item.Color;
                issueSlipMaterial.CategoryId = item.CategoryName;
                issueSlipMaterial.GroupId = item.GroupName;
                issueSlipMaterial.AlredayIssued = item.AlreadyIssued != null ? item.AlreadyIssued : 0;
                issueSlipMaterial.IsChecked = false;
                issueSlipMaterial.MaterialTypes = item.CuttingIssueType;
                issueSlipMaterial.IssueSlipType = "Multiple";
                issueSlipMaterial.Rate = item.Rate;
                issueSlipMaterial.Piecies = item.Piecies == null ? 0 : item.Piecies;
                issueSlipMaterial.CurrentStock = item.CurrentStock == null ? 0 : item.CurrentStock;
                List<MMS.Web.Models.PendingQty> ListOfPendingStockList = new List<PendingQty>();
                IssueSlipManager issueSlip = new IssueSlipManager();
                ListOfPendingStockList = issueSlip.MaterialOpeningStock(item.Materialmasterid.Value);
                issueSlipMaterial.BalanceStock = ListOfPendingStockList.Sum(x => x.BalanceStock);
                issueSlipMaterial_ = issueSlipManager.GetIssueSlipMaterial(internalOrderEntryForm.BuyerOrderSlNo, item.MaterialDescription);
                if (issueSlipMaterial_ == null)
                {
                    issueSlipMaterialList.Add(issueSlipMaterial);
                }
                else
                {
                    issueSlipMaterialList.Add(issueSlipMaterial);
                }
            }

            return Json(new { Material = issueSlipMaterialList, TotalAmount = internalOrderEntryForm.TotalAmount, SizeRange = sizeRangeQtyRate }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult MultipleIssueMaterialName(int? MaterialNameID, int? IssueSlipNo,string InternalOrderNo,string IssueDate)
        {
            IssueSlip_SingleManager issueSlipManager = new IssueSlip_SingleManager();
            IssueSlipManager issueSlip = new IssueSlipManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialManager materialManager = new MaterialManager();
            ApprovedPriceListManager approvedPricelistManager = new ApprovedPriceListManager();
            List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
            var issueItems=   issueSlipManager.IsExistsIssuewithMaterial(IssueSlipNo.ToString(), MaterialNameID.ToString(), InternalOrderNo);
            string[] formats = { "dd/MM/yyyy" };
           // var dateTime = DateTime.ParseExact(IssueDate, formats, new CultureInfo("en-US"), DateTimeStyles.None);
            DateTime FromDate = DateTime.ParseExact(IssueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (issueItems != null && issueItems.Count > 0)
            {
                return Json(new { Message = "Already existed" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                approvedPriceList = approvedPricelistManager.GetMaterialList(MaterialNameID.Value);
                if (approvedPriceList == null || approvedPriceList.Count == 0)
                {
                    string Message = "Please make a entry on approved price list";
                    return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                   
                        string CategoryName = "";
                        List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
                        ColorManager colorManager = new ColorManager();
                        var items = (from x in materialManager.Get()
                                     join y in materialNameManager.Get()
                                      on x.MaterialName equals y.MaterialMasterID
                                     join z in colorManager.Get()
                                     on x.ColorMasterId equals z.ColorMasterId into yG
                                     from y1 in yG.DefaultIfEmpty()
                                     where x.MaterialMasterId == MaterialNameID
                                     select new { MaterialDescription = string.Format("{0} {1} {2}", y.MaterialDescription, x.OptionMaterialValue, y1 != null ? y1.Color != null ? y1.Color : string.Empty : string.Empty), x.MaterialMasterId, x.Price, ColorMasterId = (y1 != null ? y1.ColorMasterId != 0 ? y1.ColorMasterId : 0 : 0), x.Uom, x.UomUnit, x.SizeRangeMasterId, x.SubstanceMasterId, x.MaterialCategoryMasterId, x.MaterialGroupMasterId, x.GradeMasterId, x.StoreMasterId, x.PurchasePrimary, x.PrimaryUnit, x.PurchasePacket, x.PacketUnit, x.CurrencyMasterId, x.isImport, x.isLocal, x.isImportCustomer });
                        int id = Convert.ToInt32(ExtensionMethod.HtmlHelper.CategoryName.Leathers);
                        var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
                        List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
                        listSizeItemMaterial = materialManager.GetSizeItemMaterial(distinctList.FirstOrDefault().MaterialMasterId);
                        listSizeItemMaterial = listSizeItemMaterial.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();
                        StoreMasterManager storeMasterManager = new StoreMasterManager();
                        StoreMaster storeMaster = new StoreMaster();
                        if (id == distinctList.FirstOrDefault().MaterialCategoryMasterId)
                        {
                            ExtensionMethod.HtmlHelper.CategoryName enumValue = (ExtensionMethod.HtmlHelper.CategoryName)id;
                            CategoryName = enumValue.ToString();
                        }
                        List<MMS.Web.Models.PendingQty> ListOfPendingStockList = new List<PendingQty>();
                        int MaterialType = distinctList.FirstOrDefault().isLocal == true ? 1 : distinctList.FirstOrDefault().isImportCustomer == true ? 2 : distinctList.FirstOrDefault().isImport == true ? 3 : 0;
                        ListOfPendingStockList = issueSlip.MaterialOpeningStockIssueDate(MaterialNameID.Value, FromDate);
                        var issueSizewiseStockList = issueSlip.MaterialOpeningSizeRangeIssueStock(MaterialNameID.Value);
                        storeMaster = storeMasterManager.GetStoreMasterId(distinctList.FirstOrDefault().StoreMasterId);
                        issueSizewiseStockList = issueSizewiseStockList.Where(x => x.IssueSize1 != 0).OrderBy(x => Convert.ToDecimal(x.IssueSize1)).ToList();
                        return Json(new { Material = distinctList, SizeRangelist = issueSizewiseStockList, store = storeMaster, CategoryName = CategoryName, BalanceStock = ListOfPendingStockList.Select(x => x.BalanceStock), approvedPriceList = approvedPriceList, fromdate_ = FromDate }, JsonRequestBehavior.AllowGet);
                   
                }

              
            }
           // return Json("", JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetMaterialWithCuttingSlip(string InternalOderID, string IssueType, string StoreName, string CategoryName, string IssueSlipNo, string LotNo, string Style, string CuttingIssueTypeID)
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
            string[] internalOrder = InternalOderID.Split(',');
            List<tbl_issueslipdetails> issueSlipMaterialList = new List<tbl_issueslipdetails>();
            SizeRangeQtyRateManager sizerangeQtyRateManager = new SizeRangeQtyRateManager();
            List<SizeRangeQtyRate> sizeRangeQtyRate = new List<SizeRangeQtyRate>();
            List<tbl_issueslipdetails> issueSlipMaterialList_ = new List<tbl_issueslipdetails>();
            tbl_issueslipdetails isCheckOrderNo = new tbl_issueslipdetails();
            string InternalOrder = "";
            string Message = "";
            string OrderNoArray = "";
            foreach (var iteration in internalOrder)
            {
                internalOrderEntryForm = buyerOrderEntryManager.GetOrderEntryId(Convert.ToInt32(iteration));
                internalOrderEntryForm.BomNo = internalOrderEntryForm.OurStyle;
                productBuyerStyleMaster = productBuyerStyleManager.GetProductOrBuyerStyleId(Convert.ToInt32(internalOrderEntryForm.OurStyle));
                billOfMaterial = billOfMaterialManager.GetBomNO(productBuyerStyleMaster.BomNo);
                InternalOrder += billOfMaterial.BomId + ",";
                OrderNoArray += internalOrderEntryForm.BuyerOrderSlNo + ",";
            }
            InternalOrder = InternalOrder.TrimEnd(',');
            List<MMS.Web.Models.SPBomMaterialList> spBOMMaterialList = new List<Models.SPBomMaterialList>();

            spBOMMaterialList = issueSlip_SingleManager.GetInternalOrderMaterialforIssueSlip(InternalOrder).ToList();
            if (StoreName != "" && StoreName != "Please Select")
            {
                spBOMMaterialList = spBOMMaterialList.Where(x => x.StoreName == StoreName).ToList();
            }
            if (CategoryName != "" && CategoryName != "Please Select")
            {
                spBOMMaterialList = spBOMMaterialList.Where(x => x.CategoryName == CategoryName).ToList();
            }
            if (LotNo != "" && LotNo != "Please Select")
            {
                spBOMMaterialList = spBOMMaterialList.Where(x => x.LotNO == Convert.ToInt32(LotNo)).ToList();
            }
            if (CuttingIssueTypeID != "" && CuttingIssueTypeID != "Please Select")
            {
                spBOMMaterialList = spBOMMaterialList.Where(x => x.CuttingIssueTypeID == Convert.ToInt32(CuttingIssueTypeID)).ToList();
            }
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            MultipleIssueSlip multipleIssue = new MultipleIssueSlip();
            multipleIssue.IssueType = Convert.ToInt32(IssueType);
            multipleIssue.IssueSlipNo = IssueSlipNo;
            multipleIssue.InternalOderID = InternalOderID;
            if (!string.IsNullOrEmpty(LotNo))
            {
                multipleIssue.LotNo = (LotNo);
            }
            if (Style != "")
            {
                multipleIssue.StyleNo = Style;
            }
            multipleIssue.CuttingIssueType = Convert.ToInt32(CuttingIssueTypeID);
            IssueSlip_SingleManager issueSlip_manager = new IssueSlip_SingleManager();
            MultipleIssueSlip multipleIssueList = new MultipleIssueSlip();
            int count = 0;
            OrderNoArray = OrderNoArray.TrimEnd();
            string[] OrderArray = OrderNoArray.Split(',');
            MultipleIssueSlip mulitiple = new MultipleIssueSlip();
            foreach (var item in OrderArray)
            {
                InternalOrderForm orderEntryForm = new InternalOrderForm();
                if (item != "")
                {
                    isCheckOrderNo = issueSlip_manager.isExixtorderNo(item);
                }
                if (multipleIssueList != null)
                {
                    count++;
                }

            }

            if (count != 0)
            {
                Message = "Already Exist";
                return Json(Message, JsonRequestBehavior.AllowGet);
            }
            else
            {

                mulitiple = issueSlip_manager.MultipleIssuePost(multipleIssue);
            }

            foreach (var item in spBOMMaterialList)
            {
                tbl_issueslipdetails issueSlip_MaterialDetails = new tbl_issueslipdetails();
                tbl_issueslipdetails issueSlipMaterial = new tbl_issueslipdetails();
                tbl_issueslipdetails issueSlipMaterial_ = new tbl_issueslipdetails();
                issueSlipMaterial.IssueSlipFK = mulitiple.MultipleIssueSlipID;
                issueSlipMaterial.OrderNo = internalOrderEntryForm.BuyerOrderSlNo;
                issueSlipMaterial.Style = item.Style;
                issueSlipMaterial.IssueType = IssueType;
                issueSlipMaterial.NoOfSets = item.NoOfSets;
                issueSlipMaterial.StoreName = item.StoreName;
                issueSlipMaterial.MaterialName = item.MaterialDescription;
                issueSlipMaterial.MaterialMasterId = item.Materialmasterid.Value;
                issueSlipMaterial.StoreMasterId = item.StoreMasterId.Value;
                issueSlipMaterial.ColourId = item.Color;
                issueSlipMaterial.CategoryId = item.CategoryName;
                issueSlipMaterial.GroupId = item.GroupName;
                issueSlipMaterial.RequiredQty = item.RequiredQty;
                issueSlipMaterial.AlredayIssued = 0;
                issueSlipMaterial.IsChecked = false;
                issueSlipMaterial.CurrentIssue = item.RequiredQty;
                issueSlipMaterial.IssueSlipType = "Multiple";
                issueSlipMaterial.Rate = item.Rate;
                issueSlipMaterial.Piecies = item.Piecies == null ? 0 : item.Piecies;
                issueSlipMaterial.CurrentStock = item.CurrentStock == null ? 0 : item.CurrentStock;
                issueSlipMaterial_ = issueSlipManager.GetIssueSlipMaterial(internalOrderEntryForm.BuyerOrderSlNo, item.MaterialDescription);
                if (issueSlipMaterial_ == null)
                {
                    issueSlip_MaterialDetails = issueSlipManager.Post(issueSlipMaterial);
                    issueSlipMaterialList.Add(issueSlip_MaterialDetails);
                }
                else
                {
                    issueSlip_MaterialDetails = issueSlipManager.Post(issueSlipMaterial);
                    issueSlipMaterialList.Add(issueSlip_MaterialDetails);
                }
            }

            return Json(new { Material = issueSlipMaterialList_, TotalAmount = internalOrderEntryForm.TotalAmount, SizeRange = sizeRangeQtyRate }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExpandDetails(string OrderNo, string MaterialName, string Color, string OrderEntryID)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            OrderEntry internalOrderEntryForm = new OrderEntry();
            SizeRangeQtyRateManager sizerangeQtyRateManager = new SizeRangeQtyRateManager();
            List<SizeRangeQtyRate> sizeRangeQtyRate = new List<SizeRangeQtyRate>();
            internalOrderEntryForm = buyerOrderEntryManager.GetBuyerOderSlNo(OrderNo);
            sizeRangeQtyRate = sizerangeQtyRateManager.GetSizeRangeByOrderEntryId(internalOrderEntryForm.OrderEntryId);
            return Json(sizeRangeQtyRate, JsonRequestBehavior.AllowGet);
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
        [HttpGet]
        public JsonResult GetLotNoWithOrderNo(string Lotno, string SeasonID)
        {
            BuyerOrderEntryManager buyerOrderManager = new BuyerOrderEntryManager();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            InternalOrderForm orderEntryList = new InternalOrderForm();
            IssueSlip_SingleManager issueSlipManager = new IssueSlip_SingleManager();
            if (Lotno != "")
            {
                Lotno = Lotno.TrimEnd(','); 
                var internalMaterialList = issueSlipManager.GetInternalOrderList(Lotno, SeasonID);
                if (internalMaterialList != null && internalMaterialList.Count() > 0)
                {
                    var distinctList = internalMaterialList.GroupBy(x => x.BuyerPoNo).Select(g => g.First()).ToList();
                    List<System.Web.Mvc.SelectListItem> items = distinctList.Select(
                                            item => new System.Web.Mvc.SelectListItem()
                                            {
                                                Text = item.BuyerPoNo,
                                                Value = item.OrderEntryId.ToString()
                                            }
                                            ).ToList();
                    var ShotName = new System.Web.Mvc.SelectListItem()
                    {
                        Value = "",
                        Text = "Please Select"
                    };
                    items.Insert(0, ShotName);
                    items = items.OrderBy(x => x.Text).ToList();
                    return Json(items, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);


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
        public ActionResult Search(string filter, int? page)
        {
            List<Data.StoredProcedureModel.IssueSlip_SingleModel> contactList = new List<Data.StoredProcedureModel.IssueSlip_SingleModel>();
            IssueSlip_SingleManager manager = new IssueSlip_SingleManager();
            Data.StoredProcedureModel.IssueSlip_SingleModel model = new Data.StoredProcedureModel.IssueSlip_SingleModel();
            contactList = manager.GetMultipleIssueGrid(filter);
            model.IssueSlip_ModelList = contactList;
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<OrderEntry> _items = new List<OrderEntry>();
            _items = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true).OrderBy(x => x.BuyerOrderSlNo).ToList();
            model.orderList = _items;
            var pager = new Data.StoredProcedureModel.Pager(contactList.Count(), page);
            var viewModel = new Data.StoredProcedureModel.IssueSlip_SingleModel
            {
                IssueSlip_ModelList = contactList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager,
                orderList = _items
            };
            return PartialView("~/Views/Stock/MultipleIssue/Partial/MultipleIssueGrid.cshtml", viewModel);
        }
        public JsonResult GetIssueSlipNo()
        {
            tblautogenissueslipdetails autoGenIssueSlipDetails = new tblautogenissueslipdetails();
            tblautogenissueslipdetails autoGenIssue = new tblautogenissueslipdetails();
            AutoGenIssueSlipDetailsManager autoManager = new AutoGenIssueSlipDetailsManager();
            autoGenIssueSlipDetails = autoManager.Get().LastOrDefault();
            if (autoGenIssueSlipDetails != null)
            {
                autoGenIssueSlipDetails = autoManager.Get().LastOrDefault();
                var autoId = Convert.ToInt32(autoGenIssueSlipDetails.IssueSlipDetailsId);
                autoGenIssue.IssueSlipDetailsId = autoId.ToString();
            }
            else
            {
                autoGenIssue.IssueSlipDetailsId = "1";
                autoManager.Post(autoGenIssue);
            }
            return Json(autoGenIssue.IssueSlipDetailsId, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStyle(int orderEntry)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            var List = buyerOrderEntryManager.Get().Where(x => x.OrderEntryId == orderEntry).FirstOrDefault();
            List<System.Web.Mvc.SelectListItem> items = billOfMaterialManager.Get().Where(x => x.LinkBomNo == List.OurStyle).Select(
            item => new System.Web.Mvc.SelectListItem()
            {
                Text = item.BomNo.Substring(0, 5),
                Value = item.BomId.ToString()
            }).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStore(int style)
        {
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            var Bom = billOfMaterialManager.Get().Where(x => x.BomId == style).FirstOrDefault();
            MaterialGroupManager MgManager = new MaterialGroupManager();
            var MaterialGroup = MgManager.Get().Where(x => x.MaterialGroupMasterId == Bom.MaterialGroupMasterId).FirstOrDefault();

            StoreMasterManager storeManager = new StoreMasterManager();
            List<System.Web.Mvc.SelectListItem> items = storeManager.Get().Where(x => x.StoreMasterId == MaterialGroup.StoreName).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.StoreName,
                                                     Value = item.StoreMasterId.ToString()
                                                 }).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCategory(int store)
        {
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            MaterialGroupManager MaterialGroupManager = new MaterialGroupManager();

            MaterialCategoryManager MaterialCategoryManager = new MaterialCategoryManager();
            var MaterialCategory = MaterialGroupManager.Get().Where(x => x.MaterialCategoryMasterId == store).FirstOrDefault();
            List<System.Web.Mvc.SelectListItem> items = new List<SelectListItem>();
            if (MaterialCategory != null)
            {

                items = MaterialCategoryManager.Get().Where(x => x.MaterialCategoryMasterId == MaterialCategory.MaterialCategoryMasterId).Select(
                                                      item => new System.Web.Mvc.SelectListItem()
                                                      {
                                                          Text = item.CategoryName,
                                                          Value = item.MaterialCategoryMasterId.ToString()
                                                      }).ToList();
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStoreCategory(int store)
        {
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            MaterialGroupManager MaterialGroupManager = new MaterialGroupManager();

            MaterialCategoryManager MaterialCategoryManager = new MaterialCategoryManager();
            var MaterialCategory = MaterialGroupManager.Get().Where(x => x.StoreName == store).ToList();
            List<System.Web.Mvc.SelectListItem> itemsList = new List<SelectListItem>();
            if (MaterialCategory != null && MaterialCategory.Count > 0)
            {

                foreach (var item in MaterialCategory)
                {
                    System.Web.Mvc.SelectListItem items = new SelectListItem();
                    items = MaterialCategoryManager.Get().Where(x => x.MaterialCategoryMasterId == item.MaterialCategoryMasterId).Select(
                                                     iteration => new System.Web.Mvc.SelectListItem()
                                                     {
                                                         Text = iteration.CategoryName,
                                                         Value = iteration.MaterialCategoryMasterId.ToString()
                                                     }).FirstOrDefault();
                    itemsList.Add(items);
                }

            }
            var itemsLists = itemsList.GroupBy(x => x.Text).Select(g => g.First()).ToList();

            return Json(itemsLists, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGroup(int category)
        {
            MaterialGroupManager MaterialGroupManager = new MaterialGroupManager();
            List<System.Web.Mvc.SelectListItem> items = MaterialGroupManager.Get().Where(x => x.MaterialCategoryMasterId == category).Select(
                                                 item => new System.Web.Mvc.SelectListItem()
                                                 {
                                                     Text = item.GroupName,
                                                     Value = item.MaterialGroupMasterId.ToString()
                                                 }).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMaterial(int group)
        {
            MaterialGroupManager MaterialGroupManager = new MaterialGroupManager();
            var MaterialGroup = MaterialGroupManager.Get().Where(x => x.MaterialGroupMasterId == group).FirstOrDefault();

            MaterialNameManager manager = new MaterialNameManager();
            List<System.Web.Mvc.SelectListItem> items = manager.Get().Where(x => x.MaterialGroupMasterId == MaterialGroup.MaterialGroupMasterId).Select(
                                             item => new System.Web.Mvc.SelectListItem()
                                             {
                                                 Text = item.MaterialDescription,
                                                 Value = item.MaterialMasterID.ToString()
                                             }).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillLotNo(string Season)
        {
            List<OrderEntry> OrderEntryList_ = new List<OrderEntry>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            SeasonManager seasonmanager = new SeasonManager();

            var items = (from x in buyerOrderEntryManager.Get().Where(x => x.IsInternal == true)
                         join y in seasonmanager.Get()
                         on x.BuyerSeason equals y.SeasonMasterId
                         where y.SeasonName == Season

                         select new { Lot = x.LotNo, order = x.OrderEntryId });

            var distinctList = items.GroupBy(x => x.Lot).Select(g => g.First()).ToList().OrderBy(x => x.Lot);

            return Json(distinctList, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Current Stock
        [HttpGet]
        public ActionResult GetCurrentStock(int MaterialID, int MaterialType)
        {
            IssueSlip_SingleManager issueSlipManager = new IssueSlip_SingleManager();
            IssueSlipManager issueSlip = new IssueSlipManager();
            List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
            List<MMS.Web.Models.PendingQty> ListOfPendingStockList = new List<PendingQty>();
            MaterialManager materialManager = new MaterialManager();
            listSizeItemMaterial = materialManager.GetSizeItemMaterial(MaterialID);
            listSizeItemMaterial = listSizeItemMaterial.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();
            ListOfPendingStockList = issueSlip.GetCurrentStock(MaterialID, MaterialType);
            decimal? BalanceStock = ListOfPendingStockList.FirstOrDefault().BalanceStock;
            return Json(new { SizeRangelist = listSizeItemMaterial, BalanceStock = BalanceStock }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
