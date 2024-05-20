using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("bomgriddetail", Schema = "public")]

    public class bomgriddetail : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [System.ComponentModel.DataAnnotations.Key]
        [Column("bomgridid")]
        public int BomGridId { get; set; }

        [Column("bommaterialid")]
        public int? BOMMaterialID { get; set; }

        [Column("sno")]
        public int? Sno { get; set; }

        [Column("bomid")]
        public int? BomId { get; set; }

        [Column("component")]
        public string Component { get; set; }

        [Column("length")]
        public string Length { get; set; }

        [Column("width")]
        public string Width { get; set; }

        [Column("nos")]
        public int? Nos { get; set; }

        [Column("samplenorms")]
        public decimal? SampleNorms { get; set; }

        [Column("wastagepercent")]
        public int? WastagePercent { get; set; }

        [Column("wastageqtygrid")]
        public decimal? WastageQtyGrid { get; set; }

        [Column("totalnormsqty")]
        public decimal? TotalNormsQty { get; set; }

        [Column("isdeleted")]
        public bool IsDeleted { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }

        [Column("requirementqty")]
        public decimal? RequirementQty { get; set; }

        [Column("ismrp")]
        public bool IsMRP { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
