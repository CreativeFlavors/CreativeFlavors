using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("productprocessdeatils", Schema = "public")]
    public class productprocessdeatils : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("processid")]
        public int ProcessID { get; set; }
        [Column("process")]
        public string process { get; set; }
        [Column("isactive")]
        public bool isactive { get; set; }
        [NotMapped]
        [Column("cratedby")]
        public int CreatedBy { get; set; }
        [NotMapped]
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [NotMapped]
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [NotMapped]
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
    }
}
