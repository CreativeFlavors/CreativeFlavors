using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.BuyerMaserModel
{
    [Table("buyermastermodel", Schema = "public")]
    public class BuyerMasterModel
    {
        public int BuyerMasterId { get; set; }
        public string CustomerName { get; set; }
        public string Account { get; set; }
        public string AccountName { get; set; }
        public string AccountDescription { get; set; }
        public string SwiftCode { get; set; }
        public string Physical1 { get; set; }    
        public string PhysicalCode { get; set; }
       
        public int? CurrencyId { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string EmailContact { get; set; }
        public string EmailAccounts { get; set; }
        public string EmailEmergency { get; set; }
        public int? AccountTypeId { get; set; }
        public string VatNumber { get; set; }
        public string RegNumber { get; set; }
        public float? CreditLimit { get; set; }
        public bool ChargeInterest { get; set; }
        public float? Interest { get; set; }
        public int? TaxTypeId { get; set; }
        public int? ForeignCurrency { get; set; }
        public bool? OnHold { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerShortName { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime DateAdded { get; set; }
        public float? DcBalance { get; set; }
        public float? ForeignDcBalance { get; set; }

        public string Website { get; set; }
        public DateTime OnHoldDate { get; set; }


        //for get purpose
        public List<BuyerMaster1> BuyerMasterList { get; set; }

        public TaxTypeMaster taxTypeMaster { get; set; }
        public CurrencyMaster currencyMaster { get; set; }


    }
}
