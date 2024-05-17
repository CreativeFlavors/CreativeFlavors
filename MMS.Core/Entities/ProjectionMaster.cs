using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class ProjectionMaster :BaseEntity
    {
        public int ProjectionId { get; set; }
        public string OrderIndicationNo { get; set; }
        public int BuyerMasterId { get; set; }
        public int SeasonMasterId { get; set; }
        public string Style { get; set; }
        public DateTime Date { get; set; }
        public int WeekNo { get; set; }
        public string CustomerPlan { get; set; }
        public int Quantity { get; set; }
        public bool IsSize { get; set; }
        public int SizeRangeMasterId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
