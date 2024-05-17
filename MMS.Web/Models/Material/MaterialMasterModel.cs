using MMS.Core.Entities;
using MMS.Data.StoredProcedureModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.Material
{
    public class MaterialMasterModel
    {
        public int MaterialMasterId { get; set; }
        public int StoreMasterId { get; set; }
        public int MaterialCategoryMasterId { get; set; }
        public int MaterialGroupMasterId { get; set; }
        public int SubGroup { get; set; }
        public string MaterialCode { get; set; }
        public int? MaterialName { get; set; }
        public string OptionMaterialValue { get; set; }
        public int? ColorMasterId { get; set; }
        public int? ColorMasterId_ { get; set; }
        public int? MachineryMasterId { get; set; }
        public int? CuttingIssueTypeID { get; set; }
        public bool SameSizeForAllSize { get; set; }
        public int? GradeMasterId { get; set; }
        public int? SubstanceMasterId { get; set; }
        public int? LeatherSizeMasterId { get; set; }
        public int? OriginMasterId { get; set; }
        public decimal WeightInKg { get; set; }
        public int? SizeRangeMasterId { get; set; }
        public decimal? Decimals { get; set; }
        public int Uom { get; set; }
        public int UomUnit { get; set; }
        public string Price { get; set; }
        public int? CurrencyMasterId { get; set; }
        public int? CuomerImportCurrencyMasterId { get; set; }
        public int? ManufacturerMasterId { get; set; }
        public string ManufacturerCode { get; set; }
        public int? PacketType { get; set; }
        public string PacketTypePrice { get; set; }
        public int? PacketCurrency { get; set; }
        public int? TaxCategory { get; set; }
        public int? PrimaryPackage { get; set; }
        public int? SecondaryPackage { get; set; }
        public string LeadTime { get; set; }
        public string MaxRoLevel { get; set; }
        public string MinRoLevel { get; set; }
        public string ReOrderLevel { get; set; }
        public string Expiry { get; set; }
        public bool IsActive { get; set; }
        public string Barcode { get; set; }
        public bool IsPurchase { get; set; }
        public bool IsIssues { get; set; }
        public bool IsCustomerSupplied { get; set; }
        public bool IsImportedMaterial { get; set; }
        public bool IsLocalMaterial { get; set; }
        public bool IsCriticalMaterial { get; set; }
        public bool IsAllowNegativeStock { get; set; }
        public bool IsLotNumberTracking { get; set; }
        public bool IsIssueStrictly { get; set; }
        public bool IsPrimaryPackage { get; set; }
        public bool IsSecondaryPackage { get; set; }
        public bool IsApplicable { get; set; }
        public decimal? PurchasePrimary { get; set; }
        public decimal? PurchasePacket { get; set; }
        public int? PrimaryUnit { get; set; }
        public int? PacketUnit { get; set; }
        public int? PacketUnitType { get; set; }
        public string ImagePath { get; set; }
        public bool componentcheck1sds { get; set; }
        public bool primarycheckpackage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string SizeQuantityRate { get; set; }
        public string Description { get; set; }
        
        public string Size { get; set; }
        public string Qty { get; set; }
        public string Rate { get; set; }
        public bool isLocal { get; set; }
        public bool isImport { get; set; }
        public bool isImportCustomer { get; set; }
        public decimal? ImportPrice { get; set; }
        public decimal? CustomerImportPrice { get; set; }
        public int? importManafactureName { get; set; }
        public int? CustomerManafactureName { get; set; }
        public int? importCurrency { get; set; }

        public List<MaterialSearch> MaterialSearch { get; set; }
        public List<MaterialMaster> MaterialMasterList { get; set; }
        public List<SizeItemMaterial> sizeItemMaterialList { get; set; }

        public string HSNCode { get; set; }
        public string BuyerItemCode { get; set; }
    }
}