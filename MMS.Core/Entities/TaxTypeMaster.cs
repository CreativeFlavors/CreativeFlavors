using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
  public class TaxTypeMaster :BaseEntity
    {
      public int TaxMasterID { get; set; }
      public string TaxName { get; set; }
      public string TaxMode { get; set; }
      public string TaxValue { get; set; }
      public string TaxCode { get; set; }
      public string TaxRef { get; set; }
      public string Form { get; set; }
      public string TaxOn { get; set; }
      //public string CostOfTheProduct { get; set; }        
        public string TaxValueMode { get; set; }         
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
