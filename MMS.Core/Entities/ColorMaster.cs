using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class ColorMaster : BaseEntity
    {
        public int ColorMasterId { get; set; }
        public string Color { get; set; }
        public string BuyerColorCode { get; set; }
        public string ColorImages { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
