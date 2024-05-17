using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class ProductionPlanning : BaseEntity
    {
        public int ProductionPlanId { get; set; }
        public string PlanNo { get; set; }
        public string CustomerName { get; set; }
        public string WeekPlan { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string InHouseCapacity { get; set; }
        public int OrderQty { get; set; }
        public int PlanQty { get; set; }
        //public string MultipleIO { get; set; }
        //public string DisplayIO { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? ShipTo { get; set; }
        public bool? IsStyleDurea { get; set; }
        public bool? IsStyle { get; set; }
        public bool? IsSelectAll { get; set; }
        public string Remarks { get; set; }
        public string PlanQtyInMultiples { get; set; }
        public bool? IsAllocateStyleProp { get; set; }
        public bool? IsAllocateSizeProp { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
