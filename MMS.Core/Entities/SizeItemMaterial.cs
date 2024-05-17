using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class SizeItemMaterial:BaseEntity
    {
        public int SizeMaterialD { get; set; }
        public int Qty { get; set; }
        public decimal? Rate { get; set; }
        public string SizeRange { get; set; }
        public int? MaterialMasterID { get; set; }
        public string CreatedBY { get; set; }
        public string UpdatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
    }
}
