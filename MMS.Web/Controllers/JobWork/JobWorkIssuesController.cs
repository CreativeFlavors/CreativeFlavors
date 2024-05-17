using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Repository.Managers.JobWork;
using MMS.Core.Entities.JobWork;
using System.Globalization;
using System.IO;
using MMS.Web.Models;
using MMS.Web.Models.JobworkModel;
using System.Web.Hosting;
using Microsoft.VisualBasic;

namespace MMS.Web.Controllers.JobWork
{
    [CustomFilter]
    public class JobWorkIssuesController : Controller
    {
        //
        // GET: /JobWorkIssues/

        #region MultipleIssue 
        public ActionResult JobWorkIssues(int? page)
        {
            List<Data.StoredProcedureModel.IssueSlip_SingleModel> contactList = new List<Data.StoredProcedureModel.IssueSlip_SingleModel>();
            IssueSlip_SingleManager manager = new IssueSlip_SingleManager();
            Data.StoredProcedureModel.IssueSlip_SingleModel model = new Data.StoredProcedureModel.IssueSlip_SingleModel();
            contactList = manager.GetJobWorkMultipleIssueGrid("");
            List<MultipleIssueSlip> issueSlipList = new List<MultipleIssueSlip>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();

            var viewModel = new Data.StoredProcedureModel.IssueSlip_SingleModel
            {
                IssueSlip_ModelList = contactList.ToList(),
            };
            return View("~/Views/Jobwork/Job_Master/JobWorkIssues/JobWorkIssues.cshtml", viewModel);
        }
        public ActionResult JobWorkIssuesGrid(int? page)
        {

            List<Data.StoredProcedureModel.IssueSlip_SingleModel> contactList = new List<Data.StoredProcedureModel.IssueSlip_SingleModel>();
            IssueSlip_SingleManager manager = new IssueSlip_SingleManager();
            Data.StoredProcedureModel.IssueSlip_SingleModel model = new Data.StoredProcedureModel.IssueSlip_SingleModel();
            contactList = manager.GetJobWorkMultipleIssueGrid("");
            List<MultipleIssueSlip> issueSlipList = new List<MultipleIssueSlip>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();

            var viewModel = new Data.StoredProcedureModel.IssueSlip_SingleModel
            {
                IssueSlip_ModelList = contactList.ToList(),
            };
            return PartialView("~/Views/Jobwork/Job_Master/JobWorkIssues/Partial/JobWorkIssuesGrid.cshtml", viewModel);
        }

        public ActionResult GetIssueDetails(int IssueSlipId)
        {
            Data.StoredProcedureModel.IssueSlip_SingleModel model = new Data.StoredProcedureModel.IssueSlip_SingleModel();
            MultipleIssueSlip arg = new MultipleIssueSlip();
            IssueSlip_SingleManager Manager = new IssueSlip_SingleManager();
            List<IssueSlip_MaterialDetails> issueSlipMaterials = new List<IssueSlip_MaterialDetails>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            IssueSlip_MaterialDetails issueSlip_MaterialDetails = new IssueSlip_MaterialDetails();
            var IssueSlip = Manager.GetMultipleIssueGRNSelectedRow(IssueSlipId);
            List<InternalOrderEntryForm> _items = new List<InternalOrderEntryForm>();
            BuyerOrderEntryManager buyerOrderManager = new BuyerOrderEntryManager();
            InternalOrderEntryForm orderEntryList = new InternalOrderEntryForm();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();

            AutoGenIssueSlipDetails autoGenIssueSlipDetails = new AutoGenIssueSlipDetails();
            AutoGenIssueSlipDetails autoGenIssue = new AutoGenIssueSlipDetails();
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
                        InternalOrderEntryForm orderform = new InternalOrderEntryForm();
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
                    InternalOrderEntryForm orderform = new InternalOrderEntryForm();
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
                InternalOrderEntryForm orderEntryForm = new InternalOrderEntryForm();
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
            return PartialView("~/Views/Jobwork/Job_Master/JobWorkIssues/Partial/JobWorkIssuesDetails.cshtml", model);
        }

