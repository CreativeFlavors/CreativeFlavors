using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("sizeschedulemaster", Schema = "public")]
    public class SizeScheduleMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("sizeschedulemasterid")]
        public int SizeScheduleMasterId { get; set; }
        [Column("sizematchingno")]
        public string SizeMatchingNo { get; set; }
        [Column("sizerangename")]
        public string SizeRangeName { get; set; }
        [Column("fromvalue")]
        public string FromValue { get; set; }
        [Column("tovalue")]
        public string ToValue { get; set; }
        [Column("equals")]
        public string Equals { get; set; }
        [Column("sketchno")]
        public int SketchNo { get; set; }
        [Column("materialname")]
        public int MaterialName { get; set; }
        [Column("shortunitid")]
        public int? ShortUnitID { get; set; }
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
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
    }
}
