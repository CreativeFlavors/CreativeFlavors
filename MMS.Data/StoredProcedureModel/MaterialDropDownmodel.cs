using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("materialdropdownmodel", Schema = "public")]
    public class MaterialDropDownmodel
    {
        //[System.ComponentModel.DataAnnotations.Key]
        [Column("materialmasterid")]
        public int MaterialMasterId { get; set; }
        [Column("materialdescription")]
        public string MaterialDescription { get; set; }
        [Column("price")]
        public string Price { get; set; }
        [Column("colormasterid")]
        public int? ColorMasterId { get; set; }
        [Column("uom")]
        public int? Uom { get; set; }
        [Column("uomunit")]
        public int? UomUnit { get; set; }
        [Column("sizerangemasterid")]
        public int? SizeRangeMasterId { get; set; }
        [Column("groupid")]
        public int? GroupID { get; set; }
        [Column("materialmasterid_processid")]
        public string MaterialMasterId_Processid { get; set; }
    }
}
