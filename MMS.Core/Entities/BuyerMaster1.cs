using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("buyermaster1", Schema = "public")]

    public class BuyerMaster1 : BaseEntity
    {

        [System.ComponentModel.DataAnnotations.Key]
        [Column("buyermasterid")]
        public int BuyerMasterId { get; set; }


        [Column("customername")]

        public string CustomerName { get; set; }

        [Column("account")]
        public string Account { get; set; }

        [Column("accountname")]

        public string AccountName { get; set; }

        [Column("accountdescription")]

        public string AccountDescription { get; set; }

        [Column("swiftcode")]
        public string SwiftCode { get; set; }

        [Column("physical1")]
        public string Physical1 { get; set; }

        [Column("physicalcode")]

        public string PhysicalCode { get; set; }
        [Column("currencyid")]

        public int? CurrencyId { get; set; }

        [Column("telephone1")]

        public string Telephone1 { get; set; }

        [Column("telephone2")]

        public string Telephone2 { get; set; }

        [Column("emailcontact")]

        public string EmailContact { get; set; }

        [Column("emailaccounts")]

        public string EmailAccounts { get; set; }

        [Column("emailemergency")]

        public string EmailEmergency { get; set; }

        [Column("accounttypeid")]

        public int? AccountTypeId { get; set; }

        [Column("vatnumber")]

        public string VatNumber { get; set; }

        [Column("regnumber")]

        public string RegNumber { get; set; }



        [Column("creditlimit")]

        public float? CreditLimit { get; set; }

        [Column("chargeinterest")]

        public bool ChargeInterest { get; set; }

        [Column("interest")]

        public float? Interest { get; set; }



        [Column("taxtypeid")]

        public int? TaxTypeId { get; set; }

        [Column("foreigncurrency")]

        public int? ForeignCurrency { get; set; }



        [Column("onhold")]
        public bool OnHold { get; set; }

        [Column("active")]
        public bool Active { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("isdeleted")]
        public bool IsDeleted { get; set; }


        [Column("buyercode")]

        public string BuyerCode { get; set; }

        [Column("buyershortname")]

        public string BuyerShortName { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }

        [Column("dateadded")]
        public DateTime? DateAdded { get; set; }


        [Column("dcbalance")]
        public float? DcBalance { get; set; }

        [Column("foreigndcbalance")]
        public float? ForeignDcBalance { get; set; }

        [Column("website")]
        public string Website { get; set; }

        [Column("onholddate")]
        public DateTime? OnHoldDate {  get; set; }



    }
}


