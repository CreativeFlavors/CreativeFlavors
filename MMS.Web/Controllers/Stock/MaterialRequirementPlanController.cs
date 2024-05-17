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
    public class MaterialRequirementPlanController : Controller
    {
        //
        // GET: /MaterialRequirementPlan/

        #region Material Requirement Planning View

        [HttpGet]
        public ActionResult MaterialRequirementPlan()
        {
            MaterialRequirementPlanModel model = new MaterialRequirementPlanModel();
            return View("~/Views/Stock/MaterialRequirementPlan/MaterialRequirementPlan.cshtml", model);
        }

        public ActionResult MaterialRequirementPlanGrid()
        {
            MaterialRequirementPlanModel model = new MaterialRequirementPlanModel();
            MaterialRequirementPlanManager materialRequirementPlanManager = new MaterialRequirementPlanManager();
            model.materialRequirementPlanningList = materialRequirementPlanManager.Get();

            return PartialView("~/Views/Stock/MaterialRequirementPlan/Partial/MaterialRequirementPlanGrid.cshtml", model);
        }

        [HttpPost]
        public ActionResult MaterialRequirementPlanDetails(int MaterialRequirementPlanId)
        {
            MaterialRequirementPlanning materialRequirementPlan = new MaterialRequirementPlanning();
            MaterialRequirementPlanModel model = new MaterialRequirementPlanModel();
            MaterialRequirementPlanManager materialRequirementPlanManager = new MaterialRequirementPlanManager();
            materialRequirementPlan = materialRequirementPlanManager.GetMaterialReqPlanId(MaterialRequirementPlanId);
            if (materialRequirementPlan.MaterialRequirementPlanId != 0)
            {
                model.MaterialRequirementPlanId = materialRequirementPlan.MaterialRequirementPlanId;
                model.IsProductionPlanBasis = materialRequirementPlan.IsProductionPlanBasis;
                model.IsOrderBasis = materialRequirementPlan.IsOrderBasis;
                model.IsRejectionOrReplacement = materialRequirementPlan.IsRejectionOrReplacement;
                model.IsOverConsumption = materialRequirementPlan.IsOverConsumption;
                model.MrpNo = materialRequirementPlan.MrpNo;
                model.Buyer = materialRequirementPlan.Buyer;
                model.SizeRangeMasterId = materialRequirementPlan.SizeRangeMasterId;
                model.Date = Convert.ToDateTime(materialRequirementPlan.Date).Date.ToString("dd/MM/yyyy");
                model.MrpType = materialRequirementPlan.MrpType;
                model.WeekNo = materialRequirementPlan.WeekNo;
                model.SeasonMasterId = materialRequirementPlan.SeasonMasterId;
                model.LotNo = materialRequirementPlan.LotNo;
                model.FROM = Convert.ToDateTime(materialRequirementPlan.FROM).Date.ToString("dd/MM/yyyy");
                model.TO = Convert.ToDateTime(materialRequirementPlan.TO).Date.ToString("dd/MM/yyyy");
                model.CustPlan = materialRequirementPlan.CustPlan;
                model.ListOfCategory = materialRequirementPlan.ListOfCategory;
                model.ListOfGroup = materialRequirementPlan.ListOfGroup;
                model.IsCritical = materialRequirementPlan.IsCritical;
                model.IsCustomerSupplied = materialRequirementPlan.IsCustomerSupplied;
                model.IsGeneral = materialRequirementPlan.IsGeneral;
                model.IsImported = materialRequirementPlan.IsImported;
                model.IsReOrder = materialRequirementPlan.IsReOrder;
                model.IsSelectAll = materialRequirementPlan.IsSelectAll;
                model.IsBalToOrder = materialRequirementPlan.IsBalToOrder;
                model.MaterialList = materialRequirementPlan.MaterialList;
                model.TotalNoOfIos = materialRequirementPlan.TotalNoOfIos;
                model.TotalNoOfPrs = materialRequirementPlan.TotalNoOfPrs;
                model.ETA = materialRequirementPlan.ETA;
                model.Weight = materialRequirementPlan.Weight;
                model.ShinkagePercent = materialRequirementPlan.ShinkagePercent;
                model.WastagePercent = materialRequirementPlan.WastagePercent;
                model.ShortagePercent = materialRequirementPlan.ShortagePercent;
                model.Category = materialRequirementPlan.Category;
                model.Material = materialRequirementPlan.Material;
                model.BomQty = materialRequirementPlan.BomQty;
                model.ConversionTable = materialRequirementPlan.ConversionTable;
                model.Wastage = materialRequirementPlan.Wastage;
                model.IsDetailed = materialRequirementPlan.IsDetailed;
                model.IsConsolidated = materialRequirementPlan.IsConsolidated;
                model.MultipleIO = "";
                model.DisplayIO = materialRequirementPlan.DisplayIO;
                model.CreatedDate = materialRequirementPlan.CreatedDate.Value;
                model.UpdatedDate = DateTime.Now;
                model.IsConversionInhouse = materialRequirementPlan.IsConversionInhouse;
            }
            return PartialView("~/Views/Stock/MaterialRequirementPlan/Partial/MaterialRequirementPlanDetails.cshtml", model);
        }

        #endregion

        #region Crud Operations

        [HttpPost]
        public ActionResult MaterialRequirementPlan(MaterialRequirementPlanModel model)
        {
            MaterialRequirementPlanning materialRequirementPlan = new MaterialRequirementPlanning();
            MaterialRequirementPlanManager materialRequirementPlanManager = new MaterialRequirementPlanManager();

            materialRequirementPlan.IsProductionPlanBasis = model.IsProductionPlanBasis;
            materialRequirementPlan.IsOrderBasis = model.IsOrderBasis;
            materialRequirementPlan.IsRejectionOrReplacement = model.IsRejectionOrReplacement;
            materialRequirementPlan.IsOverConsumption = model.IsOverConsumption;
            materialRequirementPlan.MrpNo = model.MrpNo;
            materialRequirementPlan.Buyer = model.Buyer;
            materialRequirementPlan.SizeRangeMasterId = model.SizeRangeMasterId;
            var format = "dd/MM/yyyy";
            DateTime MRPdate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
            materialRequirementPlan.Date = MRPdate.Date;
            materialRequirementPlan.MrpType = model.MrpType;
            materialRequirementPlan.WeekNo = model.WeekNo;
            materialRequirementPlan.SeasonMasterId = model.SeasonMasterId;
            materialRequirementPlan.LotNo = model.LotNo;
            DateTime MRPfrom = DateTime.ParseExact(model.FROM, format, CultureInfo.InvariantCulture);
            DateTime MRPto = DateTime.ParseExact(model.TO, format, CultureInfo.InvariantCulture);
            materialRequirementPlan.FROM = MRPfrom.Date;
            materialRequirementPlan.TO = MRPto.Date;
            materialRequirementPlan.CustPlan = model.CustPlan;
            materialRequirementPlan.ListOfCategory = model.ListOfCategory;
            materialRequirementPlan.ListOfGroup = model.ListOfGroup;
            materialRequirementPlan.IsCritical = model.IsCritical;
            materialRequirementPlan.IsCustomerSupplied = model.IsCustomerSupplied;
            materialRequirementPlan.IsGeneral = model.IsGeneral;
            materialRequirementPlan.IsImported = model.IsImported;
            materialRequirementPlan.IsReOrder = model.IsReOrder;
            materialRequirementPlan.IsSelectAll = model.IsSelectAll;
            materialRequirementPlan.IsBalToOrder = model.IsBalToOrder;
            materialRequirementPlan.MaterialList = model.MaterialList;
            materialRequirementPlan.TotalNoOfIos = model.TotalNoOfIos;
            materialRequirementPlan.TotalNoOfPrs = model.TotalNoOfPrs;
            materialRequirementPlan.ETA = model.ETA;
            materialRequirementPlan.Weight = model.Weight;
            materialRequirementPlan.ShinkagePercent = model.ShinkagePercent;
            materialRequirementPlan.WastagePercent = model.WastagePercent;
            materialRequirementPlan.ShortagePercent = model.ShortagePercent;
            materialRequirementPlan.Category = model.Category;
            materialRequirementPlan.Material = model.Material;
            materialRequirementPlan.BomQty = model.BomQty;
            materialRequirementPlan.ConversionTable = model.ConversionTable;
            materialRequirementPlan.Wastage = model.Wastage;
            materialRequirementPlan.IsDetailed = model.IsDetailed;
            materialRequirementPlan.IsConsolidated = model.IsConsolidated;
            materialRequirementPlan.MultipleIO = "";
            materialRequirementPlan.DisplayIO = model.DisplayIO;
            materialRequirementPlan.CreatedDate = DateTime.Now;
            materialRequirementPlan.IsConversionInhouse = model.IsConversionInhouse;

            materialRequirementPlanManager.Post(materialRequirementPlan);

            return Json(materialRequirementPlan, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(MaterialRequirementPlanModel model)
        {
            MaterialRequirementPlanning materialRequirementPlanning = new MaterialRequirementPlanning();
            if (ModelState.IsValid)
            {
                MaterialRequirementPlanning materialReq = new MaterialRequirementPlanning();
                MaterialRequirementPlanManager materialRequirementPlanManager = new MaterialRequirementPlanManager();
                materialReq = materialRequirementPlanManager.GetMaterialReqPlanId(model.MaterialRequirementPlanId);

                if (materialReq != null)
                {
                    materialRequirementPlanning.MaterialRequirementPlanId = model.MaterialRequirementPlanId;
                    materialRequirementPlanning.IsProductionPlanBasis = model.IsProductionPlanBasis;
                    materialRequirementPlanning.IsOrderBasis = model.IsOrderBasis;
                    materialRequirementPlanning.IsRejectionOrReplacement = model.IsRejectionOrReplacement;
                    materialRequirementPlanning.IsOverConsumption = model.IsOverConsumption;
                    materialRequirementPlanning.MrpNo = model.MrpNo;
                    materialRequirementPlanning.Buyer = model.Buyer;
                    materialRequirementPlanning.SizeRangeMasterId = model.SizeRangeMasterId;
                    var format = "dd/MM/yyyy";
                    DateTime MRPdate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                    materialRequirementPlanning.Date = MRPdate.Date;
                    materialRequirementPlanning.MrpType = model.MrpType;
                    materialRequirementPlanning.WeekNo = model.WeekNo;
                    materialRequirementPlanning.SeasonMasterId = model.SeasonMasterId;
                    materialRequirementPlanning.LotNo = model.LotNo;
                    DateTime MRPfrom = DateTime.ParseExact(model.FROM, format, CultureInfo.InvariantCulture);
                    DateTime MRPto = DateTime.ParseExact(model.TO, format, CultureInfo.InvariantCulture);
                    materialRequirementPlanning.FROM = MRPfrom.Date;
                    materialRequirementPlanning.TO = MRPto.Date;
                    materialRequirementPlanning.CustPlan = model.CustPlan;
                    materialRequirementPlanning.ListOfCategory = model.ListOfCategory;
                    materialRequirementPlanning.ListOfGroup = model.ListOfGroup;
                    materialRequirementPlanning.IsCritical = model.IsCritical;
                    materialRequirementPlanning.IsCustomerSupplied = model.IsCustomerSupplied;
                    materialRequirementPlanning.IsGeneral = model.IsGeneral;
                    materialRequirementPlanning.IsImported = model.IsImported;
                    materialRequirementPlanning.IsReOrder = model.IsReOrder;
                    materialRequirementPlanning.IsSelectAll = model.IsSelectAll;
                    materialRequirementPlanning.IsBalToOrder = model.IsBalToOrder;
                    materialRequirementPlanning.MaterialList = model.MaterialList;
                    materialRequirementPlanning.TotalNoOfIos = model.TotalNoOfIos;
                    materialRequirementPlanning.TotalNoOfPrs = model.TotalNoOfPrs;
                    materialRequirementPlanning.ETA = model.ETA;
                    materialRequirementPlanning.Weight = model.Weight;
                    materialRequirementPlanning.ShinkagePercent = model.ShinkagePercent;
                    materialRequirementPlanning.WastagePercent = model.WastagePercent;
                    materialRequirementPlanning.ShortagePercent = model.ShortagePercent;
                    materialRequirementPlanning.Category = model.Category;
                    materialRequirementPlanning.Material = model.Material;
                    materialRequirementPlanning.BomQty = model.BomQty;
                    materialRequirementPlanning.ConversionTable = model.ConversionTable;
                    materialRequirementPlanning.Wastage = model.Wastage;
                    materialRequirementPlanning.IsDetailed = model.IsDetailed;
                    materialRequirementPlanning.IsConsolidated = model.IsConsolidated;
                    materialRequirementPlanning.MultipleIO = "";
                    materialRequirementPlanning.DisplayIO = model.DisplayIO;
                    materialRequirementPlanning.CreatedDate = materialReq.CreatedDate;
                    materialRequirementPlanning.UpdatedDate = DateTime.Now;
                    materialRequirementPlanning.IsConversionInhouse = model.IsConversionInhouse;

                    materialRequirementPlanManager.Put(materialRequirementPlanning);
                }
                else {
                    return Json(materialRequirementPlanning, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(materialRequirementPlanning, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int MaterialRequirementPlanId)
        {
            string status = "";
            MaterialRequirementPlanning materialRequirementPlan = new MaterialRequirementPlanning();
            MaterialRequirementPlanManager materialRequirementPlanManager = new MaterialRequirementPlanManager();
            materialRequirementPlan = materialRequirementPlanManager.GetMaterialReqPlanId(MaterialRequirementPlanId);
            if (materialRequirementPlan != null)
            {
                status = "Success";
                materialRequirementPlanManager.Delete(materialRequirementPlan.MaterialRequirementPlanId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSelectedIOVal(int MaterialRequirementPlanId)
        {
            MaterialRequirementPlanManager materialReqPlanManager = new MaterialRequirementPlanManager();
            MaterialRequirementPlanModel model = new MaterialRequirementPlanModel();
            MaterialRequirementPlanning materialRequirementPlanning = new MaterialRequirementPlanning();

            SelectList items = MMS.Web.ExtensionMethod.HtmlHelper.GetInternalOrderIONO();

            materialRequirementPlanning = materialReqPlanManager.GetMaterialReqPlanId(MaterialRequirementPlanId);
            model.MultipleIO = materialRequirementPlanning.MultipleIO;
            model.DisplayIO = materialRequirementPlanning.DisplayIO;

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Helper Method

        public ActionResult Search(string filter)
        {
            List<MaterialRequirementPlanning> materialRequirementPlanlist = new List<MaterialRequirementPlanning>();
            MaterialRequirementPlanManager materialRequirementPlanManager = new MaterialRequirementPlanManager();
            materialRequirementPlanlist = materialRequirementPlanManager.Get();
            materialRequirementPlanlist = materialRequirementPlanlist.Where(s => s.MaterialList.ToString().Trim().Contains(filter.Trim())).ToList();

            MaterialRequirementPlanModel model = new MaterialRequirementPlanModel();
            model.materialRequirementPlanningList = materialRequirementPlanlist;

            return PartialView("~/Views/Stock/MaterialRequirementPlan/Partial/MaterialRequirementPlanGrid.cshtml", model);
        }

        #endregion
    }
}
