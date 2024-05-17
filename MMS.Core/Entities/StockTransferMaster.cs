using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class StockTransferMaster:BaseEntity
    {

       public int StockTransferID { get; set; }
       public int GrnTypeMasterId { get; set; }
       public string GRNNo { get; set; }
       public string IssuedTo { get; set; }
       public int StoreMasterId { get; set; }
       public string TransportDetails { get; set; }
       public string Remarks { get; set; }
       public string Rate { get; set; }
       public string Value { get; set; }
       public string PartyDCNo { get; set; }

       public int MatCategoryId { get; set; }
       public int MatGroupId { get; set; }
       public int ColourId { get; set; }
       public decimal Quantity { get; set; }

       public string ClosingStockInAllStores { get; set; }
       public string ClosingStockingInCurrentStores { get; set; }
       public string ReservedStock { get; set; }
       public string FreeStock { get; set; }

       public DateTime InvoiceRef { get; set; }
       public DateTime InvoiceDate { get; set; }   
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
