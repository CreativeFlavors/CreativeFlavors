using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("subassemblymaster", Schema = "public")]
    public class SubAssemblyMaster : BaseEntity
    {
       // [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("storeid")]
        public int StoreId { get; set; } = 0;

        [Column("productid")]
        public int ProductId { get; set; } = 0;

        [Column("subassemblytypeid")]
        public int SubAssemblyTypeId { get; set; } = 0;

        [Column("productname")]
        public string ProductName { get; set; }

        [Column("qty")]
        public decimal Qty { get; set; } = 0;
        [Column("transactiondate")]
        public DateTime TransactionDate { get; set; }
        [Column("inprogress_qty")]
        public int InProgress_Qty { get; set; } = 0;

        [Column("uom")]
        public int Uom { get; set; } = 0;

        [Column("minstock")]
        public decimal MinStock { get; set; } = 0;

        [Column("maxstock")]
        public decimal MaxStock { get; set; } = 0;

        [Column("tax")]
        public decimal Tax { get; set; } = 0;

        [Column("price")]
        public decimal? Price { get; set; } = 0;

        [Column("cost")]
        public decimal Cost { get; set; } = 0;

        [Column("isactive")]
        public bool IsActive { get; set; } = false;

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("productiontime")]
        public DateTime? ProductionTime { get; set; }
        [Column("productionperday")]
        public decimal ProductionPerDay { get; set; } = 0;
        public List<SubAssemblyMaster> SubAssemblydetailslist { get; set; }
    }
}
