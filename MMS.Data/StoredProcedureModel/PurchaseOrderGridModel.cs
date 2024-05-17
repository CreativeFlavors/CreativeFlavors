using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    public class PurchaseOrderGridModel
    {
        public int? PoOrderId { get; set; }
        public string PoNo { get; set; }
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
        public int? OrderType { get; set; }
    }
    public class PurchaseOrderGrid
    {
        public string PoNo { get; set; }
        public decimal? AmountTax { get; set; }
        public string SupplierName { get; set; }
        public string PoDate { get; set; }
    }
}
