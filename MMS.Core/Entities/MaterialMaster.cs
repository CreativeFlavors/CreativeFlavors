using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("materialmaster", Schema = "public")]
    public class MaterialMaster : BaseEntity
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
        [Column("machinerymasterid")]
        public int? MachineryMasterId { get; set; }
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
        [Column("manufacturermasterid")]
        public int? ManufacturerMasterId { get; set; }
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
        [Column("samesizeforallsize")]
        public bool SameSizeForAllSize { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
        [Column("cuttingissuetypeid")]
        public int? CuttingIssueTypeID { get; set; }
        [Column("importprice")]
        public decimal? ImportPrice { get; set; }
        [Column("islocal")]
        public bool isLocal { get; set; }
        [Column("isimport")]
        public bool isImport { get; set; }
        [Column("isimportcustomer")]
        public bool isImportCustomer { get; set; }
        [Column("importmanafacturename")]
        public int? importManafactureName { get; set; }
        [Column("importcurrency")]
        public int? importCurrency { get; set; }
        [Column("customerimportprice")]
        public decimal? CustomerImportPrice { get; set; }
        [Column("customermanafacturename")]
        public int? CustomerManafactureName { get; set; }
        [Column("cuomerimportcurrencymasterid")]
        public int? CuomerImportCurrencyMasterId { get; set; }
        [Column("hsncode")]
        public string HSNCode { get; set; }
        [Column("buyeritemcode")]
        public string BuyerItemCode { get; set; }

    }
}
