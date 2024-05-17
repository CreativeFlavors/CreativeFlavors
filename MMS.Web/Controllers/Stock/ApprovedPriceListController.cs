using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Stock
{
    [CustomFilter]
    public class ApprovedPriceListController : Controller
    {

        #region Accounts View

        [HttpGet]
        public ActionResult ApprovedPriceList()
        {
            ApprovedPriceListModel vm = new ApprovedPriceListModel();
            return View("~/Views/Stock/ApprovedPriceList/ApprovedPriceList.cshtml", vm);
        }
        public ActionResult ApprovedPriceListGrid()
        {
            ApprovedPriceListModel vm = new ApprovedPriceListModel();
            ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();
            vm.ApprovedPriceListMasterGridList = approvedPriceListManager.GetApprovedPriceGrid("");
            return PartialView("~/Views/Stock/ApprovedPriceList/Partial/ApprovedPriceListGrid.cshtml", vm);
        }

        public ActionResult FillMaterialName(int MaterialGroupMasterId)
        {
            List<MaterialNameMaster> materialNameMasterList = new List<MaterialNameMaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            var items = (from x in materialNameManager.Get().Where(x => x.MaterialGroupMasterId == MaterialGroupMasterId)
                         select new { MaterialDescription = x.MaterialDescription, MaterialMasterID = x.MaterialMasterID });
            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListSupplierMaster(int ListSupplierId)
        {
            ApprovedPriceListManager materialNameManager = new ApprovedPriceListManager();
            var items = materialNameManager.GetApprovedMasterId(ListSupplierId);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListApprovedPriceList(int ListSupplierId)
        {
            ApprovedPriceListManager materialNameManager = new ApprovedPriceListManager();
            var items = materialNameManager.GetID(ListSupplierId);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillColorName(int MaterialmasterId)
        {
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            ColorManager colorManager = new ColorManager();
            var items = (from x in materialNameManager.Get()
                         join y in materialManager.Get()
                          on x.MaterialMasterID equals y.MaterialName
                         join z in colorManager.Get()
                         on y.ColorMasterId equals z.ColorMasterId
                         where y.MaterialMasterId == MaterialmasterId
                         select new
                         {
                             MaterialDescription = string.Format("{0} {1} {2} {3}", x.MaterialDescription,
                                                                                             y.OptionMaterialValue, y.MaterialCode, z.Color),
                             y.MaterialCode,
                             y.MaterialMasterId,
                             y.Price,
                             z.ColorMasterId,
                             y.Uom,
                             y.UomUnit,
                             y.SizeRangeMasterId,
                             y.CurrencyMasterId
                         });          
            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();            
            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ApprovedPriceListMaterialBasedOnCategoryandGroup(int MaterialmasterId)
        {
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            MaterialManager materialManager = new MaterialManager();

            var Result = (from x in materialManager.Get()
                          join y in materialCategoryManager.Get()
                          on x.MaterialCategoryMasterId equals y.MaterialCategoryMasterId
                          join z in materialGroupManager.Get()
                          on x.MaterialGroupMasterId equals z.MaterialGroupMasterId
                          where x.MaterialMasterId == MaterialmasterId
                          select new
                          {
                              CategoryId = y.MaterialCategoryMasterId,
                              GroupId = z.MaterialGroupMasterId
                          }).FirstOrDefault();

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SupplierSuppliedMaterialPrice(int MaterialmasterId, int SupplierMasterId)
        {
            List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
            List<SupplierSuppliedMaterialPrice> supplierMaterialPrice = new List<SupplierSuppliedMaterialPrice>();
            ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();
            supplierMaterialPrice = approvedPriceListManager.SupplierSuppliedMaterialPrice(MaterialmasterId, SupplierMasterId);
            return Json(supplierMaterialPrice, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult ApprovedPriceListDetailsEdit(int ApprovedPriceID, int SupplierId)
        {
            ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            ApprovedPriceList approvedPriceList = new ApprovedPriceList();
            ApprovedPriceListModel model = new ApprovedPriceListModel();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            ColorManager colorManager = new ColorManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            UOMManager uOMManager = new UOMManager();
            approvedPriceList = approvedPriceListManager.GetApprovedPriceID(ApprovedPriceID);
            if (approvedPriceList != null)
            {
                model.ApprovedPriceID = approvedPriceList.ApprovedPriceID;
                model.SupplierId = approvedPriceList.SupplierId;
                model.Date = Convert.ToDateTime(approvedPriceList.Date).Date.ToString("dd/MM/yyyy");
                model.PriceRs = approvedPriceList.PriceRs;
                model.Uom = approvedPriceList.Uom;
                model.CategoryID = approvedPriceList.CategoryID;
                model.TaxDetails = approvedPriceList.TaxDetails;
                model.POExcessPercentage = approvedPriceList.POExcessPercentage;
                model.GroupID = approvedPriceList.GroupID;
                model.MaterialID = approvedPriceList.MaterialID;
                model.CurrencyID = approvedPriceList.CurrencyID;
                model.IsApproved = approvedPriceList.IsApproved;
                model.ApprovedBy = approvedPriceList.ApprovedBy;
                model.ColorID = approvedPriceList.ColorID;
                model.LeadTime = approvedPriceList.LeadTime;
                model.MRPMargin = approvedPriceList.MRPMargin;
                model.MRPPrice = approvedPriceList.MRPPrice;
                model.AccessibleValue = approvedPriceList.AccessibleValue;
                model.SupplierDescription = approvedPriceList.SupplierDescription;
                model.CreatedDate = approvedPriceList.CreatedDate;
                model.UpdatedDate = approvedPriceList.UpdatedDate;
                model.CreatedBy = approvedPriceList.CreatedBy;
                model.UpdatedBY = approvedPriceList.UpdatedBY;
                model.ApprovedPriceList = approvedPriceListManager.ApprovedPriceListGridBasedOnSupplierId(SupplierId);

                List<ApprovedPriceListMasterGrid> approvedPriceListGridList = new List<ApprovedPriceListMasterGrid>();
                foreach (var item in model.ApprovedPriceList)
                {
                    ApprovedPriceListMasterGrid approvedPriceListGrid = new ApprovedPriceListMasterGrid();
                    approvedPriceListGrid.AccessibleValue = item.AccessibleValue;
                    approvedPriceListGrid.GroupID = item.GroupID;
                    approvedPriceListGrid.LeadTime = item.LeadTime;
                    approvedPriceListGrid.MaterialID = item.MaterialID;
                    approvedPriceListGrid.MRPMargin = item.MRPMargin;
                    approvedPriceListGrid.IsApproved = item.IsApproved;
                    approvedPriceListGrid.ApprovedBy = item.ApprovedBy;
                    approvedPriceListGrid.POExcessPercentage = item.POExcessPercentage;
                    approvedPriceListGrid.PriceRs = item.PriceRs;
                    approvedPriceListGrid.SupplierDescription = item.SupplierDescription;
                    approvedPriceListGrid.SupplierId = item.SupplierId;
                    approvedPriceListGrid.CurrencyID = item.CurrencyID;
                    approvedPriceListGrid.TaxDetails = item.TaxDetails;
                    approvedPriceListGrid.ApprovedPriceID = item.ApprovedPriceID;
                    approvedPriceListGrid.CategoryID = item.CategoryID;
                    approvedPriceListGrid.ColorID = item.ColorID;
                    approvedPriceListGrid.MRPPrice = item.MRPPrice;
                    approvedPriceListGrid.Uom = item.Uom;
                    approvedPriceListGrid.TextDetails = item.TaxDetails.ToString();
                    approvedPriceListGrid.UnitTypeName = uOMManager.GetUomMasterId(Convert.ToInt32(approvedPriceListGrid.Uom)).ShortUnitName;
                    approvedPriceListGrid.CategoryName = materialCategoryManager.GetMaterialCategoryMaster(approvedPriceListGrid.CategoryID).CategoryName;
                    approvedPriceListGrid.GroupName = materialGroupManager.GetMaterialGroupMaster_Id(approvedPriceListGrid.GroupID).GroupName;
                    var MaterialNameList = materialManager.GetMaterialMasterId(approvedPriceListGrid.MaterialID).MaterialName;
                    approvedPriceListGrid.MaterialName = materialNameManager.GetMaterialNameMaterial(MaterialNameList.Value).MaterialDescription;
                    approvedPriceListGrid.ColourName = colorManager.GetcolorID(approvedPriceListGrid.ColorID).Color;
                    approvedPriceListGrid.TextDetails = taxTypeManager.GetTaxMasterId(Convert.ToInt32(approvedPriceListGrid.TextDetails)).TaxCode;

                    approvedPriceListGridList.Add(approvedPriceListGrid);
                }
                model.ApprovedPriceListMasterGridList = approvedPriceListGridList;
            }
            return Json(model, JsonRequestBehavior.AllowGet);            
        }





        [HttpPost]
        public ActionResult ApprovedPriceListDetails(int ApprovedPriceID, int SupplierId)
        {
            ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            ApprovedPriceList approvedPriceList = new ApprovedPriceList();
            ApprovedPriceListModel model = new ApprovedPriceListModel();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            ColorManager colorManager = new ColorManager();
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            approvedPriceList = approvedPriceListManager.GetApprovedPriceID(ApprovedPriceID);
            if (approvedPriceList != null)
            {
               
                model.ApprovedPriceList = approvedPriceListManager.ApprovedPriceListGridBasedOnSupplierId(SupplierId);

                List<ApprovedPriceListMasterGrid> approvedPriceListGridList = new List<ApprovedPriceListMasterGrid>();
                foreach (var item in model.ApprovedPriceList)
                {
                    ApprovedPriceListMasterGrid approvedPriceListGrid = new ApprovedPriceListMasterGrid();
                    approvedPriceListGrid.AccessibleValue = item.AccessibleValue;
                    approvedPriceListGrid.GroupID = item.GroupID;
                    approvedPriceListGrid.LeadTime = item.LeadTime;
                    approvedPriceListGrid.MaterialID = item.MaterialID;
                    approvedPriceListGrid.MRPMargin = item.MRPMargin;
                    approvedPriceListGrid.POExcessPercentage = item.POExcessPercentage;
                    approvedPriceListGrid.PriceRs = item.PriceRs;
                    approvedPriceListGrid.SupplierDescription = item.SupplierDescription;
                    approvedPriceListGrid.SupplierId = item.SupplierId;
                    approvedPriceListGrid.TaxDetails = item.TaxDetails;
                    approvedPriceListGrid.ApprovedPriceID = item.ApprovedPriceID;
                    approvedPriceListGrid.CategoryID = item.CategoryID;
                    approvedPriceListGrid.IsApproved = item.IsApproved;
                    approvedPriceListGrid.ApprovedBy = item.ApprovedBy;
                    approvedPriceListGrid.ColorID = item.ColorID;
                    approvedPriceListGrid.MRPPrice = item.MRPPrice;
                    approvedPriceListGrid.TextDetails = item.TaxDetails.ToString();
                    approvedPriceListGrid.CategoryName = materialCategoryManager.GetMaterialCategoryMaster(approvedPriceListGrid.CategoryID).CategoryName;
                    approvedPriceListGrid.GroupName = materialGroupManager.GetMaterialGroupMaster_Id(approvedPriceListGrid.GroupID).GroupName;
                    var MaterialNameList = materialManager.GetMaterialMasterId(approvedPriceListGrid.MaterialID).MaterialName;
                    if (MaterialNameList != null && MaterialNameList != 0)
                    {
                        approvedPriceListGrid.MaterialName = materialNameManager.GetMaterialNameMaterial(MaterialNameList.Value).MaterialDescription;
                    }

                    approvedPriceListGrid.ColourName = colorManager.GetcolorID(approvedPriceListGrid.ColorID).Color;
                    approvedPriceListGrid.TextDetails = taxTypeManager.GetTaxMasterId(Convert.ToInt32(approvedPriceListGrid.TextDetails)).TaxCode;

                    approvedPriceListGridList.Add(approvedPriceListGrid);
                }
                model.ApprovedPriceListMasterGridList = approvedPriceListGridList;
            }         
            return PartialView("~/Views/Stock/ApprovedPriceList/Partial/ApprovedPriceListDetails.cshtml", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult ApprovedPriceList(ApprovedPriceListModel model)
        {
            ApprovedPriceList approcedPriceLists = new ApprovedPriceList();
            ApprovedPriceList approcedPriceList = new ApprovedPriceList();
            if (ModelState.IsValid)
            {
                ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();
                approcedPriceLists.ApprovedPriceID = model.ApprovedPriceID;
                approcedPriceLists.SupplierId = model.SupplierId;
                var format = "dd/MM/yyyy";
                DateTime pricDate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                // DateTime pricDate = Convert.ToDateTime(Convert.ToDateTime(model.Date).ToString("dd/MM/yyyy HH:mm:ss"));
                approcedPriceLists.Date = pricDate.Date;
                approcedPriceLists.PriceRs = model.PriceRs;
                approcedPriceLists.Uom = model.Uom;
                approcedPriceLists.CategoryID = model.CategoryID;
                approcedPriceLists.TaxDetails = model.TaxDetails;
                approcedPriceLists.GroupID = model.GroupID;
                approcedPriceLists.MaterialID = model.MaterialID;
                approcedPriceLists.CurrencyID = model.CurrencyID;
                approcedPriceLists.POExcessPercentage = model.POExcessPercentage;
                approcedPriceLists.ColorID = model.ColorID;
                approcedPriceLists.LeadTime = model.LeadTime;
                approcedPriceLists.MRPMargin = model.MRPMargin;
                approcedPriceLists.MRPPrice = model.MRPPrice;
                approcedPriceList.IsApproved = model.IsApproved;
                approcedPriceLists.AccessibleValue = model.AccessibleValue;
                approcedPriceLists.SupplierDescription = model.SupplierDescription;
                approcedPriceLists.CreatedDate = DateTime.Now;
                approvedPriceListManager.Post(approcedPriceLists);
               // approvedPriceListManager.ApprovedPriceHistoryPrice(approcedPriceLists);
            }
            else
            {
                return Json(approcedPriceLists, JsonRequestBehavior.AllowGet);
            }

            return Json(approcedPriceLists, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(ApprovedPriceListModel model)
        {
            ApprovedPriceList approcedPriceLists = new ApprovedPriceList();
            MaterialMaster materialMasterList = new MaterialMaster();
            List<MaterialMaster> materialMasterLists = new List<MaterialMaster>();
            ApprovedPriceList approcedPriceList = new ApprovedPriceList();

            if (ModelState.IsValid)
            {
                ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();
                MaterialManager materialManager = new MaterialManager();
                materialMasterLists = materialManager.GetMaterialName(model.MaterialID);
                approcedPriceList = approvedPriceListManager.GetApprovedPriceID(model.ApprovedPriceID);


                if (approcedPriceList != null)
                {

                    approcedPriceLists.ApprovedPriceID = model.ApprovedPriceID;
                    approcedPriceLists.SupplierId = model.SupplierId;
                   // var format = "dd/MM/yyyy";
                    // DateTime pricDate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);

                    DateTime pricDate=Convert.ToDateTime(Convert.ToDateTime(model.Date).ToString("dd/MM/yyyy HH:mm:ss"));
                    approcedPriceLists.Date = pricDate.Date;
                    approcedPriceLists.PriceRs = model.PriceRs;
                    approcedPriceLists.CategoryID = model.CategoryID;
                    approcedPriceLists.TaxDetails = model.TaxDetails;
                    approcedPriceLists.GroupID = model.GroupID;
                    approcedPriceLists.IsApproved = model.IsApproved;
                    approcedPriceLists.MaterialID = model.MaterialID;
                    approcedPriceList.CurrencyID = model.CurrencyID;
                    approcedPriceLists.ColorID = model.ColorID;
                    approcedPriceLists.LeadTime = model.LeadTime;
                    approcedPriceLists.POExcessPercentage = model.POExcessPercentage;
                    approcedPriceLists.MRPMargin = model.MRPMargin;
                    approcedPriceLists.MRPPrice = model.MRPPrice;
                    approcedPriceLists.AccessibleValue = model.AccessibleValue;
                    approcedPriceLists.SupplierDescription = model.SupplierDescription; 
                    approvedPriceListManager.Put(approcedPriceLists);
                    approvedPriceListManager.ApprovedPriceHistoryPrice(approcedPriceLists);
                    approvedPriceListManager.Email(model.MaterialID.ToString(), model.SupplierId.ToString());
                    approvedPriceListManager.PutPrice(approcedPriceLists);



                }
                else
                {
                    return Json(approcedPriceLists, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(approcedPriceLists, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int ApprovedPriceID)
        {
            ApprovedPriceList approvedPriceList = new ApprovedPriceList();
            string status = "";
            ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();
            approvedPriceList = approvedPriceListManager.GetApprovedPriceID(ApprovedPriceID);
            if (approvedPriceList != null)
            {
                status = "Success";
                approvedPriceListManager.Delete(approvedPriceList.ApprovedPriceID);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {

            ApprovedPriceListModel vm = new ApprovedPriceListModel();
            ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();
            vm.ApprovedPriceListMasterGridList = approvedPriceListManager.GetApprovedPriceGrid(filter);
            return PartialView("~/Views/Stock/ApprovedPriceList/Partial/ApprovedPriceListGrid.cshtml", vm);
        }
        #endregion

        public string InspectionTypeMasterID()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateID();
            ID++;
            return ID.ToString();
        }
    }
}
