using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("suppliertransaction", Schema = "public")]
    public class supplierTransaction : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("supplierid")]
        public int SupplierId { get; set; }

 
        [Column("paymentdate")]
        public DateTime? PaymentDate { get; set; }

        [Column("paymentamount")]
        public decimal? PaymentAmount { get; set; }

        [Column("grnrefnumber")]
        public int GrnRefNumber { get; set; }

        [Column("grndate")]
        public DateTime GrnDate { get; set; }

        [Column("grnduedate")]
        public DateTime? GrnDueDate { get; set; }

        [Column("grnamount")]
        public decimal? GrnAmount { get; set; }

        [Column("grnpaidamount")]
        public decimal? GrnPaidAmount { get; set; }

        [Column("grnbalanceamount")]
        public decimal? GrnBalanceAmount { get; set; }

        [Column("istransactiononhold")]
        public bool IsTransactionOnHold { get; set; }

        [Column("paymentrefno")]
        public string PaymentRefNo { get; set; }

        [Column("creditnoteref")]
        public string CreditNoteRef { get; set; }

        [Column("creditnotedate")]
        public DateTime? CreditNoteDate { get; set; }

        [Column("creditnotevalue")]
        public decimal? CreditNoteValue { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }


        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("paymentid")]
        public int paymentid { get; set; }

        [Column("cash")]
        public decimal? Cash { get; set; }

    }
}