        public ActionResult MultipleIssueEdit()
        {
            int IssueSlipId = Convert.ToInt32(Request.QueryString["id"]);
            IssueSlip_SingleManager manager = new IssueSlip_SingleManager();
            Models.StockModel.IssueSlip_SingleModel model = new Models.StockModel.IssueSlip_SingleModel();
            MultipleIssueSlip arg = new MultipleIssueSlip();
            IssueSlip_SingleManager Manager = new IssueSlip_SingleManager();
            List<IssueSlip_MaterialDetails> issueSlipMaterials = new List<IssueSlip_MaterialDetails>();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            IssueSlip_MaterialDetails issueSlip_MaterialDetails = new IssueSlip_MaterialDetails();
            var IssueSlip = Manager.GetMultipleIssueGRNSelectedRow(IssueSlipId);
            List<InternalOrderEntryForm> _items = new List<InternalOrderEntryForm>();
            BuyerOrderEntryManager buyerOrderManager = new BuyerOrderEntryManager();
            InternalOrderEntryForm orderEntryList = new InternalOrderEntryForm();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            AutoGenIssueSlipDetails autoGenIssueSlipDetails = new AutoGenIssueSlipDetails();
            AutoGenIssueSlipDetails autoGenIssue = new AutoGenIssueSlipDetails();
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
                    InternalOrderEntryForm orderform = new InternalOrderEntryForm();
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
                    InternalOrderEntryForm orderform = new InternalOrderEntryForm();
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
                InternalOrderEntryForm orderEntryForm = new InternalOrderEntryForm();
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
            List<InternalOrderEntryForm> _items = new List<InternalOrderEntryForm>();
            _items = buyerOrderEntryManager.Get().Where(x => x.IsInternal == true).OrderBy(x => x.BuyerOrderSlNo).ToList();
            Models.StockModel.IssueSlip_SingleModel model = new Models.StockModel.IssueSlip_SingleModel();
            model.orderList = _items;
            return Json(model.orderList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetInternalOrderID(int InternalOrderno)
        {
            InternalOrderEntryForm internalOrderEntryForm = new InternalOrderEntryForm();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            internalOrderEntryForm = buyerOrderEntryManager.GetInteranlOrderEntryId(InternalOrderno);
            return Json(internalOrderEntryForm, JsonRequestBehavior.AllowGet);
        }
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

        public ActionResult GetJobWorkList(string issueType)
        {
            List<JobOtherWorkMaster> JobWorklist = new List<JobOtherWorkMaster>();

            List<System.Web.Mvc.SelectListItem> items = new List<SelectListItem>();

            try
            {
                if (issueType.Trim() == "JobWork(Sheet to Pairs)")
                {
                    JobSheet_PairManager jobsheetpair = new JobSheet_PairManager();
                    items = jobsheetpair.GetJobSheetPair().Select(
                                             item => new System.Web.Mvc.SelectListItem()
                                             {
                                                 Text = item.jobsheet_pair_Code,
                                                 Value = item.jobsheet_pair_code_id.ToString()
                                             }).ToList();
                }
                else if (issueType == "JobWork(Leather To Leather)")
                {
                    JobLeather_leatherManager jobLeather = new JobLeather_leatherManager();
                    items = jobLeather.Get().Select(
                                             item => new System.Web.Mvc.SelectListItem()
                                             {
                                                 Text = item.Job_Lether_to_lether_Code,
                                                 Value = item.Job_Lether_to_lether_id.ToString()
                                             }).ToList();
                }
                else if (issueType == "JobWork Production")
                {
                    ProductionJobworkMasterManager ProductionJobworkMasterManager_ = new ProductionJobworkMasterManager();
                    items = ProductionJobworkMasterManager_.Get_production_code_Tbl().Select(
                                             item => new System.Web.Mvc.SelectListItem()
                                             {
                                                 Text = item.ProductionJobwork_Code,
                                                 Value = item.ProductionJobwork_code_id.ToString()
                                             }).ToList();
                }
                else if (issueType == "JobWork Other/Services")
                {
                    JobOtherWorkManager jobotherwork = new JobOtherWorkManager();

                    items = jobotherwork.Get().Select(
                                             item => new System.Web.Mvc.SelectListItem()
                                             {
                                                 Text = item.OtherJobWork_Code,
                                                 Value = item.OtherJobWork_Id.ToString()
                                             }).ToList();
                }
                else
                {

                }

                var ShotName = new System.Web.Mvc.SelectListItem()
                {
                    Value = "",
                    Text = "Please Select"
                };
                items.Insert(0, ShotName);
            }
            catch (Exception ex)
            {

            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetJobWorkList_By_Jwname(string issueType, int Jw_Name)
        {
            List<JobOtherWorkMaster> JobWorklist = new List<JobOtherWorkMaster>();

            List<System.Web.Mvc.SelectListItem> items = new List<SelectListItem>();
          //  List<System.Web.Mvc.SelectListItem> items_ = new List<SelectListItem>();

            try
            {
                if (issueType == "JobWork(Sheet to Pairs)")
                {
                    JobSheet_PairManager jobsheetpair = new JobSheet_PairManager();
                    var jw_id = jobsheetpair.GetJobwork_Jw_JobSheet_pairGrid(0).Where(x => x.JW_Name == Jw_Name).FirstOrDefault();
                    var list_Jwcode = jobsheetpair.Get().Where(x => x.JW_Name == jw_id.JW_Name).Select(x => x.jobsheet_pair_Code_id);

                    items = jobsheetpair.GetJobSheetPair().Where(x => list_Jwcode.Contains(x.jobsheet_pair_code_id)).Select(
                                          item => new System.Web.Mvc.SelectListItem()
                                          {
                                              Text = item.jobsheet_pair_Code,
                                              Value = item.jobsheet_pair_code_id.ToString()
                                          }).ToList();
                }
                else if (issueType == "JobWork(Leather To Leather)")
                {
                    JobLeather_leatherManager jobLeather = new JobLeather_leatherManager();
                    items = jobLeather.Get().Where(x => x.Jw_Name == Jw_Name).Select(
                                             item => new System.Web.Mvc.SelectListItem()
                                             {
                                                 Text = item.Job_Lether_to_lether_Code,
                                                 Value = item.Job_Lether_to_lether_id.ToString()
                                             }).ToList();
                }
                else if (issueType == "JobWork Production")
                {
                    ProductionJobworkMasterManager ProductionJobworkMasterManager_ = new ProductionJobworkMasterManager();
                    ProductionJobworkMaster ProductionJobworkMaster = new ProductionJobworkMaster();
                    JobworkManager JobworkManager = new JobworkManager();
                    var Jwname = JobworkManager.GetJobMasterMasterID(Jw_Name);

                    var list_Jwcode = ProductionJobworkMasterManager_.Get().Where(x => x.Jw_Name ==Convert.ToInt32( Jwname.JW_Id)).Distinct().Select(x => x.Prod_code_id);
                    items = ProductionJobworkMasterManager_.Get_ProductionGrid().Where(x => list_Jwcode.Contains(x.Prod_code_id)).Select(
                                             item => new System.Web.Mvc.SelectListItem()
                                             {
                                                 Text = item.ProductionJobwork_Code,
                                                 Value = item.Prod_code_id.ToString()
                                             }).ToList();

                    items= items.GroupBy(x => x.Text).Select(g => g.First()).ToList();

                }
                else if (issueType == "JobWork Other/Services")
                {
                    JobOtherWorkManager jobotherwork = new JobOtherWorkManager();

                    items = jobotherwork.Get().Where(x => x.JobWork_Id == Jw_Name).Select(
                                             item => new System.Web.Mvc.SelectListItem()
                                             {
                                                 Text = item.OtherJobWork_Code,
                                                 Value = item.OtherJobWork_Id.ToString()
                                             }).ToList();
                }
                else
                {

                }

                var ShotName = new System.Web.Mvc.SelectListItem()
                {
                    Value = "",
                    Text = "Please Select"
                };
                items.Insert(0, ShotName);
            }
            catch (Exception ex)
            {

            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetJobWorkList_By_Jwname_Issueid(int Jw_Name)
        {
            List<JobOtherWorkMaster> JobWorklist = new List<JobOtherWorkMaster>();

            IssueSlip_SingleManager Manager = new IssueSlip_SingleManager();

            IssueSlipManager issueSlipManager = new IssueSlipManager();


            int?[] list_new = issueSlipManager.Get().Where(x => x.Jw_Name == Jw_Name).Select(x => x.IssueSlipFK).Distinct().ToArray();

            var list_ = Manager.GetMultipleIssueSlip().Where(x => list_new.Contains(x.MultipleIssueSlipID));
            List<System.Web.Mvc.SelectListItem> items_ = list_.Where(x=>x.GateOut_No !=null).Select(
                                             item => new System.Web.Mvc.SelectListItem()
                                             {
                                                 Text = item.GateOut_No,
                                                 Value = item.MultipleIssueSlipID.ToString()
                                             }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);

            return Json(items_, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetJobWorkList_By_Job_Type(string InwardMaterialType)
        {
            List<JobOtherWorkMaster> JobWorklist = new List<JobOtherWorkMaster>();

            List<System.Web.Mvc.SelectListItem> items_ = new List<SelectListItem>();

            JobworkManager JobworkManager = new JobworkManager();
            try
            {
                int[] jobsheetpair_jwname;
                if (InwardMaterialType.Trim() == "JobWork(Sheet to Pairs)")
                {

                    JobSheet_PairManager jobsheetpair = new JobSheet_PairManager();
                    jobsheetpair_jwname = jobsheetpair.Get().GroupBy(x => x.JW_Name).Select(g => g.First()).Select(x => x.JW_Name).ToArray();

                    items_ = JobworkManager.Get_contain(jobsheetpair_jwname).OrderBy(x => x.JW_Name).Select(
                                              item => new System.Web.Mvc.SelectListItem()
                                              {

                                                  Text = item.JW_Name,
                                                  Value = Convert.ToString(item.JW_Id)
                                              }
                                      ).ToList();

                }
                else if (InwardMaterialType == "JobWork(Leather To Leather)")
                {
                    JobLeather_leatherManager jobLeather = new JobLeather_leatherManager();
                    jobsheetpair_jwname = jobLeather.Get().GroupBy(x => x.Jw_Name).Select(g => g.First()).Select(x => x.Jw_Name).ToArray();

                    items_ = JobworkManager.Get_contain(jobsheetpair_jwname).OrderBy(x => x.JW_Name).Select(
                                                item => new System.Web.Mvc.SelectListItem()
                                                {

                                                    Text = item.JW_Name,
                                                    Value = Convert.ToString(item.JW_Id)
                                                }
                                        ).ToList();
                }
                else if (InwardMaterialType == "JobWork Production")
                {
                    ProductionJobworkMasterManager ProductionJobworkMasterManager_ = new ProductionJobworkMasterManager();
                    jobsheetpair_jwname = ProductionJobworkMasterManager_.Get().GroupBy(x => x.Jw_Name).Select(g => g.First()).Select(x => x.Jw_Name).ToArray();
                    items_ = JobworkManager.Get_contain(jobsheetpair_jwname).OrderBy(x => x.JW_Name).Select(
                                             item => new System.Web.Mvc.SelectListItem()
                                             {

                                                 Text = item.JW_Name,
                                                 Value = Convert.ToString(item.JW_Id)
                                             }
                                     ).ToList();
                }
                else if (InwardMaterialType == "JobWork Other/Services")
                {
                    JobOtherWorkManager jobotherwork = new JobOtherWorkManager();

                    jobsheetpair_jwname = jobotherwork.Get().GroupBy(x => x.JobWork_Id).Select(g => g.First()).Select(x => x.JobWork_Id).ToArray();

                    items_ = JobworkManager.Get_contain(jobsheetpair_jwname).OrderBy(x => x.JW_Name).Select(
                                                   item => new System.Web.Mvc.SelectListItem()
                                                   {

                                                       Text = item.JW_Name,
                                                       Value = Convert.ToString(item.JW_Id)
                                                   }
                                           ).ToList();
                }
                else
                {

                }

                var ShotName = new System.Web.Mvc.SelectListItem()
                {
                    Value = "",
                    Text = "Please Select"
                };
                items_.Insert(0, ShotName);
            }
            catch (Exception ex)
            {

            }
            return Json(items_, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetMaterialNamebyJobId(string jobWorkId, string IssueNo)
        {
            try
            {
                StoreMasterManager StoreMasterManager = new StoreMasterManager();
                List<MaterialNameMaster> materialNameMasterList = new List<MaterialNameMaster>();
                ColorManager colorManager = new ColorManager();
                MaterialNameManager materialNameManager = new MaterialNameManager();
                MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
                MaterialManager materialManager = new MaterialManager();
                string Message = "";
                JobLeather_leatherManager jobLeather = new JobLeather_leatherManager();
                List<IssueSlip_MaterialDetails> issueSlipMaterialList = new List<IssueSlip_MaterialDetails>();
                IssueSlipManager issueSlipManager = new IssueSlipManager();
                JobLeather_leatherMaster Jw_Name = new JobLeather_leatherMaster();
                List<IssueSlip_MaterialDetails> IssueObjList = new List<IssueSlip_MaterialDetails>();
                List<System.Web.Mvc.SelectListItem> materaildesc_List = new List<System.Web.Mvc.SelectListItem>();
                BillOfMaterialManager billofMaterialManager = new BillOfMaterialManager();
                List<MMS.Data.StoredProcedureModel.MaterialDropDownmodel> list = new List<Data.StoredProcedureModel.MaterialDropDownmodel>();
                var list_Material = billofMaterialManager.GetMaterialList();
                var jobWorkId_CommoSep = jobWorkId.Split(',');
                List<int> list_ = new List<int>();
                List<int> newLst = new List<int>();
                foreach (var Code in jobWorkId_CommoSep)
                {
                    newLst = new List<int>();
                    newLst =  jobLeather.GetJob_LetherCode(Code).Select(x => x.Job_Lether_to_lether_id).ToList();
                    list_.AddRange(newLst);
                }
                var Balance_stock = Convert.ToDecimal(0);
                // int[] list_ = jobWorkId.Split(',').Select(int.Parse).ToArray();

                // int[] list_ = JobLeather_leatherManager.GetJob_LetherCode("0");.Select(int.Parse).ToArray();
                //  var materaildesc = jobLeather.GetJobLeather_leather_ID(jobWorkId);
                var materaildesc_ = jobLeather.Get_JobLeather_leatherMaster_List(list_);


                //var ll = issueSlipManager.Get().Select(x => x.jobsheet_pair_Code_id).ToList();

                //var ll2=ll.FindAll();

                //var myList = ll.Where(item => list_.Contain
                //     .Select(a => a.Title).ToList();

                //var partiallist = ll.Any(x=>list_.Contains(1036));

                //.Contains(x.jobsheet_pair_Code_id)).ToList();
                foreach (var item in list_)
                {
                    string items = item.ToString();
                    //  IssueObjList.AddRange(issueSlipManager.Get().Where(x => x.jobsheet_pair_Code_id.Trim().Contains(items.Trim())).ToList());
                    IssueObjList.AddRange(issueSlipManager.Get().Where(x => x.jobsheet_pair_Code_id == items).ToList());
                }
                Balance_stock = Convert.ToDecimal(IssueObjList.Sum(x => x.CurrentIssue));
                foreach (var items in materaildesc_)
                {
                    var distinctList = list_Material.Where(x => x.MaterialMasterId == items.Material).GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
                    distinctList = distinctList.ToList();
                    var MAterial_name = distinctList.Where(x => x.MaterialMasterId == items.Material).FirstOrDefault();
                    //  var materaildesc_List = items.Material + '_' + items.Job_Lether_to_lether_id;
                 

                    //var AA = new SelectListItem
                    //{
                    //    Text = items.Material.ToString(),
                    //    Value = items.Material + "_" + items.Job_Lether_to_lether_id
                    //};


                    materaildesc_List.Add(new SelectListItem
                    {
                        Text = MAterial_name.MaterialDescription.ToString(),
                        Value = items.Material + "_" + items.Job_Lether_to_lether_id
                    });
                
                
                    if (IssueNo != "" && IssueNo != "0")
                    {
                        var item = issueSlipManager.GetMultipleIssueid(Convert.ToInt32(IssueNo)).FirstOrDefault();
                        // Balance_stock = Convert.ToDecimal(issueSlipManager.Get().Where(x=>x.MaterialMasterId== materaildesc.Material && x.Jobworktype_Id == materaildesc.Job_Lether_to_lether_id).Sum(x=>x.CurrentIssue));
                        if (item != null)
                        {
                            Jw_Name = jobLeather.GetJobLeather_leather_ID(item.Jobworktype_Id);

                            if (items.Jw_Name != Jw_Name.Jw_Name)
                            {
                                Message = "Job work name cannot be diffrent";
                                return Json(new { Message = Message, materaildesc = "" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                var ShotName = new System.Web.Mvc.SelectListItem()
                {
                    Value = "",
                    Text = "Please Select"
                };
                materaildesc_List.Insert(0, ShotName);

                decimal? total_letervalue = materaildesc_.Sum(x => x.Qty);


                var items_Material = (from x in materialManager.Get()
                                      join y in materialNameManager.Get()
                                       on x.MaterialName equals y.MaterialMasterID
                                      join S in StoreMasterManager.Get()
                                         on x.StoreMasterId equals S.StoreMasterId
                                      join z in colorManager.Get()
                                      on x.ColorMasterId equals z.ColorMasterId into yG
                                      from y1 in yG.DefaultIfEmpty()

                                      where x.MaterialMasterId == Convert.ToInt32(materaildesc_List[1].Value.Split('_')[0])
                                      select new { MaterialDescription = string.Format("{0} {1} {2}", y.MaterialDescription, x.OptionMaterialValue, y1 != null ? y1.Color != null ? y1.Color : string.Empty : string.Empty), x.MaterialMasterId, x.Price, ColorMasterId = (y1 != null ? y1.ColorMasterId != 0 ? y1.ColorMasterId : 0 : 0), x.Uom, x.UomUnit, x.SizeRangeMasterId, x.SubstanceMasterId, x.MaterialCategoryMasterId, x.MaterialGroupMasterId, x.GradeMasterId, x.StoreMasterId, x.PurchasePrimary, x.PrimaryUnit, x.PurchasePacket, x.PacketUnit, x.CurrencyMasterId, x.isImport, x.isLocal, x.isImportCustomer,S.StoreName }).ToList();

            

                    return Json(new { Message = Message, materaildesc = materaildesc_List, Balance_stock = Balance_stock, Total_lethervalue = total_letervalue, StoreName= items_Material }, JsonRequestBehavior.AllowGet);
              
          
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ActionResult GetMaterialNamebyJobId_Selete(int jobWorkId, string IssueNo)
        {
            try
            {
                string Message = "";
                JobLeather_leatherManager jobLeather = new JobLeather_leatherManager();
                List<IssueSlip_MaterialDetails> issueSlipMaterialList = new List<IssueSlip_MaterialDetails>();
                IssueSlipManager issueSlipManager = new IssueSlipManager();
                JobLeather_leatherMaster Jw_Name = new JobLeather_leatherMaster();
                var Balance_stock = Convert.ToDecimal(0);

                var materaildesc = jobLeather.GetJobLeather_leather_ID(jobWorkId);
                Balance_stock = Convert.ToDecimal(issueSlipManager.Get().Where(x => x.MaterialMasterId == materaildesc.Material && x.Jobworktype_Id == materaildesc.Job_Lether_to_lether_id).Sum(x => x.CurrentIssue));

                if (IssueNo != "" && IssueNo != "0")
                {
                    var item = issueSlipManager.GetMultipleIssueid(Convert.ToInt32(IssueNo)).FirstOrDefault();
                    // Balance_stock = Convert.ToDecimal(issueSlipManager.Get().Where(x=>x.MaterialMasterId== materaildesc.Material && x.Jobworktype_Id == materaildesc.Job_Lether_to_lether_id).Sum(x=>x.CurrentIssue));
                    if (item != null)
                    {
                        Jw_Name = jobLeather.GetJobLeather_leather_ID(item.Jobworktype_Id);

                        if (materaildesc.Jw_Name != Jw_Name.Jw_Name)
                        {
                            Message = "Job work name cannot be diffrent";
                            return Json(new { Message = Message, materaildesc = "" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                return Json(new { Message = Message, materaildesc = materaildesc, Balance_stock = Balance_stock }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #region Production 
        public ActionResult GetMaterialDetailsbyJobId_ProductionJobwork(string jobWorkId, string IssueNo)
        {
            try
            {
                ProductionJobworkMaster materaildesc = new ProductionJobworkMaster();
                ProductionJobworkMasterManager ProductionJobworkMasterManager_ = new ProductionJobworkMasterManager();
                ProductionJobworkModel ProductionJobworkModel = new ProductionJobworkModel();
                List<MMS.Data.StoredProcedureModel.JobProductionGrid> JobProductionGrid = new List<MMS.Data.StoredProcedureModel.JobProductionGrid>();
                List<System.Web.Mvc.SelectListItem> materaildesc_List = new List<System.Web.Mvc.SelectListItem>();
                List<System.Web.Mvc.SelectListItem> materaildesc_List_ = new List<System.Web.Mvc.SelectListItem>();
                BillOfMaterialManager billofMaterialManager = new BillOfMaterialManager();
                IssueSlipManager issueSlipManager = new IssueSlipManager();
                MMS.Web.Models.JobworkModel.JobLeather_leatherModel JobLeather_leatherModel = new JobLeather_leatherModel();
                Job_ApprovedPriceManager Job_ApprovedPriceManager = Job_ApprovedPriceManager.GetInstance;
                int count = 0;
                var list_Material = billofMaterialManager.GetMaterialList();
                string[] jobWorkId_CommoSep_string = jobWorkId.Split(',');
                int[] jobWorkId_CommoSep = jobWorkId.Split(',').Select(int.Parse).ToArray(); ;
                var Production_Code_Master = ProductionJobworkMasterManager_.Get_production_code_Tbl().Where(x => jobWorkId_CommoSep.Contains(x.ProductionJobwork_code_id)).ToList();
                var Leather_Pairs = Production_Code_Master[0].Leather_Pairs;
                var component_Pairs = Production_Code_Master[0].component_Pairs;
                var Upper_Fullshoes = Production_Code_Master[0].Upper_Fullshoes;
                string error="";
                string Leather_Pairs_bool="";
                if (Leather_Pairs == true)
                {
                    Leather_Pairs_bool = "true";
                    foreach (var items in Production_Code_Master)
                    {
                        if (Leather_Pairs != items.Leather_Pairs)
                        {
                            error = "Contain Diffrent type";
                        }

                    }

                }
               else if (component_Pairs == true)
                {
                    foreach (var items in Production_Code_Master)
                    {
                        if (component_Pairs != items.component_Pairs)
                        {
                            error = "Contain Diffrent type";
                        }
                        

                    }
                }
               else if (Upper_Fullshoes == true)
                {
                    foreach (var items in Production_Code_Master)
                    {
                        if (Upper_Fullshoes != items.Upper_Fullshoes)
                        {
                            error = "Contain Diffrent type";
                        }

                    }
                }
                var ProductionJobworkMaster = ProductionJobworkMasterManager_.Get().Where(x => jobWorkId_CommoSep.Contains(x.Prod_code_id)).ToList();
                //  int Jw_Count = 0;
                int Jw_Count = ProductionJobworkMaster.Where(x => x.Jw_Name != ProductionJobworkMaster[0].Jw_Name).Count();
                decimal Qty= Convert.ToDecimal(0.0);
                    foreach (var items in ProductionJobworkMaster)
                    {
                    if (items.Leather_Pairs == true)
                    {
                        var distinctList = list_Material.Where(x => x.MaterialMasterId == items.Material_Name).GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
                        distinctList = distinctList.ToList();
                        var MAterial_name = distinctList.Where(x => x.MaterialMasterId == items.Material_Name).FirstOrDefault();


                        materaildesc_List.Add(new SelectListItem
                        {
                            Text = MAterial_name.MaterialDescription.ToString(),
                            Value = items.Material_Name + "_" + items.Production_Order_id
                        });
                        
                    }
                    else if (items.component_Pairs == true)
                     {
                        materaildesc_List_.Add(new SelectListItem
                        {
                            Text = items.Io_based+'_'+items.Stage_From+'_'+items.Stage_To.ToString(),
                            Value = items.Production_Order_id.ToString()
                        });
                        Qty = Qty + (items.Qty);


                     }
                    else if (items.Upper_Fullshoes == true)
                    {
                        Qty = Qty + (items.Qty);


                    }
                }
                
                var ShotName = new System.Web.Mvc.SelectListItem()
                {
                    Value = "",
                    Text = "Please Select"
                };
                materaildesc_List.Insert(0, ShotName);
                materaildesc_List_.Insert(0, ShotName);
                int value;
                var Balance_stock = Convert.ToDecimal(0);
                count = issueSlipManager.Get().Where(x => jobWorkId_CommoSep_string.Contains(x.jobsheet_pair_Code_id)).Count();
                //if (int.TryParse(jobWorkId, out value))
                //{ 
                // materaildesc = ProductionJobworkMasterManager_.GetproductionMaster_id(Convert.ToInt32(jobWorkId));
                //    if (materaildesc != null)
                //    {
                //        Balance_stock = Convert.ToDecimal(issueSlipManager.Get().Where(x => jobWorkId_CommoSep_string.Contains(x.jobsheet_pair_Code_id) && x.IssueType=="17").Sum(x => x.CurrentIssue));

                //         count = issueSlipManager.Get().Where(x => jobWorkId_CommoSep_string.Contains(x.jobsheet_pair_Code_id)).Count();

                //    var    ProductionJobworkMaster_ = ProductionJobworkMasterManager_.GetproductionMaster_id(ProductionJobworkMaster[0].Production_Order_id);

                //        JobLeather_leatherModel.JobApproved_list = (from y in Job_ApprovedPriceMaster.Get()
                //                                                    where y.JW_Name == ProductionJobworkMaster_.Jw_Name && y.Process_Name == ProductionJobworkMaster_.Process_Name && y.Rate_For_JW == ProductionJobworkMaster_.Rate && y.Stage_From == ProductionJobworkMaster_.Stage_From && y.Stage_To == ProductionJobworkMaster_.Stage_To
                //                                                    select new JobApproved_list { Date = y.Date, Job_ExcessPercentage = y.Job_ExcessPercentage }).OrderByDescending(y => y.Date).FirstOrDefault();
                //    }
                //}

                return Json(new { ProductionJobworkMaster = ProductionJobworkMaster, Jw_Count = Jw_Count, error = error, Production_Code_Master= Production_Code_Master, materaildesc_List= materaildesc_List, materaildesc= materaildesc, Balance_stock = Balance_stock , Leather_Pairs_bool = Leather_Pairs_bool, Qty= Qty,
                    // Excess_pecent = JobLeather_leatherModel.JobApproved_list.Job_ExcessPercentage,
                    Excess_pecent = 0,
                    count = count,
                    Io= materaildesc_List_
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult Get_Production_Order_id(int jobWorkId)
        {
            ProductionJobworkMaster materaildesc = new ProductionJobworkMaster();
            IssueSlipManager issueSlipManager = new IssueSlipManager();

            var Balance_stock = Convert.ToDecimal(0);
            ProductionJobworkMasterManager ProductionJobworkMasterManager_ = new ProductionJobworkMasterManager();
            materaildesc = ProductionJobworkMasterManager_.GetproductionMaster_id(Convert.ToInt32(jobWorkId));
            if (materaildesc != null)
            {
                Balance_stock = Convert.ToDecimal(issueSlipManager.Get().Where(x => x.MaterialMasterId == materaildesc.Material_Name && x.Jobworktype_Id == materaildesc.Production_Order_id).Sum(x => x.CurrentIssue));
            }
            return Json(new {  materaildesc = materaildesc, Balance_stock = Balance_stock }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult GetMaterialDetailsbyJobId(string jobWorkId, string IssueSlipNo, string IssueDate, string MultipleIssueSlipID_, string IssueNo)
        {
            string Message = "";
            JobSheet_PairManager jobSheet = new JobSheet_PairManager();
            List<IssueSlip_MaterialDetails> issueSlipMaterialList = new List<IssueSlip_MaterialDetails>();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            Data.StoredProcedureModel.IssueSlip_SingleModel model = new Data.StoredProcedureModel.IssueSlip_SingleModel();
            Job_ApprovedPriceManager Job_ApprovedPriceMaster = new Job_ApprovedPriceManager();
            JobSheet_PairMaster JobSheet_PairMaster = new JobSheet_PairMaster();
            String numberList = "";
            var Balance_stock = Convert.ToDecimal(0);
            try
            {
                //List<string> TagIds = jobWorkId.Split(',').ToList();
                int MultipleIssueSlipID = 0;
                int[] list_ = jobWorkId.Split(',').Select(int.Parse).ToArray();
                if (MultipleIssueSlipID_ != "")
                {

                    MultipleIssueSlipID = Convert.ToInt32(MultipleIssueSlipID_);
                }

                var materaildesc = jobSheet.Getjobsheet_pair_Materials(list_);

                foreach (var items in materaildesc)
                {
                    //result.Material[0].MaterialMasterId.ToString()
                    var Excess_pecent = (from y in Job_ApprovedPriceMaster.Get()
                                where y.JW_Name == items.JW_Name && y.Process_Name == items.Process_Name && y.Rate_For_JW ==items.Jw_Rate
                                select new {  Date = y.Date, Job_ExcessPercentage = y.Job_ExcessPercentage}).OrderByDescending(y => y.Date).FirstOrDefault();

                    dynamic result = MultipleIssueMaterialName_detail(items.Issue_Material, Convert.ToInt32(IssueSlipNo), "", IssueDate, items.jobsheet_pair_Code_id,"");

                    if (IssueNo != "" && IssueNo != "0")
                    {
                        var item = issueSlipManager.GetMultipleIssueid(Convert.ToInt32(IssueNo)).FirstOrDefault();
                        if (item != null)
                        {
                            JobSheet_PairMaster = jobSheet.Getjobsheet_pair_id(item.Jobworktype_Id);

                            if (items.JW_Name != JobSheet_PairMaster.JW_Name)
                            {
                                Message = "Job work name cannot be diffrent";
                                return Json(new { Message = Message, materaildesc = "" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (result.Message != "Already existed")
                    {

                        if (result.Message != "Please make a entry on approved price list")
                        {
                            var sum = jobSheet.Getjobsheet_pair_Materials(list_).Where(x => x.Issue_Material == result.Material[0].MaterialMasterId).Sum(x => x.Reqried_Qty);
                            var sum_qty = jobSheet.Getjobsheet_pair_Materials(list_).Where(x => x.Issue_Material == result.Material[0].MaterialMasterId).Sum(x => x.Qty);
                            var Sheet = jobSheet.Getjobsheet_pair_Materials(list_).Where(x => x.Issue_Material == result.Material[0].MaterialMasterId).Sum(x => x.sheet);
                            Balance_stock = Convert.ToDecimal(issueSlipManager.Get().Where(x => x.MaterialMasterId == items.Issue_Material && x.Jobworktype_Id == items.jobsheet_pair_id).Sum(x => x.CurrentIssue));

                            MultipleIssueSlip EntModel = new MultipleIssueSlip();
                            Models.StockModel.IssueSlip_SingleModel Model = new Models.StockModel.IssueSlip_SingleModel();


                            Model.IssueSlipID = 0;
                            Model.MultipleIssueSlipID = MultipleIssueSlipID;
                            Model.Jobworktype_Id = items.jobsheet_pair_id;
                            Model.IssueType = 15;
                            Model.IssueSlipNo = IssueSlipNo;
                            Model.InternalValue = "";
                            Model.DirectIssue_Style = "";
                            Model.StoreName = result.Material[0].StoreMasterId.ToString();
                            Model.CategoryName = result.Material[0].MaterialCategoryMasterId.ToString();
                            Model.InternalOrderingFor = 15;
                            Model.BuyerMasterId = 0;
                            Model.GroupName = result.Material[0].MaterialGroupMasterId.ToString();
                            Model.MaterialDescription = result.Material[0].MaterialMasterId.ToString();
                            Model.Piecies = 0;
                            Model.PieciesType = 0;
                            Model.LotNo = "";
                            Model.BalanceInThisListlot = (Convert.ToDouble(sum) - Convert.ToDouble(Balance_stock)).ToString();
                            Model.BalanceInthisListType = items.Uc_Noms_Uom;
                            Model.MachineName = 0;
                            Model.NoOfSets = Convert.ToDecimal(0.0);
                            Model.Color = result.Material[0].ColorMasterId.ToString();
                            Model.Rate = Convert.ToDecimal(result.Material[0].Price);
                            Model.SubtanceID = 0;

                            Model.RequiredQty = Convert.ToDecimal(sum);

                            Model.AlredayIssued = Balance_stock;
                            Model.CurrentIssue = Convert.ToDecimal(0.0);
                            Model.RequiredType = items.Qty_Uom;
                            Model.AlreadyUsedType = 0;
                            Model.CurrentIssuesType = 0;
                            Model.ConveyorID = 0;
                            Model.PiecesRequiredQTY = "0";
                            Model.PiecesRequiredQtyType = 0;
                            Model.PiecesAlreadyIssueType = 0;
                            Model.PiecesCurrentIssue = "0";
                            Model.PiecesCurrentType = 0;
                            Model.PiecesAlreadyIssue = "0";
                            Model.ConveyorID = 0;
                            Model.IssueSlipFK = 0;
                            Model.IssueDate = "0";
                            Model.TotalQty = 0;
                            Model.MaterialType = 0;
                            Model.Season = 0;
                            Model.Jw_Name = items.JW_Name;
                            Model.sheet = Sheet;
                            Model.TotalQty = sum_qty;
                            Model.jobsheet_pair_Code_id = items.jobsheet_pair_Code_id;
                            
                            Model.ExcessApproval = Excess_pecent.Job_ExcessPercentage == null ? string.Empty: Excess_pecent.Job_ExcessPercentage.ToString();
                            // Model.IssueDate = IssueDate;
                            MultipleIssueSlipID = save_Sheetlistissue_detail(Model);
                        }
                        else
                        {
                            var jobsheet_No = jobSheet.Getjobsheet_pair_id(items.jobsheet_pair_id);
                            numberList += result.Material[0].MaterialDescription + ",";
                        }
                    }
                }
                issueSlipMaterialList = issueSlipManager.GetMultipleIssueid(MultipleIssueSlipID);
                model.issueSlip_MaterialDetails = issueSlipMaterialList;
                var PartialView_ = RenderRazorViewToString("~/Views/Jobwork/Job_Master/JobWorkIssues/Partial/IssueDetailMaterial.cshtml", model);
                return Json(new { Message = numberList, MultipleIssueSlipID = MultipleIssueSlipID, issueSlipMaterialList = PartialView_ }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult MultipleIssueMaterialName(int? MaterialNameID, int? IssueSlipNo, string InternalOrderNo, string IssueDate, string jobsheet_pair_Code_id,string issueType)
        {
            //IssueSlip_SingleManager issueSlipManager = new IssueSlip_SingleManager();
            //IssueSlipManager issueSlip = new IssueSlipManager();
            //MaterialNameManager materialNameManager = new MaterialNameManager();
            //MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            //MaterialManager materialManager = new MaterialManager();
            //ApprovedPriceListManager approvedPricelistManager = new ApprovedPriceListManager();
            //List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
            //var issueItems = issueSlipManager.IsExistsIssuewithMaterial(IssueSlipNo.ToString(), MaterialNameID.ToString(), InternalOrderNo);
            //string[] formats = { "dd/MM/yyyy" };

            //DateTime FromDate = DateTime.ParseExact(IssueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //if (issueItems != null && issueItems.Count > 0)
            //{
            //    return Json(new { Message = "Already existed" }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    approvedPriceList = approvedPricelistManager.GetMaterialList(MaterialNameID.Value);
            //    if (approvedPriceList == null || approvedPriceList.Count == 0)
            //    {
            //        string Message = "Please make a entry on approved price list";
            //        return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
            //    }
            //    else
            //    {
            //        string CategoryName = "";
            //        List<MaterialNameMaster> materialNameMasterList = new List<MaterialNameMaster>();
            //        ColorManager colorManager = new ColorManager();
            //        var items = (from x in materialManager.Get()
            //                     join y in materialNameManager.Get()
            //                      on x.MaterialName equals y.MaterialMasterID
            //                     join z in colorManager.Get()
            //                     on x.ColorMasterId equals z.ColorMasterId into yG
            //                     from y1 in yG.DefaultIfEmpty()
            //                     where x.MaterialMasterId == MaterialNameID
            //                     select new { MaterialDescription = string.Format("{0} {1} {2}", y.MaterialDescription, x.OptionMaterialValue, y1 != null ? y1.Color != null ? y1.Color : string.Empty : string.Empty), x.MaterialMasterId, x.Price, ColorMasterId = (y1 != null ? y1.ColorMasterId != 0 ? y1.ColorMasterId : 0 : 0), x.Uom, x.UomUnit, x.SizeRangeMasterId, x.SubstanceMasterId, x.MaterialCategoryMasterId, x.MaterialGroupMasterId, x.GradeMasterId, x.StoreMasterId, x.PurchasePrimary, x.PrimaryUnit, x.PurchasePacket, x.PacketUnit, x.CurrencyMasterId, x.isImport, x.isLocal, x.isImportCustomer });
            //        int id = Convert.ToInt32(ExtensionMethod.HtmlHelper.CategoryName.Leathers);
            //        var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            //        List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
            //        listSizeItemMaterial = materialManager.GetSizeItemMaterial(distinctList.FirstOrDefault().MaterialMasterId);
            //        listSizeItemMaterial = listSizeItemMaterial.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();
            //        StoreMasterManager storeMasterManager = new StoreMasterManager();
            //        StoreMaster storeMaster = new StoreMaster();
            //        if (id == distinctList.FirstOrDefault().MaterialCategoryMasterId)
            //        {
            //            ExtensionMethod.HtmlHelper.CategoryName enumValue = (ExtensionMethod.HtmlHelper.CategoryName)id;
            //            CategoryName = enumValue.ToString();
            //        }
            //        List<MMS.Web.Models.PendingQty> ListOfPendingStockList = new List<MMS.Web.Models.PendingQty>();
            //        int MaterialType = distinctList.FirstOrDefault().isLocal == true ? 1 : distinctList.FirstOrDefault().isImportCustomer == true ? 2 : distinctList.FirstOrDefault().isImport == true ? 3 : 0;
            //        ListOfPendingStockList = issueSlip.MaterialOpeningStockIssueDate(MaterialNameID.Value, FromDate);
            //        var issueSizewiseStockList = issueSlip.MaterialOpeningSizeRangeIssueStock(MaterialNameID.Value);
            //        storeMaster = storeMasterManager.GetStoreMasterId(distinctList.FirstOrDefault().StoreMasterId);
            //        issueSizewiseStockList = issueSizewiseStockList.Where(x => x.IssueSize1 != 0).ToList();
            //        return Json(new { Material = distinctList, SizeRangelist = issueSizewiseStockList, store = storeMaster, CategoryName = CategoryName, BalanceStock = ListOfPendingStockList.Select(x => x.BalanceStock), approvedPriceList = approvedPriceList, fromdate_ = FromDate }, JsonRequestBehavior.AllowGet);
            //    }
            //}
            return Json(MultipleIssueMaterialName_detail(MaterialNameID, IssueSlipNo, InternalOrderNo, IssueDate, Convert.ToInt32(jobsheet_pair_Code_id), issueType), JsonRequestBehavior.AllowGet);
        }
        public object MultipleIssueMaterialName_detail(int? MaterialNameID, int? IssueSlipNo, string InternalOrderNo, string IssueDate, int jobsheet_pair_Code_id,string issueType)
        {
            IssueSlip_SingleManager issueSlipManager = new IssueSlip_SingleManager();
            IssueSlipManager issueSlip = new IssueSlipManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialManager materialManager = new MaterialManager();
            ApprovedPriceListManager approvedPricelistManager = new ApprovedPriceListManager();
            List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
            List<MaterialNameMaster> materialNameMasterList = new List<MaterialNameMaster>();
            ColorManager colorManager = new ColorManager();
            List<string> issueItems;
            IssueSlipManager issueSlipManager_Detail = new IssueSlipManager();
            JobLeather_leatherManager jobLeather = new JobLeather_leatherManager();
            JobLeather_leatherMaster Finished_Material = new JobLeather_leatherMaster();
            Job_ApprovedPriceManager Job_ApprovedPriceMaster = new Job_ApprovedPriceManager();
            MMS.Web.Models.JobworkModel.JobLeather_leatherModel JobLeather_leatherModel = new JobLeather_leatherModel();
            ProductionJobworkMasterManager ProductionJobworkMasterManager_ = new ProductionJobworkMasterManager();
            ProductionJobworkMaster ProductionJobworkMaster = new ProductionJobworkMaster();

            //   JobLeather_leatherModel.JobApproved_list.Job_ExcessPercentage = "0";
            if (issueType == "JobWork(Leather To Leather)")
            {
                
                Finished_Material = jobLeather.GetJobLeather_leather_ID(jobsheet_pair_Code_id);

                JobLeather_leatherModel.JobApproved_list = (from y in Job_ApprovedPriceMaster.Get()
                                     where y.JW_Name == Finished_Material.Jw_Name && y.Process_Name == Finished_Material.Process_Name && y.Rate_For_JW == Finished_Material.Rate
                                     select new JobApproved_list { Date = y.Date, Job_ExcessPercentage = y.Job_ExcessPercentage }).OrderByDescending(y => y.Date).FirstOrDefault();
            }
            else if (issueType == "JobWork(Sheet to Pairs)" || issueType == "")
            {

                JobLeather_leatherModel.JobApproved_list= new JobApproved_list { Date = DateTime.Now, Job_ExcessPercentage = "0" };



            }
            else if (issueType == "JobWork Production")
            {

                ProductionJobworkMaster = ProductionJobworkMasterManager_.GetproductionMaster_id(jobsheet_pair_Code_id);

                JobLeather_leatherModel.JobApproved_list = (from y in Job_ApprovedPriceMaster.Get()
                                                            where y.JW_Name == ProductionJobworkMaster.Jw_Name && y.Process_Name == ProductionJobworkMaster.Process_Name && y.Rate_For_JW == ProductionJobworkMaster.Rate && y.Stage_From== ProductionJobworkMaster.Stage_From && y.Stage_To== ProductionJobworkMaster.Stage_To
                                                            select new JobApproved_list { Date = y.Date, Job_ExcessPercentage = y.Job_ExcessPercentage }).OrderByDescending(y => y.Date).FirstOrDefault();
            
            }
            if (jobsheet_pair_Code_id != 0)
            {
                issueItems = issueSlipManager.IsExistsIssuewithMaterial_jobwork_Jobworktype_Id(IssueSlipNo.ToString(), MaterialNameID.ToString(), InternalOrderNo, jobsheet_pair_Code_id.ToString());
            }
            else
            {
                issueItems = issueSlipManager.IsExistsIssuewithMaterial_jobwork(IssueSlipNo.ToString(), MaterialNameID.ToString(), InternalOrderNo);
            }

        var  Balance_stock = Convert.ToDecimal(issueSlipManager_Detail.Get().Where(x => x.MaterialMasterId== MaterialNameID && x.Jobworktype_Id == jobsheet_pair_Code_id).Sum(x => x.CurrentIssue));
            string[] formats = { "dd/MM/yyyy" };

            DateTime FromDate = DateTime.ParseExact(IssueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (issueItems != null && issueItems.Count > 0)
            {
                //return Json(new { Message = "Already existed" }, JsonRequestBehavior.AllowGet);
                return new
                {
                    Message = "Already existed"

                };
            }
            else
            {
                approvedPriceList = approvedPricelistManager.GetMaterialList(MaterialNameID.Value);
                if (approvedPriceList == null || approvedPriceList.Count == 0)
                {
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
                    string Message = "Please make a entry on approved price list";
                    //  return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
                    return new
                    {
                        Message = Message,
                        Material = distinctList
                    };
                }
                else
                {
                    string CategoryName = "";

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
                    List<MMS.Web.Models.PendingQty> ListOfPendingStockList = new List<MMS.Web.Models.PendingQty>();
                    int MaterialType = distinctList.FirstOrDefault().isLocal == true ? 1 : distinctList.FirstOrDefault().isImportCustomer == true ? 2 : distinctList.FirstOrDefault().isImport == true ? 3 : 0;
                    ListOfPendingStockList = issueSlip.MaterialOpeningStockIssueDate(MaterialNameID.Value, FromDate);
                    var issueSizewiseStockList = issueSlip.MaterialOpeningSizeRangeIssueStock(MaterialNameID.Value);
                    storeMaster = storeMasterManager.GetStoreMasterId(distinctList.FirstOrDefault().StoreMasterId);
                    issueSizewiseStockList = issueSizewiseStockList.Where(x => x.IssueSize1 != 0).ToList();

                    string Message = "";
                    return new
                    {
                        Material = distinctList,
                        SizeRangelist = issueSizewiseStockList,
                        store = storeMaster,
                        CategoryName = CategoryName,
                        BalanceStock = ListOfPendingStockList.Select(x => x.BalanceStock),
                        approvedPriceList = approvedPriceList,
                        fromdate_ = FromDate,
                        Message = Message,
                        Balance_stock= Balance_stock,
                        Finished_Material = Finished_Material.Finished_Material,
                        Excess_pecent= JobLeather_leatherModel.JobApproved_list.Job_ExcessPercentage
                    };
                }
            }
        }



        public ActionResult Get_detail_Requrid_qty(int jobWorkId, string MaterialDescription, int IssueType)
        {
            try
            {
                string Message = "";
                JobLeather_leatherManager jobLeather = new JobLeather_leatherManager();
                List<IssueSlip_MaterialDetails> issueSlipMaterialList = new List<IssueSlip_MaterialDetails>();
                IssueSlipManager issueSlipManager = new IssueSlipManager();
                JobLeather_leatherMaster Jw_Name = new JobLeather_leatherMaster();


                var materaildesc = jobLeather.GetJobLeather_leather_ID(jobWorkId);

                return Json(new { Message = Message, materaildesc = materaildesc }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
        #region Direct issue
        public ActionResult Save_jobLeather_issue(Models.StockModel.IssueSlip_SingleModel Model)
        {
            Data.StoredProcedureModel.IssueSlip_SingleModel model = new Data.StoredProcedureModel.IssueSlip_SingleModel();
            //   Models.StockModel.IssueSlip_SingleModel model = new Models.StockModel.IssueSlip_SingleModel();
            MultipleIssueSlip EntModel = new MultipleIssueSlip();
            IssueSlip_SingleManager ObjManager = new IssueSlip_SingleManager();
            List<IssueSlip_MaterialDetails> issueSlipMaterialList = new List<IssueSlip_MaterialDetails>();
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
            EntModel.IsJobWork = true;
            string username = Session["UserName"].ToString();
            EntModel.CreatedBy = username;
            MultipleIssueSlip issueSlip = new MultipleIssueSlip();
            MultipleIssueSlip issueSlip_ = new MultipleIssueSlip();
            GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
            IssueSlip_MaterialDetails issueSlip_MaterialDetails = new IssueSlip_MaterialDetails();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            IssueSlip_MaterialDetails issueSlipMaterial = new IssueSlip_MaterialDetails();
            IssueSlip_MaterialDetails issueSlipMaterials = new IssueSlip_MaterialDetails();
            issueSlip_ = ObjManager.MultipleIssuePost(EntModel);
 //       var IssueDate_=    DateTime.ParseExact(Model.IssueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (issueSlip_ != null && issueSlip_.MultipleIssueSlipID != 0)
            {
                GRNTypeManager grnTypeManager = new GRNTypeManager();

                StoreMaster storeMaster = new StoreMaster();
                StoreMasterManager storeMasterManager = new StoreMasterManager();
                MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
                MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
                MaterialGroupManager materialGroupManager = new MaterialGroupManager();
                MaterialGroupMaster_ materialGroupMaster = new MaterialGroupMaster_();
                MaterialMaster materialMaster = new MaterialMaster();
                MaterialManager materialManager = new MaterialManager();
                MaterialNameMaster materialNameMaster = new MaterialNameMaster();
                MaterialNameManager materialNameManager = new MaterialNameManager();
                ColorManager colorManager = new ColorManager();
                ColorMaster colorMaster = new ColorMaster();
                SubstanceMaster substanceMaster = new SubstanceMaster();
                SubstanceMasterManager substanceMasterManager = new SubstanceMasterManager();
                storeMaster = storeMasterManager.GetStoreMasterId(Convert.ToInt32(Model.StoreName));
                grnTypeMaster = grnTypeManager.GetIssueTypeMasterId(Model.IssueType);
                materialCategoryMaster = materialCategoryManager.GetMaterialCategoryMaster(Convert.ToInt32(Model.CategoryName));
                materialGroupMaster = materialGroupManager.GetMaterialGroupMaster_Id(Convert.ToInt32(Model.GroupName));
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
                issueSlipMaterials.Style = Model.Style.ToString();
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

                issueSlipMaterials.SupplierName_Lotno = Model.SupplierName_Lotno;
                issueSlipMaterials.SupplierNameId = Model.SupplierNameId;
                issueSlipMaterials.PiecesCurrentIssue = Convert.ToDecimal(Model.PiecesCurrentIssue);
                issueSlipMaterials.PiecesCurrentType = Model.PiecesCurrentType;
                issueSlipMaterials.IssueSlipId = Model.IssueSlipID;
                issueSlipMaterials.DirectIssue_Style = Model.DirectIssue_Style;
                issueSlipMaterials.CurrentStock = Model.CurrentStock;
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
                issueSlipMaterials.Component_Id = Model.Component_Id;
                if (colorMaster == null)
                {
                    issueSlipMaterials.ColourId = "";
                }
                else
                {
                    issueSlipMaterials.ColourId = colorMaster.Color;
                }
                if (Model.Type== "JobWork Production")
                {
                    issueSlipMaterials.Jobworktype_Id = Model.Jobworktype_Id;

                    issueSlipMaterials.jobsheet_pair_Code_id = Model.jobsheet_pair_Code_id.ToString();
                }
                else
                {
                    issueSlipMaterials.Jobworktype_Id = Model.Jobworktype_Id;

                    issueSlipMaterials.jobsheet_pair_Code_id = Model.Jobworktype_Id.ToString();
                }
                
                issueSlipMaterials.Jw_Name = Model.Jw_Name;
                issueSlipMaterials.BalanceInThisListlot = Model.BalanceInThisListlot.ToString();
                issueSlipMaterials.BalanceInthisListType = Model.BalanceInthisListType;
                issueSlipMaterials.Finished_Material = Model.Finished_Material;
                issueSlipMaterials.ExcessApproval = Model.ExcessApproval;
                issueSlip_MaterialDetails = issueSlipManager.Post(issueSlipMaterials);
                issueSlipMaterialList = issueSlipManager.GetMultipleIssueid(EntModel.MultipleIssueSlipID);
                model.issueSlip_MaterialDetails = issueSlipMaterialList;



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
            var PartialView_ = RenderRazorViewToString("~/Views/Jobwork/Job_Master/JobWorkIssues/Partial/IssueDetailMaterial.cshtml", model);
            return Json(new { issue = issueSlip_MaterialDetails, Parent = issueSlip_, grnTypeMaster = grnTypeMaster, issueSlipMaterialList = PartialView_ }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region partialview
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion
        #region save_sheetlistissue_detail
        public int save_Sheetlistissue_detail(Models.StockModel.IssueSlip_SingleModel Model)
        {
            Data.StoredProcedureModel.IssueSlip_SingleModel model = new Data.StoredProcedureModel.IssueSlip_SingleModel();
            //   Models.StockModel.IssueSlip_SingleModel model = new Models.StockModel.IssueSlip_SingleModel();
            MultipleIssueSlip EntModel = new MultipleIssueSlip();
            IssueSlip_SingleManager ObjManager = new IssueSlip_SingleManager();
            List<IssueSlip_MaterialDetails> issueSlipMaterialList = new List<IssueSlip_MaterialDetails>();
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
            EntModel.IsJobWork = true;
            string username = Session["UserName"].ToString();
            EntModel.CreatedBy = username;
            MultipleIssueSlip issueSlip = new MultipleIssueSlip();
            MultipleIssueSlip issueSlip_ = new MultipleIssueSlip();
            GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
            IssueSlip_MaterialDetails issueSlip_MaterialDetails = new IssueSlip_MaterialDetails();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            IssueSlip_MaterialDetails issueSlipMaterial = new IssueSlip_MaterialDetails();
            IssueSlip_MaterialDetails issueSlipMaterials = new IssueSlip_MaterialDetails();
            issueSlip_ = ObjManager.MultipleIssuePost(EntModel);

            if (issueSlip_ != null && issueSlip_.MultipleIssueSlipID != 0)
            {
                GRNTypeManager grnTypeManager = new GRNTypeManager();

                StoreMaster storeMaster = new StoreMaster();
                StoreMasterManager storeMasterManager = new StoreMasterManager();
                MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
                MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
                MaterialGroupManager materialGroupManager = new MaterialGroupManager();
                MaterialGroupMaster_ materialGroupMaster = new MaterialGroupMaster_();
                MaterialMaster materialMaster = new MaterialMaster();
                MaterialManager materialManager = new MaterialManager();
                MaterialNameMaster materialNameMaster = new MaterialNameMaster();
                MaterialNameManager materialNameManager = new MaterialNameManager();
                ColorManager colorManager = new ColorManager();
                ColorMaster colorMaster = new ColorMaster();
                SubstanceMaster substanceMaster = new SubstanceMaster();
                SubstanceMasterManager substanceMasterManager = new SubstanceMasterManager();
                storeMaster = storeMasterManager.GetStoreMasterId(Convert.ToInt32(Model.StoreName));
                grnTypeMaster = grnTypeManager.GetIssueTypeMasterId(Model.IssueType);
                materialCategoryMaster = materialCategoryManager.GetMaterialCategoryMaster(Convert.ToInt32(Model.CategoryName));
                materialGroupMaster = materialGroupManager.GetMaterialGroupMaster_Id(Convert.ToInt32(Model.GroupName));
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
                issueSlipMaterials.Style = Model.Style.ToString();
                issueSlipMaterials.NoOfSets = Model.NoOfSets;
                issueSlipMaterials.MachineName = Model.MachineName;
                issueSlipMaterials.SubtanceID = Model.SubtanceID;
                issueSlipMaterials.LotNo = !string.IsNullOrEmpty(Model.LotNo) ? Model.LotNo.ToString() : "";
                issueSlipMaterials.RequiredType = Model.RequiredType;
                issueSlipMaterials.IssueDate = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");

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
                issueSlipMaterials.BalanceInThisListlot = Model.BalanceInThisListlot;
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
                issueSlipMaterials.Jobworktype_Id = Model.Jobworktype_Id;
                issueSlipMaterials.Jw_Name = Model.Jw_Name;
                issueSlipMaterials.sheet = Model.sheet;
                issueSlipMaterials.TotalQty = Model.TotalQty;
                issueSlipMaterials.jobsheet_pair_Code_id = Model.jobsheet_pair_Code_id.ToString();
                issueSlipMaterials.ExcessApproval = Model.ExcessApproval;
                issueSlip_MaterialDetails = issueSlipManager.Post(issueSlipMaterials);
                issueSlipMaterialList = issueSlipManager.GetMultipleIssueid(EntModel.MultipleIssueSlipID);
                model.issueSlip_MaterialDetails = issueSlipMaterialList;

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
            int a = 0;
            //  var PartialView_ = RenderRazorViewToString("~/Views/Jobwork/Job_Master/JobWorkIssues/Partial/IssueDetailMaterial.cshtml", model);
            return issueSlip_.MultipleIssueSlipID;
        }
        #endregion
        #region getsheetmaterialDetail
        public object MultipleIssueMaterialName_detail_SheetMAterial(int? MaterialNameID, int? IssueSlipNo, string InternalOrderNo, string IssueDate)
        {

            IssueSlip_SingleManager issueSlipManager = new IssueSlip_SingleManager();
            IssueSlipManager issueSlip = new IssueSlipManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialManager materialManager = new MaterialManager();
            ApprovedPriceListManager approvedPricelistManager = new ApprovedPriceListManager();
            List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
            var issueItems = issueSlipManager.IsExistsIssuewithMaterial(IssueSlipNo.ToString(), MaterialNameID.ToString(), InternalOrderNo);
            string[] formats = { "dd/MM/yyyy" };

            DateTime FromDate = DateTime.ParseExact(IssueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            approvedPriceList = approvedPricelistManager.GetMaterialList(MaterialNameID.Value);
            if (approvedPriceList == null || approvedPriceList.Count == 0)
            {
                string Message = "Please make a entry on approved price list";
                return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string CategoryName = "";
                List<MaterialNameMaster> materialNameMasterList = new List<MaterialNameMaster>();
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
                List<MMS.Web.Models.PendingQty> ListOfPendingStockList = new List<MMS.Web.Models.PendingQty>();
                int MaterialType = distinctList.FirstOrDefault().isLocal == true ? 1 : distinctList.FirstOrDefault().isImportCustomer == true ? 2 : distinctList.FirstOrDefault().isImport == true ? 3 : 0;
                ListOfPendingStockList = issueSlip.MaterialOpeningStockIssueDate(MaterialNameID.Value, FromDate);
                var issueSizewiseStockList = issueSlip.MaterialOpeningSizeRangeIssueStock(MaterialNameID.Value);
                storeMaster = storeMasterManager.GetStoreMasterId(distinctList.FirstOrDefault().StoreMasterId);
                issueSizewiseStockList = issueSizewiseStockList.Where(x => x.IssueSize1 != 0).ToList();
                return Json(new { Material = distinctList, SizeRangelist = issueSizewiseStockList, store = storeMaster, CategoryName = CategoryName, BalanceStock = ListOfPendingStockList.Select(x => x.BalanceStock), approvedPriceList = approvedPriceList, fromdate_ = FromDate }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GEt_Wetblue_current_issue(string MaterialNameID, string FromDate, string LOTNo, string SupplierNameId)
        {
            List<MMS.Web.Models.PendingQty> ListOfPendingStockList = new List<MMS.Web.Models.PendingQty>();
            IssueSlipManager issueSlip = new IssueSlipManager();
            DateTime FromDate_ = DateTime.ParseExact(FromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ListOfPendingStockList = issueSlip.MaterialOpeningStockIssueDate_wetblue(Convert.ToInt32(MaterialNameID), FromDate_, Convert.ToInt32(LOTNo), Convert.ToInt32(SupplierNameId));


            // return PartialView("~/Views/Stock/GRN/Partial/GoodReceipeDetailsNew_.cshtml", model);
            return Json(new { BalanceStock = ListOfPendingStockList.Select(x => x.BalanceStock), }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PDF Generation
        public FileResult PDFGenerate(string GateEntryNo)
        {
         
            string file = HostingEnvironment.MapPath("~/App_Data/PdfTemplate/index.html");
            string contents = System.IO.File.ReadAllText(file);
            string filename = MMS.Web.PDFGeneration.PdfGeneration_Jobwork.PrintPdfGeneration_Jobwork(GateEntryNo, contents);

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
