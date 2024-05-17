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
    public class JobSheet_PairManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<JobSheet_PairMaster> JobSheet_PairRepository_;
        private RepositoryJob<JobSheet_PairMaster> JobSheet_PairRepository;
        private RepositoryJob<JobSheet_pairCodeMaster> JobSheet_pairCodRepository;
        private RepositoryJob<Jobsheet_pairSizerangeMaster> Jobsheet_pairSizerangeRepository;
        private RepositoryJob<Job_Sheet_pair_piecesMaster> Job_Sheet_pair_piecesMasterRepository;
        public JobSheet_PairManager()
        {
            // Job_ApprovedPriceRepository = unitOfWork.Repository<Job_ApprovedPriceMaster>();
            JobSheet_PairRepository = unitOfWork.Repository_<JobSheet_PairMaster>();
            JobSheet_pairCodRepository = unitOfWork.Repository_<JobSheet_pairCodeMaster>();
            Jobsheet_pairSizerangeRepository = unitOfWork.Repository_<Jobsheet_pairSizerangeMaster>();
            Job_Sheet_pair_piecesMasterRepository = unitOfWork.Repository_<Job_Sheet_pair_piecesMaster>();
        }
        #region Add/Update/Delete Operation

        public int Post_piece(Job_Sheet_pair_piecesMaster arg)
        {
            int result = 0;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedDate = DateTime.Now;

                //arg.UpdatedBy = username;
                Job_Sheet_pair_piecesMasterRepository.Insert(arg);
                result = arg.jobsheet_pair_id;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = 0;
            }
            return result;

        }
        public int Post(JobSheet_PairMaster arg)
        {
            int result = 0;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.IsDeleted = false;

                //arg.UpdatedBy = username;
                JobSheet_PairRepository.Insert(arg);
                result = arg.jobsheet_pair_id;
            }
            catch (Exception ex)
            {
                Logger.Log("Delivery_Date:  "+ arg.Delivery_Date.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex.InnerException.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex.StackTrace.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex.HResult.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex.TargetSite.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
              //  Logger.Log(ex.Data.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = 0;
            }
            return result;

        }
        public int Post_Code(JobSheet_pairCodeMaster arg)
        {
            int result = 0;
            try
            {
                JobSheet_pairCodeMaster model = JobSheet_pairCodRepository.Table.Where(p => p.jobsheet_pair_Code == arg.jobsheet_pair_Code).FirstOrDefault();
                if (model != null)
                {
                    model.jobsheet_pair_Code = arg.jobsheet_pair_Code;
                    model.CreatedDate = arg.CreatedDate;
                  
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                  
                    result = model.jobsheet_pair_code_id;
                }
                else
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    JobSheet_pairCodRepository.Insert(arg);
                    result = arg.jobsheet_pair_code_id;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return result;
        }
        public int Post_Code_size(Jobsheet_pairSizerangeMaster arg)
        {
            int result = 0;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                Jobsheet_pairSizerangeRepository.Insert(arg);
                result = arg.Job_sheet_pair_Size_id;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return result;
        }

        public int Put(JobSheet_PairMaster arg)
        {
            int result = 0;
            try
            {
                JobSheet_PairMaster model = JobSheet_PairRepository.Table.Where(p => p.jobsheet_pair_id == arg.jobsheet_pair_id).FirstOrDefault();
                if (arg != null)
                {

                    //   model.Jw_ApprovedPriceID = arg.Jw_ApprovedPriceID;
                    model.jobsheet_pair_id = arg.jobsheet_pair_id;
                    model.Date = arg.Date;
                    model.JW_Name = arg.JW_Name;
                    model.Process_Name = arg.Process_Name;
                    model.UC_NO_id = arg.UC_NO_id;
                    model.Buyer = arg.Buyer;
                    model.Season = arg.Season;
                    model.Stores = arg.Stores;
                    model.Category = arg.Category;

                    model.Group_ = arg.Group_;
                    model.Material = arg.Material;
                    model.Qty = arg.Qty;
                    model.Qty_Uom = arg.Qty_Uom;
                    model.Uc_Noms = arg.Uc_Noms;
                    model.Uc_Noms_Uom = arg.Uc_Noms_Uom;
                    model.Uc_value = arg.Uc_value;
                    model.Delivery_Date = arg.Delivery_Date;

                    model.Group_ = arg.Group_;
                    model.Issue_Material = arg.Issue_Material; 
                    model.Material = arg.Material;
                    model.Qty = arg.Qty;
                    model.Qty_Uom = arg.Qty_Uom;
                    model.Uc_Noms = arg.Uc_Noms;
                    model.Uc_Noms_Uom = arg.Uc_Noms_Uom;
                    model.Uc_value = arg.Uc_value;
                    model.Delivery_Date = arg.Delivery_Date;


                    model.Jw_Rate = arg.Jw_Rate;
                    model.Value = arg.Value;
                    model.Qty = arg.Qty;
                    model.Exertnal_work = arg.Exertnal_work;

                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    model.Reqried_Qty = arg.Reqried_Qty;
                    model.sheet = arg.sheet;
                    model.Extra_Piece = arg.Extra_Piece;
                    model.Reduce_Piece = arg.Reduce_Piece;
                    model.GST = arg.GST;
                    model.Gst_Amt = arg.Gst_Amt;
                    model.Total = arg.Total;

                    JobSheet_PairRepository.Update(arg);
                    result = arg.jobsheet_pair_id;
                }
                else
                {
                    result = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = 0;
            }

            return result;
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                JobSheet_PairMaster model = JobSheet_PairRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                JobSheet_PairRepository.Update(model);
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
                List<JobSheet_PairMaster> JobSheet_PairMaster = new List<JobSheet_PairMaster>();

                JobSheet_PairMaster = JobSheet_PairRepository.Table.Where(x => x.jobsheet_pair_id == id).ToList();
                foreach (var item in JobSheet_PairMaster)
                {
                    item.IsDeleted = true;
                    item.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                    item.DeletedDate = DateTime.Now;
                    JobSheet_PairRepository.Update(item);
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
        public bool Delete_size(Jobsheet_pairSizerangeMaster arg)
        {
            Jobsheet_pairSizerangeMaster Jobsheet_pairSizerangeMaster = new Jobsheet_pairSizerangeMaster();
            bool result = false;
            List<Jobsheet_pairSizerangeMaster> model = Getjobsheet_SizeList(arg.jobsheet_pair_Code_id, arg.jobsheet_pair_id);
            try
            {
                foreach (var item in model)
                {
                    Jobsheet_pairSizerangeRepository.Delete(item);
                }
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return result;
        }
        #endregion

        public List<JobSheet_PairMaster> Get()
        {
            List<JobSheet_PairMaster> JobSheet_PairMaster = new List<JobSheet_PairMaster>();
            try
            {
                JobSheet_PairMaster = JobSheet_PairRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return JobSheet_PairMaster;
        }

        public List<JobSheet_pairCodeMaster> GetJobSheetPair()
        {
            List<JobSheet_pairCodeMaster> JobSheet_PairMaster = new List<JobSheet_pairCodeMaster>();
            try
            {
                JobSheet_PairMaster = JobSheet_pairCodRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return JobSheet_PairMaster;
        }

        public List<JobSheet_PairMaster> save_check_material(int Material ,int jobsheet_pair_Code_id)
        {
            List<JobSheet_PairMaster> JobSheet_PairMaster = new List<JobSheet_PairMaster>();
            try
            {
                JobSheet_PairMaster = JobSheet_PairRepository.Table.Where(X =>X.Material==Material && X.jobsheet_pair_Code_id == jobsheet_pair_Code_id && X.IsDeleted == false || X.IsDeleted == null).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return JobSheet_PairMaster;
        }
        public List<JobSheet_PairMaster> Get_byCode( int jobsheet_pair_Code_id)
        {
            List<JobSheet_PairMaster> JobSheet_PairMaster = new List<JobSheet_PairMaster>();
            try
            {
                JobSheet_PairMaster = JobSheet_PairRepository.Table.Where(X => X.IsDeleted == true || X.IsDeleted == null).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return JobSheet_PairMaster;
        }
        public List<JobSheet_pairCodeMaster> GetJobsheet_paircode_Tbl()
        {
            List<JobSheet_pairCodeMaster> JobSheet_pairCodeMaster = new List<JobSheet_pairCodeMaster>();
            try
            {
                JobSheet_pairCodeMaster = JobSheet_pairCodRepository.Table.Where(y => y.IsDeleted == false || y.IsDeleted == null).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return JobSheet_pairCodeMaster;
        }

        public JobSheet_PairMaster Getjobsheet_pair_id(int? jobsheet_pair_id)
        {
            JobSheet_PairMaster JobSheet_PairMaster = new JobSheet_PairMaster();
            try
            {
                JobSheet_PairMaster = JobSheet_PairRepository.Table.Where(x => x.jobsheet_pair_id == jobsheet_pair_id).FirstOrDefault();
                return JobSheet_PairMaster;
            }
            catch (Exception ex)
            {

            }
            return JobSheet_PairMaster;
        }
        public List<MMS.Data.StoredProcedureModel.Jw_JobSheet_pair> GetJobwork_Jw_JobSheet_pairGrid(int JW_Name)
        {
            List<MMS.Data.StoredProcedureModel.Jw_JobSheet_pair> approvedlist = new List<MMS.Data.StoredProcedureModel.Jw_JobSheet_pair>();
            try
            {
                if (JW_Name != null)
                {
                    approvedlist = JobSheet_PairRepository.Jw_JobSheet_pair(JW_Name);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return approvedlist;
        }
        public List<MMS.Data.StoredProcedureModel.Jw_JobSheet_pair> GetJobwork_Jw_JobSheet_pairGrid_Forsheet(int JW_Name)
        {
            List<MMS.Data.StoredProcedureModel.Jw_JobSheet_pair> approvedlist = new List<MMS.Data.StoredProcedureModel.Jw_JobSheet_pair>();
            try
            {
                if (JW_Name != 0)
                {
                    approvedlist = JobSheet_PairRepository.Jw_JobSheet_pair(JW_Name);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return approvedlist;
        }
        public int Delete_jobsheet(int jobsheet_pair_Code_id)
        {
         
            try
            {
                if (jobsheet_pair_Code_id != 0)
                {
                    var items = JobSheet_PairRepository.Jw_JobSheet_pair_delete(jobsheet_pair_Code_id);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return 0;
        }
        public JobSheet_pairCodeMaster Getjobsheet_pair_code_id(int? jobsheet_pair_code_id)
        {
            JobSheet_pairCodeMaster JobSheet_pairCodeMaster = new JobSheet_pairCodeMaster();
            try
            {
                JobSheet_pairCodeMaster = JobSheet_pairCodRepository.Table.Where(x => x.jobsheet_pair_code_id == jobsheet_pair_code_id).FirstOrDefault();
                return JobSheet_pairCodeMaster;
            }
            catch (Exception ex)
            {

            }
            return JobSheet_pairCodeMaster;
        }
        public List<Jobsheet_pairSizerangeMaster> Getjobsheet_SizeList(int? jobsheet_pair_Code_id, int? jobsheet_pair_id)
        {
            List<Jobsheet_pairSizerangeMaster> Jobsheet_pairSizerangeMaster = new List<Jobsheet_pairSizerangeMaster>();
            try
            {
                Jobsheet_pairSizerangeMaster = Jobsheet_pairSizerangeRepository.Table.Where(x =>x.jobsheet_pair_Code_id == jobsheet_pair_Code_id && x.jobsheet_pair_id == jobsheet_pair_id).ToList();
                return Jobsheet_pairSizerangeMaster;
            }
            catch (Exception ex)
            {

            }
            return Jobsheet_pairSizerangeMaster;
        }
        public List<JobSheet_PairMaster> Getjobsheet_by_code_id(int? jobsheet_pair_code_id)
        {
            List<JobSheet_PairMaster> JobSheet_PairMaster = new List<JobSheet_PairMaster>();
            try
            {
                JobSheet_PairMaster = JobSheet_PairRepository.Table.Where(x => x.jobsheet_pair_Code_id == jobsheet_pair_code_id).ToList();
                return JobSheet_PairMaster;
            }
            catch (Exception ex)
            {

            }
            return JobSheet_PairMaster;
        }
        public List<JobSheet_PairMaster> Getjobsheet_pair_Materials(int[] list)
        {
            List<JobSheet_PairMaster> JobSheet_pair = new List<JobSheet_PairMaster>();
            try
            {
                //List<int> test = new List<int>();
                JobSheet_pair = JobSheet_PairRepository.Table.Where(x => list.Contains(x.jobsheet_pair_Code_id)).ToList();
                return JobSheet_pair;
            }
            catch (Exception ex)
            {

            }
            return JobSheet_pair;
        }
    }
}