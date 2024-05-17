using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class BuyerOrderCreation : BaseEntity
    {
        public int BuyerOrderCreationID { get; set; }
        public int BuyerId { get; set; }
       // public int GroupID { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public int StockUnit { get; set; }
        public int SizeRange { get; set; }
        public decimal StandardPrice { get; set; }

        public int ComplexitityFactor { get; set; }
        public string SketchNo { get; set; }
        public int LeatherMainRawMaterial { get; set; }
        public string ProductColor { get; set; }
        public string BuyerStyleNo { get; set; }
        public bool IsDeleted { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? Deletedon { get; set; }

    }
}
