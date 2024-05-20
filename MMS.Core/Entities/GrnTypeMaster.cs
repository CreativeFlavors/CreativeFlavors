using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("grntypemasterid", Schema = "public")]
    public class GrnTypeMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("grntypemasterid")]
        public int GrnTypeMasterId { get; set; }
        [Column("issuetype")]
        public string IssueType { get; set; }
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
        [Column("isjobwork")]
        public bool IsJobWork { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

    }
}
