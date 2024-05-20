using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Models
{
    [Table("spbommateriallist", Schema = "public")]
    public class SPBomMaterialList
    {
        [Column("categoryname")]
        public string CategoryName { get; set; }
        [Column("style")]
        public string Style { get; set; }
        [Column("groupname")]
        public string GroupName { get; set; }
        [Column("color")]
        public string Color { get; set; }
        [Column("substancerange")]
        public string SubstanceRange { get; set; }
        [Column("samplenorms")]
        public string SampleNorms { get; set; }
        [Column("wastage")]
        public string Wastage { get; set; }
        [Column("wastageqty")]
        public string WastageQty { get; set; }
        [Column("totalnorms")]
        public string TotalNorms { get; set; }
        [Column("totalnormsuom")]
        public int? TotalNormsUOM { get; set; }
        [Column("wastageqtyuom")]
        public int? WastageQtyUOM { get; set; }
        [Column("materialcategorymasterid")]
        public int? MaterialCategoryMasterId { get; set; }
        [Column("materialgroupmasterid")]
        public int? MaterialGroupMasterId { get; set; }
        [Column("materialname")]
        public string MaterialName { get; set; }
        [Column("materialdescription")]
        public string MaterialDescription { get; set; }
        [Column("colormasterid")]
        public int? ColorMasterId { get; set; }
        [Column("substancemasterid")]
        public int? SubstanceMasterId { get; set; }
        [Column("bommaterialid")]
        public int? BOMMaterialID { get; set; }
        [Column("bomid")]
        public int? BOMID { get; set; }
        [Column("wastagename")]
        public string WastageName { get; set; }
        [Column("totalnormsname")]
        public string TotalNormsName { get; set; }
        [Column("buyerseason")]
        public int? BuyerSeason { get; set; }
        [Column("seasonname")]
        public string SeasonName { get; set; }
        [Column("ismrp")]
        public bool IsMRP { get; set; }
        [Column("requiredqty")]
        public decimal? RequiredQty { get; set; }
        [Column("orderentryid")]
        public int? OrderEntryId { get; set; }
        [Column("buyerorderslno")]
        public string BuyerOrderSlNo { get; set; }
        [Column("buyerfullname")]
        public string BuyerFullName { get; set; }
        [Column("buyermasterid")]
        public int? BuyerMasterId { get; set; }
        [Column("uommasterid")]
        public int? UomMasterId { get; set; }
        [Column("suppliermasterid")]
        public int? SupplierMasterId { get; set; }
        [Column("suppliername")]
        public string SupplierName { get; set; }
        [Column("materialmasterid")]
        public int? Materialmasterid { get; set; }
        [Column("mrpno")]
        public string MRPNO { get; set; }
        [Column("price")]
        public string Price { get; set; }
        [Column("storename")]
        public string StoreName { get; set; }
        [Column("noofsets")]
        public decimal? NoOfSets { get; set; }
        [Column("rate")]
        public decimal? Rate { get; set; }
        [Column("piecies")]
        public decimal? Piecies { get; set; }
        [Column("currentstock")]
        public decimal? CurrentStock { get; set; }

        //public string Materialmasterid { get; set; }
        [Column("storemasterid")]
        public int? StoreMasterId { get; set; }
        [Column("buyerpono")]
        public string BuyerPoNo { get; set; }
        [Column("materialmasterid")]
        public int? MaterialMasterId { get; set; }
        [Column("stock")]
        public decimal? Stock { get; set; }
        [Column("cuttingissueyypeid")]
        public int? CuttingIssueTypeID { get; set; }
        [Column("totalpairs")]
        public decimal? TotalPairs { get; set; }
        [Column("customerimport")]
        public bool CustomerImport { get; set; }
        [Column("directimport")]
        public bool DirectImport { get; set; }
        [Column("local")]
        public bool Local { get; set; }
        [Column("lotno")]
        public int? LotNO { get; set; }
        [Column("cuttingissuetype")]
        public string CuttingIssueType { get; set; }
        [Column("currentissueqty")]
        public decimal? CurrentIssueQty { get; set; }
        [Column("alreadyissued")]
        public decimal? AlreadyIssued { get; set; }
        [Column("sno")]
        public int? sno { get; set; }
    }

    [Table("indentmaterialslist", Schema = "public")]
    public class IndentMaterialsList
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("indentmaterialid")]
        public int IndentMaterialID { get; set; }
        [Column("indentid")]
        public int? IndentID { get; set; }
        [Column("categoryname")]
        public string CategoryName { get; set; }
        [Column("groupname")]
        public string GroupName { get; set; }
        [Column("materialdescription")]
        public string MaterialDescription { get; set; }
        [Column("color")]
        public string Color { get; set; }
        [Column("wastagename")]
        public string WastageName { get; set; }
        [Column("totalnormsname")]
        public string TotalNormsName { get; set; }
        [Column("samplenorms")]
        public decimal? SampleNorms { get; set; }
        [Column("wastage")]
        public decimal? Wastage { get; set; }
        [Column("wastageqty")]
        public decimal? WastageQty { get; set; }
        [Column("wastageqtyuom")]
        public int? WastageQtyUOM { get; set; }
        [Column("totalnorms")]
        public decimal? TotalNorms { get; set; }
        [Column("mrpno")]
        public string MRPNO { get; set; }
        [Column("materialcategorymasterid")]
        public int? MaterialCategoryMasterId { get; set; }
        [Column("materialgroupmasterid")]
        public int? MaterialGroupMasterId { get; set; }
        [Column("storeid")]
        public int? StoreId { get; set; }
        [Column("materialmasterid")]
        public int? MaterialMasterID { get; set; }
        [Column("substancerange")]
        public string SubstanceRange { get; set; }
        [Column("substancemasterid")]
        public int? SubstanceMasterId { get; set; }
        [Column("sizerangemasterid")]
        public int? SizeRangeMasterID { get; set; }
        [Column("sizerangename")]
        public string SizeRangeName { get; set; }
        [Column("bommaterialid")]
        public int? BOMMaterialID { get; set; }
        [Column("bomid")]
        public int? BOMID { get; set; }
        [Column("colormasterid")]
        public int? ColorMasterID { get; set; }
        [Column("buyerseason")]
        public int? BuyerSeason { get; set; }
        [Column("seasonname")]
        public string SeasonName { get; set; }
        [Column("requiredqty")]
        public decimal? RequiredQty { get; set; }
        [Column("indentqty")]
        public decimal? IndentQTY { get; set; }
        [Column("storestock")]
        public decimal? StoreStock { get; set; }
        [Column("freestock")]
        public decimal? FreeStock { get; set; }
        [Column("value")]
        public decimal? Value { get; set; }
        [Column("supplierid")]
        public int? SupplierId { get; set; }
        [Column("orderentryid")]
        public int? OrderEntryId { get; set; }
        [Column("uommasterid")]
        public int? UomMasterId { get; set; }
        [Column("buyerfullname")]
        public string BuyerFullName { get; set; }
        [Column("buyermasterid")]
        public int? BuyerMasterId { get; set; }
        [Column("price")]
        public string Price { get; set; }
        [Column("suppliermastername")]
        public string SupplierMasterName { get; set; }
        [Column("ispo")]
        public bool IsPo { get; set; }
        [Column("buyerpono")]
        public string BuyerPoNo { get; set; }
        [Column("isimport")]
        public bool isImport { get; set; }
        [Column("customerimport")]
        public bool CustomerImport { get; set; }
        [Column("directimport")]
        public bool DirectImport { get; set; }
        [Column("local")]
        public bool Local { get; set; }
        [Column("ordertype")]
        public int? OrderType { get; set; }
        [Column("totalpairs")]
        public string TotalPairs { get; set; }
        [Column("totalindentpairs")]
        public string TotalIndentPairs { get; set; }
    }
    [Table("pendingqty", Schema = "public")]
    public class PendingQty
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("materialmasterid")]
        public int? MaterialMasterId { get; set; }
        [Column("newgrn")]
        public decimal? newgrn { get; set; }
        [Column("qty")]
        public decimal? Qty { get; set; }
        [Column("plusstock")]
        public decimal? PlusStock { get; set; }
        [Column("minusstock")]
        public decimal? Minusstock { get; set;}
        [Column("floorqty")]
        public decimal? FloorQty { get; set; }
        [Column("acceptedqty")]
        public decimal? AcceptedQty { get; set; }
        [Column("alreadyissued")]
        public decimal? AlreadyIssued { get; set; }
        [Column("currentissue")]
        public decimal? CurrentIssue { get; set; }
        [Column("balancestock")]
        public decimal? BalanceStock { get; set; }
    }
    [Table("pendingqty", Schema = "public")]
    public class StockAdjustMent
    {
        [Column("categoryid")]
        public int? CategoryId { get; set; }
        [Column("groupid")]
        public int? GroupId { get; set; }
    }

    [Table("subspbommateriallist", Schema = "public")]
    public class subspbommateriallist
    {
        [Column("categoryname")]
        public string categoryname { get; set; }
        [Column("groupname")]
        public string groupname { get; set; }
        [Column("materialdescription")]
        public string materialdescription { get; set; }
        [Column("storemasterid")]
        public int storemasterid { get; set; }
        [Column("reservestock")]
        public double reservestock { get; set; }
        [Column("freestock")]
        public double freestock { get; set; }
        [Column("storestock")]
        public double storestock { get; set; }
        [Column("totalpairs")]
        public double totalpairs { get; set; }
        [Column("color")]
        public string color { get; set; }
        [Column("colormasterid")]
        public int colormasterid { get; set; }
        [Column("requiredqty")]
        public double requiredqty { get; set; }
        [Column("rate")]
        public double rate { get; set; }
        [Column("value")]
        public double value { get; set; }
        [Column("wastagename")]
        public string wastagename { get; set; }
        [Column("wastage")]
        public double? wastage { get; set; }
        [Column("wastageqty")]
        public double? wastageqty { get; set; }
        [Column("wastageqtyuom")]
        public string wastageqtyuom { get; set; }
        [Column("materialcategorymasterid")]
        public int materialcategorymasterid { get; set; }
        [Column("materialgroupmasterid")]
        public int materialgroupmasterid { get; set; }
        [Column("materialname")]
        public string materialname { get; set; }
        [Column("buyerfullname")]
        public string buyerfullname { get; set; }
        [Column("buyermasterid")]
        public int buyermasterid { get; set; }
        [Column("substancemasterid")]
        public int? substancemasterid { get; set; }
        [Column("buyerseason")]
        public string buyerseason { get; set; }
        [Column("materialmasterid")]
        public int materialmasterid { get; set; }
        [Column("seasonname")]
        public string seasonname { get; set; }
        [Column("bommaterialid")]
        public int? bommaterialid { get; set; }
        [Column("totalnorms")]
        public double? totalnorms { get; set; }
        [Column("substancerange")]
        public string substancerange { get; set; }
    }
}