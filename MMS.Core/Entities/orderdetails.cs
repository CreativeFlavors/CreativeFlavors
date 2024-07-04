using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("orderdetails", Schema = "public")]
    public class orderdetails :BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("invoicedt_id")]
        public int invoicedt_id { get; set; }
        [Column("invoicehd_id")]
        public int invoicehd_id { get; set; }
        [Column("salesorderid_dt")]
        public int SalesOrderId_dt { get; set; }
        [Column("productcode")]
        public string ProductCode { get; set; }
        [Column("productid")]
        public int ProductId { get; set; }
        [Column("customerid")]
        public int CustomerId { get; set; }
        [Column("uommasterid")]
        public int UomMasterId { get; set; }
        [Column("taxperid")]
        public int TaxPerId { get; set; }
        [Column("taxvalue")]
        public decimal? TaxValue { get; set; }
        [Column("unitprice")]
        public decimal? UnitPrice { get; set; }
        [Column("quantity")]
        public decimal? Quantity { get; set; } = 0;
        [Column("subtotal")]
        public decimal? SubTotal { get; set; }
        [Column("totalprice")]
        public decimal? TotalPrice { get; set; }
        [Column("discountper")]
        public decimal? DiscountPer { get; set; }
        [Column("discountvalue")]
        public decimal? DiscountValue { get; set; }
        [Column("invoicedate")]
        public DateTime invoicedate { get; set; }
        [Column("currencyid")]
        public int CurrencyId { get; set; }
        [Column("custaddcode")]
        public string CustAddCode { get; set; }
        [Column("custshipcode")]
        public string CustShipCode { get; set; }
        [Column("custbillcode")]
        public string CustBillCode { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
}
