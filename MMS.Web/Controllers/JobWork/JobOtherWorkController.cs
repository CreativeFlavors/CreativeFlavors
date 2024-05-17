using MMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Web.Models.JobOtherWork;
using MMS.Repository.Managers.JobWork;
using MMS.Core.Entities.JobWork;

namespace MMS.Web.Controllers.JobWork
{
    [CustomFilter]
    public class JobOtherWorkController : Controller
    {
        //
        // GET: /OtherJobWork/
        JobOtherWorkModel jobotherwork = new JobOtherWorkModel();
        JobOtherWorkManager jobOtherworkManager = new JobOtherWorkManager();
        JobOtherWorkMaster jobOtherWorkMaster = new JobOtherWorkMaster();
        Job_ApprovedPriceManager Job_ApprovedPriceManager = Job_ApprovedPriceManager.GetInstance;

        public ActionResult JobOtherWork()
        {
            MMS.Web.Models.JobOtherWork.JobOtherWorkModel jobOtherWorkModel = new MMS.Web.Models.JobOtherWork.JobOtherWorkModel();
            return View("~/Views/Jobwork/Job_Master/JobOtherWork/JobOtherWork.cshtml", jobOtherWorkModel);
        }

        public ActionResult JobOtherWorkList()
        {
            List<MMS.Data.StoredProcedureModel.JobOtherWorkList> jobOtherWorkList = new List<MMS.Data.StoredProcedureModel.JobOtherWorkList>();
            jobotherwork.JobOtherWorkList = jobOtherworkManager.GetJobOtherWork();

            return PartialView("~/Views/Jobwork/Job_Master/JobOtherWork/Partial/JobOtherWorkGrid.cshtml", jobotherwork);
        }

        public ActionResult EditJobOtherworkList(int OtherJobWork_Id)
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.GetJob_OtherWork_code_id();

            jobOtherWorkMaster = jobOtherworkManager.GetJobOtherWork_id(OtherJobWork_Id);
            
            if (jobOtherWorkMaster != null)
            {
                jobotherwork.OtherJobWork_Id = jobOtherWorkMaster.OtherJobWork_Id;
                jobotherwork.Services = jobOtherWorkMaster.Services;
                jobotherwork.OtherJobWork_Code = jobOtherWorkMaster.OtherJobWork_Code;
                jobotherwork.Department_Id = jobOtherWorkMaster.Department_Id;
                jobotherwork.OtherJobWork_Date = jobOtherWorkMaster.OtherJobWork_Date;
                jobotherwork.JobWork_Id = jobOtherWorkMaster.JobWork_Id;
                jobotherwork.Process_Id = jobOtherWorkMaster.Process_Id;
                jobotherwork.Buyer_Id = jobOtherWorkMaster.Buyer_Id;
                jobotherwork.Season_Id = jobOtherWorkMaster.Season_Id;
                jobotherwork.Stoeres_Id = jobOtherWorkMaster.Stoeres_Id;
                jobotherwork.Group_Id = jobOtherWorkMaster.Group_Id;
                jobotherwork.Category_Id = jobOtherWorkMaster.Category_Id;
                jobotherwork.Machinery_Id = jobOtherWorkMaster.Machinery_Id;
                jobotherwork.Spare_Id = jobOtherWorkMaster.Spare_Id;
                jobotherwork.Quantity = jobOtherWorkMaster.Quantity;
                jobotherwork.UOM = jobOtherWorkMaster.UOM;
                jobotherwork.ServiceDescription = jobOtherWorkMaster.ServiceDescription;
                jobotherwork.JobWork_Price = jobOtherWorkMaster.JobWork_Price;
                jobotherwork.JobWork_Price_Value = jobOtherWorkMaster.JobWork_Price_Value;
                jobotherwork.GST = jobOtherWorkMaster.GST;
                jobotherwork.GST_Amount = jobOtherWorkMaster.GST_Amount;
                jobotherwork.TotalCost = jobOtherWorkMaster.TotalCost;                
            }

            else
            {   
                jobotherwork.OtherJobWork_Code ="JOW"+  Convert.ToString(++ID);                      
            }
            return PartialView("~/Views/Jobwork/Job_Master/JobOtherWork/Partial/JobOtherWorkDetails.cshtml", jobotherwork);
        }

