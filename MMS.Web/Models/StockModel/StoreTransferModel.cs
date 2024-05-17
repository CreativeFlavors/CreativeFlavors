using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StockModel
{
    public class StoreTransferModel
    {
        public int StoreTransferId { get; set; }
        public string TrnNo { get; set; }
        public string Date { get; set; }
        public string FromStores { get; set; }
        public string ToStores { get; set; }
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
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

       

        public List<StoreTransfer> storeTransferList { get; set; }
    }
}