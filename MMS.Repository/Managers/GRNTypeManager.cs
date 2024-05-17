using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.GateEntry;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;  

namespace MMS.Repository.Managers
{
   public class GRNTypeManager:IGRNService,IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GrnTypeMaster> IssueTypeManagerRepository;
        private Repository<GateEntryInwardMaterialsMaster> gateEntryRepository;

        #region Add/Update/Delete Operation

        public bool Post(GrnTypeMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;

                IssueTypeManagerRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(GrnTypeMaster arg)
        {
            bool result = false;
            try
            {
                GrnTypeMaster model = IssueTypeManagerRepository.Table.Where(p => p.GrnTypeMasterId == arg.GrnTypeMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.GrnTypeMasterId = arg.GrnTypeMasterId;
                    model.IssueType = arg.IssueType;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();                   
                    model.UpdatedBy = username;

                    IssueTypeManagerRepository.Update(model);
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
                GrnTypeMaster model = IssueTypeManagerRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                IssueTypeManagerRepository.Update(model);
                //IssueTypeManagerRepository.Delete(model);
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

        #region Helper Method

        public GRNTypeManager()
        {
            IssueTypeManagerRepository = unitOfWork.Repository<GrnTypeMaster>();
        }

        public GrnTypeMaster GetIssueType(string IssueType)
        {
            GrnTypeMaster issueTypeMaster = new GrnTypeMaster();
            if (IssueType != "" && IssueType != null)
            {
                issueTypeMaster = IssueTypeManagerRepository.Table.Where(x => x.IssueType == IssueType).SingleOrDefault();
            }
            return issueTypeMaster;
        }

        public GrnTypeMaster GetIssueTypeMasterId(int IssueTypeMasterId)
        {
            GrnTypeMaster issueTypeMaster = new GrnTypeMaster();
            try
            {
                if (IssueTypeMasterId != 0)
                {
                    issueTypeMaster = IssueTypeManagerRepository.Table.Where(x => x.GrnTypeMasterId == IssueTypeMasterId).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return issueTypeMaster;

        }
        public GrnTypeMaster Get(int id)
        {
            return null;
        }

        public List<GrnTypeMaster> Get()
        {
            List<GrnTypeMaster> issueTypeMasterzlist = new List<GrnTypeMaster>();
            try
            {
                issueTypeMasterzlist = IssueTypeManagerRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<GrnTypeMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return issueTypeMasterzlist;
        }

        public List<GrnTypeMaster> GetJobWork()
        {
            List<GrnTypeMaster> issueTypeMasterzlist = new List<GrnTypeMaster>();
            try
            {
                issueTypeMasterzlist = IssueTypeManagerRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null && X.IsJobWork==true).ToList<GrnTypeMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return issueTypeMasterzlist;
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
        #endregion
    }
}
