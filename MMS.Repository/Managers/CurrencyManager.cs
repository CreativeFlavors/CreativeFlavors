using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Data.Mapping;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace MMS.Repository.Managers
{
    public class CurrencyManager : ICurrencyService, IDisposable
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<CurrencyMaster> CurrencyRepository;
        private Repository<currency> CurrencyRepository1;
        private Repository<CurrencyConversion> CurrencycunversionRepository1;

        public CurrencyManager()
        {
            CurrencyRepository = unitOfWork.Repository<CurrencyMaster>();
            CurrencyRepository1 = unitOfWork.Repository<currency>();
            CurrencycunversionRepository1 = unitOfWork.Repository<CurrencyConversion>();


        }
        public List<currency> Getcurrency()
        {
            List<currency> currency = new List<currency>();
            try
            {
                currency = CurrencyRepository1.Table.ToList<currency>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return currency;
        }
        public CurrencyConversion Getcurrencyid(int currencyid)
        {
            CurrencyConversion currencyconversiongrid = new CurrencyConversion();
            if (currencyid != 0)
            {
                try
                {
                    currencyconversiongrid = CurrencycunversionRepository1.Table.Where(x => x.Id == currencyid).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return currencyconversiongrid;
        }
        public CurrencyConversion POST(CurrencyConversion arg)
        {
            CurrencyConversion CurrencyConversion = new CurrencyConversion();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.IsActive = true;
                CurrencycunversionRepository1.Insert(arg);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return CurrencyConversion;
        }
        public bool Put(CurrencyConversion arg)
        {
            bool result = false;
            CurrencyConversion model = CurrencycunversionRepository1.Table.Where(p => p.Id == arg.Id).FirstOrDefault();
            if (model != null)
            {
                model.FromDate = arg.FromDate;
                model.ToDate = arg.ToDate;
                model.ConversionValue = arg.ConversionValue;
                model.PrimaryCurrency = arg.PrimaryCurrency;
                model.SecondaryCurrency = arg.SecondaryCurrency;
                model.UpdatedDate = DateTime.Now;
                string username = HttpContext.Current.Session["UserName"].ToString();
                model.UpdatedBy = username;
                CurrencycunversionRepository1.Update(model);
                result = true;
            }
            else
            {
                return false;
            }
            return result;
        }
        public bool currencystatuschange(int CurrencyId, bool IsChecked)
        {
            bool result = false;
            try
            {
                CurrencyConversion model = CurrencycunversionRepository1.GetById(CurrencyId);

                model.UpdatedDate = DateTime.Now;
                string username = HttpContext.Current.Session["UserName"].ToString();
                model.UpdatedBy = username;
                model.IsActive = IsChecked;
                CurrencycunversionRepository1.Update(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public List<currencyconversiongrid> GetccList()
        {
            List<currencyconversiongrid> CurrencyConversion = new List<currencyconversiongrid>();
            try
            {
                CurrencyConversion = CurrencycunversionRepository1.Get_currencyconversiongrid();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return CurrencyConversion;
        }
        public List<CurrencyConversion> GetCurrencycunversion()
        {
            List<CurrencyConversion> currency = new List<CurrencyConversion>();
            try
            {
                currency = CurrencycunversionRepository1.Table.ToList<CurrencyConversion>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return currency;
        }
        public bool Post(CurrencyMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                //arg.UpdatedBy = username;
                CurrencyRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }

        public CurrencyMaster Get(int id)
        {
            return null;
        }

        public CurrencyMaster GetCurrentMasterId(int? CurrencyMasterId)
        {
            CurrencyMaster currencyMaster = new CurrencyMaster();
            if (CurrencyMasterId != 0)
            {
                currencyMaster = CurrencyRepository.Table.Where(x => x.CurrencyMasterId == CurrencyMasterId).SingleOrDefault();
            }
            return currencyMaster;
        }
        public CurrencyMaster GetCurrencyFullName(string LongForm)
        {
            CurrencyMaster currencyMaster = new CurrencyMaster();
            if (LongForm != "" && LongForm != null)
            {
                currencyMaster = CurrencyRepository.Table.Where(x => x.LongForm == LongForm).SingleOrDefault();
            }
            return currencyMaster;
        }
        public CurrencyMaster GetContainCurrencyFullName(string LongForm)
        {
            CurrencyMaster currencyMaster = new CurrencyMaster();
            if (LongForm != "" && LongForm != null)
            {
                currencyMaster = CurrencyRepository.Table.Where(x => x.ShortForm.ToLower().Contains(LongForm.ToLower())).FirstOrDefault();
            }
            return currencyMaster;
        }
        //public currency GetContainCurrencyid(int currencycode)
        //{
        //    currency currencyMaster = new currency();
        //    if (currencycode != 0 )
        //    {
        //        currencyMaster = CurrencyRepository1.Table.Where(x => x.currencycode =currencycode).FirstOrDefault();
        //    }
        //    return currencyMaster;
        //}
        public CurrencyConversion GetConversionValueid(int? currencycode)
        {
            CurrencyConversion currencyMaster = new CurrencyConversion();
            if (currencycode != 0)
            {
                currencyMaster = CurrencycunversionRepository1.Table.Where(x => x.Id == currencycode).FirstOrDefault();
            }
            return currencyMaster;
        }
        public bool Put(CurrencyMaster arg)
        {
            bool result = false;
            try
            {

                CurrencyMaster model = CurrencyRepository.Table.Where(p => p.CurrencyMasterId == arg.CurrencyMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.Symbol = arg.Symbol;
                    model.ShortForm = arg.ShortForm;
                    model.LongForm = arg.LongForm;
                    model.CurrencyMasterId = arg.CurrencyMasterId;
                    model.LowerDenomination = arg.LowerDenomination;
                    //model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    CurrencyRepository.Update(model);
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
                CurrencyMaster model = CurrencyRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                CurrencyRepository.Update(model);
                //CurrencyRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }



        public List<CurrencyMaster> Get()
        {
            List<CurrencyMaster> currencyMaster = new List<CurrencyMaster>();

            try
            {
                currencyMaster = CurrencyRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<CurrencyMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return currencyMaster;
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
