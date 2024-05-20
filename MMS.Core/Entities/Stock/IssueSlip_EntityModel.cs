using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Core.Entities.Stock
{
    [Table("tbl_issueslipdetails", Schema = "public")]
    public class tbl_issueslipdetails : BaseEntity
    {
        [Column("issueslipid")]
        public int IssueSlipId { get; set; }

        [Column("issueslipfk")]
        public int? IssueSlipFK { get; set; }

        [Column("orderno")]
        public string OrderNo { get; set; }

        [Column("style")]
        public string Style { get; set; }

        [Column("issuetype")]
        public string IssueType { get; set; }

        [Column("noofsets")]
        public decimal? NoOfSets { get; set; }

        [Column("storename")]
        public string StoreName { get; set; }

        [Column("materialname")]
        public string MaterialName { get; set; }

        [Column("colourid")]
        public string ColourId { get; set; }

        [Column("categoryid")]
        public string CategoryId { get; set; }

        [Column("groupid")]
        public string GroupId { get; set; }

        [Column("requiredqty")]
        public decimal? RequiredQty { get; set; }

        [Column("alredayissued")]
        public decimal? AlredayIssued { get; set; }

        [Column("currentissue")]
        public decimal? CurrentIssue { get; set; }

        [Column("rate")]
        public decimal? Rate { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("ischecked")]
        public bool IsChecked { get; set; }

        [Column("piecies")]
        public decimal? Piecies { get; set; }

        [Column("currentstock")]
        public decimal? CurrentStock { get; set; }

        [Column("issuesliptype")]
        public string IssueSlipType { get; set; }

        [Column("materialmasterid")]
        public int MaterialMasterId { get; set; }

        [Column("storemasterid")]
        public int StoreMasterId { get; set; }

        [Column("internalvalue")]
        public string InternalValue { get; set; }

        [Column("directissue_style")]
        public string DirectIssue_Style { get; set; }

        [Column("pieciestype")]
        public int? PieciesType { get; set; }

        [Column("lotno")]
        public string LotNo { get; set; }

        [Column("balanceinthislistlot")]
        public string BalanceInThisListlot { get; set; }

        [Column("balanceinthislisttype")]
        public int? BalanceInthisListType { get; set; }

        [Column("machinename")]
        public int? MachineName { get; set; }

        [Column("subtanceid")]
        public int? SubtanceID { get; set; }

        [Column("requiredtype")]
        public int? RequiredType { get; set; }

        [Column("alreadyusedtype")]
        public int? AlreadyUsedType { get; set; }

        [Column("currentissuestype")]
        public int? CurrentIssuesType { get; set; }

        [Column("conveyorid")]
        public int? ConveyorID { get; set; }

        [Column("issueslipno")]
        public string IssueSlipNo { get; set; }

        [Column("piecesrequiredqty")]
        public decimal? PiecesRequiredQTY { get; set; }

        [Column("piecesalreadyissue")]
        public decimal? PiecesAlreadyIssue { get; set; }

        [Column("piecescurrentissue")]
        public decimal? PiecesCurrentIssue { get; set; }

        [Column("piecesrequiredqtytype")]
        public int? PiecesRequiredQtyType { get; set; }

        [Column("piecesalreadyissuetype")]
        public int? PiecesAlreadyIssueType { get; set; }

        [Column("piecescurrenttype")]
        public int? PiecesCurrentType { get; set; }

        [Column("materialtype")]
        public int? MaterialType { get; set; }

        [Column("internalorderingfor")]
        public int? InternalOrderingFor { get; set; }

        [Column("buyermasterid")]
        public int? BuyerMasterId { get; set; }

        [Column("issuedate")]
        public string IssueDate { get; set; }

        [Column("totalqty")]
        public int? TotalQty { get; set; }

        [Column("bomstyle")]
        public int? BomStyle { get; set; }

        [Column("season")]
        public int? Season { get; set; }

        [Column("balancestock")]
        public decimal? BalanceStock { get; set; }

        [Column("materialtypes")]
        public string MaterialTypes { get; set; }

        [Column("jobworktype_id")]
        public int? Jobworktype_Id { get; set; }

        [Column("jw_name")]
        public int? Jw_Name { get; set; }

        [Column("sheet")]
        public decimal? Sheet { get; set; }

        [Column("jobsheet_pair_code_id")]
        public string Jobsheet_pair_Code_id { get; set; }

        [Column("suppliername_lotno")]
        public int? SupplierName_Lotno { get; set; }

        [Column("suppliernameid")]
        public int? SupplierNameId { get; set; }

        [Column("finished_material")]
        public int? Finished_Material { get; set; }

        [Column("excessapproval")]
        public string ExcessApproval { get; set; }

        [Column("component_id")]
        public int? Component_Id { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }

}
