using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("get_bom_materials", Schema = "public")]
    public class Parentbommatertial
    {
        [Column("Uomname")]
        public string UOMName { get; set; }
        [Column("productname")]
        public string productname { get; set; }
        [Column("requiredqty")]
        public decimal Requiredqty { get; set; }
        [Column("bommaterialid")]
        public int Bommaterialid { get; set; }
        [Column("bomid")]
        public int Bomid { get; set; }
    }
}
