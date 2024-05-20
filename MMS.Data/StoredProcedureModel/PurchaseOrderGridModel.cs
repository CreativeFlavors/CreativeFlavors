using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("purchaseordergridmodel", Schema = "public")]
    public class PurchaseOrderGridModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("poorderid")]
        public int? PoOrderId { get; set; }

        [Column("pono")]
        public string PoNo { get; set; }

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

        [Column("ordertype")]
        public int? OrderType { get; set; }
    }

    //[Table("purchaseordergrid", Schema = "public")]

    public class PurchaseOrderGrid
    {

        [Column("pono")]
        public string PoNo { get; set; }

        [Column("amounttax")]
        public decimal? AmountTax { get; set; }

        [Column("suppliername")]
        public string SupplierName { get; set; }

        [Column("podate")]
        public string PoDate { get; set; }
    }
}
