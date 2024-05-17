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
    public class CommisionCritiriaManager : CommisionCritiriaService, IDisposable
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
         private Repository<CommisionCriteria> CommisionCriteriaRepository;

        #region Add/Update/Delete Operation

         public bool Post(CommisionCriteria arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                CommisionCriteriaRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(CommisionCriteria arg)
        {
            bool result = false;
            try
            {
                CommisionCriteria model = CommisionCriteriaRepository.Table.Where(p => p.CommisionCriteriaId == arg.CommisionCriteriaId).FirstOrDefault();
                if (model != null)
                {
                    model.CommisionCriteriaId = arg.CommisionCriteriaId;
                    model.CommisionName = arg.CommisionName;
                    model.CommisionPercent = arg.CommisionPercent;
                    model.Criteria = arg.Criteria;
                    model.Value = arg.Value;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();                    
                    model.UpdatedBy = username;
                    CommisionCriteriaRepository.Update(model);
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
                CommisionCriteria model = CommisionCriteriaRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                CommisionCriteriaRepository.Update(model);
                // CommisionCriteriaRepository.Delete(model);
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

        public CommisionCritiriaManager()
        {
            CommisionCriteriaRepository = unitOfWork.Repository<CommisionCriteria>();
        }

        public CommisionCriteria GetCommisionName(string CommisionName)
        {
            CommisionCriteria commisionCriteria = new CommisionCriteria();
            if (CommisionName != "" && CommisionName != null)
            {
                commisionCriteria = CommisionCriteriaRepository.Table.Where(x => x.CommisionName == CommisionName).SingleOrDefault();
            }
            return commisionCriteria;
        }

        public CommisionCriteria GetCommisionCriteriaId(int CommisionCriteriaId)
        {
            CommisionCriteria commisionCriteria = new CommisionCriteria();
            if (CommisionCriteriaId != 0)
            {
                commisionCriteria = CommisionCriteriaRepository.Table.Where(x => x.CommisionCriteriaId == CommisionCriteriaId).SingleOrDefault();
            }
            return commisionCriteria;
        }
        public CommisionCriteria Get(int id)
        {
            return null;
        }

        public List<CommisionCriteria> Get()
        {
            List<CommisionCriteria> commisionCriteria = new List<CommisionCriteria>();
            try
            {
                commisionCriteria = CommisionCriteriaRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<CommisionCriteria>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return commisionCriteria;
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
