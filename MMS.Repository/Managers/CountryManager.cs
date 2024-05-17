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
    public class CountryManager : IContryService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<CountryMaster> CountryMasterRepository;

        #region Add/Update/Delete Operation

        public bool Post(CountryMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                CountryMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public bool Put(CountryMaster arg)
        {
            bool result = false;
            try
            {
                CountryMaster model = CountryMasterRepository.Table.Where(p => p.CountryMasterId == arg.CountryMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.CountryMasterId = arg.CountryMasterId;
                    model.LongCountryName = arg.LongCountryName;
                    model.ShortCountryName = arg.ShortCountryName;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedBy = model.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();                  
                    model.UpdatedBy = username;
                    CountryMasterRepository.Update(model);
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
                CountryMaster model = CountryMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                CountryMasterRepository.Update(model);
                //  CountryMasterRepository.Delete(model);
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

        public CountryManager()
        {
            CountryMasterRepository = unitOfWork.Repository<CountryMaster>();
        }
        public CountryMaster GetLongCountryName(string LongCountryName)
        {
            CountryMaster countryMaster = new CountryMaster();
            if (LongCountryName != "" && LongCountryName != null)
            {
                countryMaster = CountryMasterRepository.Table.Where(x => x.LongCountryName == LongCountryName).SingleOrDefault();
            }
            return countryMaster;
        }
        public CountryMaster GetContainCountryName(string LongCountryName)
        {
            CountryMaster countryMaster = new CountryMaster();
            if (LongCountryName != "" && LongCountryName != null)
            {
                countryMaster = CountryMasterRepository.Table.Where(x => x.LongCountryName.ToLower().Contains(LongCountryName.ToLower())).FirstOrDefault();
            }
            return countryMaster;
        }
        public CountryMaster GetCountryMasterId(int CountryMasterId)
        {
            CountryMaster countryMaster = new CountryMaster();
            if (CountryMasterId != 0)
            {
                countryMaster = CountryMasterRepository.Table.Where(x => x.CountryMasterId == CountryMasterId).SingleOrDefault();
            }
            return countryMaster;
        }
        public CountryMaster Get(int id)
        {
            return null;
        }
        public List<CountryMaster> Get()
        {
            List<CountryMaster> countryList = new List<CountryMaster>();
            try
            {
                countryList = CountryMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<CountryMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return countryList;
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
