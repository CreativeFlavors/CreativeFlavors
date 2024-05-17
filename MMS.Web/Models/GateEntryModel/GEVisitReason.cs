using MMS.Core.Entities.GateEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.GateEntryModel
{
    public class GEVisitReason
    {

        public int GEVisitReasonId { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public List<GateEntryVisitReasonMaster> VisitReasonList { get; set; }

    }
}