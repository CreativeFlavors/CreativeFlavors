using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service;
using MMS.Repository.Service.StockService;
using MMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace MMS.Repository.Managers
{
    public class MaterialManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<MaterialMaster> MaterialMasterRepository;
        private Repository<MaterialNameMaster> MaterialNameMasterRepository;
        private Repository<ApprovedPriceList> ApprovedPriceListRepository;
        private Repository<SizeItemMaterial> sizeItemMaterialRepository;

        #region Add/Update/Delete Operation

        public MaterialMaster Post(MaterialMaster arg)
        {
            bool result = false;
            MaterialMaster materialMaster = new MaterialMaster();
            try
            {
                if (arg.MaterialMasterId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username; 
                    MaterialMasterRepository.Insert(arg);
                    result = true;
                    materialMaster = arg;
                }
                else if (arg.MaterialMasterId != 0)
                {
                    MaterialMaster model = MaterialMasterRepository.Table.Where(p => p.MaterialMasterId == arg.MaterialMasterId).FirstOrDefault();
                    if (model != null)
                    {
                        model.StoreMasterId = arg.StoreMasterId;
                        model.MaterialCategoryMasterId = arg.MaterialCategoryMasterId;
                        model.MaterialGroupMasterId = arg.MaterialGroupMasterId;
                        model.SubGroup = arg.SubGroup;
                        model.MaterialCode = arg.MaterialCode;
                        model.MaterialName = arg.MaterialName;                       
                        model.ColorMasterId = arg.ColorMasterId;
                        model.isImportCustomer = arg.isImportCustomer;
                        model.ImportPrice = arg.ImportPrice;
                        model.CustomerImportPrice = arg.CustomerImportPrice;
                        model.CuttingIssueTypeID = arg.CuttingIssueTypeID;
                        model.MachineryMasterId = arg.MachineryMasterId;
                        model.GradeMasterId = arg.GradeMasterId;
                        model.isImport = arg.isImport;
                        model.isLocal = arg.isLocal;                     
                        model.importManafactureName = arg.importManafactureName;
                        model.CustomerManafactureName = arg.CustomerManafactureName;
                        model.CuomerImportCurrencyMasterId = arg.CuomerImportCurrencyMasterId;
                        model.importCurrency = arg.importCurrency;
                        model.SubstanceMasterId = arg.SubstanceMasterId;
                        model.LeatherSizeMasterId = arg.LeatherSizeMasterId;
                        model.OriginMasterId = arg.OriginMasterId;
                        model.WeightInKg = arg.WeightInKg;
                        model.SizeRangeMasterId = arg.SizeRangeMasterId;
                        model.Decimals = arg.Decimals;
                        model.Uom = arg.Uom;
                        model.UomUnit = arg.UomUnit;
                        model.Price = arg.Price;
                        model.CurrencyMasterId = arg.CurrencyMasterId;
                        model.ManufacturerMasterId = arg.ManufacturerMasterId;
                        model.PacketType = arg.PacketType;
                        model.PacketTypePrice = arg.PacketTypePrice;
                        model.PacketCurrency = arg.PacketCurrency;
                        model.TaxCategory = arg.TaxCategory;
                        model.PrimaryPackage = arg.PrimaryPackage;
                        model.SecondaryPackage = arg.SecondaryPackage;
                        model.LeadTime = arg.LeadTime;
                        model.MaxRoLevel = arg.MaxRoLevel;
                        model.MinRoLevel = arg.MinRoLevel;
                        model.ReOrderLevel = arg.ReOrderLevel;
                        model.Expiry = arg.Expiry;
                        model.IsActive = arg.IsActive;
                        model.Barcode = arg.Barcode;
                        model.IsPurchase = arg.IsPurchase;
                        model.IsIssues = arg.IsIssues;
                        model.IsCustomerSupplied = arg.IsCustomerSupplied;
                        model.IsImportedMaterial = arg.IsImportedMaterial;
                        model.IsLocalMaterial = arg.IsLocalMaterial;
                        model.IsCriticalMaterial = arg.IsCriticalMaterial;
                        model.IsAllowNegativeStock = arg.IsAllowNegativeStock;
                        model.IsLotNumberTracking = arg.IsLotNumberTracking;
                        model.IsIssueStrictly = arg.IsIssueStrictly;
                        model.IsPrimaryPackage = arg.IsPrimaryPackage;
                        model.IsSecondaryPackage = arg.IsSecondaryPackage;
                        model.SameSizeForAllSize = arg.SameSizeForAllSize;
                        model.CuttingIssueTypeID = arg.CuttingIssueTypeID;
                        model.IsApplicable = arg.IsApplicable;
                        model.PurchasePrimary = arg.PurchasePrimary;
                        model.PurchasePacket = arg.PurchasePacket;
                        model.HSNCode = arg.HSNCode;
                        model.BuyerItemCode = arg.BuyerItemCode;
                        model.PrimaryUnit = arg.PrimaryUnit;
                        model.PacketUnit = arg.PacketUnit;
                        string username = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username;
                        model.CreatedDate = arg.CreatedDate.Value;
                        model.UpdatedDate = DateTime.Now;
                        model.ImagePath = arg.ImagePath;
                        MaterialMasterRepository.Update(model);
                        result = true;
                        materialMaster = model;
                    }
                }
             
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return materialMaster;
        }

        public bool Put(MaterialMaster arg)
        {
            bool result = false;
            try
            {
                MaterialMaster model = MaterialMasterRepository.Table.Where(p => p.MaterialMasterId == arg.MaterialMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.StoreMasterId = arg.StoreMasterId;
                    model.MaterialCategoryMasterId = arg.MaterialCategoryMasterId;
                    model.MaterialGroupMasterId = arg.MaterialGroupMasterId;
                    model.SubGroup = arg.SubGroup;
                    model.MaterialCode = arg.MaterialCode;
                    model.MaterialName = arg.MaterialName;
                    model.ColorMasterId = arg.ColorMasterId;
                    model.MachineryMasterId = arg.MachineryMasterId;
                    model.GradeMasterId = arg.GradeMasterId;
                    model.SubstanceMasterId = arg.SubstanceMasterId;
                    model.LeatherSizeMasterId = arg.LeatherSizeMasterId;
                    model.OriginMasterId = arg.OriginMasterId;
                    model.WeightInKg = arg.WeightInKg;
                    model.SizeRangeMasterId = arg.SizeRangeMasterId;
                    model.Decimals = arg.Decimals;
                    model.Uom = arg.Uom;
                    model.UomUnit = arg.UomUnit;
                    model.Price = arg.Price;
                    model.CurrencyMasterId = arg.CurrencyMasterId;
                    model.ManufacturerMasterId = arg.ManufacturerMasterId;
                    model.PacketType = arg.PacketType;
                    model.PacketTypePrice = arg.PacketTypePrice;
                    model.PacketCurrency = arg.PacketCurrency;
                    model.TaxCategory = arg.TaxCategory;
                    model.PrimaryPackage = arg.PrimaryPackage;
                    model.SecondaryPackage = arg.SecondaryPackage;
                    model.LeadTime = arg.LeadTime;
                    model.MaxRoLevel = arg.MaxRoLevel;
                    model.MinRoLevel = arg.MinRoLevel;
                    model.ReOrderLevel = arg.ReOrderLevel;
                    model.Expiry = arg.Expiry;
                    model.IsActive = arg.IsActive;
                    model.Barcode = arg.Barcode;
                    model.IsPurchase = arg.IsPurchase;
                    model.IsIssues = arg.IsIssues;
                    model.IsCustomerSupplied = arg.IsCustomerSupplied;
                    model.IsImportedMaterial = arg.IsImportedMaterial;
                    model.IsLocalMaterial = arg.IsLocalMaterial;
                    model.IsCriticalMaterial = arg.IsCriticalMaterial;
                    model.IsAllowNegativeStock = arg.IsAllowNegativeStock;
                    model.IsLotNumberTracking = arg.IsLotNumberTracking;
                    model.IsIssueStrictly = arg.IsIssueStrictly;
                    model.IsPrimaryPackage = arg.IsPrimaryPackage;
                    model.IsSecondaryPackage = arg.IsSecondaryPackage;

                    model.IsApplicable = arg.IsApplicable;
                    model.PurchasePrimary = arg.PurchasePrimary;
                    model.PurchasePacket = arg.PurchasePacket;
                    model.PrimaryUnit = arg.PrimaryUnit;
                    model.PacketUnit = arg.PacketUnit;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    model.CreatedDate = arg.CreatedDate.Value;
                    model.UpdatedDate = DateTime.Now;
                    model.ImagePath = arg.ImagePath;
                    MaterialMasterRepository.Update(model);
                    result = true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }



        public bool Delete(int id)
        {
            bool result = false;
            try
            {
               
                MaterialMaster model = MaterialMasterRepository.GetById(id);
                SizeItemMaterial models = sizeItemMaterialRepository.GetById(id);
                List<SizeItemMaterial> model_ = sizeItemMaterialRepository.Table.Where(p => p.MaterialMasterID == models.MaterialMasterID&& p.IsDeleted==false).ToList();

                if (model_ != null && model_.Count > 0)
                {
                    foreach (var item in model_)
                    {
                        SizeItemMaterial models_ = sizeItemMaterialRepository.GetById(item.SizeMaterialD);
                        models_.IsDeleted = true;
                        models_.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                        models_.DeletedOn = DateTime.Now;
                        sizeItemMaterialRepository.Update(models_);
                    }
                }
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                MaterialMasterRepository.Update(model);
                // MaterialMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        #endregion

        #region Helper Method
        public MaterialManager()
        {
            MaterialMasterRepository = unitOfWork.Repository<MaterialMaster>();
            MaterialNameMasterRepository = unitOfWork.Repository<MaterialNameMaster>();
            sizeItemMaterialRepository = unitOfWork.Repository<SizeItemMaterial>();
        }
        public MaterialMaster GetMaterialExist(int? MaterialName, int ColorMasterId, string OptionMaterialValue)
        {
            MaterialMaster materialMaster = new MaterialMaster();
            if (((!string.IsNullOrEmpty(OptionMaterialValue)) && (MaterialName != null && MaterialName != 0) && ColorMasterId != 0))
            {
                materialMaster = MaterialMasterRepository.Table.Where(x => x.MaterialName == MaterialName && x.OptionMaterialValue == OptionMaterialValue && x.ColorMasterId == ColorMasterId && x.IsDeleted == false).FirstOrDefault();
            }
            else if (((!string.IsNullOrEmpty(OptionMaterialValue)) && (MaterialName == 0 || MaterialName == null) && ColorMasterId != 0))
            {
                materialMaster = MaterialMasterRepository.Table.Where(x => x.OptionMaterialValue == OptionMaterialValue && x.ColorMasterId == ColorMasterId && x.IsDeleted == false).FirstOrDefault();
            }
            else if (((OptionMaterialValue == null || OptionMaterialValue == "") && (MaterialName != 0 || MaterialName != null) && ColorMasterId != 0))
            {
                materialMaster = MaterialMasterRepository.Table.Where(x => x.MaterialName == MaterialName && x.ColorMasterId == ColorMasterId && x.IsDeleted == false).FirstOrDefault();
            }
            return materialMaster;
        }
        public List<MMS.Web.Models.PendingQty> MaterialOpeningStock(int materialMasterID)
        {
            List<MMS.Web.Models.PendingQty> list = new List<PendingQty>();
            list= MaterialMasterRepository.MaterialOpeningStock(materialMasterID);
            return list;
        }
       
        
        public MaterialMaster GetMaterialMasterId(int? MaterialMasterId)
        {
            MaterialMaster materialMaster = new MaterialMaster();
            if (MaterialMasterId != 0)
            {
                materialMaster = MaterialMasterRepository.Table.Where(x => x.MaterialMasterId == MaterialMasterId && x.IsDeleted==false).FirstOrDefault();
                if(materialMaster == null)
                {
                    
                }
            }
            return materialMaster;
        }
        public List<MaterialMaster> MaterialList()
        {
            List<MaterialMaster> listOfAllMaterial = new List<MaterialMaster>();
            listOfAllMaterial = MaterialMasterRepository.MaterialList();
            return listOfAllMaterial;
        }
        public List<MMS.Data.StoredProcedureModel.MaterialSearch> GetMaterialList(string MaterialDescription)
        {
            List<MMS.Data.StoredProcedureModel.MaterialSearch> MaterialList = new List<MMS.Data.StoredProcedureModel.MaterialSearch>();
            try
            {
                MaterialList = MaterialNameMasterRepository.SearchMaterial(MaterialDescription);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return MaterialList;
        }
        public MaterialNameMaster GetMaterialNameMasterId(int MaterialMasterID)
        {
            MaterialNameMaster materialMaster = new MaterialNameMaster();
            if (MaterialMasterID != 0)
            {
                materialMaster = MaterialNameMasterRepository.Table.Where(x => x.MaterialMasterID == MaterialMasterID).FirstOrDefault();
            }
            return materialMaster;
        }
        public List<MaterialMaster> GetMaterialName(int MaterialMasterID)
        {
            List<MaterialMaster> materialMaster = new List<MaterialMaster>();
            if (MaterialMasterID != 0)
            {
                materialMaster = MaterialMasterRepository.Table.Where(x => x.MaterialName == MaterialMasterID && x.IsDeleted == false).ToList();
            }
            return materialMaster;
        }

        public MaterialMaster GetMaterialName_(int MaterialMasterID)
        {
            MaterialMaster materialMaster = new MaterialMaster();
            if (MaterialMasterID != 0)
            {
                materialMaster = MaterialMasterRepository.Table.Where(x => x.MaterialName == MaterialMasterID && x.IsDeleted==false).FirstOrDefault();
            }
            return materialMaster;
        }

        public MaterialMaster Get(int id)
        {
            return null;
        }

        public List<MaterialMaster> Get()
        {
            List<MaterialMaster> materialMaster = new List<MaterialMaster>();
            try
            {
                materialMaster = MaterialMasterRepository.Table.Where(X => X.IsDeleted == false).ToList<MaterialMaster>();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return materialMaster;
        }
        public bool PutPrice(ApprovedPriceList arg)
        {
            bool result = false;
            try
            {
                int materialID = arg.MaterialID;
                MaterialMaster model = MaterialMasterRepository.Table.Where(p => p.MaterialName == materialID).FirstOrDefault();
                if (model != null)
                {

                    model.Price = arg.PriceRs.ToString();
                    MaterialMasterRepository.Update(model);
                    result = true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }


        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        #endregion
        #region Si-ze Item Material
        public SizeItemMaterial SizeItemMaterialPost(SizeItemMaterial arg)
        {
            bool result = false;
            SizeItemMaterial sizeItemMaterial = new SizeItemMaterial();
            try
            {
                //List<SizeItemMaterial> model_ = sizeItemMaterialRepository.Table.Where(p => p.MaterialMasterID == arg.MaterialMasterID && p.IsDeleted==false).ToList();

                //if(model_ != null&&model_.Count>0)
                //{
                //   foreach(var item in model_)
                //    {
                //        SizeItemMaterial models = sizeItemMaterialRepository.GetById(item.SizeMaterialD);
                //        models.IsDeleted = true;
                //        models.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                //        models.DeletedOn = DateTime.Now;
                //        sizeItemMaterialRepository.Update(models);
                //    }
                //}
                if (arg.SizeMaterialD == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBY = username;
                    sizeItemMaterialRepository.Insert(arg);
                    result = true;
                    sizeItemMaterial = arg;
                }
                else if (arg.SizeMaterialD != 0)
                {
                    SizeItemMaterial model = sizeItemMaterialRepository.Table.Where(p => p.SizeMaterialD == arg.SizeMaterialD&&p.IsDeleted==false).FirstOrDefault();
                    if (model != null)
                    {
                        model.SizeMaterialD = arg.SizeMaterialD;
                        string username = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username;
                        model.CreatedDate = arg.CreatedDate.Value;
                        model.UpdatedDate = DateTime.Now;
                        model.MaterialMasterID = arg.MaterialMasterID;
                        model.SizeRange = arg.SizeRange;
                        model.Qty = arg.Qty;
                        model.Rate = arg.Rate;
                        sizeItemMaterialRepository.Update(model);
                        result = true;
                        sizeItemMaterial = model;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return sizeItemMaterial;
        }
        public SizeItemMaterial SizeItemMaterialDelete(int Materialmasterid)
        {
            bool result = false;
            SizeItemMaterial sizeItemMaterial = new SizeItemMaterial();
            try
            {
                List<SizeItemMaterial> model_ = sizeItemMaterialRepository.Table.Where(p => p.MaterialMasterID == Materialmasterid).ToList();

                if (model_ != null && model_.Count > 0)
                {
                    foreach (var item in model_)
                    {
                        SizeItemMaterial models = sizeItemMaterialRepository.GetById(item.SizeMaterialD);
                        models.IsDeleted = true;
                        models.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                        models.DeletedOn = DateTime.Now;
                        sizeItemMaterialRepository.Delete(models);
                    }
                }              

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return sizeItemMaterial;
        }
        public List<SizeItemMaterial> GetSizeItemMaterial(int MaterialMasterID)
        {
            List<SizeItemMaterial> sizeItemMaterialList = new List<SizeItemMaterial>();
            if (MaterialMasterID != 0)
            {
                sizeItemMaterialList = sizeItemMaterialRepository.Table.Where(x => x.MaterialMasterID == MaterialMasterID&&x.IsDeleted==false).ToList();
            }
            return sizeItemMaterialList;
        }
        #endregion
    }
}
