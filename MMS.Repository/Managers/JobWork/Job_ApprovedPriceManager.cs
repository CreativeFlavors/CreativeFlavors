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
    public sealed class Job_ApprovedPriceManager
    {
        private static int counter = 0;
        private static Job_ApprovedPriceManager instance = null;
        public static Job_ApprovedPriceManager GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new Job_ApprovedPriceManager();
                return instance;
            }
        }

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Job_ApprovedPriceMaster> Job_ApprovedPriceRepository_;
        private RepositoryJob<Job_ApprovedPriceMaster> Job_ApprovedPriceRepository;
        public Job_ApprovedPriceManager()
        {
            // Job_ApprovedPriceRepository = unitOfWork.Repository<Job_ApprovedPriceMaster>();
            Job_ApprovedPriceRepository = unitOfWork.Repository_<Job_ApprovedPriceMaster>();
        }
        #region Add/Update/Delete Operation

        public bool Post(Job_ApprovedPriceMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;

                //arg.UpdatedBy = username;
                Job_ApprovedPriceRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }


        public bool Put(Job_ApprovedPriceMaster arg)
        {
            bool result = false;
            try
            {
                Job_ApprovedPriceMaster model = Job_ApprovedPriceRepository.Table.Where(p => p.Jw_ApprovedPriceID == arg.Jw_ApprovedPriceID).FirstOrDefault();
                if (arg != null)
                {

                    //   model.Jw_ApprovedPriceID = arg.Jw_ApprovedPriceID;
                    model.Date = arg.Date;
                    model.JW_Name = arg.JW_Name;
                    model.Process_Name = arg.Process_Name;
                    model.Stage_From = arg.Stage_From;
                    model.Stage_To = arg.Stage_To;
                    model.Rate_For_JW = arg.Rate_For_JW;
                    model.Rate_For_JW_id = arg.Rate_For_JW_id;
                    model.Tax_Details = arg.Tax_Details;
                    model.Job_ExcessPercentage = arg.Job_ExcessPercentage;
                    model.Lead_Time_Days = arg.Lead_Time_Days;
                    model.CreatedDate = model.CreatedDate;
                    model.Product_BuyerStyle = arg.Product_BuyerStyle;
                    model.GSt = model.GSt;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;

                    Job_ApprovedPriceRepository.Update(arg);
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
                Job_ApprovedPriceMaster model = Job_ApprovedPriceRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                Job_ApprovedPriceRepository.Update(model);
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

        public bool Delete_Jobname(int id)
        {
            bool result = false;
            try
            {
               List<Job_ApprovedPriceMaster> Job_ApprovedPriceMaster = new List<Job_ApprovedPriceMaster>();

                Job_ApprovedPriceMaster = Job_ApprovedPriceRepository.Table.Where(x => x.JW_Name == id).ToList();
                foreach (var item in Job_ApprovedPriceMaster)
                {
                    item.IsDeleted = true;
                    item.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                    item.DeletedDate = DateTime.Now;
                    Job_ApprovedPriceRepository.Update(item);
                }
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

        public List<Job_ApprovedPriceMaster> Get()
        {
            List<Job_ApprovedPriceMaster> Job_ApprovedPrice = new List<Job_ApprovedPriceMaster>();
            try
            {
                Job_ApprovedPrice = Job_ApprovedPriceRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Job_ApprovedPrice;
        }
        public Job_ApprovedPriceMaster GetJw_ApprovedPriceID(int? Jw_ApprovedPriceID)
        {
            Job_ApprovedPriceMaster JobworkMaster = new Job_ApprovedPriceMaster();
            try
            {
                JobworkMaster = Job_ApprovedPriceRepository.Table.Where(x => x.Jw_ApprovedPriceID == Jw_ApprovedPriceID).FirstOrDefault();
                return JobworkMaster;
            }
            catch (Exception ex)
            {

            }
            return JobworkMaster;
        }
        public List<Job_ApprovedPriceMaster> Process_NameCheck_Manager(int JW_Name, int Process_Name)
        {
            List<Job_ApprovedPriceMaster> JobworkMaster = new List<Job_ApprovedPriceMaster>();
            try
            {
                JobworkMaster = Job_ApprovedPriceRepository.Table.Where(x => x.JW_Name == JW_Name && x.Process_Name == Process_Name).ToList();
                return JobworkMaster;
            }
            catch (Exception ex)
            {

            }
            return JobworkMaster;
        }
        public List<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist> GetApprovedPriceGrid()
        {
            List<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist> approvedlist = new List<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist>();
            try
            {
                approvedlist = Job_ApprovedPriceRepository.SearchApprovedPriceList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return approvedlist;
        }
        public List<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist> GetJobwork_ApprovedPriceGrid(int JW_Name)
        {
            List<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist> approvedlist = new List<MMS.Data.StoredProcedureModel.Jw_Approvedpricelist>();
            try
            {
                approvedlist = Job_ApprovedPriceRepository.SearchJobwork_Aprovrdlist(JW_Name);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return approvedlist;
        }

        public decimal GetJobPrice(int jobworkId,int processId)
        {
            decimal? getPrice = 0;
            try
            {
                getPrice = Job_ApprovedPriceRepository.Table.Where(x => x.JW_Name == jobworkId && x.Process_Name==processId).OrderByDescending(x=>x.Jw_ApprovedPriceID).Select(x=>x.Rate_For_JW).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return getPrice.HasValue ? getPrice.Value : 0;
        }

    }
}
