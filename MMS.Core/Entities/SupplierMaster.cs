using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("suppliermaster", Schema = "public")]
    public class SupplierMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("suppliermasterid")]
        public int SupplierMasterId { get; set; }

        [Column("suppliercode")]
        public string SupplierCode { get; set; }
        [Column("suppliername")]
        public string SupplierName { get; set; }
        [Column("currency")]
        public int? Currency { get; set; }
        [Column("addressline1")]
        public string AddressLine1 { get; set; }
        [Column("addressline2")]
        public string AddressLine2 { get; set; }
        [Column("addressline3")]
        public string AddressLine3 { get; set; }
        [Column("country")]
        public string Country { get; set; }
        [Column("contactperson")]
        public string ContactPerson { get; set; }
        [Column("designation")]
        public string Designation { get; set; }
        [Column("mobile")]
        public string Mobile { get; set; }
        [Column("landline")]
        public string LandLine { get; set; }
        [Column("fax")]
        public string Fax { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("stno")]
        public string StNo { get; set; }
        [Column("cstno")]
        public string CstNo { get; set; }
        [Column("range")]
        public string Range { get; set; }
        [Column("division")]
        public string Division { get; set; }
        [Column("plano")]
        public string PLANo { get; set; }
        [Column("eocno")]
        public string EOCNo { get; set; }
        [Column("regnno")]
        public string RegnNo { get; set; }
        [Column("paymentterms")]
        public string PaymentTerms { get; set; }
        [Column("deliveryterms")]
        public string DeliveryTerms { get; set; }
        [Column("packingdetails")]
        public string PackingDetails { get; set; }
        [Column("insurance")]
        public string Insurance { get; set; }
        [Column("portofloading")]
        public string PortOfLoading { get; set; }
        [Column("despatchthrough")]
        public string DespatchThrough { get; set; }
        [Column("freight")]
        public string Freight { get; set; }
        [Column("freightname")]
        public string FreightName { get; set; }
        [Column("form")]
        public string Form { get; set; }
        [Column("formname")]
        public string FormName { get; set; }
        [Column("forwarder")]
        public string Forwarder { get; set; }
        [Column("bankname")]
        public string BankName { get; set; }
        [Column("bankaddress")]
        public string BankAddress { get; set; }
        [Column("bankcode")]
        public string BankCode { get; set; }
        [Column("swiftcode")]
        public string SwiftCode { get; set; }
        [Column("accountnumber")]
        public string AccountNumber { get; set; }
        [Column("iban")]
        public string IBAN { get; set; }
        [Column("otherinformation")]
        public string OtherInformation { get; set; }
        [Column("isdomestic")]
        public bool? IsDomestic { get; set; }
        [Column("isimport")]
        public bool? IsImport { get; set; }

        [Column("materialmasterid")]
        public string MaterialMasterID { get; set; }
        [Column("suppliergstin")]
        public string SupplierGSTIN { get; set; }
        [Column("materialgroupmasterid")]
        public string MaterialGroupMasterID { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("creditlimit")]
        public string CreditLimit { get; set; }

        [Column("creditdays")]
        public string CreditDays { get; set; }

        [Column("accounttypeid")]
        public int? accounttypeid { get; set; }

    }
}
