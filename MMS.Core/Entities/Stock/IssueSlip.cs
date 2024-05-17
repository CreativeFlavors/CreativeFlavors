using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class IssueSlip : BaseEntity
    {
        public int IssueSlipID { get; set; }
        public string IssueSlipNo { get; set; }
        public string InternalOderID { get; set; }
        public string StyleNo { get; set; }     
      //  public int LotNo { get; set; }
       // public int BalanceInThisListlot { get; set; }
       // public int BalanceInthisListType { get; set; }          
        public int ConveyorID { get; set; }
        public int MachineName { get; set; }      
        public int SubtanceID { get; set; }        
        public decimal CurrentStock { get; set; }
        public int CurrentStockType { get; set; }     
        public decimal FreeStock { get; set; }
        public int FreeStockType { get; set; }     
        public decimal ReserverQty { get; set; }
        public int ReserverQtyType { get; set; }     
        public decimal ClosingStockinAllStores { get; set; }
        public int ClosingStockinAllStoredType { get; set; }      
        public decimal InTransitValue { get; set; }
        public int InTransitType { get; set; }      
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }


    }
}
