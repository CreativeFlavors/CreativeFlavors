using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class BomGrid : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BomGridId { get; set; }
        public int? BOMMaterialID { get; set; }
        public int? Sno { get; set; }
        public int? BomId { get; set; }
        public string Component { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public int? Nos { get; set; }
        public decimal? SampleNorms { get; set; }
        public int? WastagePercent { get; set; }
        public decimal? WastageQtyGrid { get; set; }
        public decimal? TotalNormsQty { get; set; }
        public bool IsDeleted { get; set; }

        public string DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
        public decimal? RequirementQty { get; set; }
        public bool IsMRP { get; set; }

    }
}
