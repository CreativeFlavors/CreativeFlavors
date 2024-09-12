using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class Supplier_masterManager : ISupply_MasterServices, IDisposable
    {
        private UnitOfWork unitofWork = new UnitOfWork();
        private Repository<Supplier_master> _supplyMasterRepository;

        public Supplier_masterManager()
        {
            _supplyMasterRepository = unitofWork.Repository<Supplier_master>();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                Supplier_master model = _supplyMasterRepository.GetById(id);
                model.isActive = false;
                model.deletedby = HttpContext.Current.Session["UserName"].ToString();
                model.deleteddate = DateTime.Now;
                model.isdeleted = false;
                _supplyMasterRepository.Update(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public bool Post(Supplier_master supplierMaster1)
        {
            try
            {

                supplierMaster1.CreatedDate = DateTime.Now;
                supplierMaster1.OnHold = true;
                supplierMaster1.isActive = true;
                supplierMaster1.createdby = HttpContext.Current.Session["UserName"].ToString();
                var postResult = _supplyMasterRepository.InsertSupplier(supplierMaster1);
                return postResult;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public List<Supplier_master> Get()
        {
            List<Supplier_master> Supplier_master = new List<Supplier_master>();
            try
            {
                Supplier_master = _supplyMasterRepository.Table.Where(x => x.isActive == true).ToList<Supplier_master>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Supplier_master;
        }
        public Supplier_master getsupplierId(int spId)
        {
            Supplier_master oCustAddress = new Supplier_master();
            if (spId != 0)
            {
                oCustAddress = _supplyMasterRepository.Table.Where(x => x.SupplierId == spId).FirstOrDefault();
            }
            return oCustAddress;
        }
        public bool Put(Supplier_master arg)
        {
            bool result = false;
            try
            {
                Supplier_master model = _supplyMasterRepository.Table.Where(p => p.SupplierId == arg.SupplierId).FirstOrDefault();
                if (model != null)
                {
                    model.Suppliername = arg.Suppliername;
                    model.suppliercode = arg.suppliercode;
                    model.Account = arg.Account;
                    model.AccountName = arg.AccountName;
                    model.AccountDescription = arg.AccountDescription;
                    model.Physical1 = arg.Physical1;
                    model.PhysicalCode = arg.PhysicalCode;
                    model.Telephone1 = arg.Telephone1;
                    model.Telephone2 = arg.Telephone2;
                    model.EmailContact = arg.EmailContact;
                    model.EmailAccounts = arg.EmailAccounts;
                    model.EmailEmergency = arg.EmailEmergency;
                    model.AccountTypeId = arg.AccountTypeId;
                    model.VatNumber = arg.VatNumber;
                    model.RegNumber = arg.RegNumber;
                    model.CreditLimit = arg.CreditLimit;
                    model.DcBalance = arg.DcBalance;
                    model.Interest = arg.Interest;
                    model.TaxTypeId = arg.TaxTypeId;
                    model.ForeignCurrency = arg.ForeignCurrency;
                    model.CurrencyID = arg.CurrencyID;
                    model.isActive = true;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.updatedby = username;
                    _supplyMasterRepository.Update(model);
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

    }

}
