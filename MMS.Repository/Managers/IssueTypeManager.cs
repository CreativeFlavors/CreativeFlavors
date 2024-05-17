using MMS.Common;
using MMS.Core.Entities;
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
    public class IssueTypeManager : IssueTypeService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<IssueTypeMaster> IssueTypeManagerRepository;

        #region Add/Update/Delete Operation

        public IssueTypeMaster Post(IssueTypeMaster arg)
        {
            bool result = false;
            IssueTypeMaster issueTypeMaster = new IssueTypeMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                IssueTypeManagerRepository.Insert(arg);
                issueTypeMaster = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return issueTypeMaster;

        }
        public bool Put(IssueTypeMaster arg)
        {
            bool result = false;
            try
            {
                IssueTypeMaster model = IssueTypeManagerRepository.Table.Where(p => p.IssueTypeMasterId == arg.IssueTypeMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.IssueTypeMasterId = arg.IssueTypeMasterId;
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
                IssueTypeMaster model = IssueTypeManagerRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                IssueTypeManagerRepository.Update(model);
                // IssueTypeManagerRepository.Delete(model);
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

        public IssueTypeManager()
        {
            IssueTypeManagerRepository = unitOfWork.Repository<IssueTypeMaster>();
        }

        public IssueTypeMaster GetIssueType(string IssueType)
        {
            IssueTypeMaster issueTypeMaster = new IssueTypeMaster();
            if (IssueType != "" && IssueType != null)
            {
                issueTypeMaster = IssueTypeManagerRepository.Table.Where(x => x.IssueType == IssueType).SingleOrDefault();
            }
            return issueTypeMaster;
        }

        public IssueTypeMaster GetIssueTypeMasterId(int IssueTypeMasterId)
        {
            IssueTypeMaster issueTypeMaster = new IssueTypeMaster();
            if (IssueTypeMasterId != 0)
            {
                issueTypeMaster = IssueTypeManagerRepository.Table.Where(x => x.IssueTypeMasterId == IssueTypeMasterId).SingleOrDefault();
            }
            return issueTypeMaster;
        }
        public IssueTypeMaster Get(int id)
        {
            return null;
        }

        public List<IssueTypeMaster> Get()
        {
            List<IssueTypeMaster> issueTypeMasterzlist = new List<IssueTypeMaster>();
            try
            {
                issueTypeMasterzlist = IssueTypeManagerRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<IssueTypeMaster>();
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
