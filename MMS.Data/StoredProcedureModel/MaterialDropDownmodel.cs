using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    public class MaterialDropDownmodel
    {
        public string MaterialDescription { get; set; }
        public int MaterialMasterId { get; set; }
        public string Price { get; set; }
        public int? ColorMasterId { get; set; }
        public int? Uom { get; set; }
        public int? UomUnit { get; set; }
        public int? SizeRangeMasterId { get; set; }
        public int? GroupID { get; set; }

        public string MaterialMasterId_Processid { get; set; }
    }
}
