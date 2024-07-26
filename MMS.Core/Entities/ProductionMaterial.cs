using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class ProductionMaterial :BaseEntity
    {
        [Key]
        [Column("productionmaterialid")]
        public int ProductionMaterialId { get; set; }

        [Column("productsid")]
        public int ProductsId { get; set; }

        [Column("productsmatid")]
        public int ProductsMatId { get; set; }

        [Column("consumeqty")]
        public decimal? ConsumeQty { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }

        [Column("productionid")]
        public int ProductionId { get; set; }

        [Column("stockonhand")]
        public decimal? StockOnHand { get; set; }

        [Column("productiondate")]
        public DateTime? ProductionDate { get; set; }

        [Column("storecode")]
        public int StoreCode { get; set; }

        [Column("producttype")]
        public int ProductType { get; set; }
    }
}
