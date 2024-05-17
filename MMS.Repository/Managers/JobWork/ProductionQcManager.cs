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
    public class ProductionQcManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private RepositoryJob<Production_QcMaster> JobRepositoryProduction_QcMaster;
        private RepositoryJob<Production_QcCodeMaster> JobRepositoryProduction_QcCodeMaster;
        private RepositoryJob<ProductionSizeQc_Issue> JobRepositoryProductionSizeQc_Issue;
        public ProductionQcManager()
        {
            JobRepositoryProduction_QcMaster = unitOfWork.Repository_<Production_QcMaster>();
            JobRepositoryProduction_QcCodeMaster = unitOfWork.Repository_<Production_QcCodeMaster>();
            JobRepositoryProductionSizeQc_Issue= unitOfWork.Repository_<ProductionSizeQc_Issue>();
        }
        #region Add/Update/Delete Operation

        public int Post(Production_QcMaster arg)
        {
            int result = 0;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.IsDeleted = false;

                //arg.UpdatedBy = username;
                JobRepositoryProduction_QcMaster.Insert(arg);
                result = arg.Qc_id;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = 0;
            }
            return result;

        }
        public int Post_Code(Production_QcCodeMaster arg)
        {
            int result = 0;
            try
            {
                Production_QcCodeMaster model = JobRepositoryProduction_QcCodeMaster.Table.Where(p => p.Production_Code == arg.Production_Code).FirstOrDefault();
                if (model != null)
                {
                    model.Production_Code = arg.Production_Code;
                    model.CreatedDate = arg.CreatedDate;

                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();

                    result = model.ProductionQc_ID;
                }
                else
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    JobRepositoryProduction_QcCodeMaster.Insert(arg);
                    result = arg.ProductionQc_ID;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return result;
        }
        public int Post_Code_size(ProductionSizeQc_Issue arg)
        {
            int result = 0;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                JobRepositoryProductionSizeQc_Issue.Insert(arg);
                result = arg.QcSize_id;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return result;
        }


        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                Production_QcCodeMaster model = JobRepositoryProduction_QcCodeMaster.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                JobRepositoryProduction_QcCodeMaster.Update(model);
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
        public List<Production_QcMaster> Get()
        {
            List<Production_QcMaster> Production_QcMaster = new List<Production_QcMaster>();
            try
            {
                Production_QcMaster = JobRepositoryProduction_QcMaster.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Production_QcMaster;
        }


        public List<Production_QcCodeMaster> Get_byCode()
        {
            List<Production_QcCodeMaster> Production_QcCodeMaster = new List<Production_QcCodeMaster>();
            try
            {
                Production_QcCodeMaster = JobRepositoryProduction_QcCodeMaster.Table.Where(X => X.IsDeleted == false ||X.IsDeleted == null).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Production_QcCodeMaster;
        }
        public Production_QcCodeMaster Get_byCode_Id(int ProductionQc_ID)
        {
           Production_QcCodeMaster Production_QcCodeMaster = new Production_QcCodeMaster();
            try
            {
                Production_QcCodeMaster = JobRepositoryProduction_QcCodeMaster.Table.Where(X =>X.ProductionQc_ID== ProductionQc_ID && X.IsDeleted == true || X.IsDeleted == null ).FirstOrDefault();
               // Production_QcCodeMaster = JobRepositoryProduction_QcCodeMaster.Table.Where(X => X.ProductionQc_ID == ProductionQc_ID && X.IsDeleted == false).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Production_QcCodeMaster;
        }

        public int Update_QcStage(int stage,string Lot_no,string Io,string type)
        {

            try
            {
                if (stage != 0 )
                {
                    var items = JobRepositoryProduction_QcCodeMaster.Update_QcStage_Sp(stage, Lot_no, Io, type);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return 0;
        }
    }
}
