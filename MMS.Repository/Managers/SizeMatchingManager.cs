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
    public class SizeMatchingManager 
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<SizeMatching> sizeMatchingRepository;

        public SizeMatchingManager()
        {
            sizeMatchingRepository = unitOfWork.Repository<SizeMatching>();
        }
        public SizeMatching Post(SizeMatching arg)
        {
            SizeMatching sizeMatching = new SizeMatching();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                sizeMatchingRepository.Insert(arg);
                sizeMatching = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
               
            }
            return sizeMatching;
        }
        public SizeMatching GetSizeMatchingMasterID(int? SizeMatchingMasterID)
        {
            SizeMatching sizeMatching = new SizeMatching();
            sizeMatching = sizeMatchingRepository.Table.Where(x => x.SizeMatchingMasterID == SizeMatchingMasterID).SingleOrDefault();
            return sizeMatching;
        }
        public SizeMatching GetSizeMatchingName(string SizeMatchingName)
        {
            SizeMatching sizeMatching = new SizeMatching();
            sizeMatching = sizeMatchingRepository.Table.Where(x => x.SizeMatchingName == SizeMatchingName).SingleOrDefault();
            return sizeMatching;
        }
        public bool Put(SizeMatching arg)
        {
            bool result = false;
            try
            {
                SizeMatching model = sizeMatchingRepository.Table.Where(p => p.SizeMatchingMasterID == arg.SizeMatchingMasterID).FirstOrDefault();
                if (model != null)
                {
                    model.SizeMatchingName = arg.SizeMatchingName;
                    model.SizeMatchingMasterID = arg.SizeMatchingMasterID;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = arg.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    // model.BuyerColorCode = arg.BuyerColorCode;
                    sizeMatchingRepository.Update(model);
                    result = true;
                }

                else
                {
                    result = false;
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
                SizeMatching model = sizeMatchingRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                sizeMatchingRepository.Update(model);
                // colorRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public SizeMatching Get(int id)
        {
            return null;
        }
        public List<SizeMatching> Get()
        {
            List<SizeMatching> sizeMatchinglist = new List<SizeMatching>();
            try
            {
                sizeMatchinglist = sizeMatchingRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<SizeMatching>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return sizeMatchinglist;
        }
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
    }
}
