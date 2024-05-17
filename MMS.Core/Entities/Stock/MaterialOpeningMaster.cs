using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class MaterialOpeningMaster : BaseEntity
    {
        public int MaterialOpeningId { get; set; }
        public string Store { get; set; }
        public int MaterialCategoryMasterId { get; set; }
        public int MaterialGroupMasterId { get; set; }
        public int MaterialMasterId { get; set; }
        public int ColorMasterId { get; set; }
        public DateTime Date { get; set; }
        public int PrimaryUomMasterId { get; set; }
        public int SecondaryUomMasterId { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public int? MaterialPcs { get; set; }
        public int? QtyPcs { get; set; }
        public int? SizeRangeMasterId { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string MaterialCode { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public int? MaterialType { get; set; }
    }
}
