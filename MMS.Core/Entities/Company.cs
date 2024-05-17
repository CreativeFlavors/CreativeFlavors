using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   // [Table("tbl_Company")]
    public class Company:BaseEntity
    {
      
        public int CompanyCodePK { get; set; }
        public int? StoreID { get; set; }
        public string SupplierName { get; set; }
        public string City { get; set; }       
        public string Address { get; set; }
        public string DeliverTerms { get; set; }     
        public string TermsConditions { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string PaymentTerms { get; set; }
        public string OtherTerms { get; set; }    
        public string Createdby { get; set; }

        public string Updatedby { get; set; }

        public string Deletedby { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }

}
