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
    public class SeasonManager : ISeasonService,IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<SeasonMaster> seasonMasterRepository;

        
        #region Add/Update/Delete Operation

        public SeasonMaster Post(SeasonMaster arg)
        {
            bool result = false;
            SeasonMaster seasonMaster = new SeasonMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
               // arg.UpdatedBy = username;

                seasonMasterRepository.Insert(arg);
                seasonMaster = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return seasonMaster;

        }
        public bool Put(SeasonMaster arg)
        {
            bool result = false;
            try
            {
                SeasonMaster model = seasonMasterRepository.Table.Where(p => p.SeasonMasterId == arg.SeasonMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.SeasonMasterId = arg.SeasonMasterId;
                    model.SeasonName = arg.SeasonName;
                    model.SpringSummerYear = arg.SpringSummerYear;
                    model.SpringDescription = arg.SpringDescription;
                    model.PeriodFrom = arg.PeriodFrom;
                    model.PeriodTo = arg.PeriodTo;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                   // model.CreatedBy= arg.CreatedBy;                     
                    //model.CreatedDate =  DateTime.Now;
                    model.UpdatedDate = DateTime.Now;
                    seasonMasterRepository.Update(model);
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
                SeasonMaster model = seasonMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                seasonMasterRepository.Update(model);
                // seasonMasterRepository.Delete(model);
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
        public SeasonManager()
        {
            seasonMasterRepository = unitOfWork.Repository<SeasonMaster>();
        }
        public SeasonMaster GetSeasonFullName(string SeasonFullName)
        {   
            SeasonMaster seasonMaster = new SeasonMaster();
            if (SeasonFullName != "" && SeasonFullName != null)
            {
                seasonMaster = seasonMasterRepository.Table.Where(x => x.SeasonName == SeasonFullName.ToLower().Trim()).SingleOrDefault();
            }
            return seasonMaster;
        }
        public SeasonMaster GetSeasonMasterId(int SeasonMasterId)
        {
            SeasonMaster seasonMaster = new SeasonMaster();
            if (SeasonMasterId != 0)
            {
                seasonMaster = seasonMasterRepository.Table.Where(x => x.SeasonMasterId == SeasonMasterId).SingleOrDefault();
            }
            return seasonMaster;
        }
        public SeasonMaster Get(int id)
        {
            return null;
        }
        public List<SeasonMaster> Get()
        {
            List<SeasonMaster> seasonMaster = new List<SeasonMaster>();

            try
            {
                seasonMaster = seasonMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<SeasonMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return seasonMaster;
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
