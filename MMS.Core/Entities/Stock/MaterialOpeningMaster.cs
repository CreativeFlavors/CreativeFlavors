using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("materialopeningmaster", Schema = "public")]
    public class MaterialOpeningMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("materialopeningid")]
        public int MaterialOpeningId { get; set; }

        [Column("store")]
        public string Store { get; set; }

        [Column("materialcategorymasterid")]
        public int MaterialCategoryMasterId { get; set; }

        [Column("materialgroupmasterid")]
        public int MaterialGroupMasterId { get; set; }

        [Column("materialmasterid")]
        public int MaterialMasterId { get; set; }

        [Column("colormasterid")]
        public int ColorMasterId { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("primaryuommasterid")]
        public int PrimaryUomMasterId { get; set; }

        [Column("secondaryuommasterid")]
        public int SecondaryUomMasterId { get; set; }

        [Column("qty")]
        public decimal Qty { get; set; }

        [Column("rate")]
        public decimal Rate { get; set; }

        [Column("materialpcs")]
        public int? MaterialPcs { get; set; }

        [Column("qtypcs")]
        public int? QtyPcs { get; set; }

        [Column("sizerangemasterid")]
        public int? SizeRangeMasterId { get; set; }

        [Column("remarks")]
        public string Remarks { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("materialcode")]
        public string MaterialCode { get; set; }

        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }

        [Column("isdeleted")]
        public bool IsDeleted { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("materialtype")]
        public int? MaterialType { get; set; }
    }
}

