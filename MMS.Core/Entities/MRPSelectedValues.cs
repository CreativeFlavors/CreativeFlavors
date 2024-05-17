using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("mrpselectedvalues", Schema = "public")]
    public class MRPSelectedValues : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("mrpselectedid")]
        public int MRPSelectedID { get; set; }
        [Column("ionumberid")]
        public string IONumberID { get; set; }
        [Column("simplemrpid")]
        public int SimpleMRPID { get; set; }
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
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }

}
