using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class UomMaster : BaseEntity
    {
        public int UomMasterId { get; set; }
        public string ShortUnitName { get; set; }
        public string LongUnitName { get; set; }
       // public DateTime? CreatedDate { get; set; }
      //  public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
