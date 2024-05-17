using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("materialsearch", Schema = "public")]
    public class MaterialSearch
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("materialmasterid")]
        public int MaterialMasterId { get; set; }
        [Column("storemasterid")]
        public int StoreMasterId { get; set; }
        [Column("materialcategorymasterid")]
        public int MaterialCategoryMasterId { get; set; }
        [Column("materialgroupmasterid")]
        public int MaterialGroupMasterId { get; set; }
        [Column("subgroup")]
        public int SubGroup { get; set; }
        [Column("materialcode")]
        public string MaterialCode { get; set; }
        [Column("materialname")]
        public int? MaterialName { get; set; }
        [Column("optionmaterialvalue")]
        public string OptionMaterialValue { get; set; }
        [Column("colormasterid")]
        public int? ColorMasterId { get; set; }
        [Column("colormasterid_")]
        public int? ColorMasterId_ { get; set; }
        [Column("machinerymasterid")]
        public int? MachineryMasterId { get; set; }
        [Column("cuttingissuetypeid")]
        public int? CuttingIssueTypeID { get; set; }
        [Column("samesizeforallsize")]
        public bool SameSizeForAllSize { get; set; }
        [Column("grademasterid")]
        public int? GradeMasterId { get; set; }
        [Column("substancemasterid")]
        public int? SubstanceMasterId { get; set; }
        [Column("leathersizemasterid")]
        public int? LeatherSizeMasterId { get; set; }

        [Column("originmasterid")]
        public int? OriginMasterId { get; set; }
        [Column("weightinkg")]
        public decimal WeightInKg { get; set; }
        [Column("sizerangemasterid")]
        public int? SizeRangeMasterId { get; set; }
        [Column("decimals")]
        public decimal? Decimals { get; set; }
        [Column("uom")]
        public int Uom { get; set; }
        [Column("uomunit")]
        public int UomUnit { get; set; }
        [Column("price")]
        public string Price { get; set; }
        [Column("currencymasterid")]
        public int? CurrencyMasterId { get; set; }
        [Column("cuomerimportcurrencymasterid")]
        public int? CuomerImportCurrencyMasterId { get; set; }
        [Column("manufacturermasterid")]
        public int? ManufacturerMasterId { get; set; }
        [Column("manufacturercode")]
        public string ManufacturerCode { get; set; }
        [Column("packettype")]
        public int? PacketType { get; set; }
        [Column("packettypeprice")]
        public string PacketTypePrice { get; set; }
        [Column("packetcurrency")]
        public int? PacketCurrency { get; set; }
        [Column("taxcategory")]
        public int? TaxCategory { get; set; }
        [Column("primarypackage")]
        public int? PrimaryPackage { get; set; }
        [Column("secondarypackage")]
        public int? SecondaryPackage { get; set; }
        [Column("leadtime")]
        public string LeadTime { get; set; }
        [Column("maxrolevel")]
        public string MaxRoLevel { get; set; }
        [Column("minrolevel")]
        public string MinRoLevel { get; set; }
        [Column("reorderlevel")]
        public string ReOrderLevel { get; set; }
        [Column("expiry")]
        public string Expiry { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }
        [Column("barcode")]
        public string Barcode { get; set; }
        [Column("ispurchase")]
        public bool IsPurchase { get; set; }
        [Column("isissues")]
        public bool IsIssues { get; set; }
        [Column("iscustomersupplied")]
        public bool IsCustomerSupplied { get; set; }
        [Column("isimportedmaterial")]
        public bool IsImportedMaterial { get; set; }
        [Column("islocalmaterial")]
        public bool IsLocalMaterial { get; set; }
        [Column("iscriticalmaterial")]
        public bool IsCriticalMaterial { get; set; }
        [Column("isallownegativestock")]
        public bool IsAllowNegativeStock { get; set; }
        [Column("islotnumbertracking")]
        public bool IsLotNumberTracking { get; set; }
        [Column("isissuestrictly")]
        public bool IsIssueStrictly { get; set; }
        [Column("isprimarypackage")]
        public bool IsPrimaryPackage { get; set; }
        [Column("issecondarypackage")]
        public bool IsSecondaryPackage { get; set; }
        [Column("isapplicable")]
        public bool IsApplicable { get; set; }
        [Column("purchaseprimary")]
        public decimal? PurchasePrimary { get; set; }
        [Column("purchasepacket")]
        public decimal? PurchasePacket { get; set; }
        [Column("primaryunit")]
        public int? PrimaryUnit { get; set; }
        [Column("packetunit")]
        public int? PacketUnit { get; set; }
        [Column("packetunittype")]
        public int? PacketUnitType { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("imagepath")]
        public string ImagePath { get; set; }
        [Column("componentcheck1sds")]
        public bool componentcheck1sds { get; set; }
        [Column("primarycheckpackage")]
        public bool primarycheckpackage { get; set; }
        [Column("sizequantityrate")]
        public string SizeQuantityRate { get; set; }
        [Column("description")]
        public string Description { get; set; }
        public List<MaterialMaster> MaterialMasterList { get; set; }
        public List<SizeItemMaterial> sizeItemMaterialList { get; set; }
        [Column("size")]
        public string Size { get; set; }
        [Column("qty")]
        public string Qty { get; set; }
        [Column("rate")]
        public string Rate { get; set; }
        [Column("islocal")]
        public bool isLocal { get; set; }
        [Column("isimport")]
        public bool isImport { get; set; }
        [Column("isimportcustomer")]
        public bool isImportCustomer { get; set; }
        [Column("importprice")]
        public decimal? ImportPrice { get; set; }
        [Column("customerimportprice")]
        public decimal? CustomerImportPrice { get; set; }
        [Column("importmanafacturename")]
        public int? importManafactureName { get; set; }
        [Column("customermanafacturename")]
        public int? CustomerManafactureName { get; set; }
        [Column("importcurrency")]
        public int? importCurrency { get; set; }
        [Column("materialdescription")]
        public string MaterialDescription { get; set; }

        [Column("groupname")]
        public string GroupName { get; set; }
        [Column("storename")]
        public string StoreName { get; set; }
        [Column("subgroupdescription")]
        public string SubGroupDescription { get; set; }
        [Column("machinename")]
        public string machinename { get; set; }
        [Column("color")]
        public string color { get; set; }
        public List<MaterialSearch> MaterialSearchList { get; set; }
        [Column("hsncode")]

        public string HSNCode { get; set; }
        [Column("buyeritemCode")]
        public string BuyerItemCode { get; set; }
    }
    public class ItemMaterial
    {
        [Column("materialmasterid")]
        public int? materialmasterid { get; set; }
        [Column("materialdescription")]
        public string materialdescription { get; set; }
    }
}
