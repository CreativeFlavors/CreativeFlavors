using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StockModel
{
    public class ProductionPlanModel
    {

        public int ProductionPlanId { get; set; }
        public string PlanNo { get; set; }
        public string CustomerName { get; set; }
        public string WeekPlan { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string InHouseCapacity { get; set; }
        public int OrderQty { get; set; }
        public int PlanQty { get; set; }
 public string MultipleIO { get; set; }
        public string DisplayIO { get; set; }

        public string ShipDate { get; set; }
        public string ShipTo { get; set; }
        public bool? IsStyleDurea { get; set; }
        public bool? IsStyle { get; set; }
        public bool? IsSelectAll { get; set; }
        public string Remarks { get; set; }
        public string PlanQtyInMultiples { get; set; }
        public bool? IsAllocateStyleProp { get; set; }
        public bool? IsAllocateSizeProp { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public List<ProductionPlanning> productionPlanningList { get; set; }
        
        public List<int> FromGrid { get; set; }
        public List<int> ToGrid { get; set; }

    }
}