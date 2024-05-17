using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Core.Entities
{
    [Table("projection", Schema = "public")]
    public class Projection : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("projectionid")]
        public int ProjectionId { get; set; }

        [Column("orderindicationno")]
        public string OrderIndicationNo { get; set; }

        [Column("buyermasterid")]
        public int BuyerMasterId { get; set; }

        [Column("seasonmasterid")]
        public int SeasonMasterId { get; set; }

        [Column("style")]
        public string Style { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("weekno")]
        public int WeekNo { get; set; }

        [Column("customerplan")]
        public string CustomerPlan { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("issize")]
        public bool IsSize { get; set; }

        [Column("sizerangemasterid")]
        public int SizeRangeMasterId { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }
         [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("isdeleted")]
         public bool IsDeleted { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
    }
}
