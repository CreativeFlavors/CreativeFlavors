using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
  public   class InspectionTypeMaster:BaseEntity
    {
      public int InspectionTypeMasterId { get; set; }
      public int Inspection { get; set; }      
      public string InspectionReportName { get; set; }
      public int OperationId { get; set; }
        public string Code { get; set; }
        public string Parameter { get; set; }
        public string InspectionFrequency { get; set; }
        public string Type { get; set; }
        public string CommonCause { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
