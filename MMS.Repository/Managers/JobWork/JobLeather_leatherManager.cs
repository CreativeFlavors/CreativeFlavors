using MMS.Common;
using MMS.Core.Entities.JobWork;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service.JobWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace MMS.Repository.Managers.JobWork
{
    public class JobLeather_leatherManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<JobLeather_leatherMaster> JobLeather_leatherRepository;
        public JobLeather_leatherManager()
        {
            JobLeather_leatherRepository = unitOfWork.Repository<JobLeather_leatherMaster>();
        }
        #region Add/Update/Delete Operation

        public bool Post(JobLeather_leatherMaster arg)
        {
         
            bool result = false;
            JobLeather_leatherMaster JobLeather_leatherMaster = new JobLeather_leatherMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                JobLeather_leatherRepository.Insert(arg);
                JobLeather_leatherMaster = arg;
                result = true;
               
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }


        public bool Put(JobLeather_leatherMaster model)
        {
            bool result = false;
            try
            {
                if (model != null)
                {
                    JobLeather_leatherMaster arg = JobLeather_leatherRepository.GetById(model.Job_Lether_to_lether_id);
                    arg.Job_Lether_to_lether_id = model.Job_Lether_to_lether_id;
                    arg.Job_Lether_to_lether_Code = model.Job_Lether_to_lether_Code;
                    arg.Date_from = model.Date_from;
                    arg.Jobwork_raw_material = model.Jobwork_raw_material;
                    arg.Encho_Raw_Material = model.Encho_Raw_Material;
                    arg.Jw_Name = model.Jw_Name;
                    arg.Process_Name = model.Process_Name;
                    arg.Buyer = model.Buyer;
                    arg.Season = model.Season;
                    arg.Stores = model.Stores;
                    arg.Group_ = model.Group_;
                    arg.Category = model.Category;
                    arg.Material = model.Material;


                    arg.Finished_Material = model.Finished_Material;
                    arg.Qty = model.Qty;
                    arg.Qty_Uom = model.Qty_Uom;
                    arg.Rate = model.Rate;
                    arg.GST = model.GST;
                    arg.Value = model.Value;
                    arg.Gst_Amt = model.Gst_Amt;

                    arg.Total = model.Total;
                    arg.Delivery_Date = model.Delivery_Date;
                    JobLeather_leatherRepository.Update(arg);
                    result = true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                JobLeather_leatherMaster model = JobLeather_leatherRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                JobLeather_leatherRepository.Update(model);
                //supplierMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }

        #endregion

        public List<JobLeather_leatherMaster> Get()
        {
            List<JobLeather_leatherMaster> JobLeather_leatherMaster = new List<JobLeather_leatherMaster>();
            try
            {
                JobLeather_leatherMaster = JobLeather_leatherRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return JobLeather_leatherMaster;
        }
        public JobLeather_leatherMaster GetJobLeather_leather_ID(int? Job_Lether_to_lether_id)
        {
            JobLeather_leatherMaster JobLeather_leatherMaster = new JobLeather_leatherMaster();
            JobLeather_leatherMaster = JobLeather_leatherRepository.Table.Where(x => x.Job_Lether_to_lether_id == Job_Lether_to_lether_id).FirstOrDefault();
            return JobLeather_leatherMaster;
        }
        public List<JobLeather_leatherMaster> Get_JobLeather_leatherMaster_List( List<int> list)
        {
            List<JobLeather_leatherMaster> JobLeather_leatherMaster = new List<JobLeather_leatherMaster>();
            try
            {
                //List<int> test = new List<int>();
                JobLeather_leatherMaster = JobLeather_leatherRepository.Table.Where(x => list.Contains(x.Job_Lether_to_lether_id)).ToList();
                return JobLeather_leatherMaster;
            }
            catch (Exception ex)
            {

            }
            return JobLeather_leatherMaster;
        }
        public List<MMS.Data.StoredProcedureModel.Job_LetherCode> GetJob_LetherCode(string Job_Lether_to_lether_id)
        {
            List<MMS.Data.StoredProcedureModel.Job_LetherCode> Job_LetherCode = new List<MMS.Data.StoredProcedureModel.Job_LetherCode>();
            try
            {
               
                    Job_LetherCode = JobLeather_leatherRepository.Jw_LetherGet(Job_Lether_to_lether_id);
            
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Job_LetherCode;
        }
    }
}
