using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
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
    public class CustAddressMangers : ICusAddreess
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<CustAddress> CustAddressRepository;
        private Repository<SupplierAddress> SupplierAddressRepository;
        private Repository<Customer_Addresses> Customer_AddressRepository;
        private Repository<AddressType> AddressTypeRepository;

        #region Add/Update/Delete Operation
        public bool Delete(int id,bool IsChecked)
        {
            bool result = false;
            try
            {
                CustAddress model = CustAddressRepository.GetById(id);
                model.isdeleted = IsChecked;
                model.UpdatedBy = HttpContext.Current.Session["UserName"].ToString();
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
        public bool Deletesupplier(int id,bool IsChecked)
        {
            bool result = false;
            try
            {
                SupplierAddress model = SupplierAddressRepository.GetById(id);
                model.isdeleted = IsChecked;
                model.UpdatedBy = HttpContext.Current.Session["UserName"].ToString();
                model.UpdatedDate = DateTime.Now;
                SupplierAddressRepository.Update(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }   
        public bool updateactive(int id, bool IsChecked)
        {
            bool result = false;
            try
            {
                if(IsChecked == false)
                {
                    IsChecked=true;
                }else if (IsChecked == true)
                {
                    IsChecked=false;
                }
                    CustAddress model = CustAddressRepository.GetById(id);
                model.isActive = IsChecked;
                model.UpdatedBy = HttpContext.Current.Session["UserName"].ToString();
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
        public bool updatesupplieractive(int id, bool IsChecked)
        {
            bool result = false;
            try
            {
                if(IsChecked == false)
                {
                    IsChecked=true;
                }else if (IsChecked == true)
                {
                    IsChecked=false;
                }
                SupplierAddress model = SupplierAddressRepository.GetById(id);
                model.isActive = IsChecked;
                model.UpdatedBy = HttpContext.Current.Session["UserName"].ToString();
                model.UpdatedDate = DateTime.Now;
                SupplierAddressRepository.Update(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public CustAddress Post(CustAddress arg)
        {

            CustAddress custAddress = new CustAddress();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.isActive = false;
                CustAddressRepository.Insert(arg);
                custAddress = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                custAddress = arg;
            }
            return custAddress;
        }          
        public SupplierAddress PostSupplier(SupplierAddress arg)
        {

            SupplierAddress SupplierAddress = new SupplierAddress();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.isActive = false;
                SupplierAddressRepository.Insert(arg);
                SupplierAddress = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                SupplierAddress = arg;
            }
            return SupplierAddress;
        }      
        public Customer_Addresses Postcust(Customer_Addresses arg)
        {

            Customer_Addresses custAddress = new Customer_Addresses();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.isactive = true;
                Customer_AddressRepository.Insert(arg);
                custAddress = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                custAddress = arg;
            }
            return custAddress;
        }
        public CustAddress Put(CustAddress arg)
        {
            CustAddress custAddress = new CustAddress();
            CustAddress model = CustAddressRepository.Table.Where(p => p.Addresshd_id == arg.Addresshd_id).FirstOrDefault();
            if (model != null)
            {
                model.Addresshd_id = arg.Addresshd_id;
                model.BuyerId = arg.BuyerId;
                model.AddressType = arg.AddressType;
                model.addressvarient = arg.addressvarient;
                model.CityId = arg.CityId;
                model.StateId = arg.StateId;
                model.CountryId = arg.CountryId;
                model.ContactName = arg.ContactName;
                model.Email = arg.Email;
                model.Phone = arg.Phone;
                model.Notes = arg.Notes;
                model.isActive = arg.isActive;
                model.UpdatedDate = DateTime.Now;
                string username = HttpContext.Current.Session["UserName"].ToString();
                model.UpdatedBy = username;
                CustAddressRepository.Update(model);
                custAddress = model;
            }
            return custAddress;

        }    
        public SupplierAddress Putsupplier(SupplierAddress arg)
        {
            SupplierAddress custAddress = new SupplierAddress();
            SupplierAddress model = SupplierAddressRepository.Table.Where(p => p.supplieradd_id == arg.supplieradd_id).FirstOrDefault();
            if (model != null)
            {
                model.supplieradd_id = arg.supplieradd_id;
                model.supplierid = arg.supplierid;
                model.AddressType = arg.AddressType;
                model.addressvarient = arg.addressvarient;
                model.CityId = arg.CityId;
                model.StateId = arg.StateId;
                model.CountryId = arg.CountryId;
                model.ContactName = arg.ContactName;
                model.Email = arg.Email;
                model.Phone = arg.Phone;
                model.Notes = arg.Notes;
                model.isActive = arg.isActive;
                model.UpdatedDate = DateTime.Now;
                string username = HttpContext.Current.Session["UserName"].ToString();
                model.UpdatedBy = username;
                SupplierAddressRepository.Update(model);
                custAddress = model;
            }
            return custAddress;

        }    
        public Customer_Addresses Putcustadd(Customer_Addresses arg)
        {
            Customer_Addresses custAddress = new Customer_Addresses();
            Customer_Addresses model = Customer_AddressRepository.Table.Where(p => p.Addresshd_id == arg.Addresshd_id && p.Buyerid == arg.Buyerid).FirstOrDefault();
            if (model != null)
            {
                model.Addresshd_id = arg.Addresshd_id;
                model.address1 = arg.address1;
                model.address2 = arg.address2;
                model.address3 = arg.address3;
                model.ZipCode = arg.ZipCode;
                model.addresstypeid = arg.addresstypeid;
                model.Buyerid = arg.Buyerid;
                model.isactive = arg.isactive;
                model.vatnumber = arg.vatnumber;
                model.UpdatedDate = DateTime.Now;
                string username = HttpContext.Current.Session["UserName"].ToString();
                model.UpdatedBy = username;
                Customer_AddressRepository.Update(model);
                custAddress = model;
            }
            return custAddress;

        }
             
        public Customer_Addresses Putsuppadd(Customer_Addresses arg)
        {
            Customer_Addresses custAddress = new Customer_Addresses();
            Customer_Addresses model = Customer_AddressRepository.Table.Where(p => p.Addresshd_id == arg.Addresshd_id && p.supplierid == arg.supplierid).FirstOrDefault();
            if (model != null)
            {
                model.Addresshd_id = arg.Addresshd_id;
                model.address1 = arg.address1;
                model.address2 = arg.address2;
                model.address3 = arg.address3;
                model.addresstypeid = arg.addresstypeid;
                model.ZipCode = arg.ZipCode;
                model.supplierid = arg.supplierid;
                model.isactive = arg.isactive;
                model.vatnumber = arg.vatnumber;
                model.UpdatedDate = DateTime.Now;
                string username = HttpContext.Current.Session["UserName"].ToString();
                model.UpdatedBy = username;
                Customer_AddressRepository.Update(model);
                custAddress = model;
            }
            return custAddress;

        }

        #endregion

        #region Helper Method
        public CustAddressMangers()
        {
            CustAddressRepository = unitOfWork.Repository<CustAddress>();
            SupplierAddressRepository = unitOfWork.Repository<SupplierAddress>();
            Customer_AddressRepository = unitOfWork.Repository<Customer_Addresses>();
            AddressTypeRepository = unitOfWork.Repository<AddressType>();
        }
        public CustAddress Get(int id)
        {
            return null;
        }
        public List<Buyeraddress> _Buyeraddress_Grid()
        {
            List<Buyeraddress> Salesorder_Grid = new List<Buyeraddress>();
            Salesorder_Grid = CustAddressRepository.Get_Buyeraddress_Grid();
            return Salesorder_Grid;
        }   
        public List<supplieraddress> _supplieraddress_Grid()
        {
            List<supplieraddress> Salesorder_Grid = new List<supplieraddress>();
            Salesorder_Grid = SupplierAddressRepository.Get_supplieraddress_Grid();
            return Salesorder_Grid;
        }
        public List<CustAddress> Getbuyer()
        {
            List<CustAddress> CustAddresslist = new List<CustAddress>();
            try
            {
                CustAddresslist = CustAddressRepository.Table.ToList<CustAddress>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return CustAddresslist;
        }     
        public List<Customer_Addresses> Getaddres()
        {
            List<Customer_Addresses> CustAddresslist = new List<Customer_Addresses>();
            try
            {
                CustAddresslist = Customer_AddressRepository.Table.ToList<Customer_Addresses>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return CustAddresslist;
        }     
        public List<SupplierAddress> Getsupplier()
        {
            List<SupplierAddress> SupplierAddress = new List<SupplierAddress>();
            try
            {
                SupplierAddress = SupplierAddressRepository.Table.ToList<SupplierAddress>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return SupplierAddress;
        }
        public List<AddressType> GetAddtype()
        {
            List<AddressType> AddressTypelist = new List<AddressType>();
            try
            {
                AddressTypelist = AddressTypeRepository.Table.ToList<AddressType>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return AddressTypelist;
        }
        public CustAddress GetCustAddressId(int? BuyerId)
        {
            CustAddress oCustAddress = new CustAddress();
            if (BuyerId != 0)
            {
                oCustAddress = CustAddressRepository.Table.Where(x => x.BuyerId == BuyerId).FirstOrDefault();
            }
            return oCustAddress;
        }        
        public SupplierAddress GetsuppAddressId(int? supplierid)
        {
            SupplierAddress oCustAddress = new SupplierAddress();
            if (supplierid != 0)
            {
                oCustAddress = SupplierAddressRepository.Table.Where(x => x.supplierid == supplierid).FirstOrDefault();
            }
            return oCustAddress;
        }          
        public SupplierAddress GetsuppAddresshdId(int? supplieradd_id)
        {
            SupplierAddress oCustAddress = new SupplierAddress();
            if (supplieradd_id != 0)
            {
                oCustAddress = SupplierAddressRepository.Table.Where(x => x.supplieradd_id == supplieradd_id).FirstOrDefault();
            }
            return oCustAddress;
        }    
        public Customer_Addresses GetCustAddId(int AddressId)
        {
            Customer_Addresses oCustAddress = new Customer_Addresses();
            if (AddressId != 0)
            {
                oCustAddress = Customer_AddressRepository.Table.Where(x => x.Addresshd_id == AddressId).FirstOrDefault();
            }
            return oCustAddress;
        }  
        public Customer_Addresses GetCustAddIds(int AddressId,int buyeid)
        {
            Customer_Addresses oCustAddress = new Customer_Addresses();
            if (AddressId != 0)
            {
                oCustAddress = Customer_AddressRepository.Table.Where(x => x.Addresshd_id == AddressId && x.Buyerid == buyeid).FirstOrDefault();
            }
            return oCustAddress;
        }  
        public Customer_Addresses GetCustAddIdsupp(int AddressId,int supp)
        {
            Customer_Addresses oCustAddress = new Customer_Addresses();
            if (AddressId != 0)
            {
                oCustAddress = Customer_AddressRepository.Table.Where(x => x.Addresshd_id == AddressId && x.supplierid == supp).FirstOrDefault();
            }
            return oCustAddress;
        }
        public CustAddress GetCustAddressbuyerid(int buyerid)
        {
            CustAddress oCustAddress = new CustAddress();
            if (buyerid != 0)
            {
                oCustAddress = CustAddressRepository.Table.Where(x => x.BuyerId == buyerid && x.isActive == true && x.isdeleted == true && x.AddressType ==3).FirstOrDefault();
            }
            return oCustAddress;
        } 
        public CustAddress GetCustAddressbuyeridshipp(int buyerid)
        {
            CustAddress oCustAddress = new CustAddress();
            if (buyerid != 0)
            {
                oCustAddress = CustAddressRepository.Table.Where(x => x.BuyerId == buyerid && x.isActive == true && x.isdeleted == true && x.AddressType ==2).FirstOrDefault();
            }
            return oCustAddress;
        }    
        public Customer_Addresses GetCustbuyerid(int buyerid,int addhd)
        {
            Customer_Addresses oCustAddress = new Customer_Addresses();
            if (buyerid != 0)
            {
                oCustAddress = Customer_AddressRepository.Table.Where(x => x.Buyerid == buyerid && x.Addresshd_id == addhd && x.addresstypeid == 3 ).FirstOrDefault();
            }
            return oCustAddress;
        }    
        public Customer_Addresses GetCustbuyeridship(int buyerid,int addhd)
        {
            Customer_Addresses oCustAddress = new Customer_Addresses();
            if (buyerid != 0)
            {
                oCustAddress = Customer_AddressRepository.Table.Where(x => x.Buyerid == buyerid && x.Addresshd_id == addhd && x.addresstypeid == 2).FirstOrDefault();
            }
            return oCustAddress;
        }
        public CustAddress GetCustAddressbuyerhdid(int Addresshd_id)
        {
            CustAddress oCustAddress = new CustAddress();
            if (Addresshd_id != 0)
            {
                oCustAddress = CustAddressRepository.Table.Where(x => x.Addresshd_id == Addresshd_id).FirstOrDefault();
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

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
