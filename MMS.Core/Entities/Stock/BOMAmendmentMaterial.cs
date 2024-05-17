using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class BOMAmendmentMaterial : BaseEntity
    {
        public int BOMAmendmentMaterialID { get; set; }
        public int BOMID { get; set; }
        public int MaterialCategoryMasterId { get; set; }
        public int MaterialGroupMasterId { get; set; }
        public int? SubstanceMasterId { get; set; }
        public int? MaterialName { get; set; }
        public int? ColorMasterId { get; set; }
        public string SampleNorms { get; set; }
        public decimal? Wastage { get; set; }
        public decimal? WastageQty { get; set; }
        public int? WastageQtyUOM { get; set; }
        public string TotalNorms { get; set; }
        public int? TotalNormsUOM { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public decimal? RequiredQty { get; set; }
        public bool IsMRP { get; set; }
        public int? SupplierMasterID { get; set; }
        public int? SizeScheduleMasterId { get; set; }
        public int? SizeRangeMasterID { get; set; }
        public string BuyerNorms { get; set; }
        public decimal? NoofSets { get; set; }
        public int? ParentBOMID { get; set; }
        public int? OurNorms { get; set; }
        public decimal? OurNormsPercent { get; set; }
        public int? PurchaseNorms { get; set; }
        public decimal? PurchaseNormsPercent { get; set; }
        public int? IssueNorms { get; set; }
        public decimal? IssueNormsPercent { get; set; }
        public int? CostingNorms { get; set; }
        public decimal? CostingNormsPercent { get; set; }
        public string BomNo { get; set; }
        public string Description { get; set; }
        public int? BuyerMasterId { get; set; }
        public string BuyerModel { get; set; }
        public int? MeanSize { get; set; }
        public DateTime? date { get; set; }
        public int? NoOfSets { get; set; }
        public string ParentBomNo { get; set; }
        public string LastBomNoEntered { get; set; }
        public string LinkBomNo { get; set; }
        public string Ammendment { get; set; }
        public string CommonBomNo { get; set; }
        public string CommnBOM1 { get; set; }
        public string CommnBOM2 { get; set; }
        public string CommnBOM3 { get; set; }
        public string CommnBOM4 { get; set; }
        public string CommnBOM5 { get; set; }
        public string PreparedBy { get; set; }
        public string VerifiedBy { get; set; }
        public string CheckedBy { get; set; }
        public int? MaterialMasterId { get; set; }
        public int? UomMasterId { get; set; }
        public int? ComponentName { get; set; }
        public decimal? RequirementQuantity { get; set; }
        public string Conversion { get; set; }

    }
}
