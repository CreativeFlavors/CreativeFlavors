using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
  public  class PermissionMaster : BaseEntity
    {
      public int PermissionID { get; set; }
      public string PermissionName { get; set; }
      public string PermissionDesc { get; set; }
      public string CreatedBy { get; set; }
      public string UpdatedBy { get; set; }
    }
}
