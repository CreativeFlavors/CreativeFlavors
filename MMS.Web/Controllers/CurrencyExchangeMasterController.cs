using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Web.Models.Currency;
using MMS.Repository.Managers;
using MMS.Core.Entities;
using MMS.Common;
using System.Globalization;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class CurrencyExchangeMasterController : Controller
    {
        //
        // GET: /CurrencyExchangeMaster/

        public ActionResult CurrencyExchangeMaster()
        {
            CurrencyExchangeModel model = new CurrencyExchangeModel();
            return View(model);
        }

        public ActionResult CurrencyExchangeGrid()
        {
            CurrencyExchangeModel vm = new CurrencyExchangeModel();
            CurrencyExchangeManager CurrencyManager = new CurrencyExchangeManager();
            vm.CurrencyExchangeMasterList = CurrencyManager.Get();

            return PartialView("Partial/CurrencyExchangeGrid", vm);
        }

        [HttpPost]
        public ActionResult CurrencyExchangeDetails(int CurrencyExchangeMasterId)
        {
            CurrencyExchangeManager CurrencyManager = new CurrencyExchangeManager();
            CurrencyExchangeMaster CurrencyMaster = new CurrencyExchangeMaster();
            CurrencyExchangeModel model = new CurrencyExchangeModel();
            CurrencyMaster = CurrencyManager.GetCurrencyExchangeMasterId(CurrencyExchangeMasterId);
            if (CurrencyMaster != null)
            {
                model.CurrencyExchangeMasterId = CurrencyMaster.CurrencyExchangeMasterId;
                model.ValueInRupees = CurrencyMaster.ValueInRupees;
                model.Currency = CurrencyMaster.Currency;
                model.Date = Convert.ToDateTime(CurrencyMaster.Date).Date.ToString("dd/MM/yyyy");
                model.CreatedDate = CurrencyMaster.CreatedDate;
                model.UpdatedDate = CurrencyMaster.UpdatedDate;
                model.CreatedBy = CurrencyMaster.CreatedBy;
                model.UpdatedBy = CurrencyMaster.UpdatedBy;
            }
            return PartialView("Partial/CurrencyExchangeDetails", model);
        }

        [HttpPost]
        public ActionResult CurrencyExchangeMaster(CurrencyExchangeModel model)
        {
            CurrencyExchangeModel CurrencyMasterModel = new CurrencyExchangeModel();
            bool Result = false;
                CurrencyExchangeManager CurrencyManager = new CurrencyExchangeManager();
                CurrencyExchangeMaster CurrencyMasters = new CurrencyExchangeMaster();
                    CurrencyMasters.CurrencyExchangeMasterId = model.CurrencyExchangeMasterId;
                    var format = "dd/MM/yyyy";
                    DateTime CEdate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);

                    CurrencyMasters.Date = CEdate.Date;
                    CurrencyMasters.Currency = model.Currency;
                    CurrencyMasters.ValueInRupees = model.ValueInRupees;
                    Result = CurrencyManager.Post(CurrencyMasters);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(CurrencyExchangeModel model)
        {
            CurrencyExchangeMaster CurrencyMaster = new CurrencyExchangeMaster();
            string status = "";
            CurrencyExchangeManager CurrencyManager = new CurrencyExchangeManager();
            CurrencyMaster = CurrencyManager.GetCurrencyMasterId(model.CurrencyExchangeMasterId);
            if (CurrencyMaster != null)
            {
                status = "Success";
                CurrencyManager.Delete(CurrencyMaster.CurrencyExchangeMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<CurrencyExchangeMaster> CurrencyMasterList = new List<CurrencyExchangeMaster>();
            CurrencyExchangeManager CurrencyManager = new CurrencyExchangeManager();
            CurrencyMasterList = CurrencyManager.Get();
            CurrencyMasterList = CurrencyMasterList.Where(x => x.Currency.ToString().ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            CurrencyExchangeModel vm = new CurrencyExchangeModel();
            vm.CurrencyExchangeMasterList = CurrencyMasterList;
            return PartialView("Partial/CurrencyExchangeGrid", vm);
        }
        #endregion
    }
}
