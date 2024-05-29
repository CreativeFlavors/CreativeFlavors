using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("parentbom", Schema = "public")]
    public class parentbom : BaseEntity
    {
        [Column("bomid")]
        public int BomId { get; set; }
        [Column("bomno")]
        public string BomNo { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("date")]
        public DateTime? Date { get; set; }
        [Column("lastbom")]
        public string LastBom { get; set; } 
        [Column("isactive")]
        public bool IsActive { get; set; } = true;
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
        [Column("isdelete")]
        public bool IsDelete { get; set; } = true;
    }
}
