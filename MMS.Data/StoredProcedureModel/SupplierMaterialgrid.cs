using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("Fineshedgoods_report", Schema = "public")]
    public class SupplierMaterialgrid
    {
        [Column("productname")]
        public string ProductName { get; set; }
        [Column("productcode")]
        public string Productcode{ get; set; }
        [Column("suppliername")]
        public string suppliername { get; set; }
        [Column("tax")]
        public string tax { get; set; }
        [Column("supmaterialid")]
        public int supmaterialid { get; set; }
        [Column("uom")]
        public string uom { get; set; } 
        [Column("category")]
        public string category { get; set; }
    }
}
