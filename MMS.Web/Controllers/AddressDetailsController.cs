using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Web.Models.AccountModel;
using System.Web.Security;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.ForgetPassword;
using MMS.Web.Models.UserModel;
using MMS.Web.Models.Permission;
using MMS.Common;
using MMS.Web.ExtensionMethod;
using MMS.Web.Models.Addressdetails;
using MMS.Web.Models.AgentMasterModel;
using System.Web.Providers.Entities;
using iText.Commons.Utils;
using System.Data;
using System.Web.UI;

namespace MMS.Web.Controllers
{
    public class AddressDetailsController : Controller
    {
        #region  Address View
        [HttpGet]
        public ActionResult AddressMaster()
        {

            Addressdetails model = new Addressdetails();
            return View(model);
        }
        [HttpGet]
        public ActionResult AddressDetailsGrid(int page = 1, int pageSize = 8)
        {
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            Addressdetails model = new Addressdetails();
            var Addressdetailslist = custAddressMangers._Buyeraddress_Grid();
            var Addressdetailslist1 = custAddressMangers._supplieraddress_Grid();
            model.AddressdetailbuyerLists = Addressdetailslist;
            model.AddressdetailssupplierLists = Addressdetailslist1;
            var totalCount = model.AddressdetailbuyerLists.Count();
            var totalCountsupp = model.AddressdetailssupplierLists.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int totalPagessupp = (int)Math.Ceiling((double)totalCountsupp / pageSize);
            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            model.AddressdetailbuyerLists = model.AddressdetailbuyerLists.OrderByDescending(s => s.addresshd_id)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPages = totalPages;
            ViewBag.totalPagessupp = totalPagessupp;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return PartialView("Partial/AddressDetailsGrid", model);
        }     
        [HttpGet]
        public ActionResult AddressDetailssupplierGrid(int page = 1, int pageSize = 8)
        {
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            Addressdetails model = new Addressdetails();
            var Addressdetailslist1 = custAddressMangers._supplieraddress_Grid();
            model.AddressdetailssupplierLists = Addressdetailslist1;

            var totalCountsupp = model.AddressdetailssupplierLists.Count();
            int totalPagessupp = (int)Math.Ceiling((double)totalCountsupp / pageSize);
            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCountsupp - 1);
            model.AddressdetailssupplierLists = model.AddressdetailssupplierLists.OrderByDescending(s => s.addresshd_id)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.totalPagessupp = totalPagessupp;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View("~/Views/AddressDetails/Partial/SupplierAddressGrid.cshtml", model);
        }
        public ActionResult AddressDetails()
        {
            Addressdetails model = new Addressdetails();

            return View("Partial/AddressDetails", model);
        }

        [HttpPost]
        public ActionResult EditAddressDetails(int AddressId)
        {
            CustAddressMangers CustAddressManager = new CustAddressMangers();
            Addressdetails model = new Addressdetails();
            var CustAddress = CustAddressManager.GetCustAddressbuyerhdid(AddressId);
           var  Cust = CustAddressManager.GetCustAddIds(AddressId, CustAddress.BuyerId);
            if (CustAddress != null)
            {
                model.AddressId = CustAddress.Addresshd_id;
                model.BuyerId = CustAddress.BuyerId;
                model.AddressType = CustAddress.AddressType;
                model.addressvarient = CustAddress.addressvarient;
                model.City = CustAddress.CityId;
                model.State = CustAddress.StateId;
                model.Country = CustAddress.CountryId;
                model.ContactName = CustAddress.ContactName;
                model.Email = CustAddress.Email;
                model.Phone = CustAddress.Phone;
                model.Notes = CustAddress.Notes;
                model.Active = CustAddress.isActive;
                model.Add1 = Cust.address1;
                model.Add2 = Cust.address2;
                model.Add3 = Cust.address3;
                model.ZipCode = Cust.ZipCode;
                model.vatnumber = Cust.vatnumber;
            }
            return PartialView("Partial/AddressDetails", model);
        }      
        [HttpPost]
        public ActionResult EditsupplierAddressDetails(int AddressId)
        {
            CustAddressMangers CustAddressManager = new CustAddressMangers();
            Addressdetails model = new Addressdetails();
            var CustAddress = CustAddressManager.GetsuppAddresshdId(AddressId);
           var  Cust = CustAddressManager.GetCustAddIdsupp(AddressId, CustAddress.supplierid);
            if (CustAddress != null)
            {
                model.suppAddressId = CustAddress.supplieradd_id;
                model.SupplierId = CustAddress.supplierid;
                model.AddressType = CustAddress.AddressType;
                model.addressvarient = CustAddress.addressvarient;
                model.City = CustAddress.CityId;
                model.State = CustAddress.StateId;
                model.Country = CustAddress.CountryId;
                model.ContactName = CustAddress.ContactName;
                model.Email = CustAddress.Email;
                model.Phone = CustAddress.Phone;
                model.Notes = CustAddress.Notes;
                model.Active = CustAddress.isActive;
                model.Add1 = Cust.address1;
                model.Add2 = Cust.address2;
                model.Add3 = Cust.address3;
                model.ZipCode = Cust.ZipCode;
                model.vatnumber = Cust.vatnumber;
            }
            return PartialView("Partial/AddressDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult CustAddressInsert(Addressdetails model)
        {
            string Alertmessage = "";
            CustAddress oCustAddress = new CustAddress();
            SupplierAddress supplierAddress = new SupplierAddress();
            Customer_Addresses customer_Addresses = new Customer_Addresses();
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            var addid = 0;
            if (model.BuyerId !=0)
            {
                var data = oCustAddressMangers.GetCustAddressId(model.BuyerId);
                if(data!= null)
                {
                    if (data.addressvarient == 0)
                    {
                        Alertmessage = "already";
                        return Json(Alertmessage, JsonRequestBehavior.AllowGet);
                    }
                }
                oCustAddress.BuyerId = model.BuyerId;
                oCustAddress.AddressType = model.AddressType;
                oCustAddress.addressvarient = model.addressvarient;
                oCustAddress.CityId = model.CityId;
                oCustAddress.StateId = model.StateId;
                oCustAddress.CountryId = model.CountryId;
                oCustAddress.ContactName = model.ContactName;
                oCustAddress.Email = model.Email;
                oCustAddress.Phone = model.Phone;
                oCustAddress.Notes = model.Notes;
                oCustAddress.CreatedDate = DateTime.Now;
                var postdata = oCustAddressMangers.Post(oCustAddress);
                addid = postdata.Addresshd_id;
                customer_Addresses.Addresshd_id = addid;
            }
            else if (model.SupplierId != 0)
            {
                var data = oCustAddressMangers.GetsuppAddressId(model.SupplierId);
                if (data != null)
                {
                    if (data.addressvarient == 0)
                    {
                        Alertmessage = "supplieralready";
                        return Json(Alertmessage, JsonRequestBehavior.AllowGet);
                    }
                }
                supplierAddress.supplierid = model.SupplierId;
                supplierAddress.AddressType = model.AddressType;
                supplierAddress.addressvarient = model.addressvarient;
                supplierAddress.CityId = model.CityId;
                supplierAddress.StateId = model.StateId;
                supplierAddress.CountryId = model.CountryId;
                supplierAddress.ContactName = model.ContactName;
                supplierAddress.Email = model.Email;
                supplierAddress.Phone = model.Phone;
                supplierAddress.Notes = model.Notes;
                supplierAddress.CreatedDate = DateTime.Now;
                var postdata = oCustAddressMangers.PostSupplier(supplierAddress);
                addid = postdata.supplieradd_id;
                customer_Addresses.suppaddresshdid = addid;

            }
            customer_Addresses.Buyerid = model.BuyerId;
            customer_Addresses.supplierid = model.SupplierId;
            customer_Addresses.address1 = model.Add1;
            customer_Addresses.addresstypeid = model.AddressType;
            customer_Addresses.address2 = model.Add2;
            customer_Addresses.address3 = model.Add3;
            customer_Addresses.ZipCode = model.ZipCode;
            customer_Addresses.vatnumber = model.vatnumber;
            oCustAddressMangers.Postcust(customer_Addresses);
            Alertmessage = "Success";
            return Json(Alertmessage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(Addressdetails model)
        {
            CustAddress oCustAddress = new CustAddress();
            Customer_Addresses customer_Addresses = new Customer_Addresses();
            CustAddress objCustAddress = new CustAddress();
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            objCustAddress = oCustAddressMangers.GetCustAddressId(model.AddressId);
            customer_Addresses = oCustAddressMangers.GetCustAddId(model.AddressId);
            if (model != null)
            {
                oCustAddress.Addresshd_id = model.AddressId;
                oCustAddress.BuyerId = model.BuyerId;
                oCustAddress.AddressType = model.AddressType;
                oCustAddress.addressvarient = model.addressvarient;
                oCustAddress.CityId = model.CityId;
                oCustAddress.StateId = model.StateId;
                oCustAddress.CountryId = model.CountryId;
                oCustAddress.ContactName = model.ContactName;
                oCustAddress.Email = model.Email;
                oCustAddress.Phone = model.Phone;
                oCustAddress.Notes = model.Notes;
                oCustAddress.isActive =true;
                oCustAddress.UpdatedDate = DateTime.Now;
                oCustAddressMangers.Put(oCustAddress);
                customer_Addresses.Addresshd_id = model.AddressId;
                customer_Addresses.Buyerid = model.BuyerId;
                customer_Addresses.addresstypeid = model.AddressType;
                customer_Addresses.address1 = model.Add1;
                customer_Addresses.address2 = model.Add2;
                customer_Addresses.address3 = model.Add3;
                customer_Addresses.ZipCode = model.ZipCode;
                customer_Addresses.vatnumber = model.vatnumber;
                oCustAddressMangers.Putcustadd(customer_Addresses);
            }
            else
            {
                return Json(oCustAddress, JsonRequestBehavior.AllowGet);
            }

            return Json(oCustAddress, JsonRequestBehavior.AllowGet);
        } 
        [HttpPost]
        public ActionResult Updatesupplier(Addressdetails model)
        {
            SupplierAddress oCustAddress = new SupplierAddress();
            Customer_Addresses customer_Addresses = new Customer_Addresses();
            CustAddress objCustAddress = new CustAddress();
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            objCustAddress = oCustAddressMangers.GetCustAddressId(model.SupplierId);
            customer_Addresses = oCustAddressMangers.GetCustAddsuppId(model.suppAddressId);
            if (model != null)
            {
                oCustAddress.supplieradd_id = model.suppAddressId;
                oCustAddress.supplierid = model.SupplierId;
                oCustAddress.AddressType = model.AddressType;
                oCustAddress.addressvarient = model.addressvarient;
                oCustAddress.CityId = model.CityId;
                oCustAddress.StateId = model.StateId;
                oCustAddress.CountryId = model.CountryId;
                oCustAddress.ContactName = model.ContactName;
                oCustAddress.Email = model.Email;
                oCustAddress.Phone = model.Phone;
                oCustAddress.Notes = model.Notes;
                oCustAddress.isActive =true;
                oCustAddress.UpdatedDate = DateTime.Now;
                oCustAddressMangers.Putsupplier(oCustAddress);
                customer_Addresses.suppaddresshdid = model.AddressId;
                customer_Addresses.addresstypeid = model.AddressType;
                customer_Addresses.supplierid = model.SupplierId;
                customer_Addresses.address1 = model.Add1;
                customer_Addresses.address2 = model.Add2;
                customer_Addresses.address3 = model.Add3;
                customer_Addresses.ZipCode = model.ZipCode;
                customer_Addresses.vatnumber = model.vatnumber;
                oCustAddressMangers.Putsuppadd(customer_Addresses);
            }
            else
            {
                return Json(oCustAddress, JsonRequestBehavior.AllowGet);
            }

            return Json(oCustAddress, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(int AddressId, bool IsChecked)
        {
            CustAddress oCustAddress = new CustAddress();
            string status = "";
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            oCustAddress = oCustAddressMangers.GetCustAddressbuyerhdid(AddressId);
            if (AddressId != 0)
            {
                if(IsChecked == true)
                {
                    status = "Success";
                }
                else
                {
                    status = "failer";
                }
                oCustAddressMangers.Delete(oCustAddress.Addresshd_id, IsChecked);
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }     
        [HttpPost]
        public ActionResult Deletesupplier(int AddressId, bool IsChecked)
        {
            SupplierAddress oCustAddress = new SupplierAddress();
            string status = "";
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            oCustAddress = oCustAddressMangers.GetsuppAddresshdId(AddressId);
            if (AddressId != 0)
            {
                if (IsChecked == true)
                {
                    status = "Success";
                }
                else
                {
                    status = "failer";
                }
                oCustAddressMangers.Deletesupplier(oCustAddress.supplieradd_id, IsChecked);
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult updatestatus(int AddressId, bool IsChecked)
        {
            CustAddress oCustAddress = new CustAddress();
            string status = "";
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            oCustAddress = oCustAddressMangers.GetCustAddressbuyerhdid(AddressId);
            if (AddressId != 0)
            {
                if (IsChecked == true)
                {
                    status = "failer";
                }
                else if (IsChecked == false)
                {
                    status = "Success";

                }
                oCustAddressMangers.updateactive(oCustAddress.Addresshd_id, IsChecked);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }   
        [HttpPost]
        public ActionResult updatesupplierstatus(int AddressId, bool IsChecked)
        {
            SupplierAddress oCustAddress = new SupplierAddress();
            string status = "";
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            oCustAddress = oCustAddressMangers.GetsuppAddresshdId(AddressId);
            if (AddressId != 0)
            {
                if (IsChecked == true)
                {
                    status = "failer";
                }
                else if (IsChecked == false)
                {
                    status = "Success";

                }
                oCustAddressMangers.updatesupplieractive(oCustAddress.supplieradd_id, IsChecked);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        [HttpGet]
        public ActionResult Search(string filter)
        {
            List<CustAddress> oCustAddressList = new List<CustAddress>();
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            Addressdetails model = new Addressdetails();
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            countryListmanager countryListmanager = new countryListmanager();
            StatelistManager StatelistManager = new StatelistManager();
            CitylistManager cityListmanager = new CitylistManager();
            BuyerMasterManager BuyerManager = new BuyerMasterManager();
            oCustAddressList = oCustAddressMangers.Getbuyer();
            var totaldata = (from d in oCustAddressList
                             join d1 in countryListmanager.Get() on d.CountryId equals d1.Id into d1Group
                             from d1 in d1Group.DefaultIfEmpty()
                             join ad in custAddressMangers.GetAddtype() on d.AddressType equals ad.AddTypeID into adGroup
                             from ad in adGroup.DefaultIfEmpty()
                             join ca in custAddressMangers.Getaddres() on d.Addresshd_id equals ca.Addresshd_id into caGroup
                             from ca in caGroup.DefaultIfEmpty()
                             join s in StatelistManager.Get() on d.StateId equals s.Id into sGroup
                             from s in sGroup.DefaultIfEmpty()
                             join c in cityListmanager.Get() on d.CityId equals c.Id into cGroup
                             from c in cGroup.DefaultIfEmpty()
                             join b in BuyerManager.Get() on d.BuyerId equals b.BuyerMasterId into bGroup
                             from b in bGroup.DefaultIfEmpty()
                             select new Addressdetails
                             {
                                 AddressId = d.Addresshd_id,
                                 ContactName = d.ContactName,
                                 Email = d.Email,
                                 Phone = d.Phone,
                                 isdeleted = d.isdeleted,
                                 Active = d.isActive,
                                 BuyerMaster = b != null ? new BuyerMaster1
                                 {
                                     CustomerName = b.CustomerName,
                                     BuyerCode = b.BuyerCode,
                                     
                                 } : null,
                                 cities = c != null ? new cities
                                 {
                                     Cityname = c.Cityname
                                 } : null,
                                 AddressTypes = ad != null ? new AddressType
                                 {
                                     AddTypeName = ad.AddTypeName
                                 } : null,
                                 Customer_Addresses = ca != null ? new Customer_Addresses
                                 {
                                     ZipCode = ca.ZipCode,
                                     address1 = ca.address1
                                 } : null,
                                 states = s != null ? new states
                                 {
                                     Statename = s.Statename
                                 } : null,
                                 countries = d1 != null ? new countries
                                 {
                                     Name = d1.Name
                                 } : null,
                             }).ToList();
            var totallist = totaldata.Where(x => x.BuyerMaster.CustomerName.ToLower().Trim().Contains(filter.ToLower().Trim())).OrderByDescending(m=>m.AddressId).ToList();
            return Json(totallist, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Searchsalesorder(int Buyerid)
        {
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            Addressdetails model = new Addressdetails();
            var Addressdetailslist = custAddressMangers._Buyeraddress_Grid();
            var Addressdetailslist1 = custAddressMangers._supplieraddress_Grid();


            model.AddressdetailssupplierLists = Addressdetailslist1;
            var totallist = Addressdetailslist.Where(x => x.buyermasterid == Buyerid).ToList();
            model.AddressdetailbuyerLists = totallist;

            return View("Partial/AddressDetailsGrid", model);
        }

        public ActionResult Searchsuppllier(string filter)
        {
            List<CustAddress> oCustAddressList = new List<CustAddress>();
            CustAddressMangers CustAddressMangers = new CustAddressMangers();
            var Addressdetailslist1 = CustAddressMangers._supplieraddress_Grid();

            var totallist = Addressdetailslist1.Where(x => x.suppliername.ToLower().Trim().Contains(filter.ToLower().Trim())).OrderByDescending(m => m.addresshd_id).ToList();

            return Json(totallist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult statelist(int country)
        {
            List<countries> countriesList_ = new List<countries>();
            countryListmanager countryListmanager = new countryListmanager();
            StatelistManager StatelistManager = new StatelistManager();
            countriesList_ = countryListmanager.Get().Where(x => x.Id == country).ToList();

            var items = (from x in countryListmanager.Get()
                         join state in StatelistManager.Get() on x.Id equals state.Countryid
                         where state.Countryid == country
                         select new { Statename = state.Statename, Id = state.Id });

            var distinctList = items.GroupBy(x => x.Id).Select(g => g.First()).ToList();



            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult citylist(int state)
        {
            List<states> countriesList_ = new List<states>();
            StatelistManager StatelistManager = new StatelistManager();
            CitylistManager cityListmanager = new CitylistManager();
            countriesList_ = StatelistManager.Get().Where(x => x.Id == state).ToList();

            var items = (from x in StatelistManager.Get()
                         join y in cityListmanager.Get() on x.Id equals y.Stateid
                         where y.Stateid == state
                         select new { Cityname = y.Cityname, Id = y.Id });

            var distinctList = items.GroupBy(x => x.Id).Select(g => g.First()).ToList();



            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
