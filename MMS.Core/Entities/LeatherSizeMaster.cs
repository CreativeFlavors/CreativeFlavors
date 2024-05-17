using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
  public  class LeatherSizeMaster:BaseEntity
    {
      public int LeatherSizeMasterId { get; set; }
      public string Length { get; set; }
      public string Width { get; set; }
      public string Average { get; set; }
      public string CreatedBy { get; set; }
      public string UpdatedBy { get; set; }
      public bool? IsDeleted { get; set; }
      public string DeletedBy { get; set; }
      public DateTime? DeletedDate { get; set; }
    }
}
