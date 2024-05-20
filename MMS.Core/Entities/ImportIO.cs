using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("importio", Schema = "public")]
    public class ImportIO : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("ionumberid")]
        public int IoNumberID { get; set; }
        [Column("simplemrpid")]
        public int SimpleMRPID { get; set; }
        [Column("sno")]
        public int Sno { get; set; }
        [Column("iono")]
        public string IoNO { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deletedon")]
        public DateTime? Deletedon { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
    }
}
