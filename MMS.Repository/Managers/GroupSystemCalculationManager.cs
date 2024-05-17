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
    public class GroupSystemCalculationManager : IGroupSystemCalculationService,IDisposable
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
         private Repository<GroupSystemCalculation> GroupSystemCalculationRepository;

        #region Add/Update/Delete Operation

         public bool Post(GroupSystemCalculation arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                GroupSystemCalculationRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(GroupSystemCalculation arg)
        {
            bool result = false;
            try
            {
                GroupSystemCalculation model = GroupSystemCalculationRepository.Table.Where(p => p.GroupSystemCalculationId == arg.GroupSystemCalculationId).FirstOrDefault();
                if (model != null)
                {
                    model.GroupSystemCalculationId = arg.GroupSystemCalculationId;
                    model.ProductionPercent = arg.ProductionPercent;
                    model.Amount = arg.Amount;                               
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                   // model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();                  
                    model.UpdatedBy = username;
                    GroupSystemCalculationRepository.Update(model);
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
                GroupSystemCalculation model = GroupSystemCalculationRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                GroupSystemCalculationRepository.Update(model);
                //GroupSystemCalculationRepository.Delete(model);
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

        public GroupSystemCalculationManager()
        {
            GroupSystemCalculationRepository = unitOfWork.Repository<GroupSystemCalculation>();
        }

       

        public GroupSystemCalculation GetGroupSystemCalculationId(int GroupSystemCalculationId)
        {
            GroupSystemCalculation groupSystemCalculation = new GroupSystemCalculation();
            if (GroupSystemCalculationId != 0)
            {
                groupSystemCalculation = GroupSystemCalculationRepository.Table.Where(x => x.GroupSystemCalculationId == GroupSystemCalculationId).SingleOrDefault();
            }
            return groupSystemCalculation;
        }
        public GroupSystemCalculation Get(int id)
        {
            return null;
        }

        public List<GroupSystemCalculation> Get()
        {
            List<GroupSystemCalculation> groupSystemCalculationlist = new List<GroupSystemCalculation>();
            try
            {
                groupSystemCalculationlist = GroupSystemCalculationRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<GroupSystemCalculation>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return groupSystemCalculationlist;
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
