using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.ProductionMaterial
{
    public class ProductionMaterialModel
    {
        public int ProductionMaterialId { get; set; }
        public int ProductsId { get; set; }
        public int ProductsMatId { get; set; }
        public decimal? ConsumeQty { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int ProductionId { get; set; }
        public decimal? StockOnHand { get; set; }
        public DateTime? ProductionDate { get; set; }
        public int StoreCode { get; set; }
        public int ProductType { get; set; }
    }
}