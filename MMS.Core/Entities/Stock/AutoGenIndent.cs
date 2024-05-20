using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("tblautogenindent", Schema = "public")]
    public class tblAutoGenIndent : BaseEntity
    {
        [Column("autogenerateid")]
        public int AutoGenerateId { get; set; }
        [Column("indentid")]
        public string IndentId { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
