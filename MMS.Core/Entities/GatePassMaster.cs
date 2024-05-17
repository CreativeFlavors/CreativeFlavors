using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class GatePassMaster : BaseEntity
    {
        public int GatePassId { get; set; }
        public bool IsReturnable { get; set; }
        public string GatePassNo { get; set; }
        public DateTime Date { get; set; }
        public bool  IsSupplier { get; set; }
        public string PartyName { get; set; }
        public string DeliveryAddress { get; set; }
        public int Material { get; set; }
        public string Quantity { get; set; }
        public int UOM { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public DateTime IfReturnable { get; set; }
        public string Remarks { get; set; }
        public bool IsPrintWithRateValue { get; set; }
        public bool IsPrintWithoutCompanyAddress { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
