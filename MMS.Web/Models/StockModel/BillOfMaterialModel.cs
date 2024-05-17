using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Models.StockModel
{
    public class BillOfMaterialModel
    {
        public int BomId { get; set; }
        public string BomNo { get; set; }
        public string Description { get; set; }
        public int BuyerMasterId { get; set; }
        public string BuyerModel { get; set; }
        public int MeanSize { get; set; }
        public string Date { get; set; }
        public string ParentBomNo { get; set; }
        public string LastBomNoEntered { get; set; }
        public string LinkBomNo { get; set; }
        public string Ammendment { get; set; }
        public string CommonBomNo { get; set; }
        public string PreparedBy { get; set; }
        public string VerifiedBy { get; set; }
        public string CheckedBy { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int MaterialName { get; set; }

        public int GroupID { get; set; }
        public int MaterialGroupMasterId { get; set; }
        public int MaterialCategoryMasterId { get; set; }
        public int ColorMasterId { get; set; }
        public int SubstanceMasterId { get; set; }
        public int? HiddenParentBOMID { get; set; }
        public string SampleNorms { get; set; }
        public decimal? Wastage { get; set; }
        public int SupplierMasterId { get; set; }
        public int UomMasterId { get; set; }
        public int SizeRangeMasterId { get; set; }
        public int? SizeScheduleMasterId { get; set; }
        public decimal? WastageQty { get; set; }
        public int WastageQtyUOM { get; set; }
        public string TotalNorms { get; set; }
        public int TotalNormsUOM { get; set; }
        public int NoOfSets { get; set; }
        public string Conversion { get; set; }
        public string BuyerNorms { get; set; }
        public int ComponentName { get; set; }
        public int? OurNorms { get; set; }
        public decimal? OurNormsPercent { get; set; }
        public int? PurchaseNorms { get; set; }
        public decimal? PurchaseNormsPercent { get; set; }
        public int? IssueNorms { get; set; }
        public decimal? IssueNormsPercent { get; set; }
        public int? CostingNorms { get; set; }
        public decimal? CostingNormsPercent { get; set; }
        public List<int> BomGridlst { get; set; }
        public bool Edit { get; set; }
        public string CommnBOM1 { get; set; }
        public string CommnBOM2 { get; set; }
        public string CommnBOM3 { get; set; }
        public string CommnBOM4 { get; set; }
        public string CommnBOM5 { get; set; }
        public string Grid { get; set; }
        public decimal? NoOfSet { get; set; }
        public int? MaterialSuppliedBY { get; set; }
        public int? SubtanceMasterID { get; set; }
        public bool Status { get; set; }
        public string AddRowFields { get; set; }
        public List<BillOfMaterial> BillOfMaterialList { get; set; }
        public List<BillOfMaterialGrid> BOMMaterialLists { get; set; }
        public List<BillOfMaterial> BillList { get; set; }
        public List<BOMMaterialList> materialList { get; set; }
        public List<BOMAmendmentMaterial> amendmentmaterialList { get; set; }
        public List<BomGrid> bomGridList { get; set; }
        public List<BomHistory> bomHistoryList { get; set; }
        public List<BOMMaterilGrid> bomMaterialGridList { get; set; }

        public List<SelectListItem> bomStyleList { get; set; }
        public string BOMMaterialID { get; set; }
        public int? SimpleMRPID { get; set; }
        public bool IsMRP { get; set; }
        public string CheckBoxsize { get; set; }
        public string CheckBoxIschecked { get; set; }
        public string UploadError { get; set; }
        public string SNO { get; set; }
		  [NotMapped]
         public int? BOMAmendmentMaterialID { get; set; }
    }
    
    public class BOMMaterilGrid
    {
        public int BOMMaterialID { get; set; }
        public int BOMID { get; set; }
        public int MaterialCategoryMasterId { get; set; }
        public int MaterialGroupMasterId { get; set; }
        public int SubstanceMasterId { get; set; }
        public int MaterialMasterId { get; set; }
        public int ColorMasterId { get; set; }
        public string SampleNorms { get; set; }
        public decimal? Wastage { get; set; }
        public decimal? WastageQty { get; set; }
        public decimal? WastageQtyUOM { get; set; }
        public string TotalNorms { get; set; }
        public decimal? TotalNormsUOM { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? Deletedon { get; set; }
        public bool IsDeleted { get; set; }
        public int BomId { get; set; }
        public string BomNo { get; set; }
        public string Description { get; set; }
        public int BuyerMasterId { get; set; }
        public string BuyerModel { get; set; }
        public int MeanSize { get; set; }
        public string Date { get; set; }
        public string ParentBomNo { get; set; }
        public string LastBomNoEntered { get; set; }
        public string LinkBomNo { get; set; }
        public string Ammendment { get; set; }
        public string CommonBomNo { get; set; }
        public string PreparedBy { get; set; }
        public string VerifiedBy { get; set; }
        public string CheckedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int GroupID { get; set; }
        public int SupplierMasterId { get; set; }
        public int UomMasterId { get; set; }
        public int SizeRangeMasterId { get; set; }
        public int NoOfSets { get; set; }
        public string BuyerNorms { get; set; }
        public int ComponentName { get; set; }
        public int? OurNorms { get; set; }
        public decimal? OurNormsPercent { get; set; }
        public int? PurchaseNorms { get; set; }
        public decimal? PurchaseNormsPercent { get; set; }
        public int? IssueNorms { get; set; }
        public decimal? IssueNormsPercent { get; set; }
        public int? CostingNorms { get; set; }
        public decimal? CostingNormsPercent { get; set; }
        public List<int> BomGridlst { get; set; }
        public bool Edit { get; set; }
        public string CommnBOM1 { get; set; }
        public string CommnBOM2 { get; set; }
        public string CommnBOM3 { get; set; }
        public string CommnBOM4 { get; set; }
        public string CommnBOM5 { get; set; }
        public string Grid { get; set; }
        public decimal? NoOfSet { get; set; }
        public int? MaterialSuppliedBY { get; set; }
        public int? SubtanceMasterID { get; set; }
        public int? SizeScheduleMasterId { get; set; }
        public bool Status { get; set; }
        public string AddRowFields { get; set; }
        public List<BillOfMaterial> BillOfMaterialList { get; set; }
        public List<BillOfMaterialGrid> BOMMaterialLists { get; set; }
        public List<BillOfMaterial> BillList { get; set; }
        public List<BOMMaterialList> materialList { get; set; }
        public List<BomGrid> bomGridList { get; set; }
        public List<BomHistory> bomHistoryList { get; set; }
        public List<BOMMaterial> bomMaterialGridList { get; set; }
        List<BOMMaterilGrid> MaterilGrid { get; set; }
        public List<SelectListItem> bomStyleList { get; set; }
        public int BomGridId { get; set; }
        public int? SimpleMRPID { get; set; }
        public bool IsMRP { get; set; }
        public string SNO { get; set; }
        public string CategoryName { get; set; }
        public string GroupName { get; set; }
        public string ColorName { get; set; }
        public string MaterialName { get; set; }
        public string SubtranceRangeName { get; set; }
        public string WastageqtyUOM { get; set; }
        public string Conversion { get; set; }
    }
}