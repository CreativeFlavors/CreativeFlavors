using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
   public class JobworkMaster:BaseEntity
    {
        public int JW_Id { get; set; }
        
        public string JW_Code { get; set; }
        
        public string JW_Name { get; set; }

        public int? Currency { get; set; }

        public string Address { get; set; }

        public int? Country { get; set; }
        public bool Jobworker_Type { get; set; }


        public string Contact_Person { get; set; }
        
        public string Designation { get; set; }
        
        public string Mobile { get; set; }
        
        public string Phone { get; set; }
        
        public string Fax { get; set; }
        
        public string Email { get; set; }
        
        public string St_No_Head { get; set; }
        
        public string Cst_No_Head { get; set; }
        
        public string Range { get; set; }
        
        public string Division { get; set; }
        
        public string PLA_No { get; set; }
        
        public string EOC_No { get; set; }
        
        public string Regn_No { get; set; }
        
        public string Payment_Terms { get; set; }
        
        public string Delivery_Terms { get; set; }
        
        public string Packing_Details { get; set; }
        
        public string Insurance { get; set; }
        
        public string Port_Of_Loading { get; set; }
        
        public string Despatch_Through { get; set; }
        
        public string Freight { get; set; }
        
        public string FreightName { get; set; }
        
        public string Form { get; set; }
        
        public string FormName { get; set; }
        
        public string Forwarder { get; set; }
        
        public string BankName { get; set; }

        public string BankAddress { get; set; }
        
        public string BankCode { get; set; }
        
        public string SwiftCode { get; set; }
        
        public string AccountNumber { get; set; }
        
        public string IBAN { get; set; }

        public string OtherInformation { get; set; }

        public bool? IsDomestic { get; set; }

        public bool? IsImport { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
