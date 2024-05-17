using MMS.Common;
using MMS.Core.Entities.JobWork;
using MMS.Core.Entities;

using MMS.Repository.Managers.JobWork;
using MMS.Repository.Managers;
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


namespace MMS.Web.Controllers.JobWork
{

    [CustomFilter]
    public class Jw_ApprovedPriceController : Controller
    {
        //
        // GET: /Jw_ApprovedPrice/

        [HttpGet]
        public ActionResult Jw_ApprovedPrice()
        {
            MMS.Web.Models.JobworkModel.Jw_ApprovedPriceModel Pm = new Jw_ApprovedPriceModel();
            return View("~/Views/Jobwork/Job_Master/Jw_ApprovedPrice/Jw_ApprovedPriceMaster.cshtml", Pm);
        }
        public ActionResult Jw_ApprovedPriceGrid()
        {
            Jw_ApprovedPriceModel vm = new Jw_ApprovedPriceModel();
        //    Job_ApprovedPriceManager Job_ApprovedPriceManager = new Job_ApprovedPriceManager();

            Job_ApprovedPriceManager Job_ApprovedPriceManager = Job_ApprovedPriceManager.GetInstance;

            List<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist> Aplist = new List<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist>();
            Jw_ApprovedPriceModel Jw_ApprovedPriceModel = new Jw_ApprovedPriceModel();

            Aplist = Job_ApprovedPriceManager.GetApprovedPriceGrid();
            Jw_ApprovedPriceModel.Job_ApprovedPriceList = Aplist;
            return PartialView("~/Views/Jobwork/Job_Master/Jw_ApprovedPrice/Partial/Jw_ApprovedPriceMasterGrid.cshtml", Jw_ApprovedPriceModel);
        }
        [HttpPost]
        public ActionResult EditJw_ApprovedPriceList(int Jw_ApprovedPriceID)
        {



            Job_ApprovedPriceManager Job_ApprovedPriceManager = Job_ApprovedPriceManager.GetInstance;
            Job_ApprovedPriceMaster Job_ApprovedPriceMaster = new Job_ApprovedPriceMaster();
            Jw_ApprovedPriceModel model = new Jw_ApprovedPriceModel();
            Job_ApprovedPriceMaster = Job_ApprovedPriceManager.GetJw_ApprovedPriceID(Jw_ApprovedPriceID);
            // int ID = MMS.Web.ExtensionMethod.HtmlHelper.getJobCodeId();
            List<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist> Aplist = new List<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist>();
            Jw_ApprovedPriceModel Jw_ApprovedPriceModel = new Jw_ApprovedPriceModel();
            Aplist = Job_ApprovedPriceManager.GetJobwork_ApprovedPriceGrid(Jw_ApprovedPriceID);
            Jw_ApprovedPriceModel.Job_ApprovedPriceList = Aplist;

            if (Job_ApprovedPriceMaster != null)
            {
                model.Jw_ApprovedPriceID = Job_ApprovedPriceMaster.Jw_ApprovedPriceID;
                model.Date = Job_ApprovedPriceMaster.Date;
                model.JW_Name = Job_ApprovedPriceMaster.JW_Name;
                model.Process_Name = Job_ApprovedPriceMaster.Process_Name;
                model.Stage_From = Job_ApprovedPriceMaster.Stage_From;
                model.Stage_To = Job_ApprovedPriceMaster.Stage_To;
                model.Rate_For_JW = Job_ApprovedPriceMaster.Rate_For_JW;
                model.Rate_For_JW_id = Job_ApprovedPriceMaster.Rate_For_JW_id;
                model.Tax_Details = Job_ApprovedPriceMaster.Tax_Details;
                model.Lead_Time_Days = Job_ApprovedPriceMaster.Lead_Time_Days;
                model.Job_ExcessPercentage = Job_ApprovedPriceMaster.Job_ExcessPercentage;
                model.Issue_Type = Job_ApprovedPriceMaster.Issue_Type;
                model.Finished_Material = Job_ApprovedPriceMaster.Finished_Material;
                model.Product_BuyerStyle = Job_ApprovedPriceMaster.Product_BuyerStyle;
                model.GSt = Job_ApprovedPriceMaster.GSt;
            }

            else
            {
                //  ID++;
            }
            return PartialView("~/Views/Jobwork/Job_Master/Jw_ApprovedPrice/Partial/Jw_ApprovedPriceMasterDetail.cshtml", Jw_ApprovedPriceModel);
        }
        [HttpPost]
        public ActionResult EditJw_ApprovedPriceList_jwid(int Jw_ApprovedPriceID)
        {



            Job_ApprovedPriceManager Job_ApprovedPriceManager = Job_ApprovedPriceManager.GetInstance;
            Job_ApprovedPriceMaster Job_ApprovedPriceMaster = new Job_ApprovedPriceMaster();
            Jw_ApprovedPriceModel model = new Jw_ApprovedPriceModel();
            Job_ApprovedPriceMaster = Job_ApprovedPriceManager.GetJw_ApprovedPriceID(Jw_ApprovedPriceID);
            // int ID = MMS.Web.ExtensionMethod.HtmlHelper.getJobCodeId();
           
            if (Job_ApprovedPriceMaster != null)
            {
                model.Jw_ApprovedPriceID = Job_ApprovedPriceMaster.Jw_ApprovedPriceID;
                model.Date = Job_ApprovedPriceMaster.Date;
                model.JW_Name = Job_ApprovedPriceMaster.JW_Name;
                model.Process_Name = Job_ApprovedPriceMaster.Process_Name;
                model.Stage_From = Job_ApprovedPriceMaster.Stage_From;
                model.Stage_To = Job_ApprovedPriceMaster.Stage_To;
                model.Rate_For_JW = Job_ApprovedPriceMaster.Rate_For_JW;
                model.Rate_For_JW_id = Job_ApprovedPriceMaster.Rate_For_JW_id;
                model.Tax_Details = Job_ApprovedPriceMaster.Tax_Details;
                model.Lead_Time_Days = Job_ApprovedPriceMaster.Lead_Time_Days;
                model.Job_ExcessPercentage = Job_ApprovedPriceMaster.Job_ExcessPercentage;
                model.Issue_Type = Job_ApprovedPriceMaster.Issue_Type;
                model.Finished_Material = Job_ApprovedPriceMaster.Finished_Material;
                model.Product_BuyerStyle = Job_ApprovedPriceMaster.Product_BuyerStyle;
                model.GSt = Job_ApprovedPriceMaster.GSt;

            }

            else
            {
                //  ID++;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #region Curd Operation
        [HttpPost]
        public ActionResult saveJob_ApprovedPrice(Jw_ApprovedPriceModel Jw_ApprovedPriceModel)
        {
            Job_ApprovedPriceMaster Job_ApprovedPriceMaster = new Job_ApprovedPriceMaster();

                Job_ApprovedPriceMaster Job_ApprovedPriceMaster_ = new Job_ApprovedPriceMaster();
                Job_ApprovedPriceManager Job_ApprovedPriceManager = new Job_ApprovedPriceManager();
                Job_ApprovedPriceMaster.Date = DateTime.Now;
                Job_ApprovedPriceMaster.JW_Name = Jw_ApprovedPriceModel.JW_Name;
                Job_ApprovedPriceMaster.Process_Name = Jw_ApprovedPriceModel.Process_Name;
                Job_ApprovedPriceMaster.Stage_From = Jw_ApprovedPriceModel.Stage_From;
                Job_ApprovedPriceMaster.Stage_To = Jw_ApprovedPriceModel.Stage_To;
                Job_ApprovedPriceMaster.Rate_For_JW = Jw_ApprovedPriceModel.Rate_For_JW;
                Job_ApprovedPriceMaster.Rate_For_JW_id = Jw_ApprovedPriceModel.Rate_For_JW_id;
                Job_ApprovedPriceMaster.Tax_Details = Jw_ApprovedPriceModel.Tax_Details;
                Job_ApprovedPriceMaster.Lead_Time_Days = Jw_ApprovedPriceModel.Lead_Time_Days;
            Job_ApprovedPriceMaster.Job_ExcessPercentage = Jw_ApprovedPriceModel.Job_ExcessPercentage;
            Job_ApprovedPriceMaster.Finished_Material = Jw_ApprovedPriceModel.Finished_Material;
            Job_ApprovedPriceMaster.Issue_Type = Jw_ApprovedPriceModel.Issue_Type;

            Job_ApprovedPriceMaster.Product_BuyerStyle = Jw_ApprovedPriceModel.Product_BuyerStyle;
            Job_ApprovedPriceMaster.GSt = Jw_ApprovedPriceModel.GSt;
            Job_ApprovedPriceManager.Post(Job_ApprovedPriceMaster);
    

            return Json(Job_ApprovedPriceMaster, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update_Approve(Jw_ApprovedPriceModel Jw_ApprovedPriceModel)
        {
            Job_ApprovedPriceMaster Job_ApprovedPriceMaster = new Job_ApprovedPriceMaster();

      
                Job_ApprovedPriceMaster Jw_ApprovedPriceID = new Job_ApprovedPriceMaster();

                Job_ApprovedPriceManager Job_ApprovedPriceManager = new Job_ApprovedPriceManager();
                Jw_ApprovedPriceID = Job_ApprovedPriceManager.GetJw_ApprovedPriceID(Jw_ApprovedPriceModel.Jw_ApprovedPriceID);
                if (Jw_ApprovedPriceID != null)
                {
                    Job_ApprovedPriceMaster.Jw_ApprovedPriceID = Jw_ApprovedPriceModel.Jw_ApprovedPriceID;
                    Job_ApprovedPriceMaster.Date = Jw_ApprovedPriceModel.Date;
                    Job_ApprovedPriceMaster.JW_Name = Jw_ApprovedPriceModel.JW_Name;
                    Job_ApprovedPriceMaster.Process_Name = Jw_ApprovedPriceModel.Process_Name;
                    Job_ApprovedPriceMaster.Stage_From = Jw_ApprovedPriceModel.Stage_From;
                    Job_ApprovedPriceMaster.Stage_To = Jw_ApprovedPriceModel.Stage_To;
                    Job_ApprovedPriceMaster.Rate_For_JW = Jw_ApprovedPriceModel.Rate_For_JW;
                    Job_ApprovedPriceMaster.Rate_For_JW_id = Jw_ApprovedPriceModel.Rate_For_JW_id;
                    Job_ApprovedPriceMaster.Tax_Details = Jw_ApprovedPriceModel.Tax_Details;
                    Job_ApprovedPriceMaster.Lead_Time_Days = Jw_ApprovedPriceModel.Lead_Time_Days;
                Job_ApprovedPriceMaster.Job_ExcessPercentage = Jw_ApprovedPriceModel.Job_ExcessPercentage;
                Job_ApprovedPriceMaster.Finished_Material = Jw_ApprovedPriceModel.Finished_Material;
                Job_ApprovedPriceMaster.Issue_Type = Jw_ApprovedPriceModel.Issue_Type;
                Job_ApprovedPriceMaster.Product_BuyerStyle = Jw_ApprovedPriceModel.Product_BuyerStyle;
                Job_ApprovedPriceMaster.GSt = Jw_ApprovedPriceModel.GSt;
                Job_ApprovedPriceManager.Put(Job_ApprovedPriceMaster);
                }
                else
                {
                    return Json(Job_ApprovedPriceMaster, JsonRequestBehavior.AllowGet);
                }

           
            return Json(Job_ApprovedPriceMaster, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteApprove(int Jw_ApprovedPriceID)
        {
            Job_ApprovedPriceMaster Job_ApprovedPriceMaster = new Job_ApprovedPriceMaster();
            string status = "";
            Job_ApprovedPriceManager Job_ApprovedPriceManager = new Job_ApprovedPriceManager();
            Job_ApprovedPriceMaster = Job_ApprovedPriceManager.GetJw_ApprovedPriceID(Jw_ApprovedPriceID);
            if (Job_ApprovedPriceMaster != null)
            {
                status = "Success";
                Job_ApprovedPriceManager.Delete(Job_ApprovedPriceMaster.Jw_ApprovedPriceID);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteApprove_Jobname(int Jobname)
        {
            Job_ApprovedPriceMaster Job_ApprovedPriceMaster = new Job_ApprovedPriceMaster();
            string status = "";
            Job_ApprovedPriceManager Job_ApprovedPriceManager = new Job_ApprovedPriceManager();
            
                status = "Success";
               Job_ApprovedPriceManager.Delete_Jobname(Jobname);
       
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Viwe list
        [HttpGet]
        public ActionResult View_otherjobwork(string Stage_From, string Stage_To,string JW_Name, string Process_Name)
        {
            Job_ApprovedPriceManager Job_ApprovedPriceMaster = new Job_ApprovedPriceManager();
            JobworkManager JobworkManager = new JobworkManager();
            StageMasterManager StageMasters = new StageMasterManager();
            StageMasterManager StageMasters_To = new StageMasterManager();
            StageMaster stageMaster_from = new StageMaster();
            StageMaster stageMaster_to = new StageMaster();
            List<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist> Aplist = new List<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist>();
            int count =0;

            var items = (from x in JobworkManager.Get()
                          join y in Job_ApprovedPriceMaster.Get()
                          on x.JW_Id equals y.JW_Name

                         where y.Process_Name== Convert.ToInt32(Process_Name) &&y.JW_Name!= Convert.ToInt32(JW_Name)
                         select new { Jobwork_name = x.JW_Name, Approved_Price = y.Rate_For_JW , Approved_Date=y.Date }).ToList();

            //var stageto = (from x in Job_ApprovedPriceMaster.Get()
            //               join s in StageMasters.Get()
            //                  on x.Stage_From equals s.StageMasterId
            //               join s_To in StageMasters_To.Get()
            //               on x.Stage_To equals s_To.StageMasterId

            //               where s.Sequence >= Stage_To
            //               select new { stagemasterid = x.Stage_To }).ToList();

            //stageMaster_from = StageMasters.GetStageMasterId(Convert.ToInt32(Stage_From));
            //stageMaster_to = StageMasters.GetStageMasterId(Convert.ToInt32(Stage_To));
            //if (stageMaster_from.Sequence > stageMaster_to.Sequence)
            //{
            //    count = 1;
            //}

            //return Json(items, JsonRequestBehavior.AllowGet);
            return Json(new { items=items, Count= count }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult Process_NameCheck(int JW_Name, int Process_Name)
        {

            Job_ApprovedPriceManager Job_ApprovedPriceManager = Job_ApprovedPriceManager.GetInstance;
            List<Job_ApprovedPriceMaster> Job_ApprovedPriceMaster = new List<Job_ApprovedPriceMaster>();

            Job_ApprovedPriceMaster = Job_ApprovedPriceManager.Process_NameCheck_Manager(JW_Name, Process_Name);
                int status = Job_ApprovedPriceMaster.Count();
            //return Json(items, JsonRequestBehavior.AllowGet);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}
