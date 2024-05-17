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
    public class CurrencyManager : ICurrencyService, IDisposable
    {

            private UnitOfWork unitOfWork = new UnitOfWork();
            private Repository<CurrencyMaster> CurrencyRepository;

            public CurrencyManager()
            {
                CurrencyRepository = unitOfWork.Repository<CurrencyMaster>();
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
