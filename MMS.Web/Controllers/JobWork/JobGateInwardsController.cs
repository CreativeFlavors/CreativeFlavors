using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Core.Entities;
using MMS.Core.Entities.GateEntry;
using MMS.Core.Entities.Stock;
using MMS.Data.Context;
using MMS.Repository.Managers;
using MMS.Repository.Managers.GateEntryManager;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.GateEntryModel;
using MMS.Core.Entities.JobWork;
using MMS.Repository.Managers.JobWork;
using MMS.Web.Models.JobworkModel;
using System.Globalization;
using System.IO;
using MMS.Common;
using MMS.Data.StoredProcedureModel;

namespace MMS.Web.Controllers.JobWork
{
    [CustomFilter]
    public class JobGateInwardsController : Controller
    {
        //
        // GET: /JobGateInwards/

       // [CustomFilter]

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult JobGateInwardsMaster()
        {
            GateEntryInwardMaterials model = new GateEntryInwardMaterials();
            return View("~/Views/Jobwork/Job_Master/JobGateway/JobGateInwardsMaster.cshtml", model);
        }
        public ActionResult JobGateEntryInwardMaterialsGrid()
        {
            GateEntryInwardMaterials model = new GateEntryInwardMaterials();
            List<GateEntryInwardMaterialsMaster> geMaterialGridList = new List<GateEntryInwardMaterialsMaster>();
            GateEntryInwardMaterialsManager manager = new GateEntryInwardMaterialsManager();
            model.GateEntryInwardMaterialList = manager.Get();
            geMaterialGridList = manager.Get().ToList();
            var groupList = geMaterialGridList.GroupBy(x => x.GateEntryNo.ToString().Trim()).Select(g => g.First()).ToList();
            model.GateEntryInwardMaterialList = groupList;
            return PartialView("~/Views/Jobwork/Job_Master/JobGateway/Partial/Jobgateway_Grid.cshtml", model);
        }
        [HttpPost]
        public ActionResult GateEntryInwardMaterialsDetails(int GateEntryInwardId)
        {
            JobworkModel vm = new JobworkModel();
            JobworkManager jobworkManager = new JobworkManager();
            GateEntryInwardMaterialsMaster model = new GateEntryInwardMaterialsMaster();
            GateEntryInwardMaterials viewmodel = new GateEntryInwardMaterials();
            GateEntryInwardMaterialsManager inwardMaterialManager = new GateEntryInwardMaterialsManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            UOMManager uOMManager = new UOMManager();
            SizeRangeDetailsManager srdManager = new SizeRangeDetailsManager();
            StoreMasterManager stManager = new StoreMasterManager();
            List<GEInwardMaterialGrid> geMaterialGridList = new List<GEInwardMaterialGrid>();
            GateEntryInwardMaterialsMaster GateEntryInwardMaterialsMaster = new GateEntryInwardMaterialsMaster();
            List<Gridedit> items_list = new List<Gridedit>();
            IssueSlipManager issueSlipManager = new IssueSlipManager();

            model = inwardMaterialManager.GetGateEntryInMaterialById(GateEntryInwardId);
            int count= inwardMaterialManager.GEMaterialListGridBasedOnGateEntryNo_(model.GateEntryNo).Count();
            if (count == 0)
            {
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
                    viewmodel.PendingQuantity = viewmodel.PendingQuantity;
                    viewmodel.Quantity = model.Quantity;
                    viewmodel.StoresId = model.StoresId;
                    viewmodel.MaterialNameId = model.MaterialNameId;
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
                    viewmodel.GEMaterialsGrid = GridList;
                    ViewBag.Name = model.InwardPo;
                    var PartialView_ = RenderRazorViewToString("~/Views/Jobwork/Job_Master/JobGateway/Partial/JobGateway_EditDetail.cshtml", viewmodel);
                    return Json(new { PartialView = PartialView_, items = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string year = DateTime.Now.Year.ToString();
                    viewmodel.GateEntryNo = GetInwardEntryID();
                    viewmodel.GateEntryNo = "GEIM " + viewmodel.GateEntryNo + "/" + year;
                    viewmodel.GEMaterialsGrid = geMaterialGridList;
                    //var PartialView_= PartialView("~/Views/Jobwork/Job_Master/JobGateway/Partial/JobGateway_EditDetail.cshtml", viewmodel);
                    var PartialView_ = RenderRazorViewToString("~/Views/Jobwork/Job_Master/JobGateway/Partial/JobGateway_EditDetail.cshtml", viewmodel);
                    return Json(new { PartialView = PartialView_, items = "" }, JsonRequestBehavior.AllowGet);

                }
                // return PartialView("~/Views/Jobwork/Job_Master/JobGateway/Partial/JobGateway_EditDetail.cshtml", viewmodel);
            }
            else if (count >= 1)
            {
                var issue_mat = "";
                ComponentManager ComponentManager = new ComponentManager();
                if (model.InwardMaterialType == 17)
                {
                    //Production Jobwork 
                    MaterialNameManager MaterialNameManager = new MaterialNameManager();
                    MaterialManager MaterialManager = new MaterialManager();
                    viewmodel.InwardEntryDateTime = model.InwardEntryDateTime;
                    viewmodel.InwardMaterialType = model.InwardMaterialType;
                    viewmodel.IsReturned = model.IsReturned;
                    viewmodel.GateEntryNo = model.GateEntryNo;
                    viewmodel.IsNewInward = model.IsNewInward;
                    viewmodel.IsJobWork = model.IsJobWork;
                    viewmodel.DcRefNo = model.DcRefNo;
                    viewmodel.DcRefDate = model.DcRefDate;
                    viewmodel.SupplierId = model.SupplierId;
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
                    //var items_list = "";
                   
                    var items_list_ = (from G in inwardMaterialManager.Get()
                                       join X in ComponentManager.Get()
                                       on G.ComponentId equals X.ComponentMasterId
                                      
                                       // join J in jobworkManager.Get()
                                       //on G.SupplierId equals J.JW_Id
                                       where G.GateEntryNo == model.GateEntryNo && G.IsDeleted == false
                                       select new { MaterialDescription = X.ComponentName, G.Quantity, G.GateEntryInwardId, G.PendingQuantity, MaterialNameId=G.ComponentId, G.TotalQty, G.Rate, G.Value, G.DcRefNo, G.Process_Name, G.jobsheet_pair_Code_id, G.IssueSlipId }).ToList();


                    Gridedit Gridedit = new Gridedit();
                    var IssueSlipId = items_list_.GroupBy(x => x.IssueSlipId).Select(g => g.First()).Select(x => x.IssueSlipId).ToList();
                    foreach (var item in items_list_)
                    {
                        var IssueSlipId_grid = issueSlipManager.Get().Where(x => x.IssueSlipId == Convert.ToInt32(item.IssueSlipId)).FirstOrDefault();


                        GateEntryInwardMaterialsMaster = inwardMaterialManager.Getpendinglist_jobsheet_pair_Code_id_Production(Convert.ToInt32(item.MaterialNameId), item.DcRefNo.ToString());
                        int pending_qty = 0;


                        var pending_qty_total = inwardMaterialManager.Getpendinglist_sumqty_jobsheet_pair_Code_id_Production(Convert.ToInt32(item.MaterialNameId), item.DcRefNo.ToString()).Sum(x => Convert.ToInt32(x.Quantity));

                        Gridedit = new Gridedit();
                        Gridedit.pending_qty = Convert.ToInt32(GateEntryInwardMaterialsMaster.TotalQty) - (pending_qty_total);
                        Gridedit.MaterialDescription = item.MaterialDescription;
                        Gridedit.Quantity = item.Quantity;
                        Gridedit.GateEntryInwardId = item.GateEntryInwardId;
                        Gridedit.MaterialNameId = item.MaterialNameId.ToString();
                        Gridedit.TotalQty = item.TotalQty;
                        Gridedit.Rate = item.Rate;
                        Gridedit.Value = item.Value;
                        Gridedit.DcRefNo = item.DcRefNo;
                        Gridedit.Process_Name = item.Process_Name;
                        Gridedit.jobsheet_pair_Code_id = item.jobsheet_pair_Code_id;

                        Gridedit.Issue_Material = 0;
                        Gridedit.IssueSlipId = 0;
                        items_list.Add(Gridedit);
                    }
                }
                else
                {
                    MaterialNameManager MaterialNameManager = new MaterialNameManager();
                    MaterialManager MaterialManager = new MaterialManager();
                    viewmodel.InwardEntryDateTime = model.InwardEntryDateTime;
                    viewmodel.InwardMaterialType = model.InwardMaterialType;
                    viewmodel.IsReturned = model.IsReturned;
                    viewmodel.GateEntryNo = model.GateEntryNo;
                    viewmodel.IsNewInward = model.IsNewInward;
                    viewmodel.IsJobWork = model.IsJobWork;
                    viewmodel.DcRefNo = model.DcRefNo;
                    viewmodel.DcRefDate = model.DcRefDate;
                    viewmodel.SupplierId = model.SupplierId;
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
                    //var items_list = "";
                  //  var issue_mat = "";
                    var items_list_ = (from G in inwardMaterialManager.Get()
                                       join X in MaterialManager.Get()
                                       on G.MaterialNameId equals X.MaterialMasterId
                                       join y in MaterialNameManager.Get()
                                       on X.MaterialName equals y.MaterialMasterID
                                       // join J in jobworkManager.Get()
                                       //on G.SupplierId equals J.JW_Id
                                       where G.GateEntryNo == model.GateEntryNo && G.IsDeleted == false
                                       select new { MaterialDescription = y.MaterialDescription, G.Quantity, G.GateEntryInwardId, G.PendingQuantity, G.MaterialNameId, G.TotalQty, G.Rate, G.Value, G.DcRefNo, G.Process_Name, G.jobsheet_pair_Code_id, G.IssueSlipId }).ToList();


                    Gridedit Gridedit = new Gridedit();
                    var IssueSlipId = items_list_.GroupBy(x => x.IssueSlipId).Select(g => g.First()).Select(x => x.IssueSlipId).ToList();


                    foreach (var item in IssueSlipId)
                    {
                        var issue_gateqty_currrent_val = inwardMaterialManager.Get().Where(x => x.GateEntryInwardId == GateEntryInwardId).Sum(x => Convert.ToInt32(x.Quantity));
                        var issue_gateqty = inwardMaterialManager.Get().Where(x => x.IssueSlipId == item).Sum(x => Convert.ToInt32(x.Quantity));
                        var dcid = issueSlipManager.GetGRNSelectedRow(Convert.ToInt32(item));
                        decimal current_val = Convert.ToDecimal(dcid.CurrentIssue) + Convert.ToDecimal(issue_gateqty_currrent_val);
                        if (issue_gateqty == 0)
                        {
                            issue_mat += "," + dcid.MaterialMasterId + "_" + dcid.IssueSlipId + ":" + current_val;
                        }
                        else
                        {
                            decimal CurrentIssue = Convert.ToDecimal(current_val) - Convert.ToDecimal(issue_gateqty);
                            issue_mat += "," + dcid.MaterialMasterId + "_" + dcid.IssueSlipId + ":" + CurrentIssue;
                        }
                    }

                    foreach (var item in items_list_)
                    {
                        var IssueSlipId_grid = issueSlipManager.Get().Where(x => x.IssueSlipId == Convert.ToInt32(item.IssueSlipId)).FirstOrDefault();
                        Gridedit = new Gridedit();
                        GateEntryInwardMaterialsMaster = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_jobsheet_pair_Code_id(Convert.ToInt32(item.MaterialNameId), item.DcRefNo.ToString(), Convert.ToInt32(item.Process_Name), Convert.ToInt32(item.jobsheet_pair_Code_id));
                        int pending_qty = 0;


                        var pending_qty_total = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty_jobsheet_pair_Code_id(Convert.ToInt32(item.MaterialNameId), item.DcRefNo.ToString(), Convert.ToInt32(item.Process_Name), Convert.ToInt32(item.jobsheet_pair_Code_id)).Sum(x => Convert.ToInt32(x.Quantity));

                        var pending_qty_GateEntryInwardId = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty_jobsheet_pair_Code_id(Convert.ToInt32(item.MaterialNameId), item.DcRefNo.ToString(), Convert.ToInt32(item.Process_Name), Convert.ToInt32(item.jobsheet_pair_Code_id)).Sum(x => Convert.ToInt32(x.Quantity));


                        Gridedit.pending_qty = Convert.ToInt32(GateEntryInwardMaterialsMaster.TotalQty) - (pending_qty_total);
                        Gridedit.MaterialDescription = item.MaterialDescription;
                        Gridedit.Quantity = item.Quantity;
                        Gridedit.GateEntryInwardId = item.GateEntryInwardId;
                        Gridedit.MaterialNameId = item.MaterialNameId.ToString();
                        Gridedit.TotalQty = item.TotalQty;
                        Gridedit.Rate = item.Rate;
                        Gridedit.Value = item.Value;
                        Gridedit.DcRefNo = item.DcRefNo;
                        Gridedit.Process_Name = item.Process_Name;
                        Gridedit.jobsheet_pair_Code_id = item.jobsheet_pair_Code_id;

                        Gridedit.Issue_Material = IssueSlipId_grid.MaterialMasterId;
                        Gridedit.IssueSlipId = IssueSlipId_grid.IssueSlipId;
                        items_list.Add(Gridedit);


                    }
                }
                    var PartialView_ =  RenderRazorViewToString("~/Views/Jobwork/Job_Master/JobGateway/Partial/JobGateway_EditDetail.cshtml", viewmodel);
                if(issue_mat !="")
                {

                    issue_mat= issue_mat.Substring(1);
                }
                return Json(new { PartialView= PartialView_, items= items_list, Issue_material = issue_mat }, JsonRequestBehavior.AllowGet);
            }
            return PartialView("~/Views/Jobwork/Job_Master/JobGateway/Partial/JobGateway_EditDetail.cshtml", viewmodel);
        }
        public string GetInwardEntryID()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateGEInwardID();
            ID++;
            return ID.ToString();
        }
        #region getdetail
        public ActionResult Get_jobgetIssuelist_forgateinwards(string IssueID,string GateEntryInwardId)
        {
            IssueSlip_SingleManager Manager = new IssueSlip_SingleManager();
            MultipleIssueSlip MultipleIssueSlip = new MultipleIssueSlip();
            List<MultipleIssueSlip> MultipleIssueSlip_List = new List<MultipleIssueSlip>();
            JobSheet_PairManager JobSheet_PairManager = new JobSheet_PairManager();
            JobSheet_pairCodeMaster JobSheet_pairCodeMaster = new JobSheet_pairCodeMaster();
            JobLeather_leatherManager JobLeather_leatherManager = new JobLeather_leatherManager();
            JobLeather_leatherMaster JobLeather_leatherMaster = new JobLeather_leatherMaster();
            JobOtherWorkManager JobOtherWorkManager = new JobOtherWorkManager();
            JobOtherWorkMaster JobOtherWorkMaster = new JobOtherWorkMaster();
            GRNTypeManager GRNTypeManager = new GRNTypeManager();
            GateEntryInwardMaterialsManager inwardMaterialManager = new GateEntryInwardMaterialsManager();
            GateEntryInwardMaterialsMaster GateEntryInwardMaterialsMaster = new GateEntryInwardMaterialsMaster();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            List<JobSheet_PairMaster> JobSheet_PairMaster = new List<JobSheet_PairMaster>();
            List<IssueSlip_MaterialDetails> IssueObj = new List<IssueSlip_MaterialDetails>();
         //   List<IssueSlip_MaterialDetails> IssueObj_ = new List<IssueSlip_MaterialDetails>();
            int[] list_ = IssueID.Split(',').Select(int.Parse).ToArray();
            //var IssueObj_ = "";
            MultipleIssueSlip_List = Manager.GetMultipleIssueGRNSelectedRow_CONTAIN(list_);
           // foreach ()
            MultipleIssueSlip = Manager.GetMultipleIssueGRNSelectedRow(Convert.ToInt32(list_[0]));
            var IssueType_= GRNTypeManager.GetIssueTypeMasterId(MultipleIssueSlip.IssueType);

            int[] list_new = MultipleIssueSlip_List.Select(x => x.MultipleIssueSlipID).ToArray();
            //foreach (var item in MultipleIssueSlip_List)
            //{
            //     IssueObj_+= (issueSlipManager.GetMultipleIssueid(MultipleIssueSlip.MultipleIssueSlipID).GroupBy(x => x.Jobworktype_Id).Select(x => x.Key).ToList());
            //}
          var  IssueObj_ = (issueSlipManager.GetMultipleIssueid_list(list_new).GroupBy(x => x.Jobworktype_Id).Select(x => x.Key).ToList());
            //Job sheet 
            if (MultipleIssueSlip.IssueType == 15 || IssueType_.IssueType == "JobWork(Sheet to Pairs)") 
            {
                List<MMS.Data.StoredProcedureModel.Jw_JobSheet_pair> Aplist = new List<MMS.Data.StoredProcedureModel.Jw_JobSheet_pair>();

                List<MMS.Data.StoredProcedureModel.Jw_JobSheet_pair> Aplist_withDc = new List<MMS.Data.StoredProcedureModel.Jw_JobSheet_pair>();
                JobSheet_pairModel Jw_ApprovedPriceModel = new JobSheet_pairModel();
                MMS.Data.StoredProcedureModel.Jw_JobSheet_pair Aplist_new = new MMS.Data.StoredProcedureModel.Jw_JobSheet_pair();


                Aplist = JobSheet_PairManager.GetJobwork_Jw_JobSheet_pairGrid(Convert.ToInt32(MultipleIssueSlip.Jobworktype_Id));
                var issue_mat ="";
                foreach (var item in IssueObj_)
                {
                    var dcid= (issueSlipManager.GetMultipleIssueid_list(list_new).Where(x=>x.Jobworktype_Id==item.Value).FirstOrDefault());
                    var Code_id = JobSheet_PairManager.GetJobwork_Jw_JobSheet_pairGrid(0).Where(x => x.jobsheet_pair_id == item.Value).FirstOrDefault();
                    var issue_gateqty = inwardMaterialManager.Get().Where(x => x.IssueSlipId == dcid.IssueSlipId.ToString()).Sum(x => Convert.ToInt32(x.Quantity));
                    if (issue_gateqty == 0)
                    {
                       // issue_mat += "," + dcid.MaterialMasterId+"_"+dcid.IssueSlipId + ":" + dcid.CurrentIssue;
                        issue_mat += "," + dcid.MaterialMasterId + "_" + dcid.IssueSlipId + ":" + Code_id.qty;
                    }
                    else
                    {
                    decimal CurrentIssue =   Convert.ToDecimal( dcid.CurrentIssue) - Convert.ToDecimal(issue_gateqty);
                       // issue_mat += "," + dcid.MaterialMasterId + "_" + dcid.IssueSlipId + ":" + CurrentIssue;
                        issue_mat += "," + dcid.MaterialMasterId + "_" + dcid.IssueSlipId + ":" + Code_id.qty;
                    }
                    //Aplist.AddRange(JobSheet_PairManager.GetJobwork_Jw_JobSheet_pairGrid(0).Where(x => x.jobsheet_pair_Code_id == Code_id.jobsheet_pair_Code_id && x.Issue_Material == Code_id.Issue_Material));
                    Aplist = JobSheet_PairManager.GetJobwork_Jw_JobSheet_pairGrid(0).Where(x => x.jobsheet_pair_Code_id == Code_id.jobsheet_pair_Code_id && x.Issue_Material == Code_id.Issue_Material).ToList();
                    foreach (var items in Aplist)
                    {
                        var process = JobSheet_PairManager.Get().Where(x=>x.jobsheet_pair_id==items.jobsheet_pair_id).FirstOrDefault();
                        GateEntryInwardMaterialsMaster = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_jobsheet_pair_Code_id(Convert.ToInt32(items.Material), dcid.IssueSlipFK.ToString(), Convert.ToInt32(process.Process_Name), Convert.ToInt32(items.jobsheet_pair_Code_id));
                        int pending_qty = 0;


                        var pending_qty_total = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty_jobsheet_pair_Code_id(Convert.ToInt32(items.Material), dcid.IssueSlipFK.ToString(), Convert.ToInt32(process.Process_Name), Convert.ToInt32(items.jobsheet_pair_Code_id)).Sum(x => Convert.ToInt32(x.Quantity));

                        var pending_qty_GateEntryInwardId = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty_jobsheet_pair_Code_id(Convert.ToInt32(items.Material), dcid.IssueSlipFK.ToString(), Convert.ToInt32(process.Process_Name), Convert.ToInt32(items.jobsheet_pair_Code_id)).Sum(x => Convert.ToInt32(x.Quantity));


                        Aplist_new = new MMS.Data.StoredProcedureModel.Jw_JobSheet_pair();


                        Aplist_new = items;
                        Aplist_new.DcRefNo =Convert.ToInt32(dcid.IssueSlipFK);
                        if (GateEntryInwardMaterialsMaster != null)
                        {
                            Aplist_new.Pending_qty = Convert.ToInt32(GateEntryInwardMaterialsMaster.TotalQty) - (pending_qty_total);
                        }
                        else
                        {

                            Aplist_new.Pending_qty = items.qty;
                        }
                        Aplist_new.IssueSlipId = dcid.IssueSlipId;
                        Aplist_withDc.Add(Aplist_new);
                    }
                    Aplist.Clear();
                } 
                return Json(new { JobSheet_PairMaster = Aplist_withDc, Issue_material=issue_mat.Substring(1)}, JsonRequestBehavior.AllowGet);
            }
            //Job Leather  
            else if (MultipleIssueSlip.IssueType == 16 || IssueType_.IssueType == "JobWork(Leather To Leather)")
            {
                int pending_qty = 0;
                if (IssueObj_.Count == 0)
                {
                    foreach (var item in IssueObj_)
                    {
                        JobLeather_leatherMaster = JobLeather_leatherManager.GetJobLeather_leather_ID(item.Value);
                        GateEntryInwardMaterialsMaster = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist(JobLeather_leatherMaster.Finished_Material, IssueID.ToString());

                        if (GateEntryInwardMaterialsMaster != null && GateEntryInwardId != null)
                        {
                            var pending_qty_total = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty(JobLeather_leatherMaster.Finished_Material, IssueID.ToString()).Sum(x => Convert.ToInt32(x.Quantity));

                            var pending_qty_GateEntryInwardId = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty(JobLeather_leatherMaster.Finished_Material, IssueID.ToString()).Where(x => x.GateEntryInwardId == Convert.ToInt32(GateEntryInwardId)).Sum(x => Convert.ToInt32(x.Quantity));
                            pending_qty = Convert.ToInt32(GateEntryInwardMaterialsMaster.TotalQty) - (pending_qty_total - pending_qty_GateEntryInwardId);
                        }
                        else if (GateEntryInwardMaterialsMaster != null)
                        {
                            int total_qty = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty(JobLeather_leatherMaster.Finished_Material, IssueID.ToString()).Sum(x => Convert.ToInt32(x.Quantity));
                            int qty = Convert.ToInt32(GateEntryInwardMaterialsMaster.TotalQty);
                            pending_qty = qty - total_qty;

                        }
                    }
                }
                else {
                    List<string> AuthorList = new List<string>();

                    // Add items using Add method   
                    AuthorList.Add("MaterialDescription");
                    AuthorList.Add("MaterialMasterId");
                    BillOfMaterialManager billofMaterialManager = new BillOfMaterialManager();
                    List<MMS.Data.StoredProcedureModel.MaterialDropDownmodel> list = new List<Data.StoredProcedureModel.MaterialDropDownmodel>();

                    JobLeather_leatherModel model = new JobLeather_leatherModel();
                    var listnew = JobLeather_leatherManager.Get().Where(x=> IssueObj_.Contains(x.Job_Lether_to_lether_id)).ToList();
                    //JobLeather_leatherMaster = JobLeather_leatherManager.GetJobLeather_leather_ID(Job_Lether_to_lether_id);
                    //list_ = billofMaterialManager.GetMaterialList().Where(x => listnew.Contains(x.MaterialMasterId.ToString())).ToList();

                    var JobLeather_leatherMaster_ = JobLeather_leatherManager.GetJob_LetherCode(listnew[0].Job_Lether_to_lether_Code).Select(x=>x.Job_Lether_to_lether_id).ToList();
                    foreach (var item in JobLeather_leatherMaster_)
                    {
                        JobLeather_leatherMaster = JobLeather_leatherManager.GetJobLeather_leather_ID(item);
                        //list.AddRange( billofMaterialManager.GetMaterialList().Where(x => x.MaterialMasterId== JobLeather_leatherMaster.Finished_Material).ToList());
                        var dcid = (issueSlipManager.GetMultipleIssueid_list(list_new).Where(x => x.Jobworktype_Id == item).FirstOrDefault());
                        try
                        {
                            list.AddRange(billofMaterialManager.GetMaterialList().Where(x => x.MaterialMasterId == JobLeather_leatherMaster.Finished_Material)
                               .Select(x => new Data.StoredProcedureModel.MaterialDropDownmodel
                               {
                                   MaterialDescription = x.MaterialDescription,
                                   GroupID = x.GroupID,
                                   ColorMasterId = x.ColorMasterId,
                                   MaterialMasterId_Processid = x.MaterialMasterId.ToString() + "," + JobLeather_leatherMaster.Process_Name.ToString() + "," + JobLeather_leatherMaster.Job_Lether_to_lether_id+","+ list_new[0],
                                   Price = x.Price,
                                   SizeRangeMasterId = x.SizeRangeMasterId,
                                   Uom = x.Uom,
                                   UomUnit = x.UomUnit
                               }).ToList());

                          
                        }
                        catch (Exception ex)
                        {
                           
                        }
                    }
                    var ShotName = new Data.StoredProcedureModel.MaterialDropDownmodel
                    {
                        MaterialDescription = "Please Select",
                        GroupID = 0,
                        ColorMasterId = 0,
                        MaterialMasterId_Processid = "",
                        Price = "",
                        SizeRangeMasterId = 0,
                        Uom = 0,
                        UomUnit = 0
                    };
                    list.Insert(0, ShotName);
                    return Json(new { JobLeather_leatherMaster = JobLeather_leatherMaster, list = list, JobSheet_PairMaster = "" }, JsonRequestBehavior.AllowGet);

                }
                //else
                //{
                //    pending_qty = Convert.ToInt32(GateEntryInwardMaterialsMaster.PendingQuantity);
                //}
                return Json(new { JobLeather_leatherMaster = JobLeather_leatherMaster, JobSheet_PairMaster="", GateEntryInwardMaterialsMaster= GateEntryInwardMaterialsMaster, pending_qty= pending_qty , list =""}, JsonRequestBehavior.AllowGet);
            }
            //JobWork Production
            else if (MultipleIssueSlip.IssueType == 17 || IssueType_.IssueType == "JobWork Production")
            {
                try
                {
                    List<ProductionJobworkMaster> ProductionJobworkMaster = new List<ProductionJobworkMaster>();
                    ProductionJobworkModel ProductionJobworkModel = new ProductionJobworkModel();
                    ProductionJobwork_Code_Master ProductionJobwork_Code_Master = new ProductionJobwork_Code_Master();
                    ProductionJobworkMasterManager ProductionJobworkMasterManager_ = new ProductionJobworkMasterManager();
                    List<ProductionJobSizerangeMaster> ProductionJobSizerangeMaster = new List<ProductionJobSizerangeMaster>();
                    ProductionJobSizerangeMaster ProductionJobSizerangeMaster_ = new ProductionJobSizerangeMaster();

                    string Message = "";
                    string LinkBomNo = "";
                    // IssueObj_
                    var Issue_list = (issueSlipManager.GetMultipleIssueid_list(list_new).ToList());
                    int?[] Production_List = Issue_list.Select(x => x.Jobworktype_Id).ToArray();
                    foreach (var item in IssueObj_)
                    {
                        var Model = ProductionJobworkMasterManager_.GetproductionMaster_id(item);
                        ProductionJobworkMaster = ProductionJobworkMasterManager_.Get().Where(x => x.Prod_code_id == Model.Prod_code_id).ToList();
                        foreach (var items_ in ProductionJobworkMaster)
                        {
                            LinkBomNo += items_.Io_based + ',';

                        }
                    }
                    // Group by Production
                    //foreach (var item in Production_List)
                    //{
                    //    ProductionJobSizerangeMaster.AddRange(ProductionJobworkMasterManager_.Get_Size(item));
                    //}
                    //ProductionJobSizerangeMaster = ProductionJobSizerangeMaster.GroupBy(x => new { x.Sizerange })
                    //    .Select(x => new ProductionJobSizerangeMaster()
                    //    {
                    //        Sizerange = x.Key.Sizerange,
                    //        Qty=x.Sum(y=>y.Qty)
                    //    }).ToList();

                    BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
                    List<BillOfMaterial> billOfMaterial = new List<BillOfMaterial>();
                    List<SizeRangeQtyRate> sizeRangeQtyRateList = new List<SizeRangeQtyRate>();
                    List<SizeRangeQtyRate> listOfSizeRange = new List<SizeRangeQtyRate>();
                    BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
                    BuyerOrderEntryManager BuyerOrderEntryManager = new BuyerOrderEntryManager();
                    List<System.Web.Mvc.SelectListItem> items = BuyerOrderEntryManager.Get().OrderBy(x => x.OurStyle).Where(X => X.IsInternal == true).GroupBy(x => new { x.OurStyle, x.BuyerOrderSlNo }).Select(
                                                    item => new System.Web.Mvc.SelectListItem()
                                                    {
                                                        Text = item.Key.BuyerOrderSlNo,
                                                        Value = Convert.ToString(item.Key.OurStyle)
                                                    }).ToList();
                    List<System.Web.Mvc.SelectListItem> items_new = BuyerOrderEntryManager.Get().OrderBy(x => x.OurStyle).Where(X => X.IsInternal == true).GroupBy(x => new { x.OurStyle, x.BuyerOrderSlNo }).Select(
                                                   item => new System.Web.Mvc.SelectListItem()
                                                   {
                                                       Text = item.Key.BuyerOrderSlNo,
                                                       Value = Convert.ToString(item.Key.OurStyle)
                                                   }).ToList();
                    string OrderEntryId = "";
                    string[] LinkBom_No = LinkBomNo.Substring(0, LinkBomNo.Length - 1).Split(',');
                    //   billOfMaterial = billOfMaterialManager.Get().Where(X => X.LinkBomNo == LinkBomNo && X.IsDeleted== false).FirstOrDefault();
                    string[] io_val = items.Where(x => LinkBom_No.Contains(x.Text)).Select(x => x.Value).ToArray();
                    string[] Orderenty = items_new.Where(x => LinkBom_No.Contains(x.Text)).Select(x => x.Value).ToArray();

                    billOfMaterial = billOfMaterialManager.Get_Arraylist(io_val);
                    var BomNo = billOfMaterial.FirstOrDefault().BomNo;
                    string Style = BomNo.Substring(0, 4);
                    //  var BomNo_ = billOfMaterial.Select();
                    foreach (var item in billOfMaterial)
                    {
                        if (Style != item.BomNo.Substring(0, 4))
                        {
                            Message = "Style cant be diffrent";
                        }
                    }
                    foreach (var item in Orderenty)
                    {
                        var Id = buyerOrderEntryManager.Get().Where(X => X.IsInternal == true && X.OurStyle == item).FirstOrDefault();
                        OrderEntryId += Id.OrderEntryId + ",";
                    }
                    // var result = (BomNo_.Count == BomNo_.Distinct().Count());
                    //  var i    = BomNo_.Contains(false);
                    listOfSizeRange = buyerOrderEntryManager.GetAllOrderSize_OrderEntryId(OrderEntryId);

                    decimal? Totalqty = Issue_list[0].CurrentIssue;

                    string[] ProductionJobworkMaster_list = ProductionJobworkMasterManager_.Get().Where(x => Production_List.Contains(x.Production_Order_id)).Select(x => x.Fg_ComponentId).ToArray();

                    ComponentManager Manager_ = new ComponentManager();
                    List<System.Web.Mvc.SelectListItem> items_compo = Manager_.Get().OrderBy(x => x.ComponentName).Select(
                                            item => new System.Web.Mvc.SelectListItem()
                                            {
                                                Text = item.ComponentName,
                                                Value = item.ComponentMasterId.ToString()
                                            }
                                            ).ToList();
                    List<System.Web.Mvc.SelectListItem> items_Component = items_compo.Where(x => ProductionJobworkMaster_list.Contains(x.Value)).ToList();


                    //var items_list_ = (from G in inwardMaterialManager.Get()
                    //                   join X in MaterialManager.Get()
                    //                   on G.MaterialNameId equals X.MaterialMasterId
                    //                   join y in MaterialNameManager.Get()
                    //                   on X.MaterialName equals y.MaterialMasterID
                    //                   // join J in jobworkManager.Get()
                    //                   //on G.SupplierId equals J.JW_Id
                    //                   where G.GateEntryNo == model.GateEntryNo && G.IsDeleted == false
                    //                   select new { MaterialDescription = y.MaterialDescription, G.Quantity, G.GateEntryInwardId, G.PendingQuantity, G.MaterialNameId, G.TotalQty, G.Rate, G.Value, G.DcRefNo, G.Process_Name, G.jobsheet_pair_Code_id, G.IssueSlipId }).ToList();
                    int[] ProductionJobworkMaster_list_int = ProductionJobworkMasterManager_.Get().Where(x => Production_List.Contains(x.Production_Order_id)).Select(x => int.Parse( x.Fg_ComponentId)).ToArray();

                    List<System.Web.Mvc.SelectListItem> itemmmm = (from P in ProductionJobworkMasterManager_.Get()
                                                                   join c in Manager_.Get()
                                                                on Convert.ToInt32(P.Fg_ComponentId) equals c.ComponentMasterId

                                                                   where (ProductionJobworkMaster_list_int.Contains(c.ComponentMasterId
                                                               ))
                                                                   select new SelectListItem { Text = c.ComponentName, 
                                                        Value = c.ComponentMasterId +"_"+ P.Production_Order_id.ToString() }).ToList();

                    var ShotName = new System.Web.Mvc.SelectListItem()
                    {
                        Value = "",
                        Text = "Please Select"
                    };

                    itemmmm.Insert(0, ShotName);
                    return Json(new { Style = Style, BillOfMaterial = billOfMaterial, Message = Message, listOfSizeRange = listOfSizeRange, Totalqty = Totalqty, JobWork_Price = ProductionJobworkMaster[0].Rate, Style_Fg = BomNo, items_Component = itemmmm }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {

                }

              
            }
            //JobWork Other/Services
            else if (MultipleIssueSlip.IssueType == 18 || IssueType_.IssueType == "JobWork Other/Services")
            {
                JobOtherWorkMaster = JobOtherWorkManager.GetJobOtherWork_id(MultipleIssueSlip.Jobworktype_Id);
                GateEntryInwardMaterialsMaster = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist(JobLeather_leatherMaster.Finished_Material, IssueID.ToString());
                int pending_qty;
                if (GateEntryInwardMaterialsMaster != null)
                {
                    pending_qty = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty(JobLeather_leatherMaster.Finished_Material, IssueID.ToString()).Sum(x => Convert.ToInt32(x.Quantity));
                }
                else
                {
                    pending_qty = Convert.ToInt32(GateEntryInwardMaterialsMaster.PendingQuantity);
                }
                return Json(new { JobOtherWorkMaster = JobOtherWorkMaster, JobSheet_PairMaster = "" , JobLeather_leatherMaster ="", GateEntryInwardMaterialsMaster = GateEntryInwardMaterialsMaster, pending_qty = pending_qty }, JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }


        public ActionResult Get_detail_by_ComponentId(string Production_List)
        {
            ProductionJobworkMasterManager ProductionJobworkMasterManager_ = new ProductionJobworkMasterManager();
            List<ProductionJobSizerangeMaster> ProductionJobSizerangeMaster = new List<ProductionJobSizerangeMaster>();
            //foreach (var item in Production_List)
            //{
            //    ProductionJobSizerangeMaster.AddRange(ProductionJobworkMasterManager_.Get_Size(item));
            //}
           var Items= Production_List.Split('_');
            var Production_JobSizerangeMaster= ProductionJobworkMasterManager_.GetproductionMaster_id(Convert.ToInt32(Items[1]));
            ProductionJobSizerangeMaster = ProductionJobworkMasterManager_.Get_Size(Convert.ToInt32(Items[1])).GroupBy(x => new { x.Sizerange })
                .Select(x => new ProductionJobSizerangeMaster()
                {
                    Sizerange = x.Key.Sizerange,
                    Qty = x.Sum(y => y.Qty)
                }).ToList();
            return Json(new { TotalQty = Production_JobSizerangeMaster.Qty, Rate= Production_JobSizerangeMaster.Rate, listOfSizeRange = ProductionJobSizerangeMaster }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Get_letherMAterialDetail(string IssueID, string MaterialNameId, string GateEntryInwardId)
        {
            IssueSlip_SingleManager Manager = new IssueSlip_SingleManager();
            MultipleIssueSlip MultipleIssueSlip = new MultipleIssueSlip();
            JobSheet_PairManager JobSheet_PairManager = new JobSheet_PairManager();
            JobSheet_pairCodeMaster JobSheet_pairCodeMaster = new JobSheet_pairCodeMaster();
            JobLeather_leatherManager JobLeather_leatherManager = new JobLeather_leatherManager();
            JobLeather_leatherMaster JobLeather_leatherMaster = new JobLeather_leatherMaster();
            JobOtherWorkManager JobOtherWorkManager = new JobOtherWorkManager();
            JobOtherWorkMaster JobOtherWorkMaster = new JobOtherWorkMaster();
            GRNTypeManager GRNTypeManager = new GRNTypeManager();
            GateEntryInwardMaterialsManager inwardMaterialManager = new GateEntryInwardMaterialsManager();
            GateEntryInwardMaterialsMaster GateEntryInwardMaterialsMaster = new GateEntryInwardMaterialsMaster();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            List<JobSheet_PairMaster> JobSheet_PairMaster = new List<JobSheet_PairMaster>();
            List<IssueSlip_MaterialDetails> IssueObj = new List<IssueSlip_MaterialDetails>();

            int[] list_ = IssueID.Split(',').Select(int.Parse).ToArray();
            MultipleIssueSlip = Manager.GetMultipleIssueGRNSelectedRow(list_[0]);
            var IssueType_ = GRNTypeManager.GetIssueTypeMasterId(MultipleIssueSlip.IssueType);
            String[] spearator = MaterialNameId.Split(',');
            //var IssueObj_ = issueSlipManager.GetMultipleIssueid(MultipleIssueSlip.MultipleIssueSlipID).Where(x => x.MaterialMasterId == Convert.ToInt32(MaterialNameId)).FirstOrDefault();
            //Job sheet 
            var Lether_id = issueSlipManager.GetMultipleIssueid(Convert.ToInt32(spearator[0])).GroupBy(x => x.Jobworktype_Id).Select(x => x.Key).ToList();
            //chenged spearator[3] to spearator[0]
            //var Lether_id = issueSlipManager.GetMultipleIssueid(Convert.ToInt32(spearator[3])).GroupBy(x => x.Jobworktype_Id).Select(x => x.Key).ToList();
            //var Lether_id = issueSlipManager.Get().Where(x => x.Jobworktype_Id == Convert.ToInt32(spearator[2]) && x.IssueSlipFK == Convert.ToInt32(spearator[3]) && x.MaterialMasterId == Convert.ToInt32(spearator[0])).GroupBy(x => x.Jobworktype_Id).Select(x => x.Key).ToList();
            //Job Leather  
            if (MultipleIssueSlip.IssueType == 16 || IssueType_.IssueType == "JobWork(Leather To Leather)")
            {
                
                int count_check = 0;
                decimal pending_qty = Convert.ToDecimal(-0.1);
                count_check= JobLeather_leatherManager.Get().Where(x => Lether_id.Contains(x.Job_Lether_to_lether_id) && x.Finished_Material == Convert.ToInt32(spearator[0]) && x.Process_Name == Convert.ToInt32(spearator[1])).ToList().Count();
                //if (count_check == 1)
                //{
                //    JobLeather_leatherMaster = JobLeather_leatherManager.Get().Where(x => Lether_id.Contains(x.Job_Lether_to_lether_id) && x.Finished_Material == Convert.ToInt32(spearator[0])).FirstOrDefault();
                //}
                //else
                //{
                //    JobLeather_leatherMaster = JobLeather_leatherManager.Get().Where(x => Lether_id.Contains(x.Job_Lether_to_lether_id) && x.Finished_Material == Convert.ToInt32(spearator[0])&&x.Job_Lether_to_lether_id== Convert.ToInt32(spearator[2])).FirstOrDefault();
                //}
                var IssueDetail = issueSlipManager.Get().Where(x => x.IssueSlipFK == Convert.ToInt32(IssueID)).ToList();
              var   JobLeather_leatherMaster_ = JobLeather_leatherManager.GetJobLeather_leather_ID(IssueDetail[0].Jobworktype_Id);
                JobLeather_leatherMaster = JobLeather_leatherManager.Get().Where(x => x.Job_Lether_to_lether_Code == JobLeather_leatherMaster_.Job_Lether_to_lether_Code && x.Finished_Material== Convert.ToUInt32(spearator[0])).FirstOrDefault();
                decimal? total = IssueDetail.Sum(x => x.CurrentIssue);


                 GateEntryInwardMaterialsMaster = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_jobsheet_pair_Code_id(JobLeather_leatherMaster.Finished_Material, spearator[3].ToString(), Convert.ToInt32(spearator[1]), Convert.ToInt32(spearator[2]));
                //GateEntryInwardMaterialsMaster = null;

                if (GateEntryInwardMaterialsMaster != null && GateEntryInwardId != null)
                {
                    var pending_qty_total = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty_jobsheet_pair_Code_id(JobLeather_leatherMaster.Finished_Material, spearator[3].ToString(), Convert.ToInt32(spearator[1]), Convert.ToInt32(spearator[2])).Sum(x => Convert.ToDecimal(x.Quantity));

                    var pending_qty_GateEntryInwardId = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty_jobsheet_pair_Code_id(JobLeather_leatherMaster.Finished_Material, spearator[3].ToString(), Convert.ToInt32(spearator[1]), Convert.ToInt32(spearator[2])).Where(x => x.GateEntryInwardId == Convert.ToDecimal(GateEntryInwardId)).Sum(x => Convert.ToInt32(x.Quantity));

                    pending_qty = Convert.ToDecimal(GateEntryInwardMaterialsMaster.TotalQty) - Convert.ToDecimal(pending_qty_total - pending_qty_GateEntryInwardId);
                }
                else if (GateEntryInwardMaterialsMaster != null)
                {
                    int total_qty = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty_jobsheet_pair_Code_id(JobLeather_leatherMaster.Finished_Material, spearator[3].ToString(), Convert.ToInt32(spearator[1]), Convert.ToInt32(spearator[2])).Sum(x => Convert.ToInt32(x.Quantity));
                    decimal qty = Convert.ToDecimal(GateEntryInwardMaterialsMaster.TotalQty);
                    pending_qty = qty - total_qty;
                }
                //var IssueDetail = issueSlipManager.Get().Where(x => x.MaterialMasterId == JobLeather_leatherMaster.Material && x.IssueSlipFK == Convert.ToInt32(spearator[3]) && x.jobsheet_pair_Code_id == JobLeather_leatherMaster.Job_Lether_to_lether_id.ToString()).FirstOrDefault();


                    return Json(new { JobLeather_leatherMaster = JobLeather_leatherMaster, JobSheet_PairMaster = "", GateEntryInwardMaterialsMaster = GateEntryInwardMaterialsMaster, pending_qty = pending_qty, list = "", issueSlipManager = IssueDetail, Total=total }, JsonRequestBehavior.AllowGet);
            }

            //JobWork Production
            else if (MultipleIssueSlip.IssueType == 17 || IssueType_.IssueType == "JobWork Production")
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            //JobWork Other/Services
            else if (MultipleIssueSlip.IssueType == 18 || IssueType_.IssueType == "JobWork Other/Services")
            {
                JobOtherWorkMaster = JobOtherWorkManager.GetJobOtherWork_id(MultipleIssueSlip.Jobworktype_Id);
                GateEntryInwardMaterialsMaster = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist(JobLeather_leatherMaster.Finished_Material, IssueID.ToString());
                int pending_qty;
                if (GateEntryInwardMaterialsMaster != null)
                {
                    pending_qty = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty(JobLeather_leatherMaster.Finished_Material, IssueID.ToString()).Sum(x => Convert.ToInt32(x.Quantity));
                }
                else
                {
                    pending_qty = Convert.ToInt32(GateEntryInwardMaterialsMaster.PendingQuantity);
                }
                return Json(new { JobOtherWorkMaster = JobOtherWorkMaster, JobSheet_PairMaster = "", JobLeather_leatherMaster = "", GateEntryInwardMaterialsMaster = GateEntryInwardMaterialsMaster, pending_qty = pending_qty }, JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region curdoperation 
        public ActionResult GateEntryInwardMaterials(GateEntryInwardMaterials viewmodel)
         {
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
            JobSheet_PairManager JobSheet_PairManager = new JobSheet_PairManager();
            JobSheet_PairMaster JobSheet_PairMaster = new JobSheet_PairMaster();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            List<InwardMaterialSizeQtyRateMaster> SizeQtyRateDetails_ = new List<InwardMaterialSizeQtyRateMaster>();
            
            // purchaseOrder = purchaseorderManager.GetPoExist(viewmodel.PoNoId.ToString(), viewmodel.MaterialNameId.Value);
            int? MaterialId1 = viewmodel.MaterialNameId;
            if (purchaseOrder != null && purchaseOrder.PoOrderId != 0)
            {
                viewmodel.PoNoId = purchaseOrder.PoOrderId;
            }
            /// update coding  
            /// 

            //size
        


            //foreach (var items_mat in sizewise)
            //{
            //    var MAterial= items_mat.Split('_');
            //    if (MAterial[2] == "112")
            //    {
            //        string[] qty = MAterial[0].Split(';');
            //    }
            //}
          //  string[] Material= sizewise.spli
            //
            decimal TotalQty_value = 0;
            int Materil_id_edit = 0;
            int count_;
            count_ = 0;
            string[] sizeAray = viewmodel.Size.Split(',');
            string[] Dc_total = viewmodel.DcTotal_Qty.Split(',');
            int count_dctotal = 0;
            foreach (var item in sizeAray)
            {

                string[] qty = item.Split(';');
                decimal pending;
                if (qty[0] != "," && qty[0] != "," && qty[0] != "" )
                {
                    string[] dc_value = qty[1].Split('_');
                   // var Totalqty = "";
                      int  oldqty = 0;
                      int recivied_qty = 0;
                    decimal single = Convert.ToDecimal(0.0);
                    decimal Totalqty = Convert.ToDecimal(0.0);
                    int GateEntryInwardId =0;
                    if (viewmodel.GateEntryInwardId == 0)
                    {
                        recivied_qty = Convert.ToInt32(qty[0]);
                        int jobsheet_pair_id = Convert.ToInt32(dc_value[0]);


                        count_++;
                     var   issueSlip_List = issueSlipManager.Get().Where(x => x.IssueSlipFK == Convert.ToInt32(dc_value[1])).LastOrDefault();
                        JobSheet_PairMaster = JobSheet_PairManager.Getjobsheet_pair_id(jobsheet_pair_id);
                        var Totalqty_List = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist(JobSheet_PairMaster.Material, dc_value[1]);
                        if (Totalqty_List != null)
                        {
                            Totalqty = Totalqty_List.TotalQty;
                        }
                        oldqty = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty(JobSheet_PairMaster.Material, dc_value[1]).Sum(x => Convert.ToInt32(x.Quantity));
                        if (oldqty != 0)
                        {
                            pending = ((Convert.ToInt32(Totalqty)) - recivied_qty);

                            //pending = (JobSheet_PairMaster.Qty - recivied_qty) - Convert.ToInt32(oldpending.PendingQuantity);
                        }
                        else
                        {
                            pending = (Convert.ToDecimal(issueSlip_List.CurrentIssue) - Convert.ToDecimal(recivied_qty));
                        }
                        TotalQty_value = Convert.ToDecimal(JobSheet_PairMaster.Qty);
                        single = Convert.ToDecimal(JobSheet_PairMaster.Value / JobSheet_PairMaster.Qty);
                    }
                    else
                    {
                        recivied_qty = Convert.ToInt32(qty[0]);
                        GateEntryInwardId = Convert.ToInt32(dc_value[0]);
                        var GateEntryInwardMaterialsMaster_ = inwardMaterialManager.GetGateEntryInMaterialById(GateEntryInwardId);
                        //int pending;
                        Totalqty = GateEntryInwardMaterialsMaster_.TotalQty;
                        oldqty = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty(Convert.ToInt32(GateEntryInwardMaterialsMaster_.MaterialNameId), dc_value[1]).Sum(x => Convert.ToInt32(x.Quantity));
                        count_++;
                        pending = ((Convert.ToInt32(Totalqty) - oldqty) - recivied_qty);
                       var JobSheet_PairMaster_ = JobSheet_PairManager.Get().Where(x=>x.jobsheet_pair_Code_id== GateEntryInwardMaterialsMaster_.jobsheet_pair_Code_id && x.Material== GateEntryInwardMaterialsMaster_.MaterialNameId).LastOrDefault();
                        if (GateEntryInwardMaterialsMaster_.Quantity!="0")
                        {
                            single = Convert.ToDecimal(JobSheet_PairMaster_.Value / JobSheet_PairMaster_.Qty);
                        }
                        Materil_id_edit =Convert.ToInt32(GateEntryInwardMaterialsMaster_.MaterialNameId);
                    }
                   

                   // var single = JobSheet_PairMaster.Value / JobSheet_PairMaster.Qty;
                    var value_recivied_qty = recivied_qty * single;

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
                            EntrydateValue = DateTime.ParseExact(viewmodel.EntryDate, dateFormats,
                                                            new CultureInfo("en-US"),
                                                            DateTimeStyles.None);
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
                    model.DcRefNo = dc_value[1];
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
                    if (viewmodel.GateEntryInwardId == 0)
                    {

                        model.DcTotal = Convert.ToInt32(Dc_total[count_dctotal]);
                        model.SupplierId = JobSheet_PairMaster.JW_Name;
                        model.StoresId = JobSheet_PairMaster.Stores;

                        model.TotalQty = TotalQty_value;
                        model.Rate = JobSheet_PairMaster.Jw_Rate ?? 0;
                         // model.GateEntryInwardId = 0;
                        model.MaterialNameId = JobSheet_PairMaster.Material;
                    }
                    else {
                        model.MaterialNameId = Materil_id_edit;
                        model.GateEntryInwardId = GateEntryInwardId;
                    }
                    model.HSNCode = viewmodel.HSNCode;
                    model.SizeRangeId = viewmodel.SizeRangeId;
                    model.UomId = viewmodel.UomId;
                    model.Pcs = viewmodel.Pcs;
                    model.ReceivedBy = viewmodel.ReceivedBy;
                    model.AcknowledgedBy = viewmodel.AcknowledgedBy;
                    model.Remarks = viewmodel.Remarks;
                    model.Quantity = recivied_qty.ToString();
                 
                    model.Value =Convert.ToDecimal(value_recivied_qty);
                    model.ArrivedTotal = viewmodel.ArrivedTotal;
                    model.InwardPo = viewmodel.InwardPo;
                   
                    model.PoTotal = viewmodel.PoTotal;
                    model.PendingQuantity = pending.ToString();

                    model.jobsheet_pair_Code_id = JobSheet_PairMaster.jobsheet_pair_Code_id;
                    model.Process_Name = JobSheet_PairMaster.Process_Name;
                    model.Manual_DcNo = viewmodel.Manual_DcNo;
                    model.IssueSlipId =(dc_value[2]);
                    if (viewmodel.GateEntryInwardId == 0)
                    {
                        var Gate_Id = inwardMaterialManager.Post_sheet(model);

                        //List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
                        //listSizeItemMaterial = materialManager.GetSizeItemMaterial(JobSheet_PairMaster.Material);
                        //listSizeItemMaterial = listSizeItemMaterial.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();


                        //if (viewmodel.ArrivedQty != "")
                        //{
                        //    string[] sizewise = viewmodel.ArrivedQty.Split(',');
                        //    string element_Contains = "_" + dc_value[2] + "_" + JobSheet_PairMaster.Material;
                        //    var asa = Array.FindAll(sizewise, element => element.Contains(element_Contains));
                        //    int count = 0;
                        //    foreach (var item_ in asa)
                        //    {
                        //        var result = item_.Split(';');
                        //        InwardMaterialSizeQtyRateMaster SizeQtyRateDetails = new InwardMaterialSizeQtyRateMaster();
                        //        SizeQtyRateDetails.Size = listSizeItemMaterial[count].ToString();
                        //        SizeQtyRateDetails.PoQty = "0";
                        //        SizeQtyRateDetails.DcQty = "0";
                        //        SizeQtyRateDetails.ArrivedQty = result[0];
                        //        SizeQtyRateDetails.MaterialId = JobSheet_PairMaster.Material;
                        //        SizeQtyRateDetails.GateEntryInwardId = viewmodel.GateEntryInwardId;


                        //    //    SizeQtyRateDetails_.PostMaterialSizeQtyRate(SizeQtyRateDetails);
                        //        sizeRatemanager.PostMaterialSizeQtyRate(SizeQtyRateDetails);
                        //        count++;
                        //    }
                        //}

                       var TEMP_LIST = sizeRatemanager.Get_TEMP_LIST(Convert.ToInt32(dc_value[2]), JobSheet_PairMaster.Material).OrderBy(x => Convert.ToDecimal(x.Size));
                        foreach (var itemss in TEMP_LIST)
                        {
                            InwardMaterialSizeQtyRateMaster SizeQtyRateDetails = new InwardMaterialSizeQtyRateMaster();
                            SizeQtyRateDetails.Size = itemss.Size;
                            SizeQtyRateDetails.PoQty = itemss.ArrivedQty;
                            SizeQtyRateDetails.DcQty = itemss.DcQty;
                            SizeQtyRateDetails.ArrivedQty = itemss.ArrivedQty;
                            SizeQtyRateDetails.PendingQty = "0";
                            SizeQtyRateDetails.MaterialId = itemss.MaterialId;
                            SizeQtyRateDetails.GateEntryInwardId = Gate_Id;
                            SizeQtyRateDetails.CreatedDate = DateTime.Now;
                            sizeRatemanager.PostMaterialSizeQtyRate(SizeQtyRateDetails);
                        }
                    }
                    else {
                     //   var MaterialInwardId = inwardMaterialManager.Post_sheet_Update(model);
                    }

                }
                count_dctotal++;
            }

            var Delete_Temp = sizeRatemanager.Delete_Temp(0,0);


            return Json(new { Viewmodel = viewmodel }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GateEntryInwardMaterials_save(GateEntryInwardMaterials viewmodel)
        {
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
                   
                    int count1 = 0;
                    if (model.PendingQuantity != null && model.PendingQuantity != "" && model.PendingQuantity != "0")
                    {
                        
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
                    EntrydateValue = DateTime.ParseExact(viewmodel.EntryDate, dateFormats,
                                                    new CultureInfo("en-US"),
                                                    DateTimeStyles.None);
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
            model.IssueSlipId = viewmodel.IssueSlipId;
            model.jobsheet_pair_Code_id = viewmodel.jobsheet_pair_Code_id;
            model.Process_Name = viewmodel.Process_Name;
            model.Manual_DcNo = viewmodel.Manual_DcNo;
            model.ComponentId = viewmodel.ComponentId;
            var MaterialInwardId = inwardMaterialManager.Post(model);
            var MaterialId = viewmodel.MaterialNameId;
            if (  viewmodel.GateEntryInwardId == 0)
            {
                if (viewmodel.Size != null && viewmodel.Size != "")
                {
                    string[] sizeAray = viewmodel.Size.Split(',');
                    string[] PoAray = viewmodel.PoQty.Split(',');
                   // string[] DcAray = viewmodel.DcQty.Split(',');
                    string[] ArrAray = viewmodel.ArrivedQty.Split(',');
                    
                    int count = 0;
                    foreach (var item in sizeAray)
                    {
                        if (item != null && item != "" && item != "0")
                        {
                            InwardMaterialSizeQtyRateMaster SizeQtyRateDetails = new InwardMaterialSizeQtyRateMaster();
                            SizeQtyRateDetails.Size = item;
                            SizeQtyRateDetails.PoQty = PoAray[count];
                            //   SizeQtyRateDetails.DcQty = DcAray[count];
                            SizeQtyRateDetails.ArrivedQty = ArrAray[count];
                            SizeQtyRateDetails.PendingQty = "0";
                            SizeQtyRateDetails.MaterialId = MaterialId;
                            SizeQtyRateDetails.GateEntryInwardId = MaterialInwardId;
                            SizeQtyRateDetails.CreatedDate = DateTime.Now;
                            sizeRatemanager.PostMaterialSizeQtyRate(SizeQtyRateDetails);
                            count++;
                        }
                    }
                    int count1 = 0;
                    if (model.PendingQuantity != null && model.PendingQuantity != "" && model.PendingQuantity != "0")
                    {
                        //foreach (var item in sizeAray)
                        //{
                        //    InwardMaterialPendingQuantityMaster pendingQtyDetails = new InwardMaterialPendingQuantityMaster();
                        //    pendingQtyDetails.Size = item;
                        //    pendingQtyDetails.DcQty = DcAray[count1];
                        //    pendingQtyDetails.ArrivedQty = ArrAray[count1];
                        //    pendingQtyDetails.PendingQty = PendAray[count1];
                        //    pendingQtyDetails.PoQuantity = PoAray[count1];
                        //    pendingQtyDetails.MaterialId = MaterialId;
                        //    pendingQtyDetails.PoOrderID = viewmodel.PoNoId;
                        //    pendingQtyDetails.GateEntryInwardId = MaterialInwardId;
                        //    pendingQtyDetails.CreatedDate = DateTime.Now;
                        //    pendingQtyManager.PostMaterialSizeQtyRate(pendingQtyDetails);
                        //    count1++;
                        //}
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
              //
                geInMaterialGrid.HSNCode = item.HSNCode;
                geInMaterialGrid.SizeRangeId = item.SizeRangeId;
                geInMaterialGrid.PendingQuantity = item.PendingQuantity;
                geInMaterialGrid.Piecies = item.Piecies;
                geInMaterialGrid.SupplierId = item.SupplierId;
                geInMaterialGrid.StoresId = item.StoresId;
                geInMaterialGrid.UomId = item.UomId;
                if (viewmodel.Type != "JobWork Production")
                {
                    geInMaterialGrid.MaterialNameId = item.MaterialNameId;
                    geInMaterialGrid.UnitName = uOMManager.GetUomMasterId(Convert.ToInt32(geInMaterialGrid.UomId)).ShortUnitName;
                    if (geInMaterialGrid.SizeRangeId != null && geInMaterialGrid.SizeRangeId != 0)
                    {
                        geInMaterialGrid.SizeRangeDetails = srdManager.GetSizeRangeDetailsId(geInMaterialGrid.SizeRangeId).SizeRangeName;
                    }
                    geInMaterialGrid.StoreName = stManager.GetStoreMasterId(geInMaterialGrid.StoresId.Value).StoreName;
                    var MaterialNameList = materialManager.GetMaterialMasterId(geInMaterialGrid.MaterialNameId).MaterialName;
                    geInMaterialGrid.MaterialName = materialNameManager.GetMaterialNameMaterial(MaterialNameList.Value).MaterialDescription;
                    geInMaterialGrid.InwardEntryDateTime = item.InwardEntryDateTime;
                }
                GridList.Add(geInMaterialGrid);
            }
            viewmodel.GEMaterialsGrid = GridList;

            return Json(new { Viewmodel = viewmodel }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Get_Remaingqty_forsheet(string IssueID,int Finished_Material,int GateEntryInwardId)
        {
           
            GateEntryInwardMaterialsManager inwardMaterialManager = new GateEntryInwardMaterialsManager();
            GateEntryInwardMaterialsMaster GateEntryInwardMaterialsMaster = new GateEntryInwardMaterialsMaster();

            List<JobSheet_PairMaster> JobSheet_PairMaster = new List<JobSheet_PairMaster>();
            string[] IssueIDlist = IssueID.Split(',');
            foreach (var item in IssueIDlist)
            {
                if (GateEntryInwardId == 0 || GateEntryInwardId == null)
                {
                    GateEntryInwardMaterialsMaster = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist(Finished_Material, item);
                }
                else {
                    GateEntryInwardMaterialsMaster = inwardMaterialManager.Get().Where(x => x.GateEntryInwardId == GateEntryInwardId).FirstOrDefault();
                }
                if (GateEntryInwardMaterialsMaster != null)

                {

                    break;
                }
            }
           
            int pending_qty = 0;

            if (GateEntryInwardMaterialsMaster != null && GateEntryInwardId != 0 )
            {
                var pending_qty_total = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty(Finished_Material, GateEntryInwardMaterialsMaster.DcRefNo.ToString()).Where(x=>x.jobsheet_pair_Code_id== GateEntryInwardMaterialsMaster.jobsheet_pair_Code_id).Sum(x => Convert.ToInt32(x.Quantity));
                var pending_qty_GateEntryInwardId = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty(Finished_Material, GateEntryInwardMaterialsMaster.DcRefNo.ToString()).Where(x => x.GateEntryInwardId == Convert.ToInt32(GateEntryInwardId)).Sum(x => Convert.ToInt32(x.Quantity));
                pending_qty = Convert.ToInt32(GateEntryInwardMaterialsMaster.TotalQty)-(pending_qty_total - pending_qty_GateEntryInwardId) ;
            }
            if (GateEntryInwardMaterialsMaster != null && GateEntryInwardId == 0)
            {
                int Accepted_total_qty = 0;
                foreach (var item in IssueIDlist)
                {
                     Accepted_total_qty = inwardMaterialManager.GetGateEntryInMaterialById_pendinglist_sumqty(Finished_Material, IssueID.ToString()).Sum(x => Convert.ToInt32(x.Quantity));
                    if (GateEntryInwardMaterialsMaster != null)

                    {

                        break;
                    }
                }
                int qty = Convert.ToInt32(GateEntryInwardMaterialsMaster.TotalQty);
                pending_qty = qty - Accepted_total_qty;

            }
            return Json(new { GateEntryInwardMaterialsMaster = GateEntryInwardMaterialsMaster, pending_qty= pending_qty }, JsonRequestBehavior.AllowGet);
           
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GateEntryInwardMaterials_qtytemp(string Size,string DC_Qty, string ArrivedQty)
        {
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
            JobSheet_PairManager JobSheet_PairManager = new JobSheet_PairManager();
            JobSheet_PairMaster JobSheet_PairMaster = new JobSheet_PairMaster();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            List<InwardMaterialSizeQtyRateMaster> SizeQtyRateDetails_ = new List<InwardMaterialSizeQtyRateMaster>();

            InwardMaterialSizeQtyRateMasterTemp SizeQtyRateDetails = new InwardMaterialSizeQtyRateMasterTemp();
            // purchaseOrder = purchaseorderManager.GetPoExist(viewmodel.PoNoId.ToString(), viewmodel.MaterialNameId.Value);


            var MaterialInwardId = inwardMaterialManager.Post_sheet(model);

                        List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
                        listSizeItemMaterial = materialManager.GetSizeItemMaterial(JobSheet_PairMaster.Material);
                        listSizeItemMaterial = listSizeItemMaterial.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();

                            int count = 0;
                            string[] ArrivedQty_ = ArrivedQty.Split(',');
                            string[] sizewise = Size.Split(',');
                            string[] DC_Qty_ = DC_Qty.Split(',');
            string[] ArrivedQty_Check = ArrivedQty_[0].Split('_');

            var TEMP_LIST = sizeRatemanager.Get_TEMP_LIST(Convert.ToInt32(ArrivedQty_Check[1]), Convert.ToInt32(ArrivedQty_Check[2]));
            if (TEMP_LIST != null)
            {
                var Delete_Temp = sizeRatemanager.Delete_Temp(Convert.ToInt32(ArrivedQty_Check[1]), Convert.ToInt32(ArrivedQty_Check[2]));
            }
                             foreach (var item_ in ArrivedQty_)
                            {
                if (item_ != "")
                {
                    var result = item_.Split(';');
                    var Material = result[1].Split('_');
                    SizeQtyRateDetails.Size = sizewise[count];
                    SizeQtyRateDetails.PoQty = "0";
                    SizeQtyRateDetails.DcQty = DC_Qty_[count];
                    SizeQtyRateDetails.ArrivedQty = result[0];
                    SizeQtyRateDetails.MaterialId = Convert.ToInt32(Material[2]);
                    SizeQtyRateDetails.GateEntryInwardId = Convert.ToInt32(Material[1]);


                    //    SizeQtyRateDetails_.PostMaterialSizeQtyRate(SizeQtyRateDetails);
              
                    sizeRatemanager.PostMaterialSizeQtyRate_Temp(SizeQtyRateDetails);
                    count++;
                }
                            }
                   

                
           //     count_dctotal++;
            




            return Json(new { Viewmodel = "" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Delete
        //public bool Delete(int GateEntryInwardId)
        //{
        //    bool result = false;
        //    try
        //    {
        //        GateEntryInwardMaterialsMaster model = gateEntryInwardMaterialsRepo.GetById(GateEntryInwardId);
        //        model.IsDeleted = true;
        //        gateEntryInwardMaterialsRepo.Update(model);
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        result = false;
        //    }

        //    return result;
        //}
        public ActionResult Delete(int GateEntryInwardId)
        {
            List<GateEntryInwardMaterialsMaster> modelList = new List<GateEntryInwardMaterialsMaster>();
            GateEntryInwardMaterialsMaster model_ = new GateEntryInwardMaterialsMaster();
            GateEntryInwardMaterialsManager inwardMaterialManager = new GateEntryInwardMaterialsManager();
            string status = "";

            model_ = inwardMaterialManager.GetGateEntryInMaterialById(GateEntryInwardId);
            var List = inwardMaterialManager.GEMaterialListGridBasedOnPoNo(model_.GateEntryNo);
            foreach (var model in List)
            {
                if (model != null)
                {
                    status = "Success";
                    inwardMaterialManager.Delete(model.GateEntryInwardId);
                }
            }
            return Json(new { status = status, GateEntryNo = model_.GateEntryNo }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
