using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{

    public class FloorReturnSizeRange : BaseEntity
    {
        public int SizeRangeFloorMaterialId { get; set; }
        public int FloorReturnMaterialDetailsId { get; set; }
        public string SizeRange { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? ReturnedQuantity { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
