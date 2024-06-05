using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("parentbom_material", Schema = "public")]
    public class parentbom_material : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("bommaterialid")]
        public int BomMaterialId { get; set; }
        [Column("productid")]
        public int ProductId { get; set; }
        [Column("bomid")]
        public int BomID { get; set; }

        [Column("materialcategoryid")]
        public int MaterialCategory { get; set; }
        [Column("materialgroupid")]
        public int? MaterialGroupId { get; set; }
        [Column("materialmasterid")]
        public int? MaterialMasterId { get; set; }
        [Column("uomid")]
        public int? UomId { get; set; }
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
        [Column("isdelete")]
        public bool IsDelete { get; set; } = true;
    }
}
