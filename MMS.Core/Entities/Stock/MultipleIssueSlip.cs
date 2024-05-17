using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class MultipleIssueSlip : BaseEntity
    {
        public int MultipleIssueSlipID { get; set; }
        public string IssueSlipNo { get; set; }
        public int IssueType { get; set; }
        public string InternalOderID { get; set; }
        public string StyleNo { get; set; }    
        public string LotNo { get; set; }
        public int CuttingIssueType { get; set; }
        public int ConveyorID { get; set; }
        public int MachineName { get; set; }      
        public int SubtanceID { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsJobWork { get; set; }
        public int? Jobworktype_Id { get; set; }

        public string GateOut_No { get; set; }
        public DateTime? GateOut_No_Datetime { get; set; }
    }
}
