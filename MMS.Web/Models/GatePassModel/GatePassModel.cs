using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities;
namespace MMS.Web.Models.GatePassModel
{
    public class GatePassModel
    {
        public int GatePassId { get; set; }
        public bool IsReturnable { get; set; }
        public string GatePassNo { get; set; }
        public string Date { get; set; }
        public bool IsSupplier { get; set; }
        public string PartyName { get; set; }
        public string DeliveryAddress { get; set; }
        public int MaterialName { get; set; }
        public string Quantity { get; set; }
        public int UOM { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public string IfReturnable { get; set; }
        public string Remarks { get; set; }
        public bool IsPrintWithRateValue { get; set; }
        public bool IsPrintWithoutCompanyAddress { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<GatePassMaster> GatePassMasterList { get; set; }

       
    }
}