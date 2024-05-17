using MMS.Core.Entities.GateEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.GateEntryModel
{
    public class GateEntryVisitor
    {
        public int GateEntryVisitorId { get; set; }
        public string GateEntryNo { get; set; }
        public DateTime? EntryDateTime { get; set; }
        public string EntryDate { get; set; }
        public string ReturnDate { get; set; }
        public string EntryType { get; set; }
        public string Designation { get; set; }
        public string VisitorType { get; set; }
        public string Purpose { get; set; }
        public string VisitorName { get; set; }
        public string CompanyName { get; set; }
        public string VisitorIdNo { get; set; }
        public string VisitorId { get; set; }
        public string ComeBy { get; set; }
        public string VehicleNo { get; set; }
        public string ToMeet { get; set; }
        public string MobileNo { get; set; }
        public string ReasonForVisit { get; set; }
        public string HandCarried { get; set; }
        public DateTime? ReturnDateTime { get; set; }
        public string OtherInfo { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public List<GateEntryVisitorMaster> GateEntryVisitorlist { get; set; }
    }
}