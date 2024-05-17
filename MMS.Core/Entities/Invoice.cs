using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("order_header", Schema = "public")]
    public class order_header : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("customerid")]
        public int CustomerId { get; set; }
        [Column("orderid")]
        public int OrderId { get; set; }

        [Column("doctypeid")]
        public int DoctypeId { get; set; }

        [Column("revision")]
        public int Revision { get; set; }

        [Column("docstateid")]
        public int DocStateId { get; set; }

        [Column("quoteref")]
        public string QuoteRef { get; set; }

        [Column("soref")]
        public string SoRef { get; set; }

        [Column("refdate")]
        public DateTime RefDate { get; set; }

        [Column("refitems")]
        public decimal RefItems { get; set; }

        [Column("refquantity")]
        public decimal RefQuantity { get; set; } = 0;

        [Column("refduedate")]
        public DateTime RefDueDate { get; set; }

        [Column("refextendedvalue")]
        public decimal RefExtendedValue { get; set; }

        [Column("refdiscountvalue")]
        public decimal RefDiscountValue { get; set; }

        [Column("refgrossvalue")]
        public decimal RefGrossValue { get; set; }

        [Column("reftaxvalue")]
        public decimal RefTaxValue { get; set; }

        [Column("refnetvalue")]
        public decimal RefNetValue { get; set; }

        [Column("docdate")]
        public DateTime DocDate { get; set; }

        [Column("docitems")]
        public decimal DocItems { get; set; }

        [Column("docquantity")]
        public decimal DocQuantity { get; set; }

        [Column("docduedate")]
        public DateTime DocDueDate { get; set; }

        [Column("docextendedvalue")]
        public decimal DocExtendedValue { get; set; }

        [Column("docdiscountvalue")]
        public decimal DocDiscountValue { get; set; }

        [Column("docgrossvalue")]
        public decimal DocGrossValue { get; set; }

        [Column("doctaxvalue")]
        public decimal DocTaxValue { get; set; }

        [Column("docnetvalue")]
        public decimal DocNetValue { get; set; }

        [Column("custaddcode")]
        public int CustAddCode { get; set; }

        [Column("custshipcode")]
        public int CustShipCode { get; set; }

        [Column("custbillcode")]
        public int CustBillCode { get; set; }

        [Column("headerdiscountperc")]
        public string HeaderDiscountPerc { get; set; }

        [Column("shippingnotes")]
        public string ShippingNotes { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("docdescription")]
        public string DocDescription { get; set; }

        [Column("originalquotedate")]
        public DateTime OriginalQuoteDate { get; set; }

        [Column("quotedate")]
        public DateTime QuoteDate { get; set; }

        [Column("duedate")]
        public DateTime DueDate { get; set; }

        [Column("taxinclusive")]
        public bool TaxInclusive { get; set; }

        [Column("emailsent")]
        public bool EmailSent { get; set; }

        [Column("externalref")]
        public string ExternalRef { get; set; }

        [Column("creditlimit")]
        public decimal CreditLimit { get; set; }

        [Column("dcbalance")]
        public decimal DCBalance { get; set; }

        [Column("foreigndcbalance")]
        public decimal ForeignDCBalance { get; set; }

        [Column("currencyid")]
        public int CurrencyId { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }

        [Column("createdby")]
        public int CreatedBy { get; set; }

        [Column("createddate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [NotMapped]
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
        [NotMapped]
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [NotMapped]
        [Column("custmername")]
        public string CustmerName { get; set; }
        [NotMapped]
        [Column("add1")]
        public string Add1 { get; set; }
        [NotMapped]
        [Column("add2")]
        public string Add2 { get; set; }
        [NotMapped]
        [Column("add3")]
        public string Add3 { get; set; }
        [NotMapped]
        [Column("qtyprocessed")]
        public string QtyProcessed { get; set; }

    }
}
