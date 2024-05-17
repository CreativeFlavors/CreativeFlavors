using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class SimpleMRP : BaseEntity
    {
        public int SimpleMRPID { get; set; }      
        public string MRPNO { get; set; }
        public string MRPDate { get; set; }
        public int? MRPType { get; set; }
        public int? BuyerNameid { get; set; }
        public int? WeekNO { get; set; }
        public int? SeasonID { get; set; }
        public int? LotNO { get; set; }
        public int? SizeRangeID { get; set; }
        public string From { get; set; }
        public string TO { get; set; }
        public int? CustomerPlan { get; set; }
        public bool ProductionPlanBasic { get; set; }
        public bool OrderBasic { get; set; }
        public bool JobWork { get; set; }
        public bool RejectionorReplacement { get; set; }
        public bool OverConsumption { get; set; }
        public int Material { get; set; }
        public int? Req_Qty { get; set; }
        public int? UOM { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalNorms { get; set; }
        public bool Detailed { get; set; }
        public bool Consolidate { get; set; }
        public string CreatedBy { get;set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? TotalOrderCount { get; set; }
    }
}
