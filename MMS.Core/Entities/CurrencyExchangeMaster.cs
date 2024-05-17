using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class CurrencyExchangeMaster : BaseEntity
    {
        public DateTime? Date { get; set; }
        public int Currency { get; set; }
        public int CurrencyExchangeMasterId { get; set; }
        public decimal ValueInRupees { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
