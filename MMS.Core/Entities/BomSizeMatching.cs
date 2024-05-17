using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class BomSizeMatching : BaseEntity
    {
        public int BOMSizeMatchingID { get; set; }
        public int BomMaterialID { get; set; }
        public decimal? Frame { get; set; }
        public decimal? Size { get; set; }
        public decimal? Rate { get; set; }
        public int SizeScheduleMasterID { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public decimal? Qty { get; set; }
    }
}
