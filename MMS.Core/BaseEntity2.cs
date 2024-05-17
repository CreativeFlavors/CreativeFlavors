using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core
{
    public class BaseEntity2
    {
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("lastupdateddate")]
        public DateTime? LastUpdatedDate { get; set; }
    }
}
