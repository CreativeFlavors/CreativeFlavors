using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
  public  class MaterialCategoryMaster:BaseEntity
    {
      public int MaterialCategoryMasterId { get; set; }
      public string CategoryCode { get; set; }
      public string CategoryName { get; set; }      
      public string CreatedBy { get; set; }
      public string UpdatedBy { get; set; }
      public bool IsDeleted { get; set; }
      public string DeletedBy { get; set; }
      public DateTime? DeletedDate { get; set; }

    }
}
