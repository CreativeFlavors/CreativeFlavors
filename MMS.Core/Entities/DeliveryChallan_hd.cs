using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("DeliveryChallan_hd", Schema = "public")]
    public class DeliveryChallan_hd : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("dcid_hd")]
        public int DCid_hd { get; set; }  
        [Column("currencyid")]
        public int? currencyid { get; set; }
        [Column("deliverychallandate")]
        public DateTime DeliveryChallandate { get; set; }
        [Column("customerid")]
        public int CustomerId { get; set; }
        [Column("items_dc")]
        public int ItemsDc { get; set; }
        [Column("quantity")]
        public decimal? Quantity { get; set; }
        [Column("total_price")]
        public decimal? TotalPrice { get; set; }
        [Column("total_disamount")]
        public decimal? TotalDisAmount { get; set; }
        [Column("total_subtotal")]
        public decimal? TotalSubtotal { get; set; }
        [Column("total_taxamount")]
        public decimal? TotalTaxAmount { get; set; }
        [Column("grand_total")]
        public decimal? GrandTotal { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
}
