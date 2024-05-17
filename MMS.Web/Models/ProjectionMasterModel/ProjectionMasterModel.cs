using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities;

namespace MMS.Web.Models.ProjectionMasterModel
{
    public class ProjectionMasterModel
    {
        public int ProjectionId { get; set; }
        public string OrderIndicationNo { get; set; }
        public int BuyerMasterId { get; set; }
        public int SeasonMasterId { get; set; }
        public string Style { get; set; }
        public string Date { get; set; }
        public int WeekNo { get; set; }
        public string CustomerPlan { get; set; }
        public int Quantity { get; set; }
        public bool IsSize { get; set; }
        public int SizeRangeMasterId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<Projection> ProjectionMasterList { get; set; }
    }
}