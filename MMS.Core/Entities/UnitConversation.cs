using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class UnitConversation : BaseEntity
    {
        public int UnitConversionId { get; set; }
       // public int UOMUnitmasteID { get; set; }
        public int Category { get; set; }
        public int UcGroup { get; set; }
        public int Material { get; set; }
        public decimal ShortUnitNameValue { get; set; }
        public decimal LongUnitNameValue { get; set; }
        public int ShortUnitId { get; set; }
        public int LongUnitID { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
