using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
  public  class SubGroup:BaseEntity
    {
        public int SubGroupID { get; set; }
        //public string SubGroupCode { get; set; }
        public string SubGroupDescription { get; set; }
        public bool IsDeleted { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string IsDeletedBy { get; set; }
        public DateTime? IsDeletedOn { get; set; }
    }
}
