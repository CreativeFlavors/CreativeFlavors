using MMS.Data;
using MMS.Core.Entities.JobWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Common;
using System.Web;

namespace MMS.Repository.Managers.JobWork
{
    public class JobOtherWorkManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();        
        private RepositoryJob<JobOtherWorkMaster> Job_OtherWorkRepository;

        public JobOtherWorkManager()
        {            
            Job_OtherWorkRepository = unitOfWork.Repository_<JobOtherWorkMaster>();
        }

        public List<MMS.Data.StoredProcedureModel.JobOtherWorkList> GetJobOtherWork()
        {
            List<MMS.Data.StoredProcedureModel.JobOtherWorkList> jobOtherWorkList = new List<MMS.Data.StoredProcedureModel.JobOtherWorkList>();
            try
            {
                jobOtherWorkList = Job_OtherWorkRepository.JobOtherWorkList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return jobOtherWorkList;
        }

        public List<JobOtherWorkMaster> Get()
        {
            List<JobOtherWorkMaster> JobOtherworkMaster = new List<JobOtherWorkMaster>();
            try
            {
                JobOtherworkMaster = Job_OtherWorkRepository.Table.Where(X => X.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return JobOtherworkMaster;
        }

        public JobOtherWorkMaster GetJobOtherWork_id(int? Job_OtherWork_id)
        {
            JobOtherWorkMaster jobOtherworkMaster = new JobOtherWorkMaster();
            try
            {
                jobOtherworkMaster = Job_OtherWorkRepository.Table.Where(x => x.OtherJobWork_Id == Job_OtherWork_id).FirstOrDefault();
                return jobOtherworkMaster;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return jobOtherworkMaster;
        }

        public bool Post(JobOtherWorkMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.IsDeleted = false;

                //arg.UpdatedBy = username;
                Job_OtherWorkRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }

        public bool Put(JobOtherWorkMaster arg)
        {
            bool result = false;
            try
            {
                JobOtherWorkMaster model = Job_OtherWorkRepository.Table.Where(p => p.OtherJobWork_Id == arg.OtherJobWork_Id).FirstOrDefault();
                if (arg != null)
                {
                    model.OtherJobWork_Id = arg.OtherJobWork_Id;
                    model.Services = arg.Services;
                    model.OtherJobWork_Code = arg.OtherJobWork_Code;
                    model.OtherJobWork_Date = arg.OtherJobWork_Date;
                    model.Department_Id = arg.Department_Id;
                    model.JobWork_Id = arg.JobWork_Id;
                    model.Process_Id = arg.Process_Id;
                    model.Buyer_Id = arg.Buyer_Id;
                    model.Season_Id = arg.Season_Id;
                    model.Stoeres_Id = arg.Stoeres_Id;
                    model.Group_Id = arg.Group_Id;
                    model.Category_Id = arg.Category_Id;
                    model.Machinery_Id = arg.Machinery_Id;
                    model.Spare_Id = arg.Spare_Id;
                    model.Quantity = arg.Quantity;
                    model.UOM = arg.UOM;
                    model.ServiceDescription = arg.ServiceDescription;
                    model.JobWork_Price = arg.JobWork_Price;
                    model.JobWork_Price_Value = arg.JobWork_Price_Value;
                    model.GST = arg.GST;
                    model.GST_Amount = arg.GST_Amount;
                    model.TotalCost = arg.TotalCost;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    Job_OtherWorkRepository.Update(model);
                    result = true;
                }
                else
                {
                    result = false;
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
                JobOtherWorkMaster model = Job_OtherWorkRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                Job_OtherWorkRepository.Update(model);                
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        
    }
}
