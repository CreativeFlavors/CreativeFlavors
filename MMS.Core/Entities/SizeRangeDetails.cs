using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class SizeRangeDetails : BaseEntity
    {
        public int SizeRangeDetailsId { get; set; }
        public string SizeNo { get; set; }
        public string SizeRangeName { get; set; }
       // public int SizeRangeMasterId { get; set; }     
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
