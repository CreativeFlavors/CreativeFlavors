using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class SizeScheduleMaster : BaseEntity
    {
        public int SizeScheduleMasterId { get; set; }
        public string SizeMatchingNo { get; set; }
        public string SizeRangeName { get; set; }
        public string FromValue { get; set; }
        public string ToValue { get; set; }
        public string Equals { get; set; }
        public int SketchNo { get; set; }
        public int MaterialName { get; set; }

        public int? ShortUnitID { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
