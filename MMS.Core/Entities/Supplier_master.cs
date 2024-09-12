using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("supplier_master", Schema = "public")]
    public class Supplier_master : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("supplierid")]
        public int SupplierId { get; set; }
        [Column("Suppliername")]
        public string Suppliername { get; set; }
        [Column("suppliercode")]
        public string suppliercode { get; set; }
        [Column("account")]
        public string Account { get; set; }

        [Column("accountname")]
        public string AccountName { get; set; }

        [Column("accountdescription")]
        public string AccountDescription { get; set; }

        [Column("physical1")]
        public string Physical1 { get; set; }

        [Column("physicalcode")]
        public string PhysicalCode { get; set; }

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
        public int AccountTypeId { get; set; }

        [Column("vatnumber")]
        public string VatNumber { get; set; }

        [Column("regnumber")]
        public string RegNumber { get; set; }

        [Column("dcbalance")]
        public decimal? DcBalance { get; set; }

        [Column("creditlimit")]
        public decimal? CreditLimit { get; set; }

        [Column("interest")]
        public decimal? Interest { get; set; }

        [Column("taxtypeid")]
        public int TaxTypeId { get; set; }

        [Column("foreigncurrency")]
        public int ForeignCurrency { get; set; }

        [Column("currencyid")]
        public int CurrencyID { get; set; }

        [Column("onhold")]
        public bool OnHold { get; set; }

        [Column("createdby")]
        public string createdby { get; set; }
        [Column("updatedby")]
        public string updatedby { get; set; }
        [Column("isactive")]
        public bool isActive { get; set; }
        [Column("deletedby")]
        public string deletedby { get; set; } 
        [Column("isdeleted")]
        public bool? isdeleted { get; set; }   
        [Column("deleteddate")]
        public DateTime? deleteddate { get; set; }
    }
}
