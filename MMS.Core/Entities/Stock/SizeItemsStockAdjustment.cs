using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class SizeItemsStockAdjustment : BaseEntity
    {
        public int? SizeMaterialD { get; set; }
        public string MaterialDescription { get; set; }
        public string Size { get; set; }
        public string Quantity { get; set; }
        public string ShortUnitName { get; set; }
        public decimal? Rate { get; set; }
        public int? StockAdjustmentFk { get; set; }
        public decimal? BookStock { get; set; }
        public decimal? BookStockValue { get; set; }
        public decimal? PhysicalStock { get; set; }
        public decimal? PhysicalStockValue { get; set; }
        public decimal? VariationStock { get; set; }
        public decimal? VariationStockValue { get; set; }
        public decimal? PlusStock { get; set; }
        public decimal? PlusStockValue { get; set; }
        public decimal? MinusStock { get; set; }

        public decimal? MinusStockValue { get; set; }
        public string CreatedBY { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public int? MaterialMasterId { get; set; }
    }
   
}
