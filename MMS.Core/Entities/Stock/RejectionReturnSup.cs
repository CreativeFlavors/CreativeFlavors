using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class RejectionReturnSup : BaseEntity
    {
        public int RejectionReturnId { get; set; }
        public string RejectionDcNo { get; set; }
        public string GrnNo { get; set; }
        public DateTime Date { get; set; }
        public int PoNo { get; set; }
        public int SupplierMasterId { get; set; }
        public string IMIRNo { get; set; }
        public int MaterialGroupId { get; set; }
        public int Uom { get; set; }
        public int MaterialMasterId { get; set; }
        public int Quantity { get; set; }
        public int ColourMasterId { get; set; }
        public decimal Rate { get; set; }
        public string Remarks { get; set; }
        public int Pcs { get; set; }
        public decimal AmountTotal { get; set; }
        public bool GatePassDc { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
