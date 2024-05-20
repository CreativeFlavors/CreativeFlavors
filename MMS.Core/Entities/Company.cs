using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("company", Schema = "public")]
    public class Company:BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("companycodepk")]
        public int CompanyCodePK { get; set; }

        [Column("storeid")]
        public int? StoreID { get; set; }

        [Column("suppliername")]
        public string SupplierName { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("deliverterms")]
        public string DeliverTerms { get; set; }

        [Column("termsconditions")]
        public string TermsConditions { get; set; }

        [Column("companyname")]
        public string CompanyName { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("paymentterms")]
        public string PaymentTerms { get; set; }

        [Column("otherterms")]
        public string OtherTerms { get; set; }

        [Column("createdby")]
        public string Createdby { get; set; }

        [Column("updatedby")]

        public string Updatedby { get; set; }


        [Column("deletedby")]
        public string Deletedby { get; set; }


        [Column("deletedon")]
        public DateTime? DeletedOn { get; set; }


        [Column("isdeleted")]
        public bool IsDeleted { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }

}
