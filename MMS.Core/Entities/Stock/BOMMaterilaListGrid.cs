using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
   public class BOMMaterilaListGrid
    {
        public int MaterilBomID { get; set; }
        public int BomID { get; set; }
        public string BomNo { get; set; }
        public DateTime? Date { get; set; }
        public int ParentBomNo { get; set; }
        public int CommnBOM1 { get; set; }
        public int CommnBOM2 { get; set; }
        public int CommnBOM3 { get; set; }
        public int CommnBOM4 { get; set; }
        public int CommnBOM5 { get; set; }
        public int MaterialMasterId { get; set; }
        public int MaterialCategoryMasterId { get; set; }
        public string SampleNorms { get; set; }
        public int MaterialGroupMasterId { get; set; }
        public int ColorMasterId { get; set; }
        public decimal Wastage { get; set; }
        public decimal WastageQty { get; set; }
        public int WastageQtyUOM { get; set; }
        public int SubstanceMasterId { get; set; }
        public decimal TotalNorms { get; set; }
        public int TotalNormsUOM { get; set; }
           public int? SizeScheduleMasterId { get; set; }
    }
}
