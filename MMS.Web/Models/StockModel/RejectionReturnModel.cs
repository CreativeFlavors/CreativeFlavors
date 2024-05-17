using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StockModel
{
    public class RejectionReturnModel
    {
        public int RejectionReturnId { get; set; }
        public string RejectionDcNo { get; set; }
        public string GrnNo { get; set; }
        public string Date { get; set; }
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
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public List<RejectionReturnSup> RejectionReturnSupList { get; set; }
    }
}