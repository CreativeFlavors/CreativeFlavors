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
   public class SizeScheduledDetailManager:ISizeScheduledDetailService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<SizeScheduleDetails> SizeScheduleDetailsRepository;

        #region Add/Update/Delete Operation

         public bool Post(SizeScheduleDetails arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                SizeScheduleDetailsRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(SizeScheduleDetails arg)
        {
            bool result = false;
            try
            {
                SizeScheduleDetails model = SizeScheduleDetailsRepository.Table.Where(p => p.SizeScheduleDetailsId == arg.SizeScheduleDetailsId).FirstOrDefault();
                if (model != null)
                {
                    model.SizeScheduleDetailsId = arg.SizeScheduleDetailsId;
                    model.Size = arg.Size;
                    model.SizeNo = arg.SizeNo;
                    model.SizeScheduleMasterId = arg.SizeScheduleMasterId;                   
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();       
                    model.UpdatedBy = username;
                    SizeScheduleDetailsRepository.Update(model);
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
                SizeScheduleDetails model = SizeScheduleDetailsRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                SizeScheduleDetailsRepository.Update(model);
                //SizeScheduleDetailsRepository.Delete(model);
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

        public SizeScheduledDetailManager()
        {
            SizeScheduleDetailsRepository = unitOfWork.Repository<SizeScheduleDetails>();
        }

        //public SizeScheduleDetails GetCommisionName(string CommisionName)
        //{
        //    SizeScheduleDetails commisionCriteria = new SizeScheduleDetails();
        //    if (CommisionName != "" && CommisionName != null)
        //    {
        //        commisionCriteria = CommisionCriteriaRepository.Table.Where(x => x.CommisionName == CommisionName).SingleOrDefault();
        //    }
        //    return commisionCriteria;
        //}

        public SizeScheduleDetails GetSizeScheduleDetailsId(int SizeScheduleDetailsId)
        {
            SizeScheduleDetails sizeScheduleDetails = new SizeScheduleDetails();
            if (SizeScheduleDetailsId != 0)
            {
                sizeScheduleDetails = SizeScheduleDetailsRepository.Table.Where(x => x.SizeScheduleDetailsId == SizeScheduleDetailsId).SingleOrDefault();
            }
            return sizeScheduleDetails;
        }
        public SizeScheduleDetails Get(int id)
        {
            return null;
        }

        public List<SizeScheduleDetails> Get()
        {
            List<SizeScheduleDetails> sizeScheduleDetailslist = new List<SizeScheduleDetails>();
            try
            {
                sizeScheduleDetailslist = SizeScheduleDetailsRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<SizeScheduleDetails>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return sizeScheduleDetailslist;
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
