using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.GroupSystemCalculationModel
{
    public class GroupSystemCalculationModel
    {
        public int GroupSystemCalculationId { get; set; }
        public string ProductionPercent { get; set; }
        public decimal Amount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<GroupSystemCalculation> GroupSystemCalculationList { get; set; }
    }
}