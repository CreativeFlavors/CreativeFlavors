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
        public ActionResult AddressDetailsGrid(int page = 1, int pageSize = 15)
        {
            Addressdetails model = new Addressdetails();
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            var Addressdetailslist = custAddressMangers.Get();

            countryListmanager countryListmanager = new countryListmanager();
            StatelistManager StatelistManager = new StatelistManager();
            CitylistManager cityListmanager = new CitylistManager();
            BuyerManager BuyerManager = new BuyerManager();
            var totaldata = (from d in Addressdetailslist
                             join d1 in countryListmanager.Get() on d.Country equals d1.Id
                             join s in StatelistManager.Get() on d.State equals s.Id
                             join c in cityListmanager.Get() on d.City equals c.Id
                             join b in BuyerManager.Get() on d.BuyerId equals b.BuyerMasterId
                             select new Addressdetails
                             {
                                 AddressId = d.AddressId,
                                 ZipCode = d.ZipCode,
                                 ContactName = d.ContactName,
                                 Email = d.Email,
                                 Phone = d.Phone,
                                 Add1 = d.Add1,
                                 BuyerMaster = new BuyerMaster
                                 {
                                     BuyerFullName = b.BuyerFullName
                                 },
                                 cities = new cities
                                 {
                                     Cityname = c.Cityname
                                 },
                                 states = new states
                                 {
                                     Statename = s.Statename
                                 },
                                 countries = new countries
                                 {
                                     Name = d1.Name
                                 },
                                 
                             }).ToList();

            var totalCount = totaldata.Count();

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totaldata = totaldata.OrderByDescending(s => s.AddressId)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return PartialView("Partial/AddressDetailsGrid", totaldata);
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
            CustAddress oCustAddress = new CustAddress();
            Addressdetails model = new Addressdetails();
            oCustAddress = CustAddressManager.GetCustAddressId(AddressId);
            if (oCustAddress != null)
            {
                model.AddressId = oCustAddress.AddressId;
                model.BuyerId = oCustAddress.BuyerId;
                model.AddressType = oCustAddress.AddressType;
                model.Add1 = oCustAddress.Add1;
                model.Add2 = oCustAddress.Add2;
                model.Add3 = oCustAddress.Add3;
                model.IsDefault = oCustAddress.IsDefault;
                model.City = oCustAddress.City;
                model.CityId = oCustAddress.CityId;
                model.State = oCustAddress.State;
                model.StateId = oCustAddress.StateId;
                model.Country = oCustAddress.Country;
                model.CountryId = oCustAddress.CountryId;
                model.ZipCode = oCustAddress.ZipCode;
                model.ContactName = oCustAddress.ContactName;
                model.Email = oCustAddress.Email;
                model.Phone = oCustAddress.Phone;
                model.Notes = oCustAddress.Notes;
                model.Active = oCustAddress.Active;
            }
            return PartialView("Partial/AddressDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult CustAddressInsert(Addressdetails model)
        {
            CustAddress oCustAddress = new CustAddress();
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            oCustAddress.AddressId = model.AddressId;
            oCustAddress.BuyerId = model.BuyerId;
            oCustAddress.AddressType = model.AddressType;
            oCustAddress.Add1 = model.Add1;
            oCustAddress.Add2 = model.Add2;
            oCustAddress.Add3 = model.Add3;
            oCustAddress.IsDefault = model.IsDefault;
            oCustAddress.City = model.City;
            oCustAddress.CityId = model.CityId;
            oCustAddress.State = model.State;
            oCustAddress.StateId = model.StateId;
            oCustAddress.Country = model.Country;
            oCustAddress.CountryId = model.CountryId;
            oCustAddress.ZipCode = model.ZipCode;
            oCustAddress.ContactName = model.ContactName;
            oCustAddress.Email = model.Email;
            oCustAddress.Phone = model.Phone;
            oCustAddress.Notes = model.Notes;
            oCustAddress.Active = true;
            oCustAddress.CreatedDate = DateTime.Now;
            oCustAddressMangers.Post(oCustAddress);
            return Json(oCustAddress, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(Addressdetails model)
        {
            CustAddress oCustAddress = new CustAddress();

            CustAddress objCustAddress = new CustAddress();
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            objCustAddress = oCustAddressMangers.GetCustAddressId(model.AddressId);
            if (model != null)
            {
                oCustAddress.AddressId = model.AddressId;
                oCustAddress.BuyerId = model.BuyerId;
                oCustAddress.AddressType = model.AddressType;
                oCustAddress.Add1 = model.Add1;
                oCustAddress.Add2 = model.Add2;
                oCustAddress.Add3 = model.Add3;
                oCustAddress.IsDefault = model.IsDefault;
                oCustAddress.City = model.City;
                oCustAddress.CityId = model.CityId;
                oCustAddress.State = model.State;
                oCustAddress.StateId = model.StateId;
                oCustAddress.Country = model.Country;
                oCustAddress.CountryId = model.CountryId;
                oCustAddress.ZipCode = model.ZipCode;
                oCustAddress.ContactName = model.ContactName;
                oCustAddress.Email = model.Email;
                oCustAddress.Phone = model.Phone;
                oCustAddress.Notes = model.Notes;
                oCustAddress.Active =true;
                oCustAddress.UpdatedDate = DateTime.Now;
                oCustAddressMangers.Put(oCustAddress);
            }
            else
            {
                return Json(oCustAddress, JsonRequestBehavior.AllowGet);
            }

            return Json(oCustAddress, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int AddressId)
        {
            CustAddress oCustAddress = new CustAddress();
            string status = "";
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            oCustAddress = oCustAddressMangers.GetCustAddressId(AddressId);
            if (AddressId != 0)
            {
                status = "Success";
                oCustAddressMangers.Delete(oCustAddress.AddressId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<CustAddress> oCustAddressList = new List<CustAddress>();
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            oCustAddressList = oCustAddressMangers.Get();
            ///Addressdetails vm = new Addressdetails();
            oCustAddressList = oCustAddressList.Where(x => x.ContactName.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.Add1.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.Add2.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            var Addressdetailslist = oCustAddressList;

            Addressdetails model = new Addressdetails();
            CustAddressMangers custAddressMangers = new CustAddressMangers();
            countryListmanager countryListmanager = new countryListmanager();
            StatelistManager StatelistManager = new StatelistManager();
            CitylistManager cityListmanager = new CitylistManager();
            BuyerManager BuyerManager = new BuyerManager();
            var totalList = (from d in Addressdetailslist
                             join d1 in countryListmanager.Get() on d.Country equals d1.Id
                             join s in StatelistManager.Get() on d.State equals s.Id
                             join c in cityListmanager.Get() on d.City equals c.Id
                             join b in BuyerManager.Get() on d.BuyerId equals b.BuyerMasterId
                             select new Addressdetails
                             {
                                 AddressId = d.AddressId,
                                 ZipCode = d.ZipCode,
                                 ContactName = d.ContactName,
                                 Email = d.Email,
                                 Phone = d.Phone,
                                 Add1 = d.Add1,
                                 BuyerMaster = new BuyerMaster
                                 {
                                     BuyerFullName = b.BuyerFullName
                                 },
                                 cities = new cities
                                 {
                                     Cityname = c.Cityname
                                 },
                                 states = new states
                                 {
                                     Statename = s.Statename
                                 },
                                 countries = new countries
                                 {
                                     Name = d1.Name
                                 }
                             }).ToList();

            return Json(totalList, JsonRequestBehavior.AllowGet);
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
