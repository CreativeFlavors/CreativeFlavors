using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core
{
    public abstract class BaseEntity
    {
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
    }
}
