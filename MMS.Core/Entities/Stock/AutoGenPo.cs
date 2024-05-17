using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("autogenpo", Schema = "public")]
    public class AutoGenPo : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("autogenerateid")]
        public int AutoGenerateId { get; set; }

        [Column("poid")]
        public string PoId { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
