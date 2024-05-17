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
    public class SizeRangeMasterManager 
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<SizeRangeMaster> SizeRangeMasterRepository;

        #region Add/Update/Delete Operation

        public SizeRangeMaster SizeRangeMaster(SizeRangeMaster arg)
        {
            SizeRangeMaster sizeRangeMaster = new SizeRangeMaster();
            //bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
              // arg.UpdatedBy = username;
                SizeRangeMasterRepository.Insert(arg);
                sizeRangeMaster = arg;
               // result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
              //  result = false;
            }
            return sizeRangeMaster;

        }
        public bool Put(SizeRangeMaster arg)
        {
            bool result = false;
            try
            {
                SizeRangeMaster model = SizeRangeMasterRepository.Table.Where(p => p.SizeRangeMasterId == arg.SizeRangeMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.SizeRangeMasterId = arg.SizeRangeMasterId;
                    model.SizeRangeRef = arg.SizeRangeRef;
                    model.SizeRangeRefValue = arg.SizeRangeRefValue;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedBy = string.Empty;
                    string username = HttpContext.Current.Session["UserName"].ToString();         
                    model.UpdatedBy = username;              
                    SizeRangeMasterRepository.Update(model);
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
                SizeRangeMaster model = SizeRangeMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                SizeRangeMasterRepository.Update(model);
                // SizeRangeMasterRepository.Delete(model);
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

        public SizeRangeMasterManager()
        {
            SizeRangeMasterRepository = unitOfWork.Repository<SizeRangeMaster>();
        }

        public SizeRangeMaster GetSizeRangeRef(string SizeRangeRef)
        {
          SizeRangeMaster sizeRangeMaster = new SizeRangeMaster();
            if (SizeRangeRef != "" && SizeRangeRef != null)
            {
                sizeRangeMaster = SizeRangeMasterRepository.Table.Where(x => x.SizeRangeRef == SizeRangeRef).FirstOrDefault();
            }
            return sizeRangeMaster;
        }
        public List<SizeRangeMaster> GetSizeRangeMasterList(int SizeRangeRef)
        {
            List<SizeRangeMaster> sizeRangeMasterlist = new List<SizeRangeMaster>();
            string size = SizeRangeRef.ToString();
            if (size != "" && size != null)
            {
                sizeRangeMasterlist = SizeRangeMasterRepository.Table.Where(x => x.SizeRangeRef == size&&x.IsDeleted==false).ToList().OrderBy(x => x.SizeRangeRefValue).ToList();
            }
            return sizeRangeMasterlist;
        }

        public SizeRangeMaster GetSizeRangeMasterId(int SizeRangeMasterId)
        {
            SizeRangeMaster sizeScheduleDetails = new SizeRangeMaster();
            if (SizeRangeMasterId != 0)
            {
                sizeScheduleDetails = SizeRangeMasterRepository.Table.Where(x => x.SizeRangeMasterId == SizeRangeMasterId).SingleOrDefault();
            }
            return sizeScheduleDetails;
        }
        public SizeRangeMaster Get(int id)
        {
            return null;
        }

        public List<SizeRangeMaster> Get()
        {
            List<SizeRangeMaster> sizeRangeMasterlist = new List<SizeRangeMaster>();
            try
            {
                sizeRangeMasterlist = SizeRangeMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<SizeRangeMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return sizeRangeMasterlist;
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
