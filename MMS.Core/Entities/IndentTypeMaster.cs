using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("IndentMaster", Schema = "public")]
    public class IndentMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("indentmasterid")]
        public int IndentMasterID { get; set; }
        [Column("indenttypecode")]
        public string IndentTypeCode { get; set; }
        [Column("indenttypename")]
        public string IndentTypeName { get; set; }
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
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
