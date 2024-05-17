using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class MRPRequirement : BaseEntity
    {
        public int MRPRequirementID { get; set; }
        public int BOMID { get; set; }
        public int MaterialCategoryMasterId { get; set; }
        public int MaterialGroupMasterId { get; set; }
        public int SubstanceMasterId { get; set; }
        public int MaterialName { get; set; }
        public int ColorMasterId { get; set; }
        public string SampleNorms { get; set; }
        public decimal? Wastage { get; set; }
        public decimal? WastageQty { get; set; }
        public int WastageQtyUOM { get; set; }
        public string TotalNorms { get; set; }
        public int TotalNormsUOM { get; set; }
        public int? SupplierMasterID { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? Deletedon { get; set; }
        public bool IsDeleted { get; set; }
        public decimal? RequiredQty { get; set; }
        public bool IsMRP { get; set; }
        public int? SimpleMRPID { get; set; }
        public int? SizeScheduleMasterId { get; set; }
        public decimal? Rate { get; set; }
        public string Size { get; set; }
        public int SizeRangeMasterID { get; set; }
        public string BuyerNorms { get; set; }
        public decimal? NoofSets { get; set; }
        public string OrderNo { get; set; }
        public string Conversion { get; set; }
        public int? ParentBOMID { get; set; }
        public int? ParentCommonBOMID { get; set; }
    }
}
