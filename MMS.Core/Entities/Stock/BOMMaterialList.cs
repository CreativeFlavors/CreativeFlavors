using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("bommateriallist", Schema = "public")]
    public class BOMMaterialList : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("materilbomid")]
        public int MaterilBomID { get; set; }
        [Column("bomid")]
        public int BomID { get; set; }
        [Column("bomno")]
        public string BomNo { get; set; }
        [Column("date")]
        public DateTime? Date { get; set; }
        [Column("parentbomno")]
        public string ParentBomNo { get; set; }
        [Column("commnbom1")]
        public string CommnBOM1 { get; set; }
        [Column("commnbom2")]
        public string CommnBOM2 { get; set; }
        [Column("commnbom3")]
        public string CommnBOM3 { get; set; }
        [Column("commnbom4")]
        public string CommnBOM4 { get; set; }
        [Column("commnbom5")]
        public string CommnBOM5 { get; set; }
        [Column("materialmasterid")]
        public int MaterialMasterId { get; set; }
        [Column("materialcategorymasterid")]
        public int MaterialCategoryMasterId { get; set; }
        [Column("samplenorms")]
        public string SampleNorms { get; set; }
        [Column("materialgroupmasterid")]
        public int? MaterialGroupMasterId { get; set; }
        [Column("colormasterid")]
        public int? ColorMasterId { get; set; }
        [Column("wastage")]
        public decimal? Wastage { get; set; }
        [Column("wastageqty")]
        public decimal? WastageQty { get; set; }
        [Column("wastageqtyuom")]
        public int? WastageQtyUOM { get; set; }
        [Column("substancemasterid")]
        public int? SubstanceMasterId { get; set; }
        [Column("sizeschedulemasterid")]
        public int? SizeScheduleMasterId { get; set; }
        [Column("totalnorms")]
        public decimal? TotalNorms { get; set; }
        [Column("totalnormsuom")]
        public int? TotalNormsUOM { get; set; }
        [Column("noofset")]
        public decimal? NoOfSet { get; set; }
        [Column("materialsuppliedby")]
        public int? MaterialSuppliedBY { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }

    }
}
