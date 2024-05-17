using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("sizeschedulerange", Schema = "public")]
    public class SizeScheduleRange:BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]

        [Column("sizescheduleid")]
        public int SizeScheduleID { get; set; }
        [Column("sizeschedulemasterid")]
        public int SizeScheduleMasterID { get; set; }
        [Column("frame")]
        public string Frame { get; set; }
        [Column("size")]
        public decimal? Size { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("deletedon")]
        public DateTime? DeletedOn { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }

    }
}
