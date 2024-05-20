using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Models.StockModel
{
    [Table("bom", Schema = "public")]
    public class BillOfMaterialModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("bomid")]
        public int BomId { get; set; }
        [Column("bomno")]
        public string BomNo { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("buyermasterid")]
        public int BuyerMasterId { get; set; }
        [Column("buyermodel")]
        public string BuyerModel { get; set; }
        [Column("meansize")]
        public int MeanSize { get; set; }
        [Column("date")]
        public string Date { get; set; }
        [Column("parentbomno")]
        public string ParentBomNo { get; set; }
        [Column("lastbomnoentered")]
        public string LastBomNoEntered { get; set; }
        [Column("linkbomno")]
        public string LinkBomNo { get; set; }
        [Column("ammendment")]
        public string Ammendment { get; set; }
        [Column("commonbomno")]
        public string CommonBomNo { get; set; }
        [Column("preparedby")]
        public string PreparedBy { get; set; }
        [Column("verifiedby")]
        public string VerifiedBy { get; set; }
        [Column("checkedby")]
        public string CheckedBy { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("materialname")]
        public int MaterialName { get; set; }
        [Column("groupid")]
        public int GroupID { get; set; }
        [Column("materialgroupmasterid")]
        public int MaterialGroupMasterId { get; set; }
        [Column("materialcategorymasterid")]
        public int MaterialCategoryMasterId { get; set; }
        [Column("colormasterid")]
        public int ColorMasterId { get; set; }
        [Column("substancemasterid")]
        public int SubstanceMasterId { get; set; }
        [Column("hiddenparentbomid")]
        public int? HiddenParentBOMID { get; set; }
        [Column("samplenorms")]
        public string SampleNorms { get; set; }
        [Column("wastage")]
        public decimal? Wastage { get; set; }
        [Column("suppliermasterid")]
        public int SupplierMasterId { get; set; }
        [Column("uommasterid")]
        public int UomMasterId { get; set; }
        [Column("sizerangemasterid")]
        public int SizeRangeMasterId { get; set; }
        [Column("sizeschedulemasterid")]
        public int? SizeScheduleMasterId { get; set; }
        [Column("wastageqty")]
        public decimal? WastageQty { get; set; }
        [Column("wastageqtyuom")]
        public int WastageQtyUOM { get; set; }
        [Column("wastageqtyuom")]
        public string TotalNorms { get; set; }
        [Column("totalnormsuom")]
        public int TotalNormsUOM { get; set; }
        [Column("noofsets")]
        public int NoOfSets { get; set; }
        [Column("conversion")]
        public string Conversion { get; set; }
        [Column("buyernorms")]
        public string BuyerNorms { get; set; }
        [Column("componentname")]
        public int ComponentName { get; set; }
        [Column("ournorms")]
        public int? OurNorms { get; set; }
        [Column("ournormspercent")]
        public decimal? OurNormsPercent { get; set; }
        [Column("purchasenorms")]
        public int? PurchaseNorms { get; set; }
        [Column("purchasenormspercent")]
        public decimal? PurchaseNormsPercent { get; set; }
        [Column("issuenorms")]
        public int? IssueNorms { get; set; }
        [Column("issuenormspercent")]
        public decimal? IssueNormsPercent { get; set; }
        [Column("costingnorms")]
        public int? CostingNorms { get; set; }
        [Column("costingnormspercent")]
        public decimal? CostingNormsPercent { get; set; }
        public List<int> BomGridlst { get; set; }
        [Column("edit")]
        public bool Edit { get; set; }
        [Column("commnbom1")]
        public string CommnBOM1 { get; set; }
        [Column("commnbom2")]
        public string CommnBOM2 { get; set; }
        [Column("commnbom3")]
        public string CommnBOM3 { get; set; }
        [Column("commnbom4")]
        public string CommnBOM4 { get; set; }
        [Column("commnbom5")]
        public string CommnBOM5 { get; set; }
        [Column("grid")]
        public string Grid { get; set; }
        [Column("noofset")]
        public decimal? NoOfSet { get; set; }
        [Column("materialsuppliedby")]
        public int? MaterialSuppliedBY { get; set; }
        [Column("subtancemasterid")]
        public int? SubtanceMasterID { get; set; }
        [Column("status")]
        public bool Status { get; set; }
        [Column("addrowfields")]
        public string AddRowFields { get; set; }
        public List<Bom> BillOfMaterialList { get; set; }
        public List<BillOfMaterialGrid> BOMMaterialLists { get; set; }
        public List<Bom> BillList { get; set; }
        public List<BOMMaterialList> materialList { get; set; }
        public List<BOMAmendmentMaterial> amendmentmaterialList { get; set; }
        public List<bomgriddetail> bomGridList { get; set; }
        public List<BomHistory> bomHistoryList { get; set; }
        public List<BOMMaterilGrid> bomMaterialGridList { get; set; }
        public List<SelectListItem> bomStyleList { get; set; }
        [Column("bommaterialid")]
        public string BOMMaterialID { get; set; }
        [Column("simplemrpid")]
        public int? SimpleMRPID { get; set; }
        [Column("ismrp")]
        public bool IsMRP { get; set; }
        [Column("checkboxsize")]
        public string CheckBoxsize { get; set; }
        [Column("checkboxischecked")]
        public string CheckBoxIschecked { get; set; }
        [Column("uploaderror")]
        public string UploadError { get; set; }
        [Column("sno")]
        public string SNO { get; set; }
        [NotMapped]
        public int? BOMAmendmentMaterialID { get; set; }
    }
    [Table("bommaterilgrid", Schema = "public")]
    public class BOMMaterilGrid
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("bommaterialid")]
        public int BOMMaterialID { get; set; }
        [Column("bomid")]
        public int BOMID { get; set; }
        [Column("materialcategorymasterid")]
        public int MaterialCategoryMasterId { get; set; }
        [Column("materialgroupmasterid")]
        public int MaterialGroupMasterId { get; set; }
        [Column("substancemasterid")]
        public int SubstanceMasterId { get; set; }
        [Column("materialmasterid")]
        public int MaterialMasterId { get; set; }
        [Column("colormasterid")]
        public int ColorMasterId { get; set; }
        [Column("samplenorms")]
        public string SampleNorms { get; set; }
        [Column("wastage")]
        public decimal? Wastage { get; set; }
        [Column("wastageqty")]
        public decimal? WastageQty { get; set; }
        [Column("wastageqtyuom")]
        public decimal? WastageQtyUOM { get; set; }
        [Column("totalnorms")]
        public string TotalNorms { get; set; }
        [Column("totalnormsuom")]
        public decimal? TotalNormsUOM { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deletedon")]
        public DateTime? Deletedon { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("bomid")]
        public int BomId { get; set; }
        [Column("bomno")]
        public string BomNo { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("buyermasterid")]
        public int BuyerMasterId { get; set; }
        [Column("buyermodel")]
        public string BuyerModel { get; set; }
        [Column("meansize")]
        public int MeanSize { get; set; }
        [Column("date")]
        public string Date { get; set; }
        [Column("parentbomno")]
        public string ParentBomNo { get; set; }
        [Column("lastbomnoentered")]
        public string LastBomNoEntered { get; set; }
        [Column("linkbomno")]
        public string LinkBomNo { get; set; }
        [Column("ammendment")]
        public string Ammendment { get; set; }
        [Column("commonbomno")]
        public string CommonBomNo { get; set; }
        [Column("preparedby")]
        public string PreparedBy { get; set; }
        [Column("verifiedby")]
        public string VerifiedBy { get; set; }
        [Column("checkedby")]
        public string CheckedBy { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("groupid")]
        public int GroupID { get; set; }
        [Column("suppliermasterid")]
        public int SupplierMasterId { get; set; }
        [Column("uommasterid")]
        public int UomMasterId { get; set; }
        [Column("sizerangemasterid")]
        public int SizeRangeMasterId { get; set; }
        [Column("noofsets")]
        public int NoOfSets1 { get; set; }
        [Column("buyernorms")]
        public string BuyerNorms { get; set; }
        [Column("componentname")]
        public int ComponentName { get; set; }
        [Column("ournorms")]
        public int? OurNorms { get; set; }
        [Column("ournormspercent")]
        public decimal? OurNormsPercent { get; set; }
        [Column("purchasenorms")]
        public int? PurchaseNorms { get; set; }
        [Column("purchasenormspercent")]
        public decimal? PurchaseNormsPercent { get; set; }
        [Column("issuenorms")]
        public int? IssueNorms { get; set; }
        [Column("issuenormspercent")]
        public decimal? IssueNormsPercent { get; set; }
        [Column("costingnorms")]
        public int? CostingNorms { get; set; }
        [Column("costingnormspercent")]
        public decimal? CostingNormsPercent { get; set; }
        public List<int> BomGridlst { get; set; }
        [Column("edit")]
        public bool Edit { get; set; }
        [Column("commnbom1")]
        public string CommnBOM1 { get; set; }
        [Column("commnbom2")]
        public string CommnBOM2 { get; set; }
        [Column("commnbom3")]
        public string CommnBOM3 { get; set; }
        [Column("commnbom4")]
        public string CommnBOM4 { get; set; }
        [Column("commnbom5")]
        public string CommnBOM5 { get; set; }
        [Column("grid")]
        public string Grid { get; set; }
        [Column("noofset")]
        public decimal? NoOfSet { get; set; }
        [Column("materialsuppliedby")]
        public int? MaterialSuppliedBY { get; set; }
        [Column("subtancemasterid")]
        public int? SubtanceMasterID { get; set; }
        [Column("sizeschedulemasterid")]
        public int? SizeScheduleMasterId { get; set; }
        [Column("status")]
        public bool Status { get; set; }
        [Column("addrowfields")]
        public string AddRowFields { get; set; }
        public List<Bom> BillOfMaterialList { get; set; }
        public List<BillOfMaterialGrid> BOMMaterialLists { get; set; }
        public List<Bom> BillList { get; set; }
        public List<BOMMaterialList> materialList { get; set; }
        public List<bomgriddetail> bomGridList { get; set; }
        public List<BomHistory> bomHistoryList { get; set; }
        public List<BOMMaterial> bomMaterialGridList { get; set; }
        List<BOMMaterilGrid> MaterilGrid { get; set; }
        public List<SelectListItem> bomStyleList { get; set; }
        [Column("bomgridid")]
        public int BomGridId { get; set; }
        [Column("simplemrpid")]
        public int? SimpleMRPID { get; set; }
        [Column("ismrp")]
        public bool IsMRP { get; set; }
        [Column("sno")]
        public string SNO { get; set; }
        [Column("categoryname")]
        public string CategoryName { get; set; }
        [Column("groupname")]
        public string GroupName { get; set; }
        [Column("colorname")]
        public string ColorName { get; set; }
        [Column("materialname")]
        public string MaterialName { get; set; }
        [Column("subtrancerangename")]
        public string SubtranceRangeName { get; set; }
        [Column("wastageqtyuom")]
        public string WastageqtyUOM { get; set; }
        [Column("conversion")]
        public string Conversion { get; set; }
    }
}