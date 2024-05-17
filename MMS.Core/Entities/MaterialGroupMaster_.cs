using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class MaterialGroupMaster_ : BaseEntity
    {
        public int MaterialGroupMasterId { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string SubGroup { get; set; }
        public int MaterialCategoryMasterId { get; set; }
        //public string MaterialCategoryMasterId { get; set; }
        public bool IsSubstance { get; set; }
        public bool IsSize { get; set; }
        public bool IsColor { get; set; }
        public bool IsConsumption { get; set; }
        public bool IsInspection { get; set; }
        public bool IsReservation { get; set; }
        public bool IsDisplay { get; set; }
        public bool IsBatchcode { get; set; }
        public bool IsMultipleUnits { get; set; }
        public bool IsLeatherType { get; set; }
        public int StoreName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
