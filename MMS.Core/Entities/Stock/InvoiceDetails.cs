using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("orderheader", Schema = "public")]
    public class orderheader : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("orderid")]
        public int OrderId { get; set; }

        [Column("ccode")]
        public int CCode { get; set; }

        [Column("orderdate")]
        public DateTime? OrderDate { get; set; }

        [Column("orderfullfilldate")]
        public DateTime? OrderFullFillDate { get; set; }

        [Column("notes")]
        public string Notes { get; set; }

        [Column("addcode")]
        public int AddCode { get; set; }

        [Column("shipaddcode")]
        public int ShipAddCode { get; set; }

        [Column("billaddcode")]
        public int BillAddCode { get; set; }

        [Column("totalamount")]
        public decimal TotalAmount { get; set; }

        [Column("discamount")]
        public decimal DiscAmount { get; set; }

        [Column("taxamount")]
        public decimal TaxAmount { get; set; }

        [Column("billamount")]
        public decimal BillAmount { get; set; }

        [Column("freightamount")]
        public decimal FreightAmount { get; set; }

        [Column("isquote")]
        public bool IsQuote { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }

        [Column("totalqty")]
        public int TotalQty { get; set; }

        [Column("totalitems")]
        public int TotalItems { get; set; }

        [Column("shipcode")]
        public string ShipCode { get; set; }

        [Column("expecteddeliverydate")]
        public DateTime? ExpectedDeliveryDate { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("createdby")]
        public int CreatedBy { get; set; }
        [NotMapped]
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [NotMapped]
        [Column("updatedby")]
        public string UpdatedBy { get; set; }

    }
}
