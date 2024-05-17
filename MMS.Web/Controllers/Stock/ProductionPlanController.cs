using MMS.Common;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Stock
{
    [CustomFilter]
    public class ProductionPlanController : Controller
    {
        //
        // GET: /ProductionPlan/

        #region Production Plan View

        [HttpGet]
        public ActionResult ProductionPlan()
        {
            ProductionPlanModel model = new ProductionPlanModel();
            return View("~/Views/Stock/ProductionPlan/ProductionPlan.cshtml", model);
        }

        public ActionResult ProductionPlanGrid()
        {
            ProductionPlanModel model = new ProductionPlanModel();
            ProductionPlanManager productionPlanManager = new ProductionPlanManager();
            model.productionPlanningList = productionPlanManager.Get();
            return PartialView("~/Views/Stock/ProductionPlan/Partial/ProductionPlanGrid.cshtml", model);
        }

        [HttpPost]
        public ActionResult ProductionPlanDetails(int ProductionPlanId)
        {
            ProductionPlanning productionPlanning = new ProductionPlanning();
            ProductionPlanModel model = new ProductionPlanModel();
            ProductionPlanManager productionPlanManager = new ProductionPlanManager();

            productionPlanning = productionPlanManager.GetProductionPlanId(ProductionPlanId);
            if (productionPlanning.ProductionPlanId != 0)
            {
                model.ProductionPlanId = productionPlanning.ProductionPlanId;
                model.PlanNo = productionPlanning.PlanNo;
                model.CustomerName = productionPlanning.CustomerName;
                model.WeekPlan = productionPlanning.WeekPlan;
                model.From = Convert.ToDateTime(productionPlanning.From).Date.ToString("dd/MM/yyyy");
                model.To = Convert.ToDateTime(productionPlanning.To).Date.ToString("dd/MM/yyyy");
                model.InHouseCapacity = productionPlanning.InHouseCapacity;
                model.OrderQty = productionPlanning.OrderQty;
                model.PlanQty = productionPlanning.PlanQty;
                model.MultipleIO = "";
               // model.DisplayIO = productionPlanning.DisplayIO;
                model.DisplayIO = "";
                model.ShipDate = Convert.ToDateTime(productionPlanning.ShipDate).Date.ToString("dd/MM/yyyy");
                model.ShipTo = Convert.ToDateTime(productionPlanning.ShipTo).Date.ToString("dd/MM/yyyy");
                model.IsStyleDurea = productionPlanning.IsStyleDurea;
                model.IsStyle = productionPlanning.IsStyle;
                model.IsSelectAll = productionPlanning.IsSelectAll;
                model.Remarks = productionPlanning.Remarks;
                model.PlanQtyInMultiples = productionPlanning.PlanQtyInMultiples;
                model.IsAllocateStyleProp = productionPlanning.IsAllocateStyleProp;
                model.IsAllocateSizeProp = productionPlanning.IsAllocateSizeProp;
                model.CreatedDate = productionPlanning.CreatedDate.Value;
                model.UpdatedDate = DateTime.Now;
            }

            return PartialView("~/Views/Stock/ProductionPlan/Partial/ProductionPlanDetails.cshtml", model);
        }

        #endregion

        #region Crud Operations

        [HttpPost]
        public ActionResult ProductionPlan(ProductionPlanModel model)
        {
            ProductionPlanning productionPlanning = new ProductionPlanning();
            if (ModelState.IsValid)
            {
                ProductionPlanManager productionPlanManager = new ProductionPlanManager();

                productionPlanning.PlanNo = model.PlanNo;
                productionPlanning.CustomerName = model.CustomerName;
                productionPlanning.WeekPlan = model.WeekPlan;
                var format = "dd/MM/yyyy";
                DateTime Planfrom = DateTime.ParseExact(model.From, format, CultureInfo.InvariantCulture);
                DateTime Planto = DateTime.ParseExact(model.To, format, CultureInfo.InvariantCulture);
                productionPlanning.From = Planfrom.Date;
                productionPlanning.To = Planto.Date;
                productionPlanning.InHouseCapacity = model.InHouseCapacity;
                productionPlanning.OrderQty = model.OrderQty;
                productionPlanning.PlanQty = model.PlanQty;
                //productionPlanning.MultipleIO = "";
                //productionPlanning.DisplayIO = model.DisplayIO;
                DateTime Planshipdate = DateTime.ParseExact(model.ShipDate, format, CultureInfo.InvariantCulture);
                DateTime Planshipto = DateTime.ParseExact(model.ShipTo, format, CultureInfo.InvariantCulture);
                productionPlanning.ShipDate = Planshipdate.Date;
                productionPlanning.ShipTo = Planshipto.Date;
                productionPlanning.IsStyleDurea = model.IsStyleDurea;
                productionPlanning.IsStyle = model.IsStyle;
                productionPlanning.IsSelectAll = model.IsSelectAll;
                productionPlanning.Remarks = model.Remarks;
                productionPlanning.PlanQtyInMultiples = model.PlanQtyInMultiples;
                productionPlanning.IsAllocateStyleProp = model.IsAllocateStyleProp;
                productionPlanning.IsAllocateSizeProp = model.IsAllocateSizeProp;
                productionPlanning.CreatedDate = DateTime.Now;
                productionPlanManager.Post(productionPlanning);

            }
            return Json(productionPlanning, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(ProductionPlanModel model)
        {
            ProductionPlanning productionPlanning = new ProductionPlanning();
            if (ModelState.IsValid)
            {
                ProductionPlanning productionPlan = new ProductionPlanning();
                ProductionPlanManager productionPlanManager = new ProductionPlanManager();
                productionPlan = productionPlanManager.GetProductionPlanId(model.ProductionPlanId);
                if (productionPlan != null)
                {
                    productionPlanning.ProductionPlanId = model.ProductionPlanId;
                    productionPlanning.PlanNo = model.PlanNo;
                    productionPlanning.CustomerName = model.CustomerName;
                    productionPlanning.WeekPlan = model.WeekPlan;
                    var format = "dd/MM/yyyy";
                    DateTime Planfrom = DateTime.ParseExact(model.From, format, CultureInfo.InvariantCulture);
                    DateTime Planto = DateTime.ParseExact(model.To, format, CultureInfo.InvariantCulture);
                    productionPlanning.From = Planfrom.Date;
                    productionPlanning.To = Planto.Date;
                    productionPlanning.InHouseCapacity = model.InHouseCapacity;
                    productionPlanning.OrderQty = model.OrderQty;
                    productionPlanning.PlanQty = model.PlanQty;
                    //productionPlanning.MultipleIO = "";
                    //productionPlanning.DisplayIO = model.DisplayIO;
                    DateTime Planshipdate = DateTime.ParseExact(model.ShipDate, format, CultureInfo.InvariantCulture);
                    DateTime Planshipto = DateTime.ParseExact(model.ShipTo, format, CultureInfo.InvariantCulture);
                    productionPlanning.ShipDate = Planshipdate.Date;
                    productionPlanning.ShipTo = Planshipto.Date;
                    productionPlanning.IsStyleDurea = model.IsStyleDurea;
                    productionPlanning.IsStyle = model.IsStyle;
                    productionPlanning.IsSelectAll = model.IsSelectAll;
                    productionPlanning.Remarks = model.Remarks;
                    productionPlanning.PlanQtyInMultiples = model.PlanQtyInMultiples;
                    productionPlanning.IsAllocateStyleProp = model.IsAllocateStyleProp;
                    productionPlanning.IsAllocateSizeProp = model.IsAllocateSizeProp;
                    productionPlanning.CreatedDate = productionPlan.CreatedDate;
                    productionPlanning.UpdatedDate = DateTime.Now;

                    productionPlanManager.Put(productionPlanning);
                }
                else {
                    return Json(productionPlanning, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(productionPlanning, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int ProductionPlanId)
        {
            ProductionPlanning productionPlanning = new ProductionPlanning();
            string status = "";
            ProductionPlanManager productionPlanManager = new ProductionPlanManager();
            productionPlanning = productionPlanManager.GetProductionPlanId(ProductionPlanId);
            if (productionPlanning != null)
            {
                status = "Success";
                productionPlanManager.Delete(productionPlanning.ProductionPlanId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Helper Method

        public ActionResult Search(string filter)
        {
            List<ProductionPlanning> productionPlanninglist = new List<ProductionPlanning>();
            ProductionPlanManager productionPlanManager = new ProductionPlanManager();
            productionPlanninglist = productionPlanManager.Get();
            productionPlanninglist = productionPlanninglist.ToList();
            ProductionPlanModel model = new ProductionPlanModel();
            model.productionPlanningList = productionPlanninglist;

            return PartialView("Partial/ProductionPlanGrid", model);
        }

        #endregion
    }
}
