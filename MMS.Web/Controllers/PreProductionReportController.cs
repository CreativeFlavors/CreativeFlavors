using MMS.Repository.Managers.StockManager;
using MMS.Repository.Managers;
using MMS.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Web.Models.Temp_Production;
using MMS.Data.StoredProcedureModel;

namespace MMS.Web.Controllers
{
    public class PreProductionReportController : Controller
    {
        public ActionResult PreProductionReportMaster()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PreProductionReportHeaderGrid(int page = 1, int pageSize = 2)
        {
            try
            {
                Temp_productionManager temp_ProductionManager = new Temp_productionManager();
                BuyerManager buyerManager = new BuyerManager();
                Product_TypeManger product_TypeManger = new Product_TypeManger();
                ProductionManager productionManager = new ProductionManager();
                ProductManager productManager = new ProductManager();
                // Fetch production data
                var productions = (from tp in temp_ProductionManager.Getmaterial()
                                   join p in productManager.Get() on tp.ProductId equals p.ProductId
                                   join b in buyerManager.Get() on tp.BuyerId equals b.BuyerMasterId
                                   join pt in product_TypeManger.Get() on p.ProductType equals pt.ProductTypeID
                                   group new { tp, p, b, pt } by new { tp.SalesOrderId, tp.ProductId } into grouped

                                   select new Temp_ProductionModel
                                   {
                                       SalesOrderId = grouped.Key.SalesOrderId,
                                       BuyerName = grouped.First().b.BuyerFullName,
                                       ProductName = grouped.First().p.ProductName,
                                       ProductItem = grouped.First().tp.ProductItem,
                                       ProductTypeName = grouped.First().pt.ProductTypeName,
                                       ProductionType = grouped.First().pt.ProductTypeName == "Production Finished Goods" ? "Production" :
                                                        grouped.First().pt.ProductTypeName == "Production Sub Assembly" ? "SubAssembly" : "Unknown"
                                   }).ToList();

                var totalCount = productions.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                int startIndex = (page - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
                productions = productions.OrderByDescending(d => d.Id)
                                         .Skip(startIndex)
                                         .Take(pageSize)
                                         .ToList();

                ViewBag.Productions = productions;
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;

                return PartialView("~/Views/PreProductionReport/Partial/PreProductionReportHeaderGrid.cshtml");
            }
            catch (Exception ex)
            {
                return PartialView("partial/Error");
            }
        }

        [HttpGet]
        public ActionResult PreProductionReportDetailsGrid(int page = 1, int pageSize = 4)
        {
            try
            {
                Temp_productionManager temp_ProductionManager = new Temp_productionManager();
                BuyerManager buyerManager = new BuyerManager();
                Product_TypeManger product_TypeManger = new Product_TypeManger();
                ProductionManager productionManager = new ProductionManager();
                ProductManager productManager = new ProductManager();
                // Fetch production data
                var productions = (from tp in temp_ProductionManager.Getmaterial()
                                   join p in productManager.Get() on tp.MaterialId equals p.ProductId
                                   join b in buyerManager.Get() on tp.BuyerId equals b.BuyerMasterId
                                   join pt in product_TypeManger.Get() on p.ProductType equals pt.ProductTypeID

                                   select new Temp_ProductionModel
                                   {
                                       SalesOrderId = tp.SalesOrderId,
                                       BuyerName = b.BuyerFullName,
                                       ProductName = p.ProductName,
                                       ProductTypeName = pt.ProductTypeName

                                   }).ToList();

                var totalCount = productions.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                int startIndex = (page - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
                productions = productions.OrderByDescending(d => d.Id)
                                         .Skip(startIndex)
                                         .Take(pageSize)
                                         .ToList();

                ViewBag.Productions = productions;
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;

                return PartialView("~/Views/PreProductionReport/Partial/PreProductionReportDetailsGrid.cshtml");
            }
            catch (Exception ex)
            {
                return PartialView("partial/Error");
            }
        }

        [HttpGet]
        public ActionResult PreProductionReportSearch(string filter)
        {
            try
            {
                Temp_productionManager temp_ProductionManager = new Temp_productionManager();
                BuyerManager buyerManager = new BuyerManager();
                Product_TypeManger product_TypeManger = new Product_TypeManger();
                ProductionManager productionManager = new ProductionManager();
                ProductManager productManager = new ProductManager();
                var productions = (from tp in temp_ProductionManager.Getmaterial()
                                   join p in productManager.Get() on tp.ProductId equals p.ProductId
                                   join b in buyerManager.Get() on tp.BuyerId equals b.BuyerMasterId
                                   join pt in product_TypeManger.Get() on p.ProductType equals pt.ProductTypeID
                                   group new { tp, p, b, pt } by new { tp.SalesOrderId, tp.ProductId } into grouped

                                   select new Temp_ProductionModel
                                   {
                                       SalesOrderId = grouped.Key.SalesOrderId,
                                       BuyerName = grouped.First().b.BuyerFullName,
                                       ProductName = grouped.First().p.ProductName,
                                       ProductItem = grouped.First().tp.ProductItem,
                                       ProductTypeName = grouped.First().pt.ProductTypeName,
                                       ProductionType = grouped.First().pt.ProductTypeName == "Production Finished Goods" ? "Production" :
                                                        grouped.First().pt.ProductTypeName == "Production Sub Assembly" ? "SubAssembly" : "Unknown"
                                   }).ToList();
                List<Temp_ProductionModel> temp_ProductionModels = new List<Temp_ProductionModel>();
                temp_ProductionModels = productions.Where(x =>
                        x.BuyerName.ToLower().Contains(filter.ToLower()) ||
                        x.ProductName.ToLower().Contains(filter.ToLower()) ||
                        x.SalesOrderId.ToString().Contains(filter))
                    .ToList();
                return Json(temp_ProductionModels, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
