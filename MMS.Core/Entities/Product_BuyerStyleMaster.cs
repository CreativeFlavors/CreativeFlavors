using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("product_buyerstylemaster", Schema = "public")]
    public class Product_BuyerStyleMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("productorbuyerstyleid")]
        public int ProductOrBuyerStyleId { get; set; }
        [Column("buyername_productgroup")]
        public int BuyerName_ProductGroup { get; set; }
        [Column("buyermodel_producttype")]
        public int BuyerModel_ProductType { get; set; }
        [Column("buyerstyle")]
        public string BuyerStyle { get; set; }
        [Column("last")]
        public string Last { get; set; }
        [Column("productcolour")]
        public int? ProductColour { get; set; }
        [Column("ourstyle")]
        public string OurStyle { get; set; }
        [Column("sizerange")]
        public string SizeRange { get; set; }
        [Column("bomno")]
        public string BomNo { get; set; }
        [Column("leathername_1")]
        public string LeatherName_1 { get; set; }
        [Column("leathername_2")]
        public string LeatherName_2 { get; set; }
        [Column("leathername_3")]
        public string LeatherName_3 { get; set; }
        [Column("leathername_4")]
        public string LeatherName_4 { get; set; }
        [Column("leathername_5")]
        public string LeatherName_5 { get; set; }
        [Column("uom")]
        public string UOM { get; set; }
        [Column("width_fit")]
        public string Width_Fit { get; set; }
        [Column("finishedproducttype")]
        public string FinishedProductType { get; set; }
        [Column("standardprice")]
        public string StandardPrice { get; set; }
        [Column("product_image")]
        public string Product_Image { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("weight")]
        public decimal? Weight { get; set; }
        [Column("destination")]
        public string Destination { get; set; }
    }
}
