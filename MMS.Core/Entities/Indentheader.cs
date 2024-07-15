using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("indentheader", Schema = "public")]
    public class Indentheader :BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("indentheaderid")]
        public int IndentHeaderId { get; set; }

        [Column("indentdate")]
        public DateTime? IndentDate { get; set; }

        [Column("porefnumber")]
        public string PoRefNumber { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("indentno")]
        public int? IndentNo { get; set; }
    }
}
