using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.CommisionCritriaModel
{
    public class CommisionCritriaModel
    {
        public int CommisionCriteriaId { get; set; }
        public string CommisionName { get; set; }
        public string Criteria { get; set; }
        public int Value { get; set; }
        public int CommisionPercent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<CommisionCriteria> CommisionCriteriaList { get; set; }
    }
}