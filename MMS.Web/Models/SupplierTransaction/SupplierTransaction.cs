using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.SupplierTransaction
{
    public class SupplierTransaction
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int paymentid { get; set; }

        public decimal? Cash { get; set; }
        public int PoNo { get; set; }
        public DateTime? PoDate { get; set; }

        public DateTime? PaymentDate { get; set; }
        public decimal? PaymentAmount { get; set; }
        public int GrnRefNumber { get; set; }
        public string Supplier { get; set; }
        public decimal? Grnqty { get; set; }
        public string SupplierCode { get; set; }
        public string GrnDate { get; set; }
        public DateTime? GrnDueDate { get; set; }
        public decimal? GrnAmount { get; set; }
        public decimal? GrnPaidAmount { get; set; }
        public decimal? GrnBalanceAmount { get; set; }
        public bool IsTransactionOnHold { get; set; }
        public string PaymentRefNo { get; set; }
        public string CreditNoteRef { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreditNoteDate { get; set; }
        public decimal? CreditNoteValue { get; set; }
        public SupplierMaster SupplierMaster { get; set; }

        public paymentmethod paymentmethod { get; set; } = null;
        public supplierTransaction suppliertransaction { get; set; }
        public List<supplierTransaction> supplierList { get; set; }
    }
}