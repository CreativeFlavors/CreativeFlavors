using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("product", Schema = "public")]
    public class product : BaseEntity
    {
        [Column("productid")]
        public int ProductId { get; set; }

        [Column("productcode")]
        public string ProductCode { get; set; }

        [Column("productname")]
        public string ProductName { get; set; }

        [Column("productdesc")]
        public string ProductDesc { get; set; }

        [Column("taxmasterid")]
        public int TaxMasterId { get; set; }

        [Column("uommasterid")]
        public int UomMasterId { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }
        [Column("weight")]
        public decimal? weight { get; set; }
        [Column("cost")]
        public decimal? cost { get; set; }

        [Column("bomno")]
        public int BomNo { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("materialcategorymasterid")]
        public int MaterialCategoryMasterId { get; set; }
        [Column("producttype")]
        public int ProductType { get; set; }
        [Column("storeid")]
        public int StoreId { get; set; }
        [Column("productiontime")]
        public DateTime? ProductionTime { get; set; }
        [Column("minstock")]
        public decimal MinStock { get; set; } = 0;
        [Column("maxstock")]
        public decimal MaxStock { get; set; } = 0;
        [Column("productionperday")]
        public decimal? ProductionPerDay { get; set; } = 0;
    }
}