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
    public class CustAddressMangers :  ICusAddreess 
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<CustAddress> CustAddressRepository;
        private Repository<AddressType> AddressTypeRepository;

        #region Add/Update/Delete Operation
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                CustAddress model = CustAddressRepository.GetById(id);
                model.Active = false;
                model.UpdatedBy = "admin";//HttpContext.Current.Session["UserName"].ToString();
                model.UpdatedDate = DateTime.Now;
                CustAddressRepository.Update(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public bool Post(CustAddress arg)
        {
            bool result = false;
            try
            {
                string username ="admin";
                arg.CreatedBy = username;
                //arg.UpdatedBy = username;
                arg.CreatedDate = DateTime.Now;
                CustAddressRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public bool Put(CustAddress arg)
        {
            bool result = false;
            try
            {
                CustAddress model = CustAddressRepository.Table.Where(p => p.AddressId == arg.AddressId).FirstOrDefault();
                if (model != null)
                {
                    model.AddressId = arg.AddressId;
                    model.BuyerId = arg.BuyerId;
                    model.AddressType  = arg.AddressType;
                    model.Add1 = arg.Add1;
                    model.Add2 = arg.Add2;
                    model.Add3 = arg.Add3;
                    model.IsDefault = arg.IsDefault;
                    model.City = arg.City;
                    model.CityId = arg.CityId;
                    model.State = arg.State;
                    model.StateId = arg.StateId;
                    model.Country = arg.Country;
                    model.CountryId = arg.CountryId;
                    model.ZipCode = arg.ZipCode;
                    model.ContactName = arg.ContactName;
                    model.Email = arg.Email;
                    model.Phone = arg.Phone;
                    model.Notes = arg.Notes;
                    model.Active = arg.Active;
                    model.UpdatedDate = DateTime.Now;
                    string username = "admin";

                    model.UpdatedBy = username;
                    CustAddressRepository.Update(model);
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

        #region Helper Method
        public CustAddressMangers()
        {
            CustAddressRepository = unitOfWork.Repository<CustAddress>();
            AddressTypeRepository = unitOfWork.Repository<AddressType>();
        }
        public CustAddress Get(int id)
        {
            return null;
        }
        public List<CustAddress> Get()
        {
            List<CustAddress> CustAddresslist = new List<CustAddress>();
            try
            {
                CustAddresslist = CustAddressRepository.Table.Where(x => x.Active == true).ToList<CustAddress>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return CustAddresslist;
        }
        public List<AddressType> GetAddtype()
        {
            List<AddressType> AddressTypelist = new List<AddressType>();
            try
            {
                AddressTypelist = AddressTypeRepository.Table.Where(x => x.IsActive == false || x.IsActive ? true : false).ToList<AddressType>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return AddressTypelist;
        }
        public CustAddress GetCustAddressId(int AddressId)
        {
            CustAddress oCustAddress = new CustAddress();
            if (AddressId != 0)
            {
                oCustAddress = CustAddressRepository.Table.Where(x => x.AddressId == AddressId).SingleOrDefault();
            }
            return oCustAddress;
        }
        public CustAddress GetCustAddressbuyerid(int buyerid)
        {
            CustAddress oCustAddress = new CustAddress();
            if (buyerid != 0)
            {
                oCustAddress = CustAddressRepository.Table.Where(x => x.BuyerId == buyerid && x.Active == true).SingleOrDefault();
            }
            return oCustAddress;
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
