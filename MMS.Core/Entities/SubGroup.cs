using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("subgroup", Schema = "public")]

    public class SubGroup : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("subgroupid")]
        public int SubGroupID { get; set; }
        [Column("subgroupdescription")]
        public string SubGroupDescription { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("isdeletedby")]
        public string IsDeletedBy { get; set; }
        [Column("isdeletedon")]
        public DateTime? IsDeletedOn { get; set; }
    }
}
