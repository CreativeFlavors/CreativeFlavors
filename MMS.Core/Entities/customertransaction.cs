using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("customertransaction", Schema = "public")]
    public class customertransaction : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("customerid")]
        public int CustomerId { get; set; }
        [Column("paymentdate")]
        public DateTime PaymentDate { get; set; }
        [Column("paymentamount")]
        public decimal? PaymentAmount { get; set; }
        [Column("invrefnumber")]
        public int InvRefNumber { get; set; }
        [Column("invdate")]
        public DateTime InvDate { get; set; }
        [Column("invduedate")]
        public DateTime InvDueDate { get; set; }
        [Column("invamount")]
        public decimal? InvAmount { get; set; }
        [Column("invpaidamount")]
        public decimal? InvPaidAmount { get; set; }
        [Column("invbalanceamount")]
        public decimal? InvBalanceAmount { get; set; }
        [Column("iscustomeronhold")]
        public bool IsCustomerOnHold { get; set; }
        [Column("paymentrefno")]
        public string PaymentRefNo { get; set; }
        [Column("debitnoteref")]
        public string Debitnoteref { get; set; }
        [Column("debitnotedate")]
        public DateTime? Debitnotedate { get; set; }
        [Column("debitnotevalue")]
        public decimal? Debitnotevalue { get; set; }       
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("paymentid")]
        public int PaymentId { get; set; }
        [Column("cash")]
        public decimal? Cash { get; set; }
     
    }
}
    