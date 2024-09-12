using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.SupplierMasterModel
{
    public class SupplierModel
    {
        public int SupplierId { get; set; }


        public string Account { get; set; }
        public string AccountName { get; set; }
        public string AccountDescription { get; set; }
        public string Suppliername { get; set; }
        public string suppliercode { get; set; }
        public string Physical1 { get; set; }
        public string PhysicalCode { get; set; }
        public string Telephone1 { get; set; }

        public string Telephone2 { get; set; }

        public string EmailContact { get; set; }

        public string EmailAccounts { get; set; }

        public string EmailEmergency { get; set; }

        public int AccountTypeId { get; set; }

        public string VatNumber { get; set; }

        public string RegNumber { get; set; }
        public decimal? DcBalance { get; set; }
        public decimal? CreditLimit { get; set; }
        public decimal? Interest { get; set; }

        public int TaxTypeId { get; set; }

        public int ForeignCurrency { get; set; }

        public int CurrencyID { get; set; }


        public bool? OnHold { get; set; }

        public bool? Active { get; set; }

    }
}