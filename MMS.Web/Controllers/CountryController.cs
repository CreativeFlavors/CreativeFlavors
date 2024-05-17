using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.CountryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class CountryController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult Country()
        {
            CountryModel vm = new CountryModel();
            return View(vm);
        }
        public ActionResult CountryGrid()
        {
            CountryModel vm = new CountryModel();
            CountryManager countryManager = new CountryManager();
            vm.CountryList = countryManager.Get();

            return PartialView("Partial/CountryGrid", vm);
        }
        [HttpPost]
        public ActionResult CountryDetails(int CountryMasterId)
        {
            CountryManager countryManager = new CountryManager();
            CountryMaster countryMaster = new CountryMaster();
            CountryModel model = new CountryModel();
            countryMaster = countryManager.GetCountryMasterId(CountryMasterId);
            if (countryMaster != null)
            {
                model.CountryMasterId = countryMaster.CountryMasterId;
                model.LongCountryName = countryMaster.LongCountryName;
                model.ShortCountryName = countryMaster.ShortCountryName;
                model.CreatedDate = countryMaster.CreatedDate;
                model.UpdatedDate = countryMaster.UpdatedDate;
                model.CreatedBy = countryMaster.CreatedBy;
                model.UpdatedBy = countryMaster.UpdatedBy;
            }
            return PartialView("Partial/CountryDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult Country(CountryModel model)
        {
            CountryMaster countryMasters = new CountryMaster();
            if (ModelState.IsValid)
            {
                CountryMaster countryMaster = new CountryMaster();
                CountryManager countryManager = new CountryManager();
                countryMaster = countryManager.GetLongCountryName(model.LongCountryName);
                if (countryMaster==null)
                {
                    countryMasters.LongCountryName = model.LongCountryName;
                    countryMasters.ShortCountryName = model.ShortCountryName;
                    countryMasters.CreatedDate = DateTime.Now;
                    countryManager.Post(countryMasters);
                }
                else
                {
                    return Json(countryMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(countryMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(CountryModel model)
        {
            CountryMaster countryMasters = new CountryMaster();
            if (ModelState.IsValid)
            {
                CountryMaster countryMaster = new CountryMaster();
                CountryManager countryManager = new CountryManager();
                countryMaster = countryManager.GetCountryMasterId(model.CountryMasterId);
                if (countryMaster != null)
                {
                    countryMasters.CountryMasterId = model.CountryMasterId;
                    countryMasters.LongCountryName = model.LongCountryName;
                    countryMasters.ShortCountryName = model.ShortCountryName;
                    countryMasters.CreatedDate = model.CreatedDate;
                    countryMasters.UpdatedDate = DateTime.Now;
                    countryMasters.CreatedBy = countryMaster.CreatedBy;
                    countryMasters.UpdatedBy = countryMaster.UpdatedBy;
                    countryManager.Put(countryMasters);
                }
                else
                {
                    return Json(countryMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(countryMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int CountryMasterId)
        {
            CountryMaster countryMaster = new CountryMaster();
            string status = "";
            CountryManager countryManager = new CountryManager();
            countryMaster = countryManager.GetCountryMasterId(CountryMasterId);
            if (countryMaster != null)
            {
                status = "Success";
                countryManager.Delete(countryMaster.CountryMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<CountryMaster> countryMasterList = new List<CountryMaster>();
            CountryManager countryManager = new CountryManager();
            countryMasterList = countryManager.Get();
            countryMasterList = countryMasterList.Where(x => x.LongCountryName.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.ShortCountryName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            CountryModel vm = new CountryModel();
            vm.CountryList = countryMasterList;
            return PartialView("Partial/CountryGrid", vm);
        }
        #endregion

    }
}
