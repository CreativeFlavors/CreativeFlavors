using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    public class subassemblydata
    {
        [Column("productname")]
        public string productname { get; set; }
        [Column("requiredqty")]
        public decimal? Requiredqty { get; set; }
        [Column("subassemblyid")]
        public int subassemblyid { get; set; }
        [Column("bomid")]
        public int Bomid { get; set; }
    }
}
