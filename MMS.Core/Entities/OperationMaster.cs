using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class OperationMaster:BaseEntity
    {
        public int OperationMasterId { get; set; }
        public string OperationTypeCode { get; set; }
        public string OperationName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool? ISJobWork { get; set; }
    }
}
