using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models;
using MMS.Web.Models.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MMS.Web.Controllers
{
    public class CurrencyConversionController : Controller
    {
        #region view region
        [HttpGet]
        public ActionResult CurrencyConversionmaster()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CurrencyConversionGrid(int page = 1, int pageSize = 15)
        {
            CurrencyManager CurrencyManager = new CurrencyManager();
            var griddata = CurrencyManager.GetccList();
            List<CurrencyConversionModel> data = new List<CurrencyConversionModel>();
            foreach(var i in griddata)
            {
                CurrencyConversionModel model = new CurrencyConversionModel();
                model.ConversionValue = i.conversionvalue;
                model.FromDate = i.fromdate;
                model.ToDate = i.todate;
                model.PrimaryCurrency1 = i.primarycurrency;
                model.Id = i.id;
                model.IsActive = i.isactive;
                model.SecondaryCurrency1 = i.secondarycurrency;
                data.Add(model);
            }
            var totalCount = data.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            data = data.OrderByDescending(d => d.Id)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPage = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            return PartialView("~/Views/CurrencyConversion/Partial/CurrencyConversionGrid.cshtml", data);
        }
        [HttpGet]
        public ActionResult CurrencyConversionDetails()
        {
            return View("~/Views/CurrencyConversion/Partial/CurrencyConversionDetails.cshtml");
        }
        [HttpGet]
        public ActionResult CurrencypageEdit( int Id)
        {
            CurrencyManager currencyManager = new CurrencyManager();
            CurrencyConversionModel model = new CurrencyConversionModel();
            var Getcurrecydata = currencyManager.Getcurrencyid(Id);
            model.PrimaryCurrency = Getcurrecydata.PrimaryCurrency;
            model.SecondaryCurrency= Getcurrecydata.SecondaryCurrency;
            model.ConversionValue = Getcurrecydata.ConversionValue;
            model.FromDate = Getcurrecydata.FromDate;
            model.ToDate = Getcurrecydata.ToDate;
            model.Id = Getcurrecydata.Id;
            return PartialView("~/Views/CurrencyConversion/Partial/CurrencyConversionDetails.cshtml", model);
        }
        #endregion
        #region crud operation
  
        [HttpPost]
        public ActionResult CurrencyConversionPost(CurrencyConversionModel model)
        {
            CurrencyConversion currencyConversion = new CurrencyConversion();
            CurrencyManager currencyManager = new CurrencyManager();
            string Altermessage = "";
            if (model.Id == 0)
            {
                currencyConversion.PrimaryCurrency = model.PrimaryCurrency;
                currencyConversion.SecondaryCurrency = model.SecondaryCurrency;
                currencyConversion.ConversionValue = model.ConversionValue;
                currencyConversion.FromDate = model.FromDate;
                currencyConversion.ToDate = model.ToDate;
                currencyManager.POST(currencyConversion);
                Altermessage = "success";
            }
            else
            {
                currencyConversion.Id = model.Id;
                currencyConversion.PrimaryCurrency = model.PrimaryCurrency;
                currencyConversion.SecondaryCurrency = model.SecondaryCurrency;
                currencyConversion.ConversionValue = model.ConversionValue;
                currencyConversion.FromDate = model.FromDate;
                currencyConversion.ToDate = model.ToDate;
                currencyManager.Put(currencyConversion);
                Altermessage = "updated";
            }
            return Json(Altermessage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult currencystatus(int CurrencyId, bool IsChecked)
        {
            string status = "";
            CurrencyManager CurrencyManager = new CurrencyManager();
            var Currencylist = CurrencyManager.Getcurrencyid(CurrencyId);
            if (Currencylist != null)
            {
                if (IsChecked == true)
                {
                    status = "Success";
                }
                else
                {
                    status = "failer";
                }
                CurrencyManager.currencystatuschange(CurrencyId, IsChecked);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region filteration

        public ActionResult searchs(int Secondarycurr, int Primarycurr)
        {
            CurrencyManager CurrencyManager = new CurrencyManager();
            List<CurrencyConversionModel> totaldata = new List<CurrencyConversionModel>();
            var totallist = CurrencyManager.GetccList();
            var filteredList = (Primarycurr == 0 || Secondarycurr == 0)
                ? CurrencyManager.GetccList()
                : totallist.Where(i => i.primaryid == Primarycurr && i.secondaryid == Secondarycurr);

            foreach (var i in filteredList)
            {
                CurrencyConversionModel model = new CurrencyConversionModel();
                model.ConversionValue = i.conversionvalue;
                model.FromDate = i.fromdate;
                model.ToDate = i.todate;
                model.PrimaryCurrency1 = i.primarycurrency;
                model.Id = i.id;
                model.IsActive = i.isactive;
                model.SecondaryCurrency1 = i.secondarycurrency;
                totaldata.Add(model);
            }
            var salesorderdetails = totaldata.OrderByDescending(m => m.Id);
            return Json(salesorderdetails, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
