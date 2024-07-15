using MMS.Repository.Managers.StockManager;
using MMS.Repository.Managers;
using MMS.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Web.Models.FinishedGoods;

namespace MMS.Web.Controllers
{
    public class FinishedGoodReportController : Controller
    {
       public ActionResult FinishedGoodMaster()
        {
            return View();
        }
        public ActionResult FinishedGoodGrid(int page = 1, int pageSize = 15)
        {
            FinishedGoodManager finishedgoodrep = new FinishedGoodManager();
            var totalist = finishedgoodrep.Fineshedgoods_Report();
            List<FinishedGoodModel> totaldata= new List<FinishedGoodModel>();
            foreach(var i in totalist)
            {
                FinishedGoodModel model = new FinishedGoodModel();
                model.ProductName = i.ProductName;
                model.ProductCode = i.ProductCode;
                model.Price = i.Price;
                model.Quantity = i.Quantity;
                model.Cost = i.Costs;
                model.ProductTypeName = i.ProductTypeName;
                model.StoreName = i.StoreName;
                model.ShortUnitName = i.ShortUnitName;
                totaldata.Add(model);
            }
            var totalCount = totaldata.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totaldata = totaldata.OrderByDescending(d => d.ProductName)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            return PartialView("~/Views/FinishedGoodReport/Partial/FinishedGoodGrid.cshtml", totaldata);
        }
        [HttpGet]
        public ActionResult ReportSearch(string filter)
        {
            List<FinishedGoodModel> totaldata = new List<FinishedGoodModel>();
            FinishedGoodManager FinishedGoodManager = new FinishedGoodManager();
            var data = FinishedGoodManager.Fineshedgoods_Report();
            var list = data.Where(x => x.ProductName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            var detailslist = list;
            foreach (var i in detailslist)
            {
                FinishedGoodModel model = new FinishedGoodModel();
                model.ProductName = i.ProductName;
                model.ProductCode = i.ProductCode;
                model.Price = i.Price;
                model.Quantity = i.Quantity;
                model.Cost = i.Costs;
                model.ProductTypeName = i.ProductTypeName;
                model.StoreName = i.StoreName;
                model.ShortUnitName = i.ShortUnitName;
                totaldata.Add(model);
            }


            return Json(totaldata, JsonRequestBehavior.AllowGet);
        }
    }
}
