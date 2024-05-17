using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class StageMaster:BaseEntity
    {
       public int StageMasterId { get; set; }
       public int ProcessCode { get; set; }
       public string OrderType { get; set; }
       public string StageCode { get; set; }
       public string StageName { get; set; }
       public string SubStage { get; set; }
        public int? Sequence { get; set; }
       public bool IsInspection { get; set; }      
       public string CreatedBy { get; set; }
       public string UpdatedBy { get; set; }
       public bool IsDeleted { get; set; }
       public string DeletedBy { get; set; }
       public DateTime? DeletedDate { get; set; }
    }
}
