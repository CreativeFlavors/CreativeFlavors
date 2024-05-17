using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class BasicUnitMaster : BaseEntity
    {
        public int BasicUnitMasterID { get; set; }
        public int CategoryId { get; set; }
        public int GroupID { get; set; }
        public int MaterialID { get; set; }
        public int ShortUnitValue { get; set; }
        public int ShortUnitID { get; set; }
        public int LongUnitValue { get; set; }
        public int LongUnitID { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
