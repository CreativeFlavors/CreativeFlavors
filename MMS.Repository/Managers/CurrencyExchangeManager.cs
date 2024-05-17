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
    public class CurrencyExchangeManager 
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<CurrencyExchangeMaster> currencyRepository;

        public bool Post(CurrencyExchangeMaster arg)
        {
            bool result = false;
            try
            {
                if (arg.CurrencyExchangeMasterId == 0)
                {

                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    currencyRepository.Insert(arg);
                    result = true;
                }
                else
                {
                    CurrencyExchangeMaster model = currencyRepository.Table.Where(m => m.CurrencyExchangeMasterId == arg.CurrencyExchangeMasterId).FirstOrDefault();
                    model.CurrencyExchangeMasterId = arg.CurrencyExchangeMasterId;
                    model.Currency = arg.Currency;
                    model.ValueInRupees = arg.ValueInRupees;
                    model.Date = arg.Date;
                    model.UpdatedDate = DateTime.Now;
                    model.UpdatedBy = model.UpdatedBy;
                    currencyRepository.Update(model);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public List<CurrencyExchangeMaster> Get()
        {
            List<CurrencyExchangeMaster> CurrencyList = new List<CurrencyExchangeMaster>();
            try
            {
                CurrencyList = currencyRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<CurrencyExchangeMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return CurrencyList;
        }


        public CurrencyExchangeManager()
        {
            currencyRepository = unitOfWork.Repository<CurrencyExchangeMaster>();
        }

        public CurrencyExchangeMaster GetCurrencyExchangeMasterId(int CurrencyExchangeMasterId)
        {
            CurrencyExchangeMaster CurrencyExcMaster = new CurrencyExchangeMaster();
            if (CurrencyExchangeMasterId != 0)
            {
                CurrencyExcMaster = currencyRepository.Table.Where(x => x.CurrencyExchangeMasterId == CurrencyExchangeMasterId).SingleOrDefault();
            }
            return CurrencyExcMaster;
        }

        public CurrencyExchangeMaster GetCurrencyMasterId(int CurrencyExchangeMasterId)
        {
            CurrencyExchangeMaster CurrencyExcMaster = new CurrencyExchangeMaster();
            if (CurrencyExchangeMasterId != 0)
            {
                CurrencyExcMaster = currencyRepository.Table.Where(x => x.CurrencyExchangeMasterId == CurrencyExchangeMasterId).SingleOrDefault();
            }
            return CurrencyExcMaster;
        }

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                CurrencyExchangeMaster model = currencyRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                currencyRepository.Update(model);
                //  currencyRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
    }
}
