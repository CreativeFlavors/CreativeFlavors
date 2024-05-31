using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("subassembly", Schema = "public")]
    public class subassembly :BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("subassemblyid")]
        public int SubassemblyId { get; set; }
        [Column("bomid")]
        public int BomId { get; set; }
        [Column("productid")]
        public int ProductId { get; set; }
        [Column("requiredqty")]
        public decimal? RequiredQty { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; } = true;
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; } = true;
    }
}
