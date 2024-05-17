using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public  class StockAdjustmentForm : BaseEntity
    {
        public int StockAdjustmentId { get; set; }
        public DateTime AsOnDate { get; set; }
        public string StockNo { get; set; }
        public int StoreId { get; set; }
        public int CategoryId { get; set; }
        public int GroupId { get; set; }
        public int SubGroupId { get; set; }
        public int? MaterialType { get; set; }
        public int MaterialMasterId { get; set; }
        public int UomId { get; set; }
        public decimal Rate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsApproved { get; set; }

    }
    
}
