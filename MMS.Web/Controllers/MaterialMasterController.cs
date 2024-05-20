
using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models;
using MMS.Web.Models.Material;
using MMS.Web.Models.StockModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Data.Context;
using MMS.Data.StoredProcedureModel;
using System.Data.SqlClient;
namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class MaterialMasterController : Controller
    {
        //
        // GET: /MaterialMaster/

        #region MaterialMaster View
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Material()
        {
            MaterialSearch mm = new MaterialSearch();
            return View(mm);
        }

        public ActionResult MaterialMasterGrid()
        {

            List<MaterialSearch> materialMaster = new List<MaterialSearch>();
            MaterialManager materialManager = new MaterialManager();
            MaterialSearch searchresult = new MaterialSearch();

            materialMaster = materialManager.GetMaterialList("");

            searchresult.MaterialSearchList = materialMaster;

            return PartialView("Partial/MaterialMasterGrid", searchresult);
        }

        [HttpPost]
        public ActionResult MaterialMasterDetails(int MaterialMasterId)
        {
            MaterialManager materialManager = new MaterialManager();
            MaterialMaster materialMaster = new MaterialMaster();
            MaterialSearch model = new MaterialSearch();
            List<SizeItemMaterial> sizeItemMaterialList = new List<SizeItemMaterial>();
            materialMaster = materialManager.GetMaterialMasterId(MaterialMasterId);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getMaterialCodeId();
            ID++;

            if (materialMaster.MaterialMasterId != 0)
            {
                model.sizeItemMaterialList = materialManager.GetSizeItemMaterial(materialMaster.MaterialMasterId);
                if (model.sizeItemMaterialList.Count > 0)
                {
                    model.sizeItemMaterialList = model.sizeItemMaterialList.OrderBy((x => Convert.ToDecimal(x.SizeRange))).ToList();
                }
                model.StoreMasterId = materialMaster.StoreMasterId;
                model.MaterialCategoryMasterId = materialMaster.MaterialCategoryMasterId;
                model.MaterialGroupMasterId = materialMaster.MaterialGroupMasterId;
                model.SubGroup = materialMaster.SubGroup;
                model.MaterialName = materialMaster.MaterialName;
                model.OptionMaterialValue = materialMaster.OptionMaterialValue;
                model.isImportCustomer = materialMaster.isImportCustomer;
                model.ColorMasterId = materialMaster.ColorMasterId;
                model.MachineryMasterId = materialMaster.MachineryMasterId;
                model.GradeMasterId = materialMaster.GradeMasterId;
                model.isImport = materialMaster.isImport;
                model.isLocal = materialMaster.isLocal;
                model.ImportPrice = materialMaster.ImportPrice;
                model.importCurrency = materialMaster.importCurrency;
                model.importManafactureName = materialMaster.importManafactureName;
                materialMaster.CuttingIssueTypeID = materialMaster.CuttingIssueTypeID;
                model.SubstanceMasterId = materialMaster.SubstanceMasterId;
                model.SameSizeForAllSize = materialMaster.SameSizeForAllSize;
                model.LeatherSizeMasterId = materialMaster.LeatherSizeMasterId;
                model.componentcheck1sds = materialMaster.componentcheck1sds;
                model.primarycheckpackage = materialMaster.primarycheckpackage;
                model.OriginMasterId = materialMaster.OriginMasterId;
                model.WeightInKg = materialMaster.WeightInKg;
                model.SizeRangeMasterId = materialMaster.SizeRangeMasterId;
                model.Decimals = materialMaster.Decimals;
                model.Uom = materialMaster.Uom;
                model.UomUnit = materialMaster.UomUnit;
                model.Price = materialMaster.Price;
                model.CurrencyMasterId = materialMaster.CurrencyMasterId;
                model.ManufacturerMasterId = materialMaster.ManufacturerMasterId;
                model.PacketType = materialMaster.PacketType;
                model.PacketTypePrice = materialMaster.PacketTypePrice;
                model.PacketCurrency = materialMaster.PacketCurrency;
                model.TaxCategory = materialMaster.TaxCategory;
                model.PrimaryPackage = materialMaster.PrimaryPackage;
                model.SecondaryPackage = materialMaster.SecondaryPackage;
                model.LeadTime = materialMaster.LeadTime;
                model.MaxRoLevel = materialMaster.MaxRoLevel;
                model.MinRoLevel = materialMaster.MinRoLevel;
                model.ReOrderLevel = materialMaster.ReOrderLevel;
                model.Expiry = materialMaster.Expiry;
                model.IsActive = materialMaster.IsActive;
                model.Barcode = materialMaster.Barcode;
                model.IsPurchase = materialMaster.IsPurchase;
                model.IsIssues = materialMaster.IsIssues;
                model.IsCustomerSupplied = materialMaster.IsCustomerSupplied;
                model.IsImportedMaterial = materialMaster.IsImportedMaterial;
                model.IsLocalMaterial = materialMaster.IsLocalMaterial;
                model.IsCriticalMaterial = materialMaster.IsCriticalMaterial;
                model.IsAllowNegativeStock = materialMaster.IsAllowNegativeStock;
                model.IsLotNumberTracking = materialMaster.IsLotNumberTracking;
                model.IsIssueStrictly = materialMaster.IsIssueStrictly;
                model.IsPrimaryPackage = materialMaster.IsPrimaryPackage;
                model.IsSecondaryPackage = materialMaster.IsSecondaryPackage;

                model.IsApplicable = materialMaster.IsApplicable;
                model.PurchasePrimary = materialMaster.PurchasePrimary;
                model.PurchasePacket = materialMaster.PurchasePacket;
                model.PrimaryUnit = materialMaster.PrimaryUnit;
                model.PacketUnit = materialMaster.PacketUnit;
                model.PacketUnitType = materialMaster.PacketUnitType;
                model.CreatedDate = materialMaster.CreatedDate.Value;
                model.UpdatedDate = DateTime.Now;
                model.MaterialMasterId = materialMaster.MaterialMasterId;
                model.ImagePath = materialMaster.ImagePath;
                model.ImportPrice = materialMaster.ImportPrice;
                model.importCurrency = materialMaster.importCurrency;
                model.importManafactureName = materialMaster.importManafactureName;
                model.isImport = materialMaster.isImport;
                model.isLocal = materialMaster.isLocal;
                model.isImportCustomer = materialMaster.isImportCustomer;
                model.CustomerImportPrice = materialMaster.CustomerImportPrice;
                model.CustomerManafactureName = materialMaster.CustomerManafactureName;
                model.CuomerImportCurrencyMasterId = materialMaster.CuomerImportCurrencyMasterId;
                model.HSNCode = materialMaster.HSNCode;
                model.BuyerItemCode = materialMaster.BuyerItemCode;


            }
            if (model.MaterialMasterId != 0)
            {
                model.MaterialCode = materialMaster.MaterialCode;
            }
            else
            {
                model.MaterialCode = "MAT" + ID;
            }

            return PartialView("Partial/MaterialMasterDetails", model);
        }
        #endregion

        #region Crud Operation
        [HttpPost]
        public ActionResult Material(MaterialMasterModel model, HttpPostedFileBase imagepath)
        {
            MaterialMaster materialMaster = new MaterialMaster();
            MaterialMaster materialMaster_ = new MaterialMaster();
            Alert alert = new Alert();
            MaterialMaster material = new MaterialMaster();
            MaterialManager materialManager = new MaterialManager();
            material = materialManager.GetMaterialExist(model.MaterialName, model.ColorMasterId.Value, !string.IsNullOrEmpty(model.OptionMaterialValue)?  model.OptionMaterialValue.Trim():"");
            if (Session["UploadImage"] != null && Session["UploadImage"].ToString() != "")
            {
                model.ImagePath = Session["UploadImage"].ToString();
            }

            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getMaterialCodeId();
            ID++;
            materialMaster.ImagePath = model.ImagePath;
            materialMaster.StoreMasterId = model.StoreMasterId;
            materialMaster.MaterialCategoryMasterId = model.MaterialCategoryMasterId;
            materialMaster.MaterialGroupMasterId = model.MaterialGroupMasterId;
            materialMaster.SubGroup = model.SubGroup;
            materialMaster.MaterialCode = model.MaterialCode;
            materialMaster.MaterialName = model.MaterialName;
            materialMaster.OptionMaterialValue = model.OptionMaterialValue;
            materialMaster.ColorMasterId = model.ColorMasterId;
            materialMaster.MachineryMasterId = model.MachineryMasterId;
            materialMaster.GradeMasterId = model.GradeMasterId;
            materialMaster.SubstanceMasterId = model.SubstanceMasterId;
            materialMaster.LeatherSizeMasterId = model.LeatherSizeMasterId;
            materialMaster.OriginMasterId = model.OriginMasterId;
            materialMaster.WeightInKg = model.WeightInKg;
            materialMaster.ImportPrice = model.ImportPrice;
            materialMaster.isImport = model.isImport;
            materialMaster.isLocal = model.isLocal;
            materialMaster.isImportCustomer = model.isImportCustomer;
            materialMaster.importManafactureName = model.importManafactureName;
            materialMaster.importCurrency = model.importCurrency;
            materialMaster.SizeRangeMasterId = model.SizeRangeMasterId;
            materialMaster.componentcheck1sds = model.componentcheck1sds;
            materialMaster.primarycheckpackage = model.primarycheckpackage;
            materialMaster.Decimals = model.Decimals;
            materialMaster.SameSizeForAllSize = model.SameSizeForAllSize;
            materialMaster.Uom = model.Uom;
            materialMaster.UomUnit = model.UomUnit;
            materialMaster.CuttingIssueTypeID = model.CuttingIssueTypeID;
            materialMaster.Price = model.Price;
            materialMaster.CurrencyMasterId = model.CurrencyMasterId;
            materialMaster.ManufacturerMasterId = model.ManufacturerMasterId;
            materialMaster.PacketType = model.PacketType;
            materialMaster.PacketTypePrice = model.PacketTypePrice;
            materialMaster.PacketCurrency = model.PacketCurrency;
            materialMaster.TaxCategory = model.TaxCategory;
            materialMaster.PrimaryPackage = model.PrimaryPackage;
            materialMaster.SecondaryPackage = model.SecondaryPackage;
            materialMaster.LeadTime = model.LeadTime;
            materialMaster.MaxRoLevel = model.MaxRoLevel;
            materialMaster.MinRoLevel = model.MinRoLevel;
            materialMaster.ReOrderLevel = model.ReOrderLevel;
            materialMaster.Expiry = model.Expiry;
            materialMaster.IsActive = model.IsActive;
            materialMaster.Barcode = model.Barcode;
            materialMaster.IsPurchase = model.IsPurchase;
            materialMaster.IsIssues = model.IsIssues;
            materialMaster.IsCustomerSupplied = model.IsCustomerSupplied;
            materialMaster.IsImportedMaterial = model.IsImportedMaterial;
            materialMaster.IsLocalMaterial = model.IsLocalMaterial;
            materialMaster.IsCriticalMaterial = model.IsCriticalMaterial;
            materialMaster.IsAllowNegativeStock = model.IsAllowNegativeStock;
            materialMaster.IsLotNumberTracking = model.IsLotNumberTracking;
            materialMaster.IsIssueStrictly = model.IsIssueStrictly;
            materialMaster.IsPrimaryPackage = model.IsPrimaryPackage;
            materialMaster.IsSecondaryPackage = model.IsSecondaryPackage;
            materialMaster.IsApplicable = model.IsApplicable;
            materialMaster.PurchasePrimary = model.PurchasePrimary;
            materialMaster.PurchasePacket = model.PurchasePacket;
            materialMaster.PrimaryUnit = model.PrimaryUnit;
            materialMaster.PacketUnit = model.PacketUnit;
            materialMaster.PacketUnitType = model.PacketUnitType;
            materialMaster.ImportPrice = model.ImportPrice;
            materialMaster.importCurrency = model.importCurrency;
            materialMaster.importManafactureName = model.importManafactureName;
            materialMaster.isImport = model.isImport;
            materialMaster.isLocal = model.isLocal;
            materialMaster.isImportCustomer = model.isImportCustomer;
            materialMaster.CustomerImportPrice = model.CustomerImportPrice;
            materialMaster.CustomerManafactureName = model.CustomerManafactureName;
            materialMaster.HSNCode = model.HSNCode;
            materialMaster.BuyerItemCode = model.BuyerItemCode;
            materialMaster.CuomerImportCurrencyMasterId = model.CuomerImportCurrencyMasterId;
            materialMaster.CreatedDate = DateTime.Now;    
            if ((material == null || material.MaterialMasterId == 0) && model.MaterialMasterId == 0)
            {
                materialMaster_ = materialManager.Post(materialMaster);
            }
            else if (material != null && model.MaterialMasterId != 0)
            {
                materialMaster.MaterialMasterId = model.MaterialMasterId;
                materialMaster_ = materialManager.Post(materialMaster);
            }
            else if (material == null && model.MaterialMasterId != 0)
            {
                materialMaster.MaterialMasterId = model.MaterialMasterId;
                materialMaster_ = materialManager.Post(materialMaster);
            }
            else if (material != null && model.MaterialMasterId == 0)
            {
                alert.Status = "Already Existed";
                return Json(alert, JsonRequestBehavior.AllowGet);
            }
            materialManager.SizeItemMaterialDelete(materialMaster_.MaterialMasterId);
            if (model.Size != null)
            {
                string[] sizeAray = model.Size.Split(',');
                string[] RateAray = model.Rate.Split(',');
                int count = 0;
                foreach (var item in sizeAray)
                {
                    SizeItemMaterial sizeItemMaterial = new SizeItemMaterial();
                    sizeItemMaterial.SizeRange = item.Trim();
                    sizeItemMaterial.Qty = 0;
                    sizeItemMaterial.Rate = Convert.ToDecimal(RateAray[count]);
                    sizeItemMaterial.MaterialMasterID = materialMaster_.MaterialMasterId;
                    sizeItemMaterial.CreatedDate = DateTime.Now;
                    materialManager.SizeItemMaterialPost(sizeItemMaterial);
                    count++;
                }
            }
       

            if (materialMaster_ != null && model.MaterialMasterId == 0)
            {
                alert.Status = "Saved";
            }
            else if (materialMaster_ != null && model.MaterialMasterId != 0)
            {
                alert.Status = "Updated";
            }
            else
            {
                alert.Status = "Failed";
            }
            return Json(alert, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(MaterialMasterModel model)
        {
            MaterialMaster materialMaster = new MaterialMaster();
            MaterialMaster material = new MaterialMaster();
            MaterialManager materialManager = new MaterialManager();
            material = materialManager.GetMaterialMasterId(model.MaterialMasterId);
            if (material != null)
            {
                if (Session["UploadImage"] != null && Session["UploadImage"].ToString() != "")
                {
                    model.ImagePath = Session["UploadImage"].ToString();
                }

                materialMaster.ImagePath = model.ImagePath;
                materialMaster.MaterialMasterId = model.MaterialMasterId;
                materialMaster.StoreMasterId = model.StoreMasterId;
                materialMaster.MaterialCategoryMasterId = model.MaterialCategoryMasterId;
                materialMaster.MaterialGroupMasterId = model.MaterialGroupMasterId;
                materialMaster.SubGroup = model.SubGroup;
                materialMaster.MaterialCode = model.MaterialCode;
                materialMaster.MaterialName = model.MaterialName;
                materialMaster.OptionMaterialValue = model.OptionMaterialValue;
                materialMaster.ColorMasterId = model.ColorMasterId;
                materialMaster.MachineryMasterId = model.MachineryMasterId;
                materialMaster.GradeMasterId = model.GradeMasterId;
                materialMaster.SubstanceMasterId = model.SubstanceMasterId;
                materialMaster.LeatherSizeMasterId = model.LeatherSizeMasterId;
                materialMaster.OriginMasterId = model.OriginMasterId;
                materialMaster.WeightInKg = model.WeightInKg;
                materialMaster.SizeRangeMasterId = model.SizeRangeMasterId;
                materialMaster.Decimals = model.Decimals;
                materialMaster.isImportCustomer = model.isImportCustomer;
                materialMaster.Uom = model.Uom;
                materialMaster.UomUnit = model.UomUnit;
                materialMaster.componentcheck1sds = model.componentcheck1sds;
                materialMaster.primarycheckpackage = model.primarycheckpackage;
                materialMaster.Price = model.Price;
                materialMaster.CurrencyMasterId = model.CurrencyMasterId;
                materialMaster.isImport = model.isImport;
                materialMaster.isLocal = model.isLocal;
                materialMaster.importManafactureName = model.importManafactureName;
                materialMaster.importCurrency = model.importCurrency;
                materialMaster.importManafactureName = model.importManafactureName;
                materialMaster.importCurrency = model.importCurrency;
                materialMaster.ManufacturerMasterId = model.ManufacturerMasterId;
                materialMaster.PacketType = model.PacketType;
                materialMaster.PacketTypePrice = model.PacketTypePrice;
                materialMaster.PacketCurrency = model.PacketCurrency;
                materialMaster.TaxCategory = model.TaxCategory;
                materialMaster.PrimaryPackage = model.PrimaryPackage;
                materialMaster.SecondaryPackage = model.SecondaryPackage;
                materialMaster.LeadTime = model.LeadTime;
                materialMaster.MaxRoLevel = model.MaxRoLevel;
                materialMaster.MinRoLevel = model.MinRoLevel;
                materialMaster.ReOrderLevel = model.ReOrderLevel;
                materialMaster.Expiry = model.Expiry;
                materialMaster.IsActive = model.IsActive;
                materialMaster.Barcode = model.Barcode;
                materialMaster.IsPurchase = model.IsPurchase;
                materialMaster.IsIssues = model.IsIssues;
                materialMaster.IsCustomerSupplied = model.IsCustomerSupplied;
                materialMaster.IsImportedMaterial = model.IsImportedMaterial;
                materialMaster.IsLocalMaterial = model.IsLocalMaterial;
                materialMaster.IsCriticalMaterial = model.IsCriticalMaterial;
                materialMaster.IsAllowNegativeStock = model.IsAllowNegativeStock;
                materialMaster.IsLotNumberTracking = model.IsLotNumberTracking;
                materialMaster.IsIssueStrictly = model.IsIssueStrictly;
                materialMaster.IsPrimaryPackage = model.IsPrimaryPackage;
                materialMaster.IsSecondaryPackage = model.IsSecondaryPackage;
                materialMaster.IsApplicable = model.IsApplicable;
                materialMaster.PurchasePrimary = model.PurchasePrimary;
                materialMaster.PurchasePacket = model.PurchasePacket;
                materialMaster.PrimaryUnit = model.PrimaryUnit;
                materialMaster.PacketUnit = model.PacketUnit;
                materialMaster.PacketUnitType = model.PacketUnitType;
                materialMaster.CuttingIssueTypeID = model.CuttingIssueTypeID;
                materialMaster.CreatedDate = material.CreatedDate;
                materialMaster.ImportPrice = model.ImportPrice;
                materialMaster.importCurrency = model.importCurrency;
                materialMaster.importManafactureName = model.importManafactureName;
                materialMaster.isImport = model.isImport;
                materialMaster.isLocal = model.isLocal;
                materialMaster.isImportCustomer = model.isImportCustomer;
                materialMaster.CustomerImportPrice = model.CustomerImportPrice;
                materialMaster.CustomerManafactureName = model.CustomerManafactureName;
                materialMaster.CuomerImportCurrencyMasterId = model.CuomerImportCurrencyMasterId;
                materialMaster.HSNCode = model.HSNCode;
                materialMaster.BuyerItemCode = model.BuyerItemCode;
                materialMaster.UpdatedDate = DateTime.Now;
                materialManager.Post(materialMaster);
            }
            else
            {
                return Json(materialMaster, JsonRequestBehavior.AllowGet);
            }


            return Json(materialMaster, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int MaterialMasterId)
        {
            MaterialMaster materialMaster = new MaterialMaster();
            string status = "";
            MaterialManager materialManager = new MaterialManager();
            materialMaster = materialManager.GetMaterialMasterId(MaterialMasterId);
            if (materialMaster != null)
            {
                status = "Success";
                materialManager.Delete(materialMaster.MaterialMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public JsonResult MaterialGroupCheckboxeails(int MaterialGroupMasterId)
        {
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            materialgroupmaster materialGroupMaster = new materialgroupmaster();
            materialGroupMaster = materialGroupManager.GetmaterialgroupmasterId(MaterialGroupMasterId);
            return Json(materialGroupMaster, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult MaterialGroup(int MaterialGroupMasterId)
        {
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            materialgroupmaster materialGroupMaster = new materialgroupmaster();
            materialGroupMaster = materialGroupManager.GetmaterialgroupmasterId(MaterialGroupMasterId);
            return Json(materialGroupMaster, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillMaterialCateogoryid(int StoreMasterId)
        {
            List<materialgroupmaster> materialGroupMasterList_ = new List<materialgroupmaster>();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            materialGroupMasterList_ = materialGroupManager.Get().Where(x => x.StoreName == StoreMasterId).ToList();
            var items = (from x in materialGroupManager.Get()
                         join y in materialCategoryManager.Get()
                          on x.MaterialCategoryMasterId equals y.MaterialCategoryMasterId
                         where x.StoreName == StoreMasterId
                         select new { CategoryName = y.CategoryName, MaterialCategoryMasterId = y.MaterialCategoryMasterId });

            var distinctList = items.GroupBy(x => x.MaterialCategoryMasterId).Select(g => g.First()).ToList();
            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult FillCateogory(string StoreMasterId)
        {
            List<materialgroupmaster> materialGroupMasterList_ = new List<materialgroupmaster>();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            int storeID = 0;
            storeID = Convert.ToInt32(StoreMasterId);
            materialGroupMasterList_ = materialGroupManager.Get().Where(x => x.StoreName == storeID).ToList();
            var items = (from x in materialGroupManager.Get()
                         join y in materialCategoryManager.Get()
                          on x.MaterialCategoryMasterId equals y.MaterialCategoryMasterId
                         where x.StoreName == storeID
                         select new { CategoryName = y.CategoryName, MaterialCategoryMasterId = y.MaterialCategoryMasterId });

            var distinctList = items.GroupBy(x => x.MaterialCategoryMasterId).Select(g => g.First()).ToList();
            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillCateogoryid(int MaterialCategoryMasterId)
        {
            List<materialgroupmaster> materialGroupMasterList_ = new List<materialgroupmaster>();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            var items = (from x in materialGroupManager.Get().Where(x => x.MaterialCategoryMasterId == MaterialCategoryMasterId)
                         select new { GroupName = x.GroupName, MaterialGroupMasterId = x.MaterialGroupMasterId });

            var distinctList = items.GroupBy(x => x.GroupName).Select(g => g.First()).ToList();

            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillGroupid(int MaterialGroupId)
        {
            List<SubGroup> subgroupList_ = new List<SubGroup>();
            SubGroupManager subGroupManager = new SubGroupManager();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();

            var items = (from x in subGroupManager.Get().Where(x => x.SubGroupID == MaterialGroupId)
                         select new { SubGroup = x.SubGroupDescription, SubGroupMasterId = x.SubGroupID });

            var distinctList = items.GroupBy(x => x.SubGroup).Select(g => g.First()).ToList();

            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillMaterialMasterNamebasedCategory(int MaterialCategoryMasterId)
        {
            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();

            MaterialManager materialManager = new MaterialManager();
            MaterialCategoryManager categoryManager = new MaterialCategoryManager();
            var items = (from x in materialManager.Get()
                         join y in materialNameManager.Get() on x.MaterialName equals y.MaterialMasterID
                         join z in categoryManager.Get() on x.MaterialCategoryMasterId equals z.MaterialCategoryMasterId
                         where x.MaterialCategoryMasterId == MaterialCategoryMasterId
                         select new
                         {
                             MaterialName = y.MaterialDescription,
                             MaterialMasterID = x.MaterialMasterId
                         });

            var distinctList = items.GroupBy(x => x.MaterialName).Select(g => g.First()).ToList();
            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillMaterialMasterNamebasedgroup(int MaterialGroupMasterId)
        {
            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();

            MaterialNameManager materialNameManager = new MaterialNameManager();

            MaterialManager materialManager = new MaterialManager();

            var items = (from x in materialNameManager.Get().Where(x => x.MaterialGroupMasterId == MaterialGroupMasterId)
                         select new { MaterialDescription = x.MaterialDescription, MaterialMasterID = x.MaterialMasterID });

            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillUOMbasedmaterialname(int materialmasterid)
        {
            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            var items = (from x in materialNameManager.Get().Where(x => x.MaterialGroupMasterId == materialmasterid)
                         select new { MaterialDescription = x.MaterialDescription, MaterialMasterID = x.MaterialMasterID });
            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StockAdjustmentListDetails(int Store, string From, int Category, int Group, int MaterialType)
        {
            StockAdjustmentManager stockAdjustmentManager = new StockAdjustmentManager();
            DateTime From1=DateTime.ParseExact(From, "dd/MM/yyyy", null);
            var items = stockAdjustmentManager.GetList(Store, From1, Category, Group, MaterialType);
            return new JsonResult()
            {
                Data = items,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet// Use this value to set your maximum size for all of your Requests
            };

        }
        public ActionResult FillUOMBasedonMaterialName(int MaterialNameID)
        {

            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();

            MaterialManager materialManager = new MaterialManager();
            UOMManager uomManager = new UOMManager();
            var items = (from x in materialManager.Get()
                         join y in materialNameManager.Get()
                          on x.MaterialName equals y.MaterialMasterID
                         join z in uomManager.Get()
                         on x.Uom equals z.UomMasterId
                         where x.MaterialMasterId == MaterialNameID
                         select new
                         {
                             UomName = z.LongUnitName,
                             MaterialMasterID = x.MaterialMasterId
                         });

            var distinctList = items.GroupBy(x => x.UomName).Select(g => g.First()).ToList();
            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetManufacturerCode(int ManufacturerMasterId)
        {
            ManufacturerMaster manufacturerMaster = new ManufacturerMaster();
            ManufactureManager manufactureManager = new ManufactureManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            manufacturerMaster = manufactureManager.GetManufacturerMasterId(ManufacturerMasterId);
            return Json(new { manufacturerMaster.ManufacturerCode }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillMaterialMasterName(int MaterialGroupMasterId)
        {
            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialManager materialManager = new MaterialManager();
            ColorManager colorManager = new ColorManager();
            var items = (from x in materialNameManager.Get().Where(x => x.MaterialGroupMasterId == MaterialGroupMasterId)
                         select new { MaterialDescription = x.MaterialDescription, MaterialMasterID = x.MaterialMasterID });

            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MaterialMasterNameBasedByGroup(int MaterialGroupMasterId)
        {
            BillOfMaterialManager billofMaterialManager = new BillOfMaterialManager();
            List<MMS.Data.StoredProcedureModel.MaterialDropDownmodel> list = new List<Data.StoredProcedureModel.MaterialDropDownmodel>();
            var list_ = billofMaterialManager.GetMaterialList();
            list_ = list_.Where(x => x.GroupID == MaterialGroupMasterId).ToList();
            var distinctList = list_.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            distinctList = distinctList.ToList();
            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MaterialDescription,
                                                      Value = item.MaterialMasterId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);
            return Json(items_, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillMaterialName(int MaterialGroupMasterId)
        {
            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialManager materialManager = new MaterialManager();
            ColorManager colorManager = new ColorManager();
            BillOfMaterialManager billofMaterialManager = new BillOfMaterialManager();
            List<MMS.Data.StoredProcedureModel.MaterialDropDownmodel> list = new List<Data.StoredProcedureModel.MaterialDropDownmodel>();
            var list_ = billofMaterialManager.GetMaterialList();
            list_ = list_.Where(x => x.GroupID == MaterialGroupMasterId).ToList();
            var distinctList = list_.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            distinctList = distinctList.ToList();
            List<System.Web.Mvc.SelectListItem> items_ = distinctList.Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MaterialDescription,
                                                      Value = item.MaterialMasterId.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, ShotName);
            materialgroupmaster materialGroupMaster = new materialgroupmaster();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            materialGroupMaster = materialGroupManager.GetmaterialgroupmasterId(MaterialGroupMasterId);
            string host = System.Web.HttpContext.Current.Request.Url.Host.ToString();
            NormsManager normsManager = new NormsManager();
            Norms norms = new Norms();
            norms = normsManager.GetGroupID(MaterialGroupMasterId);

            return Json(new { Material = items_, IsSize = materialGroupMaster.IsSize, normDetails = norms, IsSubstance = materialGroupMaster.IsSubstance, host = host }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MaterialNameJoinType(int MaterialGroupMasterId)
        {
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            ColorManager colorManager = new ColorManager();
            var items = (from x in materialManager.Get()
                         join y in materialNameManager.Get()
                          on x.MaterialName equals y.MaterialMasterID
                         join z in colorManager.Get()
                         on x.ColorMasterId equals z.ColorMasterId
                         where x.MaterialGroupMasterId == MaterialGroupMasterId
                         select new { MaterialDescription = string.Format("{0} {1} {2}", y.MaterialDescription, x.OptionMaterialValue, z.Color), x.MaterialMasterId, x.Price, z.ColorMasterId, x.Uom, x.UomUnit, x.SizeRangeMasterId });

            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillMaterialNameBasedonColor(int MaterialNameID, string buyername)
        {

            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
            MaterialManager materialManager = new MaterialManager();
            ColorManager colorManager = new ColorManager();
            NormsManager normsManager = new NormsManager();
            Norms norms = new Norms();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            materialgroupmaster materialGroupMater = new materialgroupmaster();
            var items = (from x in materialManager.Get()
                         join y in materialNameManager.Get()
                          on x.MaterialName equals y.MaterialMasterID
                         join z in colorManager.Get()
                         on x.ColorMasterId equals z.ColorMasterId into yG
                         from y1 in yG.DefaultIfEmpty()
                         where x.MaterialMasterId == MaterialNameID
                         select new { MaterialDescription = string.Format("{0} {1} {2}", y.MaterialDescription, x.OptionMaterialValue, y1 != null ? y1.Color != null ? y1.Color : string.Empty : string.Empty), x.MaterialMasterId, x.Price, ColorMasterId = (y1 != null ? y1.ColorMasterId != 0 ? y1.ColorMasterId : 0 : 0), x.Uom, x.UomUnit, x.SizeRangeMasterId, x.SubstanceMasterId, x.MaterialCategoryMasterId, x.MaterialGroupMasterId, x.StoreMasterId });

            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            if(distinctList!=null&& distinctList.Count>0)
            {
                materialCategoryMaster = materialCategoryManager.GetMaterialCategoryMaster(distinctList.FirstOrDefault().MaterialCategoryMasterId);
                norms = normsManager.GetGroupIDWithBuyername(distinctList.FirstOrDefault().MaterialGroupMasterId, Convert.ToInt32(buyername));
            }
         
            return Json(new { distinctList = distinctList, norms = norms, materialCategoryMaster = materialCategoryMaster }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PurchaseOrderFillMaterialNameBasedonColor(int MaterialNameID, int? SupplierNameId)
        {

            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialManager materialManager = new MaterialManager();
            ColorManager colorManager = new ColorManager();
            Company company = new Company();
            IssueSlipManager issueSlip = new IssueSlipManager();
            CompanyManager companyManager = new CompanyManager();          
            var items = (from x in materialManager.Get()
                         join y in materialNameManager.Get()
                          on x.MaterialName equals y.MaterialMasterID
                         join z in colorManager.Get()
                         on x.ColorMasterId equals z.ColorMasterId into yG
                         from y1 in yG.DefaultIfEmpty()
                         where x.MaterialMasterId == MaterialNameID
                         select new { MaterialDescription = string.Format("{0} {1} {2}", y.MaterialDescription, x.OptionMaterialValue, y1 != null ? y1.Color != null ? y1.Color : string.Empty : string.Empty), x.MaterialMasterId, x.Price, ColorMasterId = (y1 != null ? y1.ColorMasterId != 0 ? y1.ColorMasterId : 0 : 0), x.Uom, x.UomUnit, x.SizeRangeMasterId, x.SubstanceMasterId, x.MaterialCategoryMasterId, x.MaterialGroupMasterId, x.GradeMasterId, x.StoreMasterId, x.PurchasePrimary, x.PrimaryUnit, x.PurchasePacket, x.PacketUnit, x.CurrencyMasterId, x.isImport, x.isLocal, x.isImportCustomer });
            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();

            List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
            List<ApprovedPriceList> approvedPriceLists = new List<ApprovedPriceList>();
            if (MaterialNameID != null&& MaterialNameID!=0)
            {
                ApprovedPriceListHistoryModel approvedPriceListHistory = new ApprovedPriceListHistoryModel();
                ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();

                approvedPriceList = (from APLH in approvedPriceListManager.GetMaterialList_Po(MaterialNameID)
                                     where APLH.MaterialID == MaterialNameID
                                     select APLH).ToList();
                SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
                foreach (var item in approvedPriceList)
                {
                    SupplierMaster supplierMaster = new SupplierMaster();
                    supplierMaster = supplierMasterManager.GetSupplierMasterId(item.SupplierId);
                    item.SupplierDescription = supplierMaster.SupplierName;
                    approvedPriceLists.Add(item);
                }

            }

            List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            if (distinctList != null && distinctList.Count > 0)
            {
                listSizeItemMaterial = materialManager.GetSizeItemMaterial(distinctList.FirstOrDefault().MaterialMasterId);
                listSizeItemMaterial = listSizeItemMaterial.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();

                storeMaster = storeMasterManager.GetStoreMasterId(distinctList.FirstOrDefault().StoreMasterId);
                company = companyManager.GetCompanyCode(storeMaster.StoreMasterId);
            }
            List<MMS.Web.Models.PendingQty> ListOfPendingStockList = new List<PendingQty>();
            ListOfPendingStockList = issueSlip.MaterialOpeningStock(MaterialNameID);
            return Json(new { Material = distinctList, SizeRange = listSizeItemMaterial, store = storeMaster, approvedPrice = approvedPriceLists, company = company, BalanceStock = ListOfPendingStockList.Select(x => x.BalanceStock) }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DirectPoPurchaseOrderFillMaterialNameBasedonColor(int MaterialNameID,string PONO)
        {
            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialManager materialManager = new MaterialManager();
            ColorManager colorManager = new ColorManager();
            Company company = new Company();
            CompanyManager companyManager = new CompanyManager();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            purchaseOrder= purchaseOrderManager.GetPoExist(PONO,MaterialNameID);
            string Message = "";
            if(purchaseOrder!=null&&purchaseOrder.PoOrderId!=0)
            {
                Message = "Already Exists";
            }
            var items = (from x in materialManager.Get()
                         join y in materialNameManager.Get()
                          on x.MaterialName equals y.MaterialMasterID
                         join z in colorManager.Get()
                         on x.ColorMasterId equals z.ColorMasterId into yG
                         from y1 in yG.DefaultIfEmpty()
                         where x.MaterialMasterId == MaterialNameID
                         select new { MaterialDescription = string.Format("{0} {1} {2}", y.MaterialDescription, x.OptionMaterialValue, y1 != null ? y1.Color != null ? y1.Color : string.Empty : string.Empty), x.MaterialMasterId, x.Price, ColorMasterId = (y1 != null ? y1.ColorMasterId != 0 ? y1.ColorMasterId : 0 : 0), x.Uom, x.UomUnit, x.SizeRangeMasterId, x.SubstanceMasterId, x.MaterialCategoryMasterId, x.MaterialGroupMasterId, x.GradeMasterId, x.StoreMasterId, x.PurchasePrimary, x.PrimaryUnit, x.PurchasePacket, x.PacketUnit, x.CurrencyMasterId, x.isImport, x.isLocal, x.isImportCustomer });
            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            ApprovedPriceListHistoryModel approvedPriceListHistory = new ApprovedPriceListHistoryModel();
            ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();
            List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
            List<ApprovedPriceList> approvedPriceLists = new List<ApprovedPriceList>();
            SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
            approvedPriceList = (from APLH in approvedPriceListManager.GetMaterialList_Po(MaterialNameID)
                                 where APLH.MaterialID == MaterialNameID
                                 select APLH).ToList();
            var distinctLists = approvedPriceList.GroupBy(x => x.SupplierId)
                         .Select(g => g.First())
                         .ToList();
            foreach (var item in distinctLists)
            {
                SupplierMaster supplierMaster = new SupplierMaster();
                supplierMaster = supplierMasterManager.GetSupplierMasterId(item.SupplierId);
                item.SupplierDescription = supplierMaster.SupplierName;
                approvedPriceLists.Add(item);
            }
        
            List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            if (distinctList != null && distinctList.Count > 0)
            {
                listSizeItemMaterial = materialManager.GetSizeItemMaterial(distinctList.FirstOrDefault().MaterialMasterId);
                listSizeItemMaterial = listSizeItemMaterial.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();
                storeMaster = storeMasterManager.GetStoreMasterId(distinctList.FirstOrDefault().StoreMasterId);
                company = companyManager.GetCompanyCode(storeMaster.StoreMasterId);
            }

            return Json(new { Material = distinctList, SizeRange = listSizeItemMaterial, store = storeMaster, approvedPrice = approvedPriceLists, company = company, Message= Message }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MaterialNameJoinType_(int MaterialmasterId)
        {
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            ColorManager colorManager = new ColorManager();
            var items = (from x in materialNameManager.Get()
                         join y in materialManager.Get()
                          on x.MaterialMasterID equals y.MaterialName
                         join z in colorManager.Get()
                         on y.ColorMasterId equals z.ColorMasterId into yG
                         from y1 in yG.DefaultIfEmpty()
                         where y.MaterialMasterId == MaterialmasterId
                         select new { MaterialDescription = string.Format("{0} {1} {2} {3}", x.MaterialDescription, y.OptionMaterialValue, y.MaterialCode, y1 != null ? y1.Color != null ? y1.Color : string.Empty : string.Empty), y.MaterialCode, y.MaterialMasterId, y.Price, ColorMasterId = (y1 != null ? y1.ColorMasterId != 0 ? y1.ColorMasterId : 0 : 0), y.Uom, y.UomUnit, y.SizeRangeMasterId, y.SameSizeForAllSize });
            //x.MaterialDescription 
            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
            listSizeItemMaterial = materialManager.GetSizeItemMaterial(distinctList.FirstOrDefault().MaterialMasterId);
            return Json(new { Material = distinctList, SizeRange = listSizeItemMaterial }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MaterialOpeningStockType_(int MaterialmasterId)
        {
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            ColorManager colorManager = new ColorManager();
            var items = (from x in materialNameManager.Get()
                         join y in materialManager.Get()
                          on x.MaterialMasterID equals y.MaterialName
                         join z in colorManager.Get()
                         on y.ColorMasterId equals z.ColorMasterId into yG
                         from y1 in yG.DefaultIfEmpty()
                         where y.MaterialMasterId == MaterialmasterId
                         select new { MaterialDescription = string.Format("{0} {1} {2} {3}", x.MaterialDescription, y.OptionMaterialValue, y.MaterialCode, y1 != null ? y1.Color != null ? y1.Color : string.Empty : string.Empty), y.MaterialCode, y.MaterialMasterId, y.Price, y.ImportPrice, y.isImport, y.isLocal, ColorMasterId = (y1 != null ? y1.ColorMasterId != 0 ? y1.ColorMasterId : 0 : 0), y.Uom, y.UomUnit, y.SizeRangeMasterId, y.SameSizeForAllSize });
  
            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
            listSizeItemMaterial = materialManager.GetSizeItemMaterial(distinctList.FirstOrDefault().MaterialMasterId);
            return Json(new { Material = distinctList, SizeRange = listSizeItemMaterial }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MaterialSizeRangeLoad(int MaterialMasterId)
        {
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            MaterialMaster materialMaster = new MaterialMaster();
            materialMaster = materialManager.GetMaterialMasterId(MaterialMasterId);
            SizeRangeMasterManager sizeRangeMasterManager = new SizeRangeMasterManager();
            MaterialMasterModel model = new MaterialMasterModel();


            var sizeRangeValue = (from x in sizeRangeMasterManager.Get()

                                  select new { SizeRangeMasterId = x.SizeRangeMasterId, SizeRangeRefValue = x.SizeRangeRefValue });
            var sizeRangeValue_ = sizeRangeValue.ToList();
            return Json(new { sizeRangeValue_, materialMaster.SizeRangeMasterId }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetMaterialCode(int MaterialGroupMasterId)
        {
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            materialgroupmaster materialGroupMaster = new materialgroupmaster();
            materialGroupMaster = materialGroupManager.GetmaterialgroupmasterId(MaterialGroupMasterId);
            string name = materialGroupMaster.GroupCode.Remove(0, 3);
            MaterialManager materialManager = new MaterialManager();
            List<MaterialMaster> materialMasterlist = new List<MaterialMaster>();
            materialMasterlist = materialManager.Get().Where(x => x.MaterialGroupMasterId == MaterialGroupMasterId).ToList();
            int count = 0;
            int Number;
            string AddNumber = "";
            if (materialMasterlist.Count > 0)
            {
                Number = materialMasterlist.Count() + 1;
                AddNumber = String.Format("{0:D4}", Number);
            }
            else
            {
                Number = materialMasterlist.Count() + 1;
                //AddNumber = "0000" + Number.ToString();
                AddNumber = String.Format("{0:D4}", Number);
            }
            name = name + " " + AddNumber;
            // string TotalAddNumber= Format((int(Number) + 1), "000");
            return Json(name, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string filter)
        {
            List<MaterialSearch> materialMaster = new List<MaterialSearch>();
            MaterialManager materialManager = new MaterialManager();
            MaterialSearch searchresult = new MaterialSearch();

            materialMaster = materialManager.GetMaterialList(filter.Trim());

            searchresult.MaterialSearchList = materialMaster;

            return PartialView("Partial/MaterialMasterGrid", searchresult);
        }
        #endregion

        [HttpPost]
        public void Upload()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                Guid pathGuid = Guid.Empty;
                pathGuid = Guid.NewGuid();
                var filename = pathGuid.ToString() + ".jpg";
                Session["UploadImage"] = filename;
                var path = Path.Combine(Server.MapPath("~/Images/"), filename);
                file.SaveAs(path);
            }
        }

        #region AutoComplete
        [HttpGet]
        public JsonResult GetMaterialName(string query = "")
        {
            //IEnumerable<Item> items = dbMedica.Items.Where(fun => fun.LatinName.Contains(query));
            //return dbMedica.Items.Where(fun => fun.LatinName.Contains(query));
            List<tbl_materialnamemaster> materialMaster = new List<tbl_materialnamemaster>();
            MaterialNameManager materialManager = new MaterialNameManager();
            materialMaster = materialManager.Get();
            List<tbl_materialnamemaster> items = materialManager.Get().OrderBy(x => x.MaterialDescription).Select(
                                    item => new tbl_materialnamemaster()
                                    {
                                        MaterialDescription = item.MaterialDescription,
                                        MaterialMasterID = item.MaterialMasterID
                                    }
                                    ).ToList();
            // materialMaster = materialMaster.Where(x => x.StoreMasterId.ToString().Contains(filter.ToLower()) || x.MaterialCategoryMasterId.ToString().Contains(filter.ToLower()) || x.MaterialGroupMasterId.ToString().Contains(filter.ToLower()) || x.MaterialCode.ToString().Contains(filter.ToLower()) || x.SubGroup.ToString().Contains(filter.ToLower()) || x.MachineryMasterId.ToString().Contains(filter.ToLower())).ToList();
            materialMaster = items.Where(p => p.MaterialDescription.Contains(query)).ToList();
            return Json(materialMaster, JsonRequestBehavior.AllowGet);
        }
		//Stock Statement        
      
       
        #endregion
    }
}
