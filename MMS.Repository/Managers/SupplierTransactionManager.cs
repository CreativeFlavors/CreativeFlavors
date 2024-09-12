using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Data.Mapping;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class SupplierTransactionManager : ISuppliertransactionServices, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<supplierTransaction> suppliertransactionrep;
        public SupplierTransactionManager()
        {
            suppliertransactionrep = unitOfWork.Repository<supplierTransaction>();

        }


        #region Helper Method



        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public List<supplierTransaction> Get()
        {

            List<supplierTransaction> suppliertransaction = new List<supplierTransaction>();
            try
            {
                suppliertransaction = suppliertransactionrep.Table.ToList<supplierTransaction>();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return suppliertransaction;
        
        }

        public List<supplierTransaction> GettypeId(int id)
        {
            List<supplierTransaction> suppliertransaction = new List<supplierTransaction>();
            try
            {
                if (id != 0)
                {
                    suppliertransaction = suppliertransactionrep.Table.Where(x => x.GrnRefNumber == id).ToList();
                }
                return suppliertransaction;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public supplierTransaction Get(int supplierid)
        {
            supplierTransaction supplierlist = new supplierTransaction();
            if (supplierid != 0)
            {
                try
                {
                    supplierlist = suppliertransactionrep.Table.Where(x => x.SupplierId == supplierid).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return supplierlist;
        }

        public bool Post(supplierTransaction arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                suppliertransactionrep.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Put(supplierTransaction arg)
        {
            bool result = false;
            try
            {

                supplierTransaction model = suppliertransactionrep.Table.Where(p => p.Id == arg.Id).FirstOrDefault();
                if (model != null)
                {
                    model.GrnBalanceAmount = arg.GrnBalanceAmount;
                    var data = model.GrnPaidAmount;
                    data += arg.GrnPaidAmount;
                    model.GrnPaidAmount = data;

                    model.Cash = arg.Cash;
                    model.CreditNoteRef = arg.CreditNoteRef;
                    model.CreditNoteDate = arg.CreditNoteDate;
                    model.CreditNoteValue = arg.CreditNoteValue;
                    model.PaymentAmount = arg.PaymentAmount;
                    model.PaymentRefNo = arg.PaymentRefNo;

                    model.UpdatedDate = DateTime.Now;
                    //string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = HttpContext.Current.Session["UserName"].ToString();
                    //model.CreatedBy = username;
                    suppliertransactionrep.Update(model);
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


        #endregion
    }
}
