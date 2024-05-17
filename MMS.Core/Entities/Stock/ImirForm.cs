using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public  class ImirForm : BaseEntity
    {
        public int ImirId { get; set; }
        public string ReportNo { get; set; }
        public DateTime? Date { get; set; }
        public string GateEntryNo { get; set; }
        public string RefInfrepNo { get; set; }
        public int GrnNo { get; set; }
        public string DcNo { get; set; }
        public int SupplierMasterId { get; set; }
        public int MaterialMasterId { get; set; }
        public int ColourMasterId { get; set; }
        public int Store { get; set; }
        public int Uom { get; set; }
        public int InspectionType { get; set; }
        public string DcQty { get; set; }
        public string ReceivedQty { get; set; }
        public string DcPcs { get; set; }
        public string ReceivedPcs { get; set; }
        public string QtyInspected { get; set; }
        public string QtyAccepted { get; set; }
        public string QtyRejected { get; set; }
        public string QtyRejectionPercent { get; set; }
        public string QtyExcess { get; set; }
        public string PcsInspected { get; set; }
        public string PcsAccepted { get; set; }
        public string PcsRejected { get; set; }
        public string PcsRejectionPercent { get; set; }
        public string PcsExcess { get; set; }
        public string Remarks { get; set; }
        public string InspectedBy { get; set; }
        public string QcExcecutive { get;set;}
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
