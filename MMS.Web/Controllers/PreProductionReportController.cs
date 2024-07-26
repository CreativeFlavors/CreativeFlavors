using MMS.Repository.Managers.StockManager;
using MMS.Repository.Managers;
using MMS.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Web.Models.Temp_Production;

namespace MMS.Web.Controllers
{
    public class PreProductionReportController : Controller
    {
        public ActionResult PreProductionReportMaster()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PreProductionReportHeaderGrid(int page = 1, int pageSize = 9)
        {
            //try
            //{
            //    Temp_productionManager temp_ProductionManager = new Temp_productionManager();
            //    BuyerManager buyerManager = new BuyerManager();
            //    SalesorderHD_Manager salesorderHD_manager = new SalesorderHD_Manager();
            //    Product_TypeManger product_TypeManger = new Product_TypeManger();
            //    ProductionManager productionManager = new ProductionManager();
            //    ProductManager productManager = new ProductManager();
            //    ProductionSubassemblyManager productionSubassemblyManager = new ProductionSubassemblyManager();
            //    StatusProductionManager statusProductionManager = new StatusProductionManager();
            //    StatusProductionSubassemblyManager statusProductionSubassemblyManager = new StatusProductionSubassemblyManager();
            //    //// Fetch production data
            //    //var productions = (from tp in temp_ProductionManager.Getmaterial()
            //    //                   join p in productManager.Get() on tp.ProductId equals p.ProductId
            //    //                   join b in buyerManager.Get() on tp.BuyerId equals b.BuyerMasterId
            //    //                   join pt in product_TypeManger.Get() on tp.pro

            //    //                   select new Temp_ProductionModel
            //    //                   {
            //    //                       SalesOrderId = tp.SalesOrderId,
            //    //                       BuyerId = b.BuyerFullName,
            //    //                       ProductId = p.ProductName,
            //    //                       RequiredQty = P.RequiredQty,
            //    //                       ProductName = pr.ProductName,
            //    //                       ProductionType = "Production",
            //    //                       ProductionStatus = sp.Status
            //    //                   }).ToList();
            //    //// Fetch production subassembly data
            //    var productionSubassemblies = (from psa in productionSubassemblyManager.GetProductions()
            //                                   join pr in productManager.Get() on psa.ProductId equals pr.ProductId
            //                                   join sps in statusProductionSubassemblyManager.Get() on psa.ProductionSubassemblyStatus equals sps.StatusId
            //                                   select new ProductionViewModel
            //                                   {
            //                                       ProductionId = psa.ProductionId,
            //                                       ProductionCode = psa.ProductionCode,
            //                                       ProductionQty = psa.ProductionQty,
            //                                       RequiredQty = psa.RequiredQty,
            //                                       ProductName = pr.ProductName,
            //                                       ProductionType = "Subassembly",
            //                                       ProductionStatus = sps.Status
            //                                   }).ToList();
            //    // Combine both lists
            //    productions.AddRange(productionSubassemblies);
            //    var totalCount = productions.Count();
            //    int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            //    int startIndex = (page - 1) * pageSize;
            //    int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            //    productions = productions.OrderByDescending(d => d.ProductionId)
            //                             .Skip(startIndex)
            //                             .Take(pageSize)
            //                             .ToList();

            //    ViewBag.Productions = productions;
            //    ViewBag.TotalPages = totalPages;
            //    ViewBag.CurrentPage = page;
            //    ViewBag.PageSize = pageSize;

                return PartialView("~/Views/PreProductionReport/Partial/PreProductionReportHeaderGrid.cshtml");
            }
            //catch (Exception ex)
            //{
            //    return PartialView("partial/Error");
            //}
        }

    }
//}