        public ActionResult SaveJobOtherWork(JobOtherWorkModel jobOtherWorModel)
        {                        
            try
            {
                int ID = MMS.Web.ExtensionMethod.HtmlHelper.GetJob_OtherWork_code_id();

                jobOtherWorkMaster.OtherJobWork_Code = "JOW" + Convert.ToString(++ID);
                jobOtherWorkMaster.Services = jobOtherWorModel.Services;
                jobOtherWorkMaster.OtherJobWork_Date = jobOtherWorModel.OtherJobWork_Date;
                jobOtherWorkMaster.Department_Id = jobOtherWorModel.Department_Id;
                jobOtherWorkMaster.JobWork_Id = jobOtherWorModel.JobWork_Id;
                jobOtherWorkMaster.Process_Id = jobOtherWorModel.Process_Id;
                jobOtherWorkMaster.Buyer_Id = jobOtherWorModel.Buyer_Id;
                jobOtherWorkMaster.Season_Id = jobOtherWorModel.Season_Id;
                jobOtherWorkMaster.Stoeres_Id = jobOtherWorModel.Stoeres_Id;
                jobOtherWorkMaster.Group_Id = jobOtherWorModel.Group_Id;
                jobOtherWorkMaster.Category_Id = jobOtherWorModel.Category_Id;
                jobOtherWorkMaster.Machinery_Id = jobOtherWorModel.Machinery_Id;
                jobOtherWorkMaster.Spare_Id = jobOtherWorModel.Spare_Id;
                jobOtherWorkMaster.Quantity = jobOtherWorModel.Quantity;
                jobOtherWorkMaster.UOM = jobOtherWorModel.UOM;
                jobOtherWorkMaster.ServiceDescription = jobOtherWorModel.ServiceDescription;
                jobOtherWorkMaster.JobWork_Price = jobOtherWorModel.JobWork_Price;
                jobOtherWorkMaster.JobWork_Price_Value = jobOtherWorModel.JobWork_Price_Value;
                jobOtherWorkMaster.GST = jobOtherWorModel.GST;
                jobOtherWorkMaster.GST_Amount = jobOtherWorModel.GST_Amount;
                jobOtherWorkMaster.TotalCost = jobOtherWorModel.TotalCost;
                jobOtherworkManager.Post(jobOtherWorkMaster);
            }
            catch(Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return Json(jobOtherWorkMaster, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateJobOtherWork(JobOtherWorkModel jobOtherWorModel)
        {                        
            try
            {
                jobOtherWorkMaster.OtherJobWork_Id = jobOtherWorModel.OtherJobWork_Id;
                jobOtherWorkMaster.Services = jobOtherWorModel.Services;
                jobOtherWorkMaster.OtherJobWork_Code = jobOtherWorModel.OtherJobWork_Code;
                jobOtherWorkMaster.OtherJobWork_Date = jobOtherWorModel.OtherJobWork_Date;
                jobOtherWorkMaster.Department_Id = jobOtherWorModel.Department_Id;
                jobOtherWorkMaster.JobWork_Id = jobOtherWorModel.JobWork_Id;
                jobOtherWorkMaster.Process_Id = jobOtherWorModel.Process_Id;
                jobOtherWorkMaster.Buyer_Id = jobOtherWorModel.Buyer_Id;
                jobOtherWorkMaster.Season_Id = jobOtherWorModel.Season_Id;
                jobOtherWorkMaster.Stoeres_Id = jobOtherWorModel.Stoeres_Id;
                jobOtherWorkMaster.Group_Id = jobOtherWorModel.Group_Id;
                jobOtherWorkMaster.Category_Id = jobOtherWorModel.Category_Id;
                jobOtherWorkMaster.Machinery_Id = jobOtherWorModel.Machinery_Id;
                jobOtherWorkMaster.Spare_Id = jobOtherWorModel.Spare_Id;
                jobOtherWorkMaster.Quantity = jobOtherWorModel.Quantity;
                jobOtherWorkMaster.UOM = jobOtherWorModel.UOM;
                jobOtherWorkMaster.ServiceDescription = jobOtherWorModel.ServiceDescription;
                jobOtherWorkMaster.JobWork_Price = jobOtherWorModel.JobWork_Price;
                jobOtherWorkMaster.JobWork_Price_Value = jobOtherWorModel.JobWork_Price_Value;
                jobOtherWorkMaster.GST = jobOtherWorModel.GST;
                jobOtherWorkMaster.GST_Amount = jobOtherWorModel.GST_Amount;
                jobOtherWorkMaster.TotalCost = jobOtherWorModel.TotalCost;
                jobOtherworkManager.Put(jobOtherWorkMaster);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return Json(jobOtherWorkMaster, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteJobOtherWork(int OtherJobWork_Id)
        {            
            string status = "";            
            try
            {
                jobOtherWorkMaster = jobOtherworkManager.GetJobOtherWork_id(OtherJobWork_Id);

                if (jobOtherWorkMaster != null)
                {
                    status = "Success";
                    jobOtherworkManager.Delete(jobOtherWorkMaster.OtherJobWork_Id);
                }
            }
            catch(Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }           
            
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetJobPrice(int JobWork_Id,int Process_Id)
        {
            decimal jobprice = 0;
            try
            {
                jobprice = Job_ApprovedPriceManager.GetJobPrice(JobWork_Id, Process_Id);
            }
            catch(Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Json(jobprice,JsonRequestBehavior.AllowGet);
        }
    }
}
