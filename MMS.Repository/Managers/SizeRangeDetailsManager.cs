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
   public class SizeRangeDetailsManager: ISizeRangeDetailService, IDisposable
    {
       private UnitOfWork unitOfWork = new UnitOfWork();
       private Repository<SizeRangeDetails> SizeRangeDetailsRepository;

        #region Add/Update/Delete Operation

        public bool Post(SizeRangeDetails arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;          
                SizeRangeDetailsRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(SizeRangeDetails arg)
        {
            bool result = false;
            try
            {
                SizeRangeDetails model = SizeRangeDetailsRepository.Table.Where(p => p.SizeRangeDetailsId == arg.SizeRangeDetailsId).FirstOrDefault();
                if (model != null)
                {
                    model.SizeRangeDetailsId = arg.SizeRangeDetailsId;                
                    model.SizeNo = arg.SizeNo;
                    model.SizeRangeName = arg.SizeRangeName;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;               
                    string username = HttpContext.Current.Session["UserName"].ToString();                   
                    model.UpdatedBy = username;
                    SizeRangeDetailsRepository.Update(model);
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
                SizeRangeDetails model = SizeRangeDetailsRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                SizeRangeDetailsRepository.Update(model);
                // SizeRangeDetailsRepository.Delete(model);
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

        public SizeRangeDetailsManager()
        {
            SizeRangeDetailsRepository = unitOfWork.Repository<SizeRangeDetails>();
        }

        //public SizeRangeDetails GetSize(string SizeRangeRef)
        //{
        //    SizeRangeDetails sizeRangeDetails = new SizeRangeDetails();
        //    if (SizeRangeRef != "" && SizeRangeRef != null)
        //    {
        //        sizeRangeDetails = SizeRangeDetailsRepository.Table.Where(x => x.Size == Size).SingleOrDefault();
        //    }
        //    return sizeRangeDetails;
        //}

        public List<SizeRangeDetails> GetSizeRangeMasterId(int SizeRangeMasterId)
        {
            List<SizeRangeDetails> sizeRangeDetails = new List<SizeRangeDetails>();
            if (SizeRangeMasterId != 0)
            {
                sizeRangeDetails = SizeRangeDetailsRepository.Table.Where(x => x.SizeRangeDetailsId == SizeRangeMasterId).ToList();
            }
            return sizeRangeDetails;
        }
       

        public SizeRangeDetails GetSizeRangeDetailsId(int? SizeRangeDetailsId)
        {
            SizeRangeDetails sizeRangeDetails = new SizeRangeDetails();
            if (SizeRangeDetailsId != 0)
            {
                sizeRangeDetails = SizeRangeDetailsRepository.Table.Where(x => x.SizeRangeDetailsId == SizeRangeDetailsId).SingleOrDefault();
            }
            return sizeRangeDetails;
        }
        public SizeRangeDetails GetSizeRangeName(string SizeRange)
        {
            SizeRangeDetails sizeRangeDetails = new SizeRangeDetails();
            if (SizeRange != "")
            {
                sizeRangeDetails = SizeRangeDetailsRepository.Table.Where(x => x.SizeRangeName == SizeRange.ToLower().Trim()).FirstOrDefault();
            }
            return sizeRangeDetails;
        }
        public SizeRangeDetails Get(int id)
        {
            return null;
        }

        public List<SizeRangeDetails> Get()
        {
            List<SizeRangeDetails> sizeRangeMasterlist = new List<SizeRangeDetails>();
            try
            {
                sizeRangeMasterlist = SizeRangeDetailsRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<SizeRangeDetails>();
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
