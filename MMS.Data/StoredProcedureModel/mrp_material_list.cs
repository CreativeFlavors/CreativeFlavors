using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("get_mrp_material_list", Schema = "public")]
    public class mrp_material_list
    {
        [Column("materialmasterid")]
        public int MaterialMasterId { get; set; }
        [Column("materialnames")]
        public string MaterialNames { get; set; }
        [Column("availablestock")]
        public decimal? AvailableStock { get; set; }
        [Column("requiredqty")]
        public decimal? RequiredQty { get; set; }
        [Column("uomname")]
        public string UOMName { get; set; }
    }
}
