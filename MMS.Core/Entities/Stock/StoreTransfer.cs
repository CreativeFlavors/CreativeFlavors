using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class StoreTransfer : BaseEntity
    {

        public int StoreTransferId { get; set; }
        public string TrnNo { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? FromStores { get; set; }
        public DateTime? ToStores { get; set; }
        public int MaterialCategoryId { get; set; }
        public int MaterialGroupId { get; set; }
        public int MaterialMasterId { get; set; }
        public int ColourMasterId { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public string ClosingInAllStores { get; set; }
        public string ClosingInCurrentStores { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        

    }
}
