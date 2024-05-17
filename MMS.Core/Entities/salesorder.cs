using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("Salesorder", Schema = "public")]
    public class salesorder : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("salesorderid")]
        public int SalesorderId { get; set; }
        [Column("customerid")]
        public int customerid { get; set; }
        [Column("productcode")]
        public string ProductCode { get; set; }
        [Column("productnameid")]
        public int ProductNameid { get; set; }
        [Column("uommasterid")]
        public int UomMasterId { get; set; }
        [Column("taxperid")]
        public int Taxperid { get; set; }
        [Column("taxvalue")]
        public decimal? TaxValue { get; set; }
        [Column("price")]
        public decimal? Price { get; set; }
        [Column("quantity")]
        public decimal? quantity { get; set; } = 0;
        [Column("subtotal")]
        public decimal? Subtotal { get; set; }
        [Column("grandtotal")]
        public decimal? Grandtotal { get; set; }
        [Column("discountperid")]
        public decimal? Discountperid { get; set; }
        [Column("discountvalue")]
        public decimal? Discountvalue { get; set; }
        [Column("specialinstruction")]
        public string Specialinstruction { get; set; }
        [Column("additionalcommends")]
        public string Additionalcommends { get; set; }
        [Column("quoteref")]
        public string quoteref { get; set; }
        [Column("quotedate")]
        public DateTime quotedate { get; set; }
        [Column("salesorderdate")]
        public DateTime salesorderdate { get; set; }
        [Column("custaddcode")]
        public string custaddcode { get; set; }
        [Column("custshipcode")]
        public string custshipcode { get; set; }
        [Column("custbillcode")]
        public string custbillcode { get; set; }
        [Column("originalquotedate")]
        public DateTime originalquotedate { get; set; }
        [Column("taxinclusive")]
        public bool taxinclusive { get; set; }
        [Column("isactive")]
        public bool isactive { get; set; }
        [Column("createdby")]
        public string createdby { get; set; }
        [Column("updatedby")]
        public string Updatedby { get; set; }

    }
}
