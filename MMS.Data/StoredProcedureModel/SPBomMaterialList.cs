using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models
{
    public class SPBomMaterialList
    {
        public string CategoryName { get; set; }
        public string Style { get; set; }
        public string GroupName { get; set; }
        public string Color { get; set; }
        public string SubstanceRange { get; set; }
        public string SampleNorms { get; set; }
        public string Wastage { get; set; }
        public string WastageQty { get; set; }
        public string TotalNorms { get; set; }
        public int? TotalNormsUOM { get; set; }
        public int? WastageQtyUOM { get; set; }
        public int? MaterialCategoryMasterId { get; set; }
        public int? MaterialGroupMasterId { get; set; }

        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public int? ColorMasterId { get; set; }
        public int? SubstanceMasterId { get; set; }
        public int? BOMMaterialID { get; set; }
        public int? BOMID { get; set; }
        public string WastageName { get; set; }
        public string TotalNormsName { get; set; }
        public int? BuyerSeason { get; set; }
        public string SeasonName { get; set; }
        public bool IsMRP { get; set; }
        public decimal? RequiredQty { get; set; }
        public int? OrderEntryId { get; set; }
        public string BuyerOrderSlNo { get; set; }
        public string BuyerFullName { get; set; }
        public int? BuyerMasterId { get; set; }
        public int? UomMasterId { get; set; }
        public int? SupplierMasterId { get; set; }
        public string SupplierName { get; set; }
        public int? Materialmasterid { get; set; }
        public string MRPNO { get; set; }
        public string Price { get; set; }
        public string StoreName { get; set; }
        public decimal? NoOfSets { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Piecies { get; set; }
        public decimal? CurrentStock { get; set; }

        //public string Materialmasterid { get; set; }
        public int? StoreMasterId { get; set; }
        public string BuyerPoNo { get; set; }
        public int? MaterialMasterId_ { get; set; }
        public decimal? Stock { get; set; }
        public int? CuttingIssueTypeID { get; set; }
        public decimal? TotalPairs { get; set; }
        public bool CustomerImport { get; set; }
        public bool DirectImport { get; set; }
        public bool Local { get; set; }
        public int? LotNO { get; set; }
        public string CuttingIssueType { get; set; }
        public decimal? CurrentIssueQty { get; set; }
        public decimal? AlreadyIssued { get; set; }
    }

    public class IndentMaterialsList
    {
        public int IndentMaterialID { get; set; }
        public int? IndentID { get; set; }
        public string CategoryName { get; set; }
        public string GroupName { get; set; }
        public string MaterialDescription { get; set; }
        public string Color { get; set; }
        public string WastageName { get; set; }
        public string TotalNormsName { get; set; }
        public decimal? SampleNorms { get; set; }
        public decimal? Wastage { get; set; }
        public decimal? WastageQty { get; set; }
        public int? WastageQtyUOM { get; set; }
        public decimal? TotalNorms { get; set; }

        public string MRPNO { get; set; }
        public int? MaterialCategoryMasterId { get; set; }
        public int? MaterialGroupMasterId { get; set; }
        public int? StoreId { get; set; }
        public int? MaterialMasterID { get; set; }

        public string SubstanceRange { get; set; }
        public int? SubstanceMasterId { get; set; }
        public int? SizeRangeMasterID { get; set; }
        public string SizeRangeName { get; set; }
        public int? BOMMaterialID { get; set; }
        public int? BOMID { get; set; }
        public int? ColorMasterID { get; set; }
        public int? BuyerSeason { get; set; }
        public string SeasonName { get; set; }
        public decimal? RequiredQty { get; set; }
        public decimal? IndentQTY { get; set; }
        public decimal? StoreStock { get; set; }
        public decimal? FreeStock { get; set; }
        public decimal? Value { get; set; }
        public int? SupplierId { get; set; }
        public int? OrderEntryId { get; set; }
        public int? UomMasterId { get; set; }
        public string BuyerFullName { get; set; }
        public int? BuyerMasterId { get; set; }
        public string Price { get; set; }
        public string SupplierMasterName { get; set; }
        public bool IsPo { get; set; }
        public string BuyerPoNo { get; set; }
        public bool isImport { get; set; }
        public bool CustomerImport { get; set; }
        public bool DirectImport { get; set; }
        public bool Local { get; set; }
        public int? OrderType { get; set; }
        public string TotalPairs { get; set; }
        public string TotalIndentPairs { get; set; }
    }
    public class PendingQty
    {
        public int? MaterialMasterId_ { get; set; }
        public decimal? Qty { get; set; }
        public decimal? AcceptedQty { get; set; }
        public decimal? AlreadyIssued { get; set; }
        public decimal? CurrentIssue { get; set; }
        public decimal? BalanceStock { get; set; }
    }
    public class StockAdjustMent
    {
        public int? CategoryId { get; set; }
        public int? GroupId { get; set; }
    }
}