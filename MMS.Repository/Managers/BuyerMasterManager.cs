using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Repository.Managers
{
    public class BuyerMasterManager : IBuyerMaster, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<BuyerMaster1> _buyerMasterRepository;

        public BuyerMasterManager()
        {
            _buyerMasterRepository = unitOfWork.Repository<BuyerMaster1>();
        }

        //insert the object 
        public bool post(BuyerModel_SP buyerMaster)
        {
            bool result = false;

            try
            {               
                if (buyerMaster != null)
                {
                    buyerMaster.CreatedBy = HttpContext.Current.Session["UserName"].ToString();
                    buyerMaster.CreatedDate = DateTime.Now;
                    buyerMaster.DateAdded = DateTime.Now;
                    _buyerMasterRepository.InsertBuyerModel(buyerMaster);
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

        //get list of employee by using sp
        public List<BuyerModel_SP> GetBuyerModels()
        {
            List<BuyerModel_SP> buyerMaster1 = new List<BuyerModel_SP>(); 
            try
            {
                buyerMaster1 = _buyerMasterRepository.GetBuyerModels();

            }
            catch (Exception)
            {

                throw;
            }
            return buyerMaster1;
        }


        //get sinle employee by using sp

        public BuyerModel_SP GetSingleBuyerModel(int? id) 
        {
            List<BuyerModel_SP> buyerMaster1 = new List<BuyerModel_SP>();
            try
            {
                buyerMaster1 = _buyerMasterRepository.GetBuyerModels();
                var  singlebuyermaster = buyerMaster1.Where(b=>b.BuyerMasterId == id).FirstOrDefault();
                return singlebuyermaster;

            }
            catch (Exception)
            {

                throw;
            }          

        }   


        //update buyer using sp
        public bool Putbuyer(BuyerModel_SP buyermaster)
        {
            BuyerMaster1 model = _buyerMasterRepository.Table.Where(p => p.BuyerMasterId == buyermaster.BuyerMasterId).FirstOrDefault();

            bool result = false;
            try
            {
                model.BuyerMasterId = buyermaster.BuyerMasterId;
                model.CustomerName = buyermaster.CustomerName;
                model.Account = buyermaster.Account;
                model.AccountName = buyermaster.AccountName;
                model.AccountDescription = buyermaster.AccountDescription;
                model.SwiftCode = buyermaster.SwiftCode;
                model.Physical1 = buyermaster.Physical1;

                model.PhysicalCode = buyermaster.PhysicalCode;

                model.CurrencyId = buyermaster.CurrencyId;
                model.Telephone1 = buyermaster.Telephone1;
                model.Telephone2 = buyermaster.Telephone2;
                model.EmailContact = buyermaster.EmailContact;
                model.EmailAccounts = buyermaster.EmailAccounts;
                model.EmailEmergency = buyermaster.EmailEmergency;
                model.AccountTypeId = buyermaster.AccountTypeId;
                model.VatNumber = buyermaster.VatNumber;
                model.RegNumber = buyermaster.RegNumber;
                model.CreditLimit = buyermaster.CreditLimit;
                model.ChargeInterest = true;
                model.Interest = buyermaster.Interest;
                model.TaxTypeId = buyermaster.TaxTypeId;
                model.ForeignCurrency = buyermaster.ForeignCurrency;
                model.OnHold = true;
                model.Active = true;
                model.BuyerCode = buyermaster.BuyerCode;
                model.BuyerShortName = buyermaster.BuyerShortName;
                model.DcBalance = buyermaster.DcBalance;
                model.Website = buyermaster.Website;
                model.UpdatedBy = HttpContext.Current.Session["UserName"].ToString();
                model.UpdatedDate = DateTime.Now;
                var updateresult = _buyerMasterRepository.Updatebuyer(model);
                result = true;
                return result;  
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }


        //deleter the buyer use sp

        public bool Deletebuyer(int id)
        {
            bool result = false;

            try
            {
                BuyerModel_SP buyerMaster1 = new BuyerModel_SP();
                 buyerMaster1 = _buyerMasterRepository.GetBuyerById(id);
                if (buyerMaster1 != null)
                {
                    buyerMaster1.IsDeleted = true;
                    buyerMaster1.Active = false;
                    buyerMaster1.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                    buyerMaster1.DeletedDate = DateTime.Now;
                    _buyerMasterRepository.deletebuyer(buyerMaster1);
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

        // using entity

        //get the list of all employees
        public List<BuyerMaster1> Get()
        {
            List<BuyerMaster1> buyerMastersList = new List<BuyerMaster1>();
            try
            {
                buyerMastersList = _buyerMasterRepository.Table.Where(x => x.Active).ToList();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), _buyerMasterRepository.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return buyerMastersList;
        }
        public BuyerMaster1 GetBuyerMasterId(int BuyerMasterId)
        {
            BuyerMaster1 buyerMaster = new BuyerMaster1();
            if (BuyerMasterId != 0)
            {
                buyerMaster = _buyerMasterRepository.Table.Where(x => x.BuyerMasterId == BuyerMasterId).FirstOrDefault();
            }
            return buyerMaster;
        }

        //get the employee by id
        public BuyerMaster1 GetBuyerId(int id)
        {
            try
            {
                BuyerMaster1 buyerdetails = new BuyerMaster1();
                if (id != 0)
                {
                    buyerdetails = _buyerMasterRepository.Table.Where(x => x.BuyerMasterId == id).FirstOrDefault();
                }
                return buyerdetails;

            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }


        //update BUyer
        public bool Put(BuyerMaster1 arg)
        {
            bool result = false;
            try
            {
                BuyerMaster1 buyerMaster1 = _buyerMasterRepository.Table.Where(x=>x.BuyerMasterId== arg.BuyerMasterId).FirstOrDefault();




                if (buyerMaster1 != null)
                {

                    buyerMaster1.BuyerMasterId = arg.BuyerMasterId;
                    buyerMaster1.BuyerCode = arg.BuyerCode;
                    buyerMaster1.BuyerShortName = arg.BuyerShortName;
                    buyerMaster1.CustomerName = arg.CustomerName;
                    buyerMaster1.Account = arg.Account;
                    buyerMaster1.AccountName = arg.AccountName;
                    buyerMaster1.AccountDescription = arg.AccountDescription;
                    buyerMaster1.SwiftCode = arg.SwiftCode;
                    buyerMaster1.Physical1 = arg.Physical1;
                  
                    buyerMaster1.PhysicalCode = arg.PhysicalCode;
                 
                    buyerMaster1.CurrencyId = arg.CurrencyId;
                    buyerMaster1.Telephone1 = arg.Telephone1;
                    buyerMaster1.Telephone2 = arg.Telephone2;
                    buyerMaster1.EmailContact = arg.EmailContact;
                    buyerMaster1.EmailAccounts = arg.EmailAccounts;
                    buyerMaster1.EmailEmergency = arg.EmailEmergency;
                    buyerMaster1.AccountTypeId = arg.AccountTypeId;
                    buyerMaster1.VatNumber = arg.VatNumber;
                    buyerMaster1.RegNumber = arg.RegNumber;
                    buyerMaster1.CreditLimit = arg.CreditLimit;
                    buyerMaster1.ChargeInterest = arg.ChargeInterest;
                    buyerMaster1.Interest = arg.Interest;
                    buyerMaster1.DateAdded = arg.DateAdded;
                    buyerMaster1.TaxTypeId = arg.TaxTypeId;
                    buyerMaster1.ForeignCurrency = arg.ForeignCurrency;
                    buyerMaster1.UpdatedDate = DateTime.Now;
                    buyerMaster1.UpdatedBy = "admin";
                    _buyerMasterRepository.Update(buyerMaster1);
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

        //delete the buyer 
        public bool Delete(int id)
        {
            bool  result = false;

            try
            {
                BuyerMaster1 buyerMaster1 = _buyerMasterRepository.GetById(id);
                if (buyerMaster1 != null)
                {
                    buyerMaster1.IsDeleted = true;
                    buyerMaster1.Active = false;
                    buyerMaster1.DeletedBy = "Deletedby";
                    buyerMaster1.DeletedDate = DateTime.Now;
                    _buyerMasterRepository.Update(buyerMaster1);
                    result= true;
                }
            }
            catch (Exception ex)
            {

                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
           
        }
        
        //dispose 
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }








    }



}
