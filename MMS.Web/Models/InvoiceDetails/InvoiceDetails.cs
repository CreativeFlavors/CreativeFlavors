using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.InvoiceDetails
{
    public class InvoiceDetails
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
        [NotMapped]
        [Column("buyername")]
        public string BuyerName { get; set; }
        [NotMapped]
        [Column("add1")]
        public string add1 { get; set; }
        [NotMapped]
        [Column("add2")]
        public string add2 { get; set; }
        [NotMapped]
        [Column("add3")]
        public string add3 { get; set; }
        public List<InvoiceDetails> Invoicedetailslist { get; set; }
    }

}