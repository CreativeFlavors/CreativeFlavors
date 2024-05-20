using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Web.Models;
//using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace MMS.Data.StoredProcedureModel
{
    [Table("issueslip_singlemodel", Schema = "public")]
    public class IssueSlip_SingleModel
    {
        public int IssueSlipID { get; set; }
        [Column("issueslipno")]
        public string IssueSlipNo { get; set; }
        [Column("issuesslipfk")]
        public int? IssueSlipFK { get; set; }
        [Column("internaloderid")]
        public string InternalOderID { get; set; }
        [Column("internalorderingfor")]
        public int? InternalOrderingFor { get; set; }
        [Column("internalvalue")]
        public string InternalValue { get; set; }
        [Column("directissue_style")]
        public string DirectIssue_Style { get; set; }
        [Column("issueslipmaterialid")]
        public int? IssueSlipMaterialId { get; set; }
        [Column("orderentryid")]
        public int? OrderEntryId { get; set; }
        [Column("issuetype_")]
        public int? IssueType_ { get; set; }
        [Column("style")]
        public int? Style { get; set; }
        [Column("styleno")]
        public string StyleNo { get; set; }
        [Column("issuetype")]
        public int? IssueType { get; set; }
        [Column("cuttingissuetypeid")]
        public int? CuttingIssueTypeID { get; set; }
        [Column("categoryname")]
        public string CategoryName { get; set; }
        [Column("groupname")]
        public string GroupName { get; set; }
        [Column("materialdescription")]
        public string MaterialDescription { get; set; }
        [Column("component_id")]
        public int? Component_Id { get; set; }
        [Column("lotno")]
        public string LotNo { get; set; }
        [Column("bomstyle")]
        public int? BomStyle { get; set; }
        [Column("season")]
        public int? Season { get; set; }
        [Column("balanceinthislistlot")]
        public int? BalanceInThisListlot { get; set; }
        [Column("balanceInthislisttype")]
        public int? BalanceInthisListType { get; set; }
        [Column("color")]
        public string Color { get; set; }
        [Column("date")]
        public DateTime? Date { get; set; }
        [Column("multipleissueslipid")]
        public int? MultipleIssueSlipID { get; set; }
        [Column("conveyorid")]
        public int? ConveyorID { get; set; }
        [Column("machinename")]
        public int? MachineName { get; set; }
        [Column("noofsets")]
        public int? NoOfSets { get; set; }
        [Column("rate")]
        public decimal? Rate { get; set; }
        [Column("storename")]
        public string StoreName { get; set; }
        [Column("subtanceid")]
        public int? SubtanceID { get; set; }
        [Column("requiredqty")]
        public decimal? RequiredQty { get; set; }
        [Column("requiredtype")]
        public int? RequiredType { get; set; }
        [Column("alredayissued")]
        public decimal? AlredayIssued { get; set; }
        [Column("alreadyusedtype")]
        public int? AlreadyUsedType { get; set; }
        [Column("suppliername_lotno")]
        public int? SupplierName_Lotno { get; set; }
        [Column("suppliernameid")]
        public int? SupplierNameId { get; set; }
        [Column("piecies")]
        public decimal? Piecies { get; set; }
        [Column("pieciestype")]
        public int? PieciesType { get; set; }
        [Column("currentissue")]
        public decimal? CurrentIssue { get; set; }
        [Column("currentissuestype")]
        public int? CurrentIssuesType { get; set; }
        [Column("totalqty")]
        public int? TotalQty { get; set; }
        [Column("currentstock")]
        public decimal? CurrentStock { get; set; }
        [Column("currentstocktype")]
        public int? CurrentStockType { get; set; }
        [Column("uomstock")]
        public decimal? UOMStock { get; set; }
        [Column("uomstockid")]
        public int? UOMStockID { get; set; }
        [Column("freestock")]
        public decimal? FreeStock { get; set; }
        [Column("freestocktype")]
        public int? FreeStockType { get; set; }
        [Column("freestockuom")]
        public decimal? FreeStockUOM { get; set; }
        [Column("freestockuomtype")]
        public int? FreeStockUOMType { get; set; }
        [Column("reserverqty")]
        public decimal? ReserverQty { get; set; }
        [Column("reserverqtytype")]
        public int? ReserverQtyType { get; set; }
        [Column("reserveruom")]
        public decimal? ReserverUom { get; set; }
        [Column("reserveduomtype")]
        public int? ReservedUOMType { get; set; }
        [Column("closingstockinallstores")]
        public decimal? ClosingStockinAllStores { get; set; }
        [Column("closingstockinallstoredtype")]
        public int? ClosingStockinAllStoredType { get; set; }
        [Column("closingstockinguom")]
        public decimal? ClosingStockingUOM { get; set; }
        [Column("closingstockinguomtype")]
        public int? ClosingStockingUOMType { get; set; }
        [Column("intransitvalue")]
        public decimal? InTransitValue { get; set; }
        [Column("intransittype")]
        public int? InTransitType { get; set; }
        [Column("intransituom")]
        public decimal? IntransitUOM { get; set; }
        [Column("intransituomtype")]
        public int? IntransitUOMType { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("orderqty")]
        public decimal? OrderQty { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("ischecked")]
        public bool IsChecked { get; set; }
        [Column("allissue")]
        public string AllIssue { get; set; }
        public List<singleissueslip> IssueSlip_SingleModelList { get; set; }
        public List<MultipleIssueSlip> multipleIssueSlipModelList { get; set; }
        public List<tbl_issueslipdetails> issueSlip_MaterialDetails { get; set; }
        public List<SizeRangeQtyRate> sizeRangeQtyRateList { get; set; }
        public List<SizeItemsIssueSlip> sizeItemsIssueSlipList { get; set; }
        // public IEnumerable<string> SelectedItemId { get; set; }
        [Column("selecteditemid")]
        public string SelectedItemId { get; set; }
        public List<OrderEntry> orderList { get; set; }
        public List<SPBomMaterialList> SPBomMaterialList { get; set; }
        [Column("piecerequiredqty")]
        public string PiecesRequiredQTY { get; set; }
        [Column("piecesalreadyissue")]
        public string PiecesAlreadyIssue { get; set; }
        [Column("piecescurrentissue")]
        public string PiecesCurrentIssue { get; set; }
        [Column("piecesqequiredqtytype")]
        public int? PiecesRequiredQtyType { get; set; }
        [Column("piecesalreadyissuetype")]
        public int? PiecesAlreadyIssueType { get; set; }
        [Column("piecescurrenttype")]
        public int? PiecesCurrentType { get; set; }
        [Column("buyermasterId")]
        public int? BuyerMasterId { get; set; }
        [Column("materialtype")]
        public int? MaterialType { get; set; }
        public List<SizeItemMaterial> sizeItemMaterialList { get; set; }
        [Column("size")]
        public string Size { get; set; }
        [Column("qty")]
        public string Qty { get; set; }
        [Column("issuedate")]
        public string IssueDate { get; set; }
        [Column("issuetypename")]
        public string IssueTypeName { get; set; }
        [Column("pager")]
        public Pager Pager { get; set; }
        [Column("isinternal")]
        public bool? IsInternal { get; set; }
        [Column("isbuyer")]
        public bool? IsBuyer { get; set; }
        public List<IssueSlip_SingleModel> IssueSlip_ModelList { get; set; }
        [Column("jobworktype_id")]
        public int? Jobworktype_Id { get; set; }
        [Column("jw_name")]
        public int? Jw_Name { get; set; }
        [Column("finished_material")]
        public int? Finished_Material { get; set; }
        [Column("excessapproval")]
        public string ExcessApproval { get; set; }
    }
    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 10)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }
}