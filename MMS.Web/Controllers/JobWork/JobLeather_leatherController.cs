using MMS.Common;
using MMS.Core.Entities.JobWork;
using MMS.Repository.Managers.JobWork;
using MMS.Web.Models;
using MMS.Web.Models.ColorMasterModel;
using MMS.Web.Models.JobworkModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using MMS.Repository.Managers;
using MMS.Core.Entities;

namespace MMS.Web.Controllers.JobWork
{
    
    [CustomFilter]
    public class JobLeather_leatherController : Controller
    {
        //
        // GET: /JobLeather_leather/

        [HttpGet]
        public ActionResult JobLeather_leatherMaster()
        {
            MMS.Web.Models.JobworkModel.JobLeather_leatherModel Pm = new JobLeather_leatherModel();
            return View("~/Views/Jobwork/Job_Master/JobLeather_leather/JobLeather_leatherMaster.cshtml", Pm);
        }
        public ActionResult JobLeather_leatheGrid()
        {
            JobLeather_leatherModel vm = new JobLeather_leatherModel();
            JobLeather_leatherManager JobLeather_leatherManager = new JobLeather_leatherManager();
            List<JobLeather_leatherModel> JobworkModel = new List<JobLeather_leatherModel>();
            // vm.JobLeather_leatherMasterlist;





            vm.Job_LetherCode = JobLeather_leatherManager.GetJob_LetherCode("0");


            return PartialView("~/Views/Jobwork/Job_Master/JobLeather_leather/Partial/JobLeather_leatherGrid.cshtml", vm);
        }
        [HttpPost]
        public ActionResult EditJobLeather_leatherList(int Job_Lether_to_lether_id)
        {

            JobLeather_leatherManager JobLeather_leatherManager = new JobLeather_leatherManager();
            JobLeather_leatherMaster JobLeather_leatherMaster = new JobLeather_leatherMaster();
            JobLeather_leatherModel model = new JobLeather_leatherModel();
            JobLeather_leatherMaster = JobLeather_leatherManager.GetJobLeather_leather_ID(Job_Lether_to_lether_id);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getJobLeatherCodeId();


            if (JobLeather_leatherMaster != null)
            {
                model.Job_Lether_to_lether_id = JobLeather_leatherMaster.Job_Lether_to_lether_id;
                model.Job_Lether_to_lether_Code = JobLeather_leatherMaster.Job_Lether_to_lether_Code;
                model.Date_from = JobLeather_leatherMaster.Date_from;
                model.Jobwork_raw_material = JobLeather_leatherMaster.Jobwork_raw_material;
                model.Encho_Raw_Material = JobLeather_leatherMaster.Encho_Raw_Material;
                model.Jw_Name = JobLeather_leatherMaster.Jw_Name;
                model.Process_Name = JobLeather_leatherMaster.Process_Name;
                model.Buyer = JobLeather_leatherMaster.Buyer;
                model.Season = JobLeather_leatherMaster.Season;
                model.Stores = JobLeather_leatherMaster.Stores;
                model.Group_ = JobLeather_leatherMaster.Group_;
                model.Category = JobLeather_leatherMaster.Category;
                model.Material = JobLeather_leatherMaster.Material;


                model.Finished_Material = JobLeather_leatherMaster.Finished_Material;
                model.Qty = JobLeather_leatherMaster.Qty;
                model.Qty_Uom = JobLeather_leatherMaster.Qty_Uom;
                model.Rate = JobLeather_leatherMaster.Rate;
                model.GST = JobLeather_leatherMaster.GST;
                model.Value = JobLeather_leatherMaster.Value;
                model.Gst_Amt = JobLeather_leatherMaster.Gst_Amt;

                model.Total = JobLeather_leatherMaster.Total;
                model.Delivery_Date = JobLeather_leatherMaster.Delivery_Date;
            }

            else
            {
                ID++;
                model.Job_Lether_to_lether_Code = "JobLet" + ID;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditJobLeather_leatherList_Model(int Job_Lether_to_lether_id)
        {

            JobLeather_leatherManager JobLeather_leatherManager = new JobLeather_leatherManager();
            JobLeather_leatherMaster JobLeather_leatherMaster = new JobLeather_leatherMaster();
            JobLeather_leatherModel model = new JobLeather_leatherModel();
            JobLeather_leatherMaster = JobLeather_leatherManager.GetJobLeather_leather_ID(Job_Lether_to_lether_id);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getJobLeatherCodeId();


            if (JobLeather_leatherMaster != null)
            {
                model.Job_Lether_to_lether_id = JobLeather_leatherMaster.Job_Lether_to_lether_id;
                model.Job_Lether_to_lether_Code = JobLeather_leatherMaster.Job_Lether_to_lether_Code;
                model.Date_from = JobLeather_leatherMaster.Date_from;
                model.Jobwork_raw_material = JobLeather_leatherMaster.Jobwork_raw_material;
                model.Encho_Raw_Material = JobLeather_leatherMaster.Encho_Raw_Material;
                model.Jw_Name = JobLeather_leatherMaster.Jw_Name;
                model.Process_Name = JobLeather_leatherMaster.Process_Name;
                model.Buyer = JobLeather_leatherMaster.Buyer;
                model.Season = JobLeather_leatherMaster.Season;
                model.Stores = JobLeather_leatherMaster.Stores;
                model.Group_ = JobLeather_leatherMaster.Group_;
                model.Category = JobLeather_leatherMaster.Category;
                model.Material = JobLeather_leatherMaster.Material;


                model.Finished_Material = JobLeather_leatherMaster.Finished_Material;
                model.Qty = JobLeather_leatherMaster.Qty;
                model.Qty_Uom = JobLeather_leatherMaster.Qty_Uom;
                model.Rate = JobLeather_leatherMaster.Rate;
                model.GST = JobLeather_leatherMaster.GST;
                model.Value = JobLeather_leatherMaster.Value;
                model.Gst_Amt = JobLeather_leatherMaster.Gst_Amt;

                model.Total = JobLeather_leatherMaster.Total;
                model.Delivery_Date = JobLeather_leatherMaster.Delivery_Date;
            }

            else
            {
                ID++;
                model.Job_Lether_to_lether_Code = "JobLet" + ID;
            }
            //  return Json(model, JsonRequestBehavior.AllowGet);
            return PartialView("~/Views/Jobwork/Job_Master/JobLeather_leather/Partial/JobLeather_leatherEdit.cshtml", model);
        }
        [HttpPost]
        public ActionResult EditJobLeather_leatherList_code(string Job_Lether_to_lether_code)
        {

            JobLeather_leatherManager JobLeather_leatherManager = new JobLeather_leatherManager();
            JobLeather_leatherMaster JobLeather_leatherMaster = new JobLeather_leatherMaster();
            JobLeather_leatherModel model = new JobLeather_leatherModel();
            model.Job_LetherCode = JobLeather_leatherManager.GetJob_LetherCode(Job_Lether_to_lether_code);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getJobLeatherCodeId();

            JobLeather_leatherMaster = JobLeather_leatherManager.GetJobLeather_leather_ID(model.Job_LetherCode[0].Job_Lether_to_lether_id);
            //model.Process_Name = JobLeather_leatherMaster.Process_Name;
            model.Jw_Name = JobLeather_leatherMaster.Jw_Name;
            //if (JobLeather_leatherMaster != null)
            //{
            //    model.Job_Lether_to_lether_id = JobLeather_leatherMaster.Job_Lether_to_lether_id;
            //    model.Job_Lether_to_lether_Code = JobLeather_leatherMaster.Job_Lether_to_lether_Code;
            //    model.Date_from = JobLeather_leatherMaster.Date_from;
            //    model.Jobwork_raw_material = JobLeather_leatherMaster.Jobwork_raw_material;
            //    model.Encho_Raw_Material = JobLeather_leatherMaster.Encho_Raw_Material;
            //    model.Jw_Name = JobLeather_leatherMaster.Jw_Name;
            //    model.Process_Name = JobLeather_leatherMaster.Process_Name;
            //    model.Buyer = JobLeather_leatherMaster.Buyer;
            //    model.Season = JobLeather_leatherMaster.Season;
            //    model.Stores = JobLeather_leatherMaster.Stores;
            //    model.Group_ = JobLeather_leatherMaster.Group_;
            //    model.Category = JobLeather_leatherMaster.Category;
            //    model.Material = JobLeather_leatherMaster.Material;


            //    model.Finished_Material = JobLeather_leatherMaster.Finished_Material;
            //    model.Qty = JobLeather_leatherMaster.Qty;
            //    model.Qty_Uom = JobLeather_leatherMaster.Qty_Uom;
            //    model.Rate = JobLeather_leatherMaster.Rate;
            //    model.GST = JobLeather_leatherMaster.GST;
            //    model.Value = JobLeather_leatherMaster.Value;
            //    model.Gst_Amt = JobLeather_leatherMaster.Gst_Amt;

            //    model.Total = JobLeather_leatherMaster.Total;
            //    model.Delivery_Date = JobLeather_leatherMaster.Delivery_Date;
            //}

            //else
            //{
            //    ID++;
            //    model.Job_Lether_to_lether_Code = "JobLet" + ID;
            //}
            return PartialView("~/Views/Jobwork/Job_Master/JobLeather_leather/Partial/JobLeather_leatherEdit.cshtml", model);
        }
        #region Curd Operation
        [HttpPost]
        public ActionResult saveJobLeather_leatherModel(JobLeather_leatherModel JobLeather_leatherModel)
        {
            JobLeather_leatherMaster JobLeather_leatherMaster = new JobLeather_leatherMaster();

                JobworkMaster jobworkMaster = new JobworkMaster();
                JobLeather_leatherManager JobLeather_leatherManager = new JobLeather_leatherManager();
                JobLeather_leatherMaster.Job_Lether_to_lether_Code = JobLeather_leatherModel.Job_Lether_to_lether_Code;
                JobLeather_leatherMaster.Date_from = JobLeather_leatherModel.Date_from;
                JobLeather_leatherMaster.Jobwork_raw_material = JobLeather_leatherModel.Jobwork_raw_material;
                JobLeather_leatherMaster.Encho_Raw_Material = JobLeather_leatherModel.Encho_Raw_Material;
                JobLeather_leatherMaster.Jw_Name = JobLeather_leatherModel.Jw_Name;
                JobLeather_leatherMaster.Process_Name = JobLeather_leatherModel.Process_Name;
                JobLeather_leatherMaster.Buyer = JobLeather_leatherModel.Buyer;
                JobLeather_leatherMaster.Season = JobLeather_leatherModel.Season;
                JobLeather_leatherMaster.Stores = JobLeather_leatherModel.Stores;
                JobLeather_leatherMaster.Group_ = JobLeather_leatherModel.Group_;
                JobLeather_leatherMaster.Category = JobLeather_leatherModel.Category;
                JobLeather_leatherMaster.Material = JobLeather_leatherModel.Material;


                JobLeather_leatherMaster.Finished_Material = JobLeather_leatherModel.Finished_Material;
                JobLeather_leatherMaster.Qty = JobLeather_leatherModel.Qty;
                JobLeather_leatherMaster.Qty_Uom = JobLeather_leatherModel.Qty_Uom;
                JobLeather_leatherMaster.Rate = JobLeather_leatherModel.Rate;
                JobLeather_leatherMaster.GST = JobLeather_leatherModel.GST;
                JobLeather_leatherMaster.Value = JobLeather_leatherModel.Value;
                JobLeather_leatherMaster.Gst_Amt = JobLeather_leatherModel.Gst_Amt;

                JobLeather_leatherMaster.Total = JobLeather_leatherModel.Total;
                JobLeather_leatherMaster.Delivery_Date = JobLeather_leatherModel.Delivery_Date;
            JobLeather_leatherMaster.Job_Lether_to_lether_id = JobLeather_leatherModel.Job_Lether_to_lether_id;
          
            if (JobLeather_leatherModel.Job_Lether_to_lether_id == 0)
            {
                JobLeather_leatherManager.Post(JobLeather_leatherMaster);
            }
            else {
                JobLeather_leatherManager.Put(JobLeather_leatherMaster);
            }

            return Json(JobLeather_leatherMaster, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateJobLeather_leather(JobLeather_leatherModel JobLeather_leatherMode)
        {
            JobLeather_leatherMaster JobLeather_leatherMaster = new JobLeather_leatherMaster();
           
                JobLeather_leatherMaster Job_Lether_to_lether_id = new JobLeather_leatherMaster();
                JobLeather_leatherManager JobLeather_leatherManager = new JobLeather_leatherManager();
                Job_Lether_to_lether_id = JobLeather_leatherManager.GetJobLeather_leather_ID(JobLeather_leatherMode.Job_Lether_to_lether_id);
                if (Job_Lether_to_lether_id != null)
                {
                    JobLeather_leatherMaster.Job_Lether_to_lether_id = JobLeather_leatherMode.Job_Lether_to_lether_id;
                    JobLeather_leatherMaster.Job_Lether_to_lether_Code = JobLeather_leatherMode.Job_Lether_to_lether_Code;
                    JobLeather_leatherMaster.Date_from = JobLeather_leatherMode.Date_from;
                    JobLeather_leatherMaster.Jobwork_raw_material = JobLeather_leatherMode.Jobwork_raw_material;
                    JobLeather_leatherMaster.Encho_Raw_Material = JobLeather_leatherMode.Encho_Raw_Material;
                    JobLeather_leatherMaster.Jw_Name = JobLeather_leatherMode.Jw_Name;
                    JobLeather_leatherMaster.Process_Name = JobLeather_leatherMode.Process_Name;
                    JobLeather_leatherMaster.Buyer = JobLeather_leatherMode.Buyer;
                    JobLeather_leatherMaster.Season = JobLeather_leatherMode.Season;
                    JobLeather_leatherMaster.Stores = JobLeather_leatherMode.Stores;
                    JobLeather_leatherMaster.Group_ = JobLeather_leatherMode.Group_;
                    JobLeather_leatherMaster.Category = JobLeather_leatherMode.Category;
                    JobLeather_leatherMaster.Material = JobLeather_leatherMode.Material;


                    JobLeather_leatherMaster.Finished_Material = JobLeather_leatherMode.Finished_Material;
                    JobLeather_leatherMaster.Qty = JobLeather_leatherMode.Qty;
                    JobLeather_leatherMaster.Qty_Uom = JobLeather_leatherMode.Qty_Uom;
                    JobLeather_leatherMaster.Rate = JobLeather_leatherMode.Rate;
                    JobLeather_leatherMaster.GST = JobLeather_leatherMode.GST;
                    JobLeather_leatherMaster.Value = JobLeather_leatherMode.Value;
                    JobLeather_leatherMaster.Gst_Amt = JobLeather_leatherMode.Gst_Amt;

                    JobLeather_leatherMaster.Total = JobLeather_leatherMode.Total;
                    JobLeather_leatherMaster.Delivery_Date = JobLeather_leatherMode.Delivery_Date;
                    JobLeather_leatherManager.Put(JobLeather_leatherMaster);
                }
                else
                {
                    return Json(JobLeather_leatherMaster, JsonRequestBehavior.AllowGet);
                }

           
            return Json(JobLeather_leatherMaster, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete_leatherJob(string  Job_Lether_to_lether_id)
        {
            JobLeather_leatherMaster JobLeather_leatherMaster = new JobLeather_leatherMaster();
            string status = "";
            JobLeather_leatherManager JobLeather_leatherManager = new JobLeather_leatherManager();
            JobLeather_leatherMaster Job_Lether_to_lether_id_ = new JobLeather_leatherMaster();
            List<MMS.Data.StoredProcedureModel.Job_LetherCode> Job_LetherCode = new List<MMS.Data.StoredProcedureModel.Job_LetherCode>();

            Job_LetherCode = JobLeather_leatherManager.GetJob_LetherCode(Job_Lether_to_lether_id);
            foreach (var item in Job_LetherCode)
            {
                if (item.Job_Lether_to_lether_id != 0)
                {
                    status = "Success";
                    JobLeather_leatherManager.Delete(item.Job_Lether_to_lether_id);
                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpGet]
        public ActionResult MultipleIssueMaterialName_Leather(int? MaterialNameID)
        {
            IssueSlip_SingleManager issueSlipManager = new IssueSlip_SingleManager();
            IssueSlipManager issueSlip = new IssueSlipManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialManager materialManager = new MaterialManager();
            ApprovedPriceListManager approvedPricelistManager = new ApprovedPriceListManager();
            List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
       
            string[] formats = { "dd/MM/yyyy" };
            // var dateTime = DateTime.ParseExact(IssueDate, formats, new CultureInfo("en-US"), DateTimeStyles.None);
            DateTime FromDate = DateTime.Now;

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
                    issueSizewiseStockList = issueSizewiseStockList.Where(x => x.IssueSize1 != 0).ToList();
                    return Json(new { Material = distinctList, SizeRangelist = issueSizewiseStockList, store = storeMaster, CategoryName = CategoryName, BalanceStock = ListOfPendingStockList.Select(x => x.BalanceStock), approvedPriceList = approvedPriceList, fromdate_ = FromDate }, JsonRequestBehavior.AllowGet);
                }


            
            // return Json("", JsonRequestBehavior.AllowGet);

        }
    }
}

