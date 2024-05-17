using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Core.Entities
{
    [Table("commisioncriteria", Schema = "public")]
    public class CommisionCriteria : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("commisioncriteriaid")]
        public int CommisionCriteriaId { get; set; }

        [Column("commisionname")]
        public string CommisionName { get; set; }

        [Column("criteria")]
        public string Criteria { get; set; }

        [Column("value")]
        public int Value { get; set; }

        [Column("commisionpercent")]
        public int CommisionPercent { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("isdeleted")]
        public bool? IsDeleted { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
    }
}
