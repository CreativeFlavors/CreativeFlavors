using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class IssuesSlip_Single_EntityModel : BaseEntity
    {
        public int IssuesSlipId { get; set; }
        public string IssuesSlipNo { get; set; }
        public int IssuesTypeId{ get; set; }
        public DateTime Date { get; set; }
        public int ConveyorId { get; set; }
        public int InternalOrderId { get; set; }
        public int MachineNameId { get; set; }
        public string StyleNo { get; set; }
        public string NoOfSets_Pairs { get; set; }
        public int StoresId { get; set; }
        public int RateId { get; set; }
        public int CategoryId { get; set; }
        public int SubstanceId { get; set; }
        public int GroupId { get; set; }
        public string RequiredQty { get; set; }
        public int MaterialId { get; set; }
        public int ColourId { get; set; }
        public string AlreadyIssued { get; set; }
        public string LotNo { get; set; }
        public string CurrentIssues { get; set; }
        public int CurrentIssuesVal { get; set; }
        public string BalanceInThisLot_Sqft { get; set; }
        public string BalanceInThisLot_Pcs { get; set; }
        public string CurrentStock { get; set; }
        public int CurrentStock_Uom { get; set; }
        public string FreeStock { get; set; }
        public int FreeStock_Uom { get; set; }
        public string ReservedStock { get; set; }
        public int ReservedStock_Uom { get; set; }
        public string ClosingStockInAllStores { get; set; }
        public int ClosingStockInAllStores_Uom { get; set; }
        public string InTransit { get; set; }
        public int InTransit_Uom { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
