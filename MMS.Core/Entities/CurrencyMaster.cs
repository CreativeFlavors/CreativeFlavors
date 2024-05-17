using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class CurrencyMaster: BaseEntity
    {
        public int CurrencyMasterId { get; set; }
        public string Symbol { get; set; }
        public string ShortForm { get; set; }
        public string LongForm { get; set; }
        public string LowerDenomination { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
