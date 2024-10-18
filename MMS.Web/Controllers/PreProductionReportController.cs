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
using MMS.Core.Entities;

namespace MMS.Web.Controllers
{
    public class PreProductionReportController : Controller
    {
        public ActionResult PreProductionReportMaster()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PreProductionReportGrid(int page = 1, int pageSize = 15)
        {
            try
            {
                Temp_productionManager temp_ProductionManager = new Temp_productionManager();
                BuyerMasterManager buyerManager = new BuyerMasterManager();
                Product_TypeManger product_TypeManger = new Product_TypeManger();
                ProductionManager productionManager = new ProductionManager();
                ProductManager productManager = new ProductManager();
                Temp_ProductionModel model = new Temp_ProductionModel();
                // Fetch production data
                var productions = (from tp in temp_ProductionManager.Getmaterial()
                                   join p in productManager.Get() on tp.ProductId equals p.ProductId into productGroup
                                   from p in productGroup.DefaultIfEmpty() // Left join
                                   join b in buyerManager.Get() on tp.BuyerId equals b.BuyerMasterId into buyerGroup
                                   from b in buyerGroup.DefaultIfEmpty() // Left join
                                   join pt in product_TypeManger.Get() on p.ProductType equals pt.ProductTypeID into typeGroup
                                   from pt in typeGroup.DefaultIfEmpty() // Left join
                                   group new { tp, p, b, pt } by new { tp.SalesOrderId, tp.ProductId } into grouped

                                   select new Temp_ProductionModel
                                   {
                                       SalesOrderId = grouped.Key.SalesOrderId,
                                       BuyerName = grouped.First().b != null ? grouped.First().b.CustomerName : "Unknown", // Handle potential null
                                       ProductName = grouped.First().p != null ? grouped.First().p.ProductName : "Unknown", // Handle potential null
                                       ProductItem = grouped.First().tp.ProductItem,
                                       ProductTypeName = grouped.First().pt != null ? grouped.First().pt.ProductTypeName : "Unknown", // Handle potential null
                                       ProductionType = grouped.First().pt != null && grouped.First().pt.ProductTypeName == "Production Finished Goods" ? "Production" :
                                                       grouped.First().pt != null && grouped.First().pt.ProductTypeName == "Production Sub Assembly" ? "SubAssembly" : "Unknown"
                                   }).ToList();


                var totalCount = productions.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                int startIndex = (page - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
                productions = productions.OrderByDescending(d => d.Id)
                                         .Skip(startIndex)
                                         .Take(pageSize)
                                         .ToList();
                model.temp_ProductionModel = productions;

                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = page;
               ViewBag.PageSize = pageSize;
                var productions1 = (from tp in temp_ProductionManager.Getmaterial()
                                   join p in productManager.Get() on tp.MaterialId equals p.ProductId into productGroup
                                   from p in productGroup.DefaultIfEmpty() // Left join
                                   join b in buyerManager.Get() on tp.BuyerId equals b.BuyerMasterId into buyerGroup
                                   from b in buyerGroup.DefaultIfEmpty() // Left join
                                   join pt in product_TypeManger.Get() on p.ProductType equals pt.ProductTypeID into typeGroup
                                   from pt in typeGroup.DefaultIfEmpty() // Left join

                                   select new Temp_ProductionModel
                                   {
                                       SalesOrderId = tp.SalesOrderId,
                                       BuyerName = b != null ? b.CustomerName : "Unknown", // Handle potential null
                                       ProductName = p != null ? p.ProductName : "Unknown", // Handle potential null
                                       ProductTypeName = pt != null ? pt.ProductTypeName : "Unknown" // Handle potential null
                                   }).ToList();


                var totalCount1 = productions1.Count();
                int totalPages1 = (int)Math.Ceiling((double)totalCount1 / pageSize);
                int startIndex1 = (page - 1) * pageSize;
                int endIndex1 = Math.Min(startIndex1 + pageSize - 1, totalCount1 - 1);
                productions1 = productions1.OrderByDescending(d => d.Id)
                                         .Skip(startIndex1)
                                         .Take(pageSize)
                                         .ToList();
                ViewBag.TotalPages1 = totalPages1;
                ViewBag.CurrentPage1 = page;
                ViewBag.PageSize1 = totalPages1;

                model.temp_ProductionModel1 = productions1;


                return PartialView("~/Views/PreProductionReport/Partial/PreProductionReportGrid.cshtml", model);
            }
            catch (Exception ex)
            {
                return PartialView("partial/Error");
            }
        }

        [HttpGet]
        public ActionResult PreProductionReportDetailsGrid(int page = 1, int pageSize = 15)
        {
            try
            {
                Temp_productionManager temp_ProductionManager = new Temp_productionManager();
                BuyerMasterManager buyerManager = new BuyerMasterManager();
                Product_TypeManger product_TypeManger = new Product_TypeManger();
                ProductionManager productionManager = new ProductionManager();
                ProductManager productManager = new ProductManager();
                Temp_ProductionModel model = new Temp_ProductionModel();
                // Fetch production data
                var productions = (from tp in temp_ProductionManager.Getmaterial()
                                   join p in productManager.Get() on tp.MaterialId equals p.ProductId into productGroup
                                   from p in productGroup.DefaultIfEmpty() // Left join
                                   join b in buyerManager.Get() on tp.BuyerId equals b.BuyerMasterId into buyerGroup
                                   from b in buyerGroup.DefaultIfEmpty() // Left join
                                   join pt in product_TypeManger.Get() on p.ProductType equals pt.ProductTypeID into typeGroup
                                   from pt in typeGroup.DefaultIfEmpty() // Left join

                                   select new Temp_ProductionModel
                                   {
                                       SalesOrderId = tp.SalesOrderId,
                                       BuyerName = b != null ? b.CustomerName : "Unknown", // Handle potential null
                                       ProductName = p != null ? p.ProductName : "Unknown", // Handle potential null
                                       ProductTypeName = pt != null ? pt.ProductTypeName : "Unknown" // Handle potential null
                                   }).ToList();
                ;

                var totalCount = productions.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                int startIndex = (page - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
                productions = productions.OrderByDescending(d => d.Id)
                                         .Skip(startIndex)
                                         .Take(pageSize)
                .ToList();

                model.temp_ProductionModel1 = productions;
                ViewBag.TotalPages1 = totalPages;
                ViewBag.CurrentPage1 = page;
                ViewBag.PageSize1 = totalPages;

                return PartialView("~/Views/PreProductionReport/Partial/PreProductionReportDetailsGrid.cshtml", model);
            }
            catch (Exception ex)
            {
                return PartialView("partial/Error");
            }
        }
        [HttpGet]
        public ActionResult PreProductionReportHeaderGrid(int page = 1, int pageSize = 15)
        {
            try
            {
                Temp_productionManager temp_ProductionManager = new Temp_productionManager();
                BuyerMasterManager buyerManager = new BuyerMasterManager();
                Product_TypeManger product_TypeManger = new Product_TypeManger();
                ProductionManager productionManager = new ProductionManager();
                ProductManager productManager = new ProductManager();
                Temp_ProductionModel model = new Temp_ProductionModel();
                var productions = (from tp in temp_ProductionManager.Getmaterial()
                                   join p in productManager.Get() on tp.ProductId equals p.ProductId into productGroup
                                   from p in productGroup.DefaultIfEmpty() // Left join
                                   join b in buyerManager.Get() on tp.BuyerId equals b.BuyerMasterId into buyerGroup
                                   from b in buyerGroup.DefaultIfEmpty() // Left join
                                   join pt in product_TypeManger.Get() on p.ProductType equals pt.ProductTypeID into typeGroup
                                   from pt in typeGroup.DefaultIfEmpty() // Left join
                                   group new { tp, p, b, pt } by new { tp.SalesOrderId, tp.ProductId } into grouped

                                   select new Temp_ProductionModel
                                   {
                                       SalesOrderId = grouped.Key.SalesOrderId,
                                       BuyerName = grouped.First().b != null ? grouped.First().b.CustomerName : "Unknown", // Handle potential null
                                       ProductName = grouped.First().p != null ? grouped.First().p.ProductName : "Unknown", // Handle potential null
                                       ProductItem = grouped.First().tp.ProductItem,
                                       ProductTypeName = grouped.First().pt != null ? grouped.First().pt.ProductTypeName : "Unknown", // Handle potential null
                                       ProductionType = grouped.First().pt != null && grouped.First().pt.ProductTypeName == "Production Finished Goods" ? "Production" :
                                                       grouped.First().pt != null && grouped.First().pt.ProductTypeName == "Production Sub Assembly" ? "SubAssembly" : "Unknown"
                                   }).ToList();

                var totalCount = productions.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                int startIndex = (page - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
                productions = productions.OrderByDescending(d => d.Id)
                                         .Skip(startIndex)
                                         .Take(pageSize)
                                         .ToList();

                model.temp_ProductionModel = productions;
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;

                return PartialView("~/Views/PreProductionReport/Partial/PreProductionReportHeaderGrid.cshtml", model);
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
                BuyerMasterManager buyerManager = new BuyerMasterManager();
                Product_TypeManger product_TypeManger = new Product_TypeManger();
                ProductionManager productionManager = new ProductionManager();
                ProductManager productManager = new ProductManager();
                var productions = (from tp in temp_ProductionManager.Getmaterial()
                                   join p in productManager.Get() on tp.MaterialId equals p.ProductId into productGroup
                                   from p in productGroup.DefaultIfEmpty() // Left join
                                   join b in buyerManager.Get() on tp.BuyerId equals b.BuyerMasterId into buyerGroup
                                   from b in buyerGroup.DefaultIfEmpty() // Left join
                                   join pt in product_TypeManger.Get() on p.ProductType equals pt.ProductTypeID into typeGroup
                                   from pt in typeGroup.DefaultIfEmpty() // Left join

                                   select new Temp_ProductionModel
                                   {
                                       SalesOrderId = tp.SalesOrderId,
                                       BuyerName = b != null ? b.CustomerName : "Unknown", // Handle potential null
                                       ProductName = p != null ? p.ProductName : "Unknown", // Handle potential null
                                       ProductTypeName = pt != null ? pt.ProductTypeName : "Unknown" // Handle potential null
                                   }).ToList();

                List<Temp_ProductionModel> temp_ProductionModels = new List<Temp_ProductionModel>();
                temp_ProductionModels = productions.Where(x =>
                        x.ProductName.ToLower().Contains(filter.ToLower())
                     )
                    .ToList();
                return Json(temp_ProductionModels, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult PreProductionReportHeaderSearch(string filter)
        {
            try
            {
                Temp_productionManager temp_ProductionManager = new Temp_productionManager();
                BuyerMasterManager buyerManager = new BuyerMasterManager();
                Product_TypeManger product_TypeManger = new Product_TypeManger();
                ProductionManager productionManager = new ProductionManager();
                ProductManager productManager = new ProductManager();
                var productions = (from tp in temp_ProductionManager.Getmaterial()
                                   join p in productManager.Get() on tp.ProductId equals p.ProductId into productGroup
                                   from p in productGroup.DefaultIfEmpty() // Left join
                                   join b in buyerManager.Get() on tp.BuyerId equals b.BuyerMasterId into buyerGroup
                                   from b in buyerGroup.DefaultIfEmpty() // Left join
                                   join pt in product_TypeManger.Get() on p.ProductType equals pt.ProductTypeID into typeGroup
                                   from pt in typeGroup.DefaultIfEmpty() // Left join
                                   group new { tp, p, b, pt } by new { tp.SalesOrderId, tp.ProductId } into grouped

                                   select new Temp_ProductionModel
                                   {
                                       SalesOrderId = grouped.Key.SalesOrderId,
                                       BuyerName = grouped.First().b != null ? grouped.First().b.CustomerName : "Unknown", // Handle potential null
                                       ProductName = grouped.First().p != null ? grouped.First().p.ProductName : "Unknown", // Handle potential null
                                       ProductItem = grouped.First().tp.ProductItem,
                                       ProductTypeName = grouped.First().pt != null ? grouped.First().pt.ProductTypeName : "Unknown", // Handle potential null
                                       ProductionType = grouped.First().pt != null && grouped.First().pt.ProductTypeName == "Production Finished Goods" ? "Production" :
                                                       grouped.First().pt != null && grouped.First().pt.ProductTypeName == "Production Sub Assembly" ? "SubAssembly" : "Unknown"
                                   }).ToList();
                List<Temp_ProductionModel> temp_ProductionModels = new List<Temp_ProductionModel>();
                temp_ProductionModels = productions.Where(x =>
                        x.ProductName.ToLower().Contains(filter.ToLower())
                     )
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
