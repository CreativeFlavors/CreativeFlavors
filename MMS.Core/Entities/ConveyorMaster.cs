using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Core.Entities
{
    [Table("conveyormaster", Schema = "public")]
    public class ConveyorMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("conveyormasterid")]
        public int ConveyorMasterId { get; set; }

        [Column("conveyorcode")]
        public string ConveyorCode { get; set; }

        [Column("conveyorname")]
        public string ConveyorName { get; set; }

        [Column("capacityperday")]
        public int CapacityPerDay { get; set; }

        [Column("capacityunits")]
        public string CapacityUnits { get; set; }

        [Column("conveyortype")]
        public string ConveyorType { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

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
