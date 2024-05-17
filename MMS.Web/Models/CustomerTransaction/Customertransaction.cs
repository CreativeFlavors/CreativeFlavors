using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.CustomerTransaction
{
    public class Customertransaction
    {
        public int Id { get; set; }
        public string customer { get; set; }
        public int buyerid { get; set; }
        public string buyercode { get; set; }
        public int paymentid { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal? PaymentAmount { get; set; }
        public int InvRefNumber { get; set; }
        public DateTime InvDate { get; set; }
        public DateTime InvDueDate { get; set; }
        public decimal InvAmount { get; set; }
        public decimal? RefItems { get; set; }
        public decimal? RefQuantity { get; set; } = 0;
        public decimal? InvPaidAmount { get; set; }
        public decimal? InvBalanceAmount { get; set; }
        public bool IsCustomerOnHold { get; set; }
        public string PaymentRefNo { get; set; }
        public decimal? Cash { get; set; }
        public string CreditNoteRef { get; set; }
        public string Sonumber { get; set; }
        public DateTime? SoDate { get; set; }
        public DateTime? CreditNoteDate { get; set; } 
        public decimal? CreditNoteValue { get; set; }
        public BuyerMaster BuyerMaster { get; set; } = null;
        public paymentmethod paymentmethod { get; set; } = null;
        public customertransaction customertransaction { get; set; } = null;
        public List<customertransaction> customerList { get; set; }


    }
}