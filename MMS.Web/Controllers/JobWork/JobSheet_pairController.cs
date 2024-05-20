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
using System.Globalization;

namespace MMS.Web.Controllers.JobWork
{
    [CustomFilter]
    public class JobSheet_pairController : Controller
    {
        //
        // GET: /JobSheet_pair/

        [HttpGet]
        public ActionResult JobSheet_pairMaster()
        {
            MMS.Web.Models.JobworkModel.JobSheet_pairModel Pm = new JobSheet_pairModel();
            return View("~/Views/Jobwork/Job_Master/JobsheetPair/Jwsheet_pairMaster.cshtml", Pm);
        }
        public ActionResult JobWorkGrid()
        {
            JobSheet_pairCodeModel vm = new JobSheet_pairCodeModel();
            JobSheet_pairCodeMaster JobSheet_pairCodeMaster = new JobSheet_pairCodeMaster();
            JobSheet_PairManager JobSheet_PairManager = new JobSheet_PairManager();
            vm.JobSheet_pairCodeMasterlist = JobSheet_PairManager.GetJobsheet_paircode_Tbl();

            return PartialView("~/Views/Jobwork/Job_Master/JobsheetPair/Partial/Jwsheet_pairGrid.cshtml", vm);
        }
        [HttpPost]
        public ActionResult EditJobLeathertoLeatherList(int jobsheet_pair_Code_id)
        {
            

            JobSheet_PairManager JobSheet_PairManager_ = new JobSheet_PairManager();
            JobSheet_PairManager JobSheet_PairManager = new JobSheet_PairManager();
            JobSheet_PairMaster jobSheet_PairMaster = new JobSheet_PairMaster();
            JobSheet_pairModel model = new JobSheet_pairModel();
        //    JobSheet_PairMaster = JobSheet_PairManager.Getjobsheet_pair_id(jobsheet_pair_id);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.Getjobsheet_pair_code_id();

           
            List<MMS.Data.StoredProcedureModel.Jw_JobSheet_pair> Aplist = new List<MMS.Data.StoredProcedureModel.Jw_JobSheet_pair>();
            JobSheet_pairModel Jw_ApprovedPriceModel = new JobSheet_pairModel();
            Aplist = JobSheet_PairManager.GetJobwork_Jw_JobSheet_pairGrid_Forsheet(jobsheet_pair_Code_id);
            Jw_ApprovedPriceModel.Jw_JobSheet_pair = Aplist;


            JobSheet_pairCodeMaster JobSheet_pairCodeMaster = new JobSheet_pairCodeMaster();
            JobSheet_pairCodeMaster = JobSheet_PairManager_.Getjobsheet_pair_code_id(jobsheet_pair_Code_id);

            //  jobSheet_PairMaster = JobSheet_PairManager.Getjobsheet_pair_id(jobsheet_pair_Code_id);
            if (Jw_ApprovedPriceModel != null && Jw_ApprovedPriceModel.Jw_JobSheet_pair.Count !=0 && jobsheet_pair_Code_id != 0)
            {
                //Jw_ApprovedPriceModel.jobsheet_pair_Code = JobSheet_pairCodeMaster.jobsheet_pair_Code;
                Jw_ApprovedPriceModel.code = new JobSheet_pairCodeModel() { jobsheet_pair_Code = JobSheet_pairCodeMaster.jobsheet_pair_Code };
                Jw_ApprovedPriceModel.jobsheet_pair_Code_id = JobSheet_pairCodeMaster.jobsheet_pair_code_id;
                Jw_ApprovedPriceModel.Exertnal_work = jobSheet_PairMaster.Exertnal_work;
                Jw_ApprovedPriceModel.Date = jobSheet_PairMaster.Date;
                Jw_ApprovedPriceModel.JW_Name = Jw_ApprovedPriceModel.Jw_JobSheet_pair[0].JW_Name;
                Jw_ApprovedPriceModel.Process_Name = jobSheet_PairMaster.Process_Name;
                Jw_ApprovedPriceModel.UC_NO_id = jobSheet_PairMaster.UC_NO_id;
                Jw_ApprovedPriceModel.Buyer = Convert.ToInt32(Jw_ApprovedPriceModel.Jw_JobSheet_pair[0].Buyer ?? 0);
                Jw_ApprovedPriceModel.Season = Convert.ToInt32(Jw_ApprovedPriceModel.Jw_JobSheet_pair[0].Season ?? 0);
                Jw_ApprovedPriceModel.Stores = jobSheet_PairMaster.Stores;
                Jw_ApprovedPriceModel.Category = jobSheet_PairMaster.Category;
                Jw_ApprovedPriceModel.Group_ = jobSheet_PairMaster.Group_;
                Jw_ApprovedPriceModel.Material = jobSheet_PairMaster.Material;

                Jw_ApprovedPriceModel.Issue_Material = jobSheet_PairMaster.Issue_Material;

        Jw_ApprovedPriceModel.Qty = jobSheet_PairMaster.Qty;
                Jw_ApprovedPriceModel.Qty_Uom = jobSheet_PairMaster.Qty_Uom;
                Jw_ApprovedPriceModel.Uc_Noms = jobSheet_PairMaster.Uc_Noms;
                Jw_ApprovedPriceModel.Uc_Noms_Uom = jobSheet_PairMaster.Uc_Noms_Uom;
                Jw_ApprovedPriceModel.Uc_value = jobSheet_PairMaster.Uc_value;
                Jw_ApprovedPriceModel.Delivery_Date = jobSheet_PairMaster.Delivery_Date;
                Jw_ApprovedPriceModel.Jw_Rate = jobSheet_PairMaster.Jw_Rate;

                Jw_ApprovedPriceModel.Value = jobSheet_PairMaster.Value;

                //Size
              
            }

            else
            {
                ID++;
                Jw_ApprovedPriceModel.code =new JobSheet_pairCodeModel() { jobsheet_pair_Code = "JBO" + ID };
              //  JobSheet_pairModel JobSheet_pairModel = new JobSheet_pairModel();
              //  JobSheet_pairModel.code.jobsheet_pair_Code = "JBO" + ID;
            }
            return PartialView("~/Views/Jobwork/Job_Master/JobsheetPair/Partial/Jwsheet_pairDetail.cshtml", Jw_ApprovedPriceModel);
        }

        [HttpPost]
        public ActionResult EditJw_sheettopairdetail_jwid(int jobsheet_pair_id)
        {


            JobSheet_PairManager JobSheet_PairManager = new JobSheet_PairManager();
            JobSheet_PairManager JobSheet_PairManager_ = new JobSheet_PairManager();
            JobSheet_PairMaster jobSheet_PairMaster = new JobSheet_PairMaster();
            JobSheet_pairModel model = new JobSheet_pairModel();
            jobSheet_PairMaster = JobSheet_PairManager.Getjobsheet_pair_id(jobsheet_pair_id);
            // int ID = MMS.Web.ExtensionMethod.HtmlHelper.getJobCodeId();
            JobSheet_pairCodeMaster JobSheet_pairCodeMaster = new JobSheet_pairCodeMaster();
            List<Jobsheet_pairSizerangeMaster> listSizeItemMaterial = new List<Jobsheet_pairSizerangeMaster>();

            JobSheet_pairCodeMaster = JobSheet_PairManager_.Getjobsheet_pair_code_id(jobSheet_PairMaster.jobsheet_pair_Code_id);
            if (jobSheet_PairMaster != null)
            {
                model.jobsheet_pair_Code = JobSheet_pairCodeMaster.jobsheet_pair_Code;
                model.jobsheet_pair_Code_id = jobSheet_PairMaster.jobsheet_pair_Code_id;
                model.jobsheet_pair_id = jobSheet_PairMaster.jobsheet_pair_id;
                model.Exertnal_work = jobSheet_PairMaster.Exertnal_work;
               model.Date = jobSheet_PairMaster.Date;
               model.JW_Name = jobSheet_PairMaster.JW_Name;
               model.Process_Name = jobSheet_PairMaster.Process_Name;
               model.UC_NO_id = jobSheet_PairMaster.UC_NO_id;
               model.Buyer = jobSheet_PairMaster.Buyer;
               model.Season = jobSheet_PairMaster.Season;
               model.Stores = jobSheet_PairMaster.Stores;
               model.Category = jobSheet_PairMaster.Category;
               model.Group_ = jobSheet_PairMaster.Group_;
               model.Material = jobSheet_PairMaster.Material;
               model.Qty = jobSheet_PairMaster.Qty;
               model.Qty_Uom = jobSheet_PairMaster.Qty_Uom;
                model.Issue_Material = jobSheet_PairMaster.Issue_Material;
                model.Uc_Noms = jobSheet_PairMaster.Uc_Noms;
               model.Uc_Noms_Uom = jobSheet_PairMaster.Uc_Noms_Uom;
               model.Uc_value = jobSheet_PairMaster.Uc_value;
               model.Delivery_Date = jobSheet_PairMaster.Delivery_Date;
               model.Jw_Rate = jobSheet_PairMaster.Jw_Rate;
               model.Value = jobSheet_PairMaster.Value;
                model.Extra_Piece = jobSheet_PairMaster.Extra_Piece;
                model.Reduce_Piece = jobSheet_PairMaster.Reduce_Piece;
                model.GST = jobSheet_PairMaster.GST;
                model.Gst_Amt = jobSheet_PairMaster.Gst_Amt;
                model.Total = jobSheet_PairMaster.Total;

                listSizeItemMaterial = JobSheet_PairManager.Getjobsheet_SizeList(model.jobsheet_pair_Code_id , model.jobsheet_pair_id);
                listSizeItemMaterial = listSizeItemMaterial.OrderBy(x => Convert.ToDecimal(x.Sizerange)).ToList();
            }

            else
            {
                //  ID++;
            }
            return Json(new { model = model, listSizeItemMaterial= listSizeItemMaterial }, JsonRequestBehavior.AllowGet);
        }
        #region Curd Operation
        [HttpPost]
        public ActionResult saveJob(JobSheet_pairModel JobSheet_pairModel)
        {
            JobSheet_PairManager JobSheet_PairManager = new JobSheet_PairManager();
            JobSheet_PairManager JobSheet_PairManager_Return = new JobSheet_PairManager();
            JobSheet_PairMaster JobSheet_PairMaster = new JobSheet_PairMaster();
            JobSheet_pairCodeMaster JobSheet_pairCodeMaster = new JobSheet_pairCodeMaster();
            Jobsheet_pairSizerangeMaster Jobsheet_pairSizerangeMaster = new Jobsheet_pairSizerangeMaster();
            JobSheet_pairModel vm = new JobSheet_pairModel();
            Job_UnitConvertionManager Job_UnitConvertionManager = new Job_UnitConvertionManager();
            string data;
            var Uc_sheet = Job_UnitConvertionManager.Get().Where(x=>x.UC_No_Id == JobSheet_pairModel.UC_NO_id).FirstOrDefault();

            Double Rec_qty = Convert.ToDouble(JobSheet_pairModel.Qty) * JobSheet_pairModel.Uc_Noms;
            decimal R_qty = Convert.ToDecimal(Rec_qty);
            decimal Sheet;
            decimal Sheetwith_value;
            decimal? result;
            if (JobSheet_pairModel.Reduce_Piece != 0)
            {
                Sheetwith_value = (Convert.ToDecimal(Rec_qty)/ Convert.ToDecimal( Uc_sheet.Sheet_Value));
                // Sheet = Convert.ToInt16(Math.Round(Sheetwith_value)) - Convert.ToInt32( JobSheet_pairModel.Reduce_Piece);
             //   var remainingpiece = JobSheet_pairModel.Extra_Piece - JobSheet_pairModel.Reduce_Piece;
                 result = (1-(Sheetwith_value - Math.Truncate(Sheetwith_value)));
                if (result == Convert.ToDecimal(0.0))
                {
                    Sheet = ((((Sheetwith_value)) + 1 )- Convert.ToInt32(JobSheet_pairModel.Reduce_Piece));
                }
                else {
                    Sheet = (((Sheetwith_value)) - Convert.ToInt32(JobSheet_pairModel.Reduce_Piece));
                }
            }
            else
            {
                Sheetwith_value =  Convert.ToDecimal(Rec_qty)/ Uc_sheet.Sheet_Value;
                //   Sheet = Convert.ToInt16(Math.Round(Sheetwith_value));
                result = (1 - (Sheetwith_value - Math.Truncate(Sheetwith_value)));
                if (result != Convert.ToDecimal(0.0))
                {
                    Sheet = ((((Sheetwith_value))) );
                }
                else
                {
                    Sheet = (((Sheetwith_value)));
                }
            }
            var remainingpiece =  JobSheet_pairModel.Reduce_Piece;
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (JobSheet_pairModel.jobsheet_pair_id ==0)
            {
                // JobworkMaster jobworkMaster = new JobworkMaster();
                // JobworkManager JobworkManager = new JobworkManager();
                JobSheet_pairCodeMaster.jobsheet_pair_Code = JobSheet_pairModel.jobsheet_pair_Code;
                JobSheet_PairMaster.jobsheet_pair_Code_id = JobSheet_PairManager.Post_Code(JobSheet_pairCodeMaster);
                JobSheet_PairMaster.Exertnal_work = JobSheet_pairModel.Exertnal_work;
                JobSheet_PairMaster.Date = JobSheet_pairModel.Date;
                JobSheet_PairMaster.JW_Name = JobSheet_pairModel.JW_Name;
                JobSheet_PairMaster.Process_Name = JobSheet_pairModel.Process_Name;
                JobSheet_PairMaster.UC_NO_id = JobSheet_pairModel.UC_NO_id;
                JobSheet_PairMaster.Buyer = JobSheet_pairModel.Buyer;
                JobSheet_PairMaster.Season = JobSheet_pairModel.Season;
                JobSheet_PairMaster.Stores = JobSheet_pairModel.Stores;
                JobSheet_PairMaster.Category = JobSheet_pairModel.Category;
                JobSheet_PairMaster.Group_ = JobSheet_pairModel.Group_;
                JobSheet_PairMaster.Issue_Material = JobSheet_pairModel.Issue_Material;
                JobSheet_PairMaster.Material = JobSheet_pairModel.Material;
                JobSheet_PairMaster.Qty = JobSheet_pairModel.Qty;
                JobSheet_PairMaster.Qty_Uom = JobSheet_pairModel.Qty_Uom;
                JobSheet_PairMaster.Uc_Noms = JobSheet_pairModel.Uc_Noms;
                JobSheet_PairMaster.Uc_Noms_Uom = JobSheet_pairModel.Uc_Noms_Uom;
                JobSheet_PairMaster.Uc_value = JobSheet_pairModel.Uc_value;
                JobSheet_PairMaster.GST = JobSheet_pairModel.GST;
                JobSheet_PairMaster.Gst_Amt = JobSheet_pairModel.Gst_Amt;
                JobSheet_PairMaster.Total = JobSheet_pairModel.Total;
                JobSheet_PairMaster.Delivery_Date = Convert.ToDateTime(JobSheet_pairModel.Delivery_Date);
                Logger.Log("Delivery_Date_test:  " + JobSheet_PairMaster.Delivery_Date, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                JobSheet_PairMaster.Jw_Rate = JobSheet_pairModel.Jw_Rate;

                JobSheet_PairMaster.Value = JobSheet_pairModel.Value;
                JobSheet_PairMaster.Reqried_Qty = R_qty;
                JobSheet_PairMaster.sheet = Sheet;
                JobSheet_PairMaster.Extra_Piece = result;
                JobSheet_PairMaster.Reduce_Piece = remainingpiece;
                var check= JobSheet_PairManager.save_check_material(JobSheet_pairModel.Material, JobSheet_PairMaster.jobsheet_pair_Code_id).Count();
                if (check <= 0 || check == null)
                {
                    int jobsheet_pair_id = JobSheet_PairManager.Post(JobSheet_PairMaster);
                    int count = Material_change_count(JobSheet_pairModel.Material);
                    if (count != 0)
                    {
                        int count_;
                        count_ = 0;
                        //int count = Material_change_count(JobSheet_pairModel.Material);
                        string[] intArray;
                        string[] sizearry;
                        intArray = JobSheet_pairModel.Sizerange.Split(',');
                        sizearry = JobSheet_pairModel.size.Split(',');
                        int intArraycount = intArray.GetLength(0);
                        if (count == intArraycount - 1)
                        {
                            foreach (var item in intArray)
                            {
                                //sizearry = item.Split(';');
                                //string a = sizearry[0];
                                //string b = sizearry[1];
                                string[] qty = item.Split(';');
                                string size = sizearry[count_];
                                Jobsheet_pairSizerangeMaster.jobsheet_pair_id = jobsheet_pair_id;
                                Jobsheet_pairSizerangeMaster.jobsheet_pair_Code_id = JobSheet_PairMaster.jobsheet_pair_Code_id;
                                if (size != "," && qty[0] != "," && qty[0] != "")
                                {
                                    Jobsheet_pairSizerangeMaster.Sizerange = Convert.ToDecimal(size);
                                    Jobsheet_pairSizerangeMaster.Qty = Convert.ToDecimal(qty[0]);
                                    JobSheet_PairManager.Post_Code_size(Jobsheet_pairSizerangeMaster);
                                }
                                count_++;
                            }
                        }
                    }
                }
                else
                {
                    return Json(data = "0", JsonRequestBehavior.AllowGet);
                }
            }
            vm.JobSheet_pairModellist = JobSheet_PairManager.Get_byCode(JobSheet_PairMaster.jobsheet_pair_Code_id);
            return Json(new { JobSheet_PairManager_Return= JobSheet_PairManager, R_qty= R_qty, Sheet= Sheet }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateJob_jobsheet(JobSheet_pairModel JobSheet_pairModel)
        {
            JobSheet_PairManager JobSheet_PairManager = new JobSheet_PairManager();
            JobSheet_PairManager JobSheet_PairManager_Return = new JobSheet_PairManager();
            JobSheet_PairMaster JobSheet_PairMaster = new JobSheet_PairMaster();
            JobSheet_pairCodeMaster JobSheet_pairCodeMaster = new JobSheet_pairCodeMaster();
            JobSheet_pairModel vm = new JobSheet_pairModel();
            Jobsheet_pairSizerangeMaster Jobsheet_pairSizerangeMaster = new Jobsheet_pairSizerangeMaster();
            Job_UnitConvertionManager Job_UnitConvertionManager = new Job_UnitConvertionManager();
            string data;
            var Uc_sheet = Job_UnitConvertionManager.Get().Where(x => x.UC_No_Id == JobSheet_pairModel.UC_NO_id).FirstOrDefault();

            //double? Rec_qty = Convert.ToDouble(JobSheet_pairModel.Qty) * JobSheet_pairModel.Uc_Noms;
            //int R_qty = Convert.ToInt16(Math.Round(Rec_qty.Value));
            //int Sheet;
            //if (R_qty != 0)
            //{
            //    Sheet = JobSheet_pairModel.Qty / R_qty;
            //}
            //else
            //{
            //    Sheet = 0;
            //}
            Double Rec_qty = Convert.ToDouble(JobSheet_pairModel.Qty) * JobSheet_pairModel.Uc_Noms;
      //      int R_qty = Convert.ToInt16(Math.Round(Rec_qty));
            decimal R_qty = Convert.ToDecimal(Rec_qty);
            decimal Sheet;
            decimal Sheetwith_value;
            decimal? result;
            if (JobSheet_pairModel.Reduce_Piece != 0)
            {
                Sheetwith_value =  Convert.ToDecimal(Rec_qty)/ Uc_sheet.Sheet_Value;
                result = (1 - (Sheetwith_value - Math.Truncate(Sheetwith_value)));
                if (result != Convert.ToDecimal(0.0))
                {
                    Sheet = ((((Sheetwith_value)) ) - Convert.ToInt32(JobSheet_pairModel.Reduce_Piece));
                }
                else
                {
                    Sheet = ((Sheetwith_value)) - Convert.ToInt32(JobSheet_pairModel.Reduce_Piece);
                }
            }
            else
            {
                Sheetwith_value =  Convert.ToDecimal(Rec_qty) / Uc_sheet.Sheet_Value;
                result = (1 - (Sheetwith_value - Math.Truncate(Sheetwith_value)));
                if (result == Convert.ToDecimal(0.0))
                {
                    Sheet = (((Sheetwith_value) ));
                }
                else
                {
                    Sheet = (((Sheetwith_value)));
                }

            }
            var remainingpiece = JobSheet_pairModel.Reduce_Piece;

            if (JobSheet_pairModel.jobsheet_pair_id != 0)
            {
                // JobworkMaster jobworkMaster = new JobworkMaster();
                // JobworkManager JobworkManager = new JobworkManager();
                JobSheet_PairMaster.jobsheet_pair_id = JobSheet_pairModel.jobsheet_pair_id;
                JobSheet_pairCodeMaster.jobsheet_pair_Code = JobSheet_pairModel.jobsheet_pair_Code;
                JobSheet_PairMaster.jobsheet_pair_Code_id = JobSheet_PairManager.Post_Code(JobSheet_pairCodeMaster);
                JobSheet_PairMaster.Exertnal_work = JobSheet_pairModel.Exertnal_work;
                JobSheet_PairMaster.Date = JobSheet_pairModel.Date;
                JobSheet_PairMaster.JW_Name = JobSheet_pairModel.JW_Name;
                JobSheet_PairMaster.Process_Name = JobSheet_pairModel.Process_Name;
                JobSheet_PairMaster.UC_NO_id = JobSheet_pairModel.UC_NO_id;
                JobSheet_PairMaster.Buyer = JobSheet_pairModel.Buyer;
                JobSheet_PairMaster.Season = JobSheet_pairModel.Season;
                JobSheet_PairMaster.Stores = JobSheet_pairModel.Stores;
                JobSheet_PairMaster.Category = JobSheet_pairModel.Category;
                JobSheet_PairMaster.Group_ = JobSheet_pairModel.Group_;
                JobSheet_PairMaster.Issue_Material = JobSheet_pairModel.Issue_Material;
                JobSheet_PairMaster.Material = JobSheet_pairModel.Material;
                JobSheet_PairMaster.Qty = JobSheet_pairModel.Qty;
                JobSheet_PairMaster.Qty_Uom = JobSheet_pairModel.Qty_Uom;
                JobSheet_PairMaster.Uc_Noms = JobSheet_pairModel.Uc_Noms;
                JobSheet_PairMaster.Uc_Noms_Uom = JobSheet_pairModel.Uc_Noms_Uom;
                JobSheet_PairMaster.Uc_value = JobSheet_pairModel.Uc_value;
                JobSheet_PairMaster.Delivery_Date = JobSheet_pairModel.Delivery_Date;
                JobSheet_PairMaster.Jw_Rate = JobSheet_pairModel.Jw_Rate;

                JobSheet_PairMaster.Value = JobSheet_pairModel.Value;
                JobSheet_PairMaster.Reqried_Qty = R_qty;
                JobSheet_PairMaster.sheet = Sheet;
                JobSheet_PairMaster.Extra_Piece = result;
                JobSheet_PairMaster.Reduce_Piece = remainingpiece;
                JobSheet_PairMaster.GST = JobSheet_pairModel.GST;
                JobSheet_PairMaster.Gst_Amt = JobSheet_pairModel.Gst_Amt;
                JobSheet_PairMaster.Total = JobSheet_pairModel.Total;
                int jobsheet_pair_id = JobSheet_PairManager.Put(JobSheet_PairMaster);


                int count_;
                count_ = 0;
                int count = Material_change_count(JobSheet_pairModel.Material);
                if (count != 0)
                {
                    Jobsheet_pairSizerangeMaster.jobsheet_pair_id = jobsheet_pair_id;
                    Jobsheet_pairSizerangeMaster.jobsheet_pair_Code_id = JobSheet_PairMaster.jobsheet_pair_Code_id;
                   bool delete= JobSheet_PairManager.Delete_size(Jobsheet_pairSizerangeMaster);
                    string[] intArray;
                    string[] sizearry;
                    intArray = JobSheet_pairModel.Sizerange.Split(',');
                    sizearry = JobSheet_pairModel.size.Split(',');
                    int intArraycount = intArray.GetLength(0);
                    if (count == intArraycount-1)
                    {
                        foreach (var item in intArray)
                        {
                            //sizearry = item.Split(';');
                            //string a = sizearry[0];
                            //string b = sizearry[1];
                            string[] qty = item.Split(';');
                            string size = sizearry[count_];
                   
                            if (size != "," && qty[0] != "," && qty[0] != "")
                            {
                                Jobsheet_pairSizerangeMaster.Sizerange = Convert.ToDecimal(size);
                                Jobsheet_pairSizerangeMaster.Qty = Convert.ToDecimal(qty[0]);
                                JobSheet_PairManager.Post_Code_size(Jobsheet_pairSizerangeMaster);
                            }
                            count_++;
                        }
                    }
                }
            }
            else
                {
                    return Json(JobSheet_PairMaster, JsonRequestBehavior.AllowGet);
                }

            
            return Json(JobSheet_PairMaster, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteJob(int JW_Id)
        {
            JobworkMaster JobworkMaster = new JobworkMaster();
            string status = "";
            JobworkManager JobworkManager = new JobworkManager();
            JobworkMaster = JobworkManager.GetJobMasterMasterID(JW_Id);
            if (JobworkMaster != null)
            {
                status = "Success";
                JobworkManager.Delete(JobworkMaster.JW_Id);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region function
        public ActionResult GetUnitcovertion_value(int UC_No_Id)
        {
            UnitConvertionMaster UnitConvertionMaster = new UnitConvertionMaster();
            Job_UnitConvertionManager Job_UnitConvertionManager =new Job_UnitConvertionManager();
            MaterialManager materialManager = new MaterialManager();
            string status = "";

            UnitConvertionMaster= Job_UnitConvertionManager.Get_UC_No_Id(UC_No_Id);
            var Uom = materialManager.Get().Where(x => x.MaterialMasterId == UnitConvertionMaster.Material_id_To).FirstOrDefault();

            return Json(new { UnitConvertionMaster = UnitConvertionMaster, Uom = Uom }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Get_jw_Approvedprice(test model)
        {
            Job_ApprovedPriceManager Job_ApprovedPriceManager = Job_ApprovedPriceManager.GetInstance;
            try
            {
                var item = (from y in Job_ApprovedPriceManager.Get()
                            where y.JW_Name == model.JW_Name && y.Process_Name == model.Process_Name
                            select new { Rate_For_JW = y.Rate_For_JW, Date = y.Date, Lead_Time_Days = y.Lead_Time_Days }).OrderByDescending(y => y.Date).FirstOrDefault();
                return Json(new { items = item }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { items = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Get_jw_Approvedprice_Leather(test model,string MaterialNameID )
        {
            Job_ApprovedPriceManager Job_ApprovedPriceManager = Job_ApprovedPriceManager.GetInstance;
            try
            {
                var item = (from y in Job_ApprovedPriceManager.Get()
                            where y.JW_Name == model.JW_Name && y.Process_Name == model.Process_Name && y.Finished_Material== Convert.ToInt32( MaterialNameID)
                            select new { Rate_For_JW = y.Rate_For_JW, Date = y.Date, Lead_Time_Days = y.Lead_Time_Days }).OrderByDescending(y => y.Date).FirstOrDefault();
                return Json(new { items = item }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { items = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Get_jw_Approvedprice_Production(test_Production model)
        {
            Job_ApprovedPriceManager Job_ApprovedPriceManager = Job_ApprovedPriceManager.GetInstance;
            try
            {
                var item = (from y in Job_ApprovedPriceManager.Get()
                            where y.JW_Name == model.JW_Name && y.Process_Name == model.Process_Name && y.Stage_From == model.Stage_From && y.Stage_To == model.Stage_To && y.Product_BuyerStyle==model.Style
                            select new { Rate_For_JW = y.Rate_For_JW, Date = y.Date, Lead_Time_Days = y.Lead_Time_Days, GSt=y.GSt }).OrderByDescending(y => y.Date).FirstOrDefault();
                return Json(new { items = item }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { items = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Material_change(int Materialid)
        {
            IssueSlip_SingleManager issueSlipManager = new IssueSlip_SingleManager();
            IssueSlipManager issueSlip = new IssueSlipManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialManager materialManager = new MaterialManager();
            ApprovedPriceListManager approvedPricelistManager = new ApprovedPriceListManager();
            List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();

            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            ColorManager colorManager = new ColorManager();
            var items = (from x in materialManager.Get()
                         join y in materialNameManager.Get()
                          on x.MaterialName equals y.MaterialMasterID
                         join z in colorManager.Get()
                         on x.ColorMasterId equals z.ColorMasterId into yG
                         from y1 in yG.DefaultIfEmpty()
                         where x.MaterialMasterId == Materialid
                         select new { MaterialDescription = string.Format("{0} {1} {2}", y.MaterialDescription, x.OptionMaterialValue, y1 != null ? y1.Color != null ? y1.Color : string.Empty : string.Empty), x.MaterialMasterId, x.Price, ColorMasterId = (y1 != null ? y1.ColorMasterId != 0 ? y1.ColorMasterId : 0 : 0), x.Uom, x.UomUnit, x.SizeRangeMasterId, x.SubstanceMasterId, x.MaterialCategoryMasterId, x.MaterialGroupMasterId, x.GradeMasterId, x.StoreMasterId, x.PurchasePrimary, x.PrimaryUnit, x.PurchasePacket, x.PacketUnit, x.CurrencyMasterId, x.isImport, x.isLocal, x.isImportCustomer });
            int id = Convert.ToInt32(ExtensionMethod.HtmlHelper.CategoryName.Leathers);
            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
            listSizeItemMaterial = materialManager.GetSizeItemMaterial(distinctList.FirstOrDefault().MaterialMasterId);
            listSizeItemMaterial = listSizeItemMaterial.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();

            return Json(new { listSizeItemMaterial = listSizeItemMaterial }, JsonRequestBehavior.AllowGet);
        }
        public int Material_change_count(int Materialid)
        {
            IssueSlip_SingleManager issueSlipManager = new IssueSlip_SingleManager();
            IssueSlipManager issueSlip = new IssueSlipManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialManager materialManager = new MaterialManager();
            ApprovedPriceListManager approvedPricelistManager = new ApprovedPriceListManager();
            List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();

            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            ColorManager colorManager = new ColorManager();
            var items = (from x in materialManager.Get()
                         join y in materialNameManager.Get()
                          on x.MaterialName equals y.MaterialMasterID
                         join z in colorManager.Get()
                         on x.ColorMasterId equals z.ColorMasterId into yG
                         from y1 in yG.DefaultIfEmpty()
                         where x.MaterialMasterId == Materialid
                         select new { MaterialDescription = string.Format("{0} {1} {2}", y.MaterialDescription, x.OptionMaterialValue, y1 != null ? y1.Color != null ? y1.Color : string.Empty : string.Empty), x.MaterialMasterId, x.Price, ColorMasterId = (y1 != null ? y1.ColorMasterId != 0 ? y1.ColorMasterId : 0 : 0), x.Uom, x.UomUnit, x.SizeRangeMasterId, x.SubstanceMasterId, x.MaterialCategoryMasterId, x.MaterialGroupMasterId, x.GradeMasterId, x.StoreMasterId, x.PurchasePrimary, x.PrimaryUnit, x.PurchasePacket, x.PacketUnit, x.CurrencyMasterId, x.isImport, x.isLocal, x.isImportCustomer });
            int id = Convert.ToInt32(ExtensionMethod.HtmlHelper.CategoryName.Leathers);
            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
            listSizeItemMaterial = materialManager.GetSizeItemMaterial(distinctList.FirstOrDefault().MaterialMasterId);
            listSizeItemMaterial = listSizeItemMaterial.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();

            return listSizeItemMaterial.Count;
        }
        #endregion

        public class test
        {
            public int Process_Name { get; set; }
            public int JW_Name { get; set; }
        }
        public class test_Production
        {
            public int Process_Name { get; set; }
            public int JW_Name { get; set; }
            public int Stage_From { get; set; }
            public int Stage_To { get; set; }
            public int Style { get; set; }
        }
        public ActionResult Get_extra_piece(int Materialid, int Process_Name, int JW_Name)
        {
            JobSheet_PairManager JobSheet_PairManager_ = new JobSheet_PairManager();
            JobSheet_PairMaster JobSheet_PairMaster = new JobSheet_PairMaster();
            try
            {
                var Extra_Piece = JobSheet_PairManager_.Get().Where(x => x.Issue_Material == Materialid && x.Process_Name == Process_Name && x.JW_Name== JW_Name && x.IsDeleted == false).Sum(x=>x.Extra_Piece);

                var Reduce_Piece = JobSheet_PairManager_.Get().Where(x => x.Issue_Material == Materialid && x.Process_Name == Process_Name && x.JW_Name == JW_Name && x.IsDeleted==false).Sum(x => x.Reduce_Piece);
                var result = Extra_Piece - Reduce_Piece;
                return Json(new { items = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { items = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        // Job_Sheet_Delete_detail

        public ActionResult Job_Sheet_Delete_detail(int jobsheet_pair_Code_id)
        {
            JobSheet_PairManager JobSheet_PairManager_ = new JobSheet_PairManager();
            try
            {
                var delete = JobSheet_PairManager_.Delete_jobsheet(jobsheet_pair_Code_id);
                       return Json(new { items = 0 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { items = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
