using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("poheader", Schema = "public")]
    public class PoHeader : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("poheaderid")]
        public int PoheaderId { get; set; }

        [Column("podate")]
        public DateTime? PoDate { get; set; }

        [Column("supplierid")]
        public int SupplierId { get; set; }

        [Column("items")]
        public decimal? Items { get; set; }

        [Column("total_discountvalue")]
        public decimal? TotalDiscountValue { get; set; }

        [Column("total_subtotalvalue")]
        public decimal? TotalSubtotalValue { get; set; }

        [Column("total_taxvalue")]
        public decimal? TotalTaxValue { get; set; }

        [Column("total_totalvalue")]
        public decimal? TotalTotalValue { get; set; }

        [Column("expdeliverydate")]
        public DateTime? ExpDeliveryDate { get; set; }

        [Column("paybydate")]
        public DateTime? PayByDate { get; set; }

        [Column("fulfilldate")]
        public DateTime? FulfillDate { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("shipmentdetails")]
        public string ShipmentDetails { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("total_price")]
        public decimal? TotalPrice { get; set; }

        [Column("ponumber")]
        public int PoNumber { get; set; }
    }
}
