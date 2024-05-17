using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class CurrencyController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult Currency()
        {
            CurrencyModel vm = new CurrencyModel();
            return View(vm);
        }
        public ActionResult CurrencyGrid()
        {
            CurrencyModel vm = new CurrencyModel();
            CurrencyManager currencyManager = new CurrencyManager();
            vm.CurrencyMasterList = currencyManager.Get();

            return PartialView("Partial/CurrencyGrid", vm);
        }
        [HttpPost]
        public ActionResult CurrencyDetails(int CurrencyMasterId)
        {
            CurrencyManager currencyManager = new CurrencyManager();
            CurrencyMaster currencyMaster = new CurrencyMaster();
            CurrencyModel model = new CurrencyModel();
            currencyMaster = currencyManager.GetCurrentMasterId(CurrencyMasterId);
            if (currencyMaster != null)
            {
                model.Symbol = currencyMaster.Symbol;
                model.ShortForm = currencyMaster.ShortForm;
                model.LongForm = currencyMaster.LongForm;
                model.CurrencyMasterId = currencyMaster.CurrencyMasterId;
                model.LowerDenomination = currencyMaster.LowerDenomination; ;
                model.CreatedDate = currencyMaster.CreatedDate;
                model.UpdatedDate = currencyMaster.UpdatedDate;
                model.CreatedBy = currencyMaster.CreatedBy;
                model.UpdatedBy = currencyMaster.UpdatedBy;
            }
            return PartialView("Partial/CurrencyDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult Currency(CurrencyModel model)
        {
            CurrencyMaster currencyMasters = new CurrencyMaster();
            if (ModelState.IsValid)
            {
                CurrencyMaster currencyMaster = new CurrencyMaster();
                CurrencyManager currencyManager = new CurrencyManager();
                currencyMaster = currencyManager.GetCurrencyFullName(model.LongForm);
                if (currencyMaster == null)
                {

                    currencyMasters.Symbol = model.Symbol;
                    currencyMasters.ShortForm = model.ShortForm;
                    currencyMasters.LongForm = model.LongForm;
                    currencyMasters.LowerDenomination = model.LowerDenomination; ;
                    currencyMasters.CreatedDate = DateTime.Now;
                    currencyManager.Post(currencyMasters);
                }
                else
                {
                    return Json(currencyMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(currencyMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(CurrencyModel model)
        {
            CurrencyMaster currencyMasters = new CurrencyMaster();
            if (ModelState.IsValid)
            {
                CurrencyMaster currencyMaster = new CurrencyMaster();
                CurrencyManager currencyManager = new CurrencyManager();
                currencyMaster = currencyManager.GetCurrentMasterId(model.CurrencyMasterId);
                if (currencyMaster != null)
                {
                    currencyMasters.Symbol = model.Symbol;
                    currencyMasters.ShortForm = model.ShortForm;
                    currencyMasters.LongForm = model.LongForm;
                    currencyMasters.CurrencyMasterId = currencyMaster.CurrencyMasterId;
                    currencyMasters.LowerDenomination = model.LowerDenomination; ;
                    currencyMasters.CreatedDate = currencyMaster.CreatedDate;
                    currencyMasters.UpdatedDate = DateTime.Now;
                    currencyMasters.CreatedBy = "";
                    currencyMasters.UpdatedBy = "";
                    currencyManager.Put(currencyMasters);
                }
                else
                {
                    return Json(currencyMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(currencyMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int CurrencyMasterId)
        {
            CurrencyMaster currencyMaster = new CurrencyMaster();
            string status = "";
            CurrencyManager currencyManager = new CurrencyManager();
            currencyMaster = currencyManager.GetCurrentMasterId(CurrencyMasterId);
            if (currencyMaster != null)
            {
                status = "Success";
                currencyManager.Delete(currencyMaster.CurrencyMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<CurrencyMaster> currencyMasterList = new List<CurrencyMaster>();
            CurrencyManager currencyManager = new CurrencyManager();
            currencyMasterList = currencyManager.Get();
            currencyMasterList = currencyMasterList.Where(x => x.Symbol.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.ShortForm.ToLower().Trim().Contains(filter.ToLower().Trim())|| x.LongForm.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            CurrencyModel model = new CurrencyModel();
            model.CurrencyMasterList = currencyMasterList;
            return PartialView("Partial/CurrencyGrid", model);
        }
        #endregion

    }
}
