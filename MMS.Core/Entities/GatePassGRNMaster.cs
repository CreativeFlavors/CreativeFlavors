using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class GatePassGRNMaster: BaseEntity   
    {
        public int GatePassInvwardId { get; set; }         
        public string  ReceiptNo { get; set; }
        public DateTime Date { get; set; }
        public bool IsSupplier { get; set; }
        public string PartyName { get; set; }
        public string RefGatePassNo { get; set; }
        public int Material { get; set; }
        public string Quantity { get; set; }
        public int UOM { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }         
        public string Instructions { get; set; }
        public bool IsPrintWithRateValue { get; set; }
        public bool IsPrintWithoutCompanyAddress { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
