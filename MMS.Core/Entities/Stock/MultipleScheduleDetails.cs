using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("multiplescheduledetails", Schema = "public")]
    public class MultipleScheduleDetails : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("multiplescheduledetailsid")]
        public int MultipleScheduleDetailsId { get; set; }
        [Column("io")]
        public string Io { get; set; }
        [Column("size")]
        public string Size { get; set; }
        [Column("qty")]
        public string Qty { get; set; }
        [Column("exfdt")]
        public string ExFDt { get; set; }
        [Column("orderentryid")]
        public int OrderEntryId { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
