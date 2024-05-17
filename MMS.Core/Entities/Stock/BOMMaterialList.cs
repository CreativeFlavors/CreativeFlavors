using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class BOMMaterialList : BaseEntity
    {
        public int MaterilBomID { get; set; }
        public int BomID { get; set; }
        public string BomNo { get; set; }
        public DateTime? Date { get; set; }
        public string ParentBomNo { get; set; }
        public string CommnBOM1 { get; set; }
        public string CommnBOM2 { get; set; }
        public string CommnBOM3 { get; set; }
        public string CommnBOM4 { get; set; }
        public string CommnBOM5 { get; set; }
        public int MaterialMasterId { get; set; }
        public int MaterialCategoryMasterId { get; set; }
        public string SampleNorms { get; set; }
        public int? MaterialGroupMasterId { get; set; }
        public int? ColorMasterId { get; set; }
        public decimal? Wastage { get; set; }
        public decimal? WastageQty { get; set; }
        public int? WastageQtyUOM { get; set; }
        public int? SubstanceMasterId { get; set; }
        public int? SizeScheduleMasterId { get; set; }
        public decimal? TotalNorms { get; set; }
        public int? TotalNormsUOM { get; set; }
        public decimal? NoOfSet { get; set; }
        public int? MaterialSuppliedBY { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
