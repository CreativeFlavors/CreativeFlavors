using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Web.Models;
//using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;


namespace MMS.Data.StoredProcedureModel
{
    public class IssueSlip_SingleModel
    {
        public int IssueSlipID { get; set; }
        public string IssueSlipNo { get; set; }
        public int? IssueSlipFK { get; set; }
        public string InternalOderID { get; set; }
        public int? InternalOrderingFor { get; set; }
        public string InternalValue { get; set; }
        public string DirectIssue_Style { get; set; }
        public int? IssueSlipMaterialId { get; set; }
        public int? OrderEntryId { get; set; }
        public int? IssueType_ { get; set; }
        public int? Style { get; set; }
        public string StyleNo { get; set; }
        public int? IssueType { get; set; }
        public int? CuttingIssueTypeID { get; set; }
        public string CategoryName { get; set; }
        public string GroupName { get; set; }
        public string MaterialDescription { get; set; }
        public int? Component_Id { get; set; }
        public string LotNo { get; set; }
        public int? BomStyle { get; set; }
        public int? Season { get; set; }     
        public int? BalanceInThisListlot { get; set; }
        public int? BalanceInthisListType { get; set; }
        public string Color { get; set; }
        public DateTime? Date { get; set; }
        public int? MultipleIssueSlipID { get; set; }
        public int? ConveyorID { get; set; }
        public int? MachineName { get; set; }
        public int? NoOfSets { get; set; }
        public decimal? Rate { get; set; }
        public string StoreName { get; set; }
        public int? SubtanceID { get; set; }
        public decimal? RequiredQty { get; set; }
        public int? RequiredType { get; set; }
        public decimal? AlredayIssued { get; set; }
        public int? AlreadyUsedType { get; set; }
        public int? SupplierName_Lotno { get; set; }

        public int? SupplierNameId { get; set; }
        public decimal? Piecies { get; set; }
        public int? PieciesType { get; set; }
        public decimal? CurrentIssue { get; set; }
        public int? CurrentIssuesType { get; set; }
        public int? TotalQty { get; set; }
        public decimal? CurrentStock { get; set; }
        public int? CurrentStockType { get; set; }
        public decimal? UOMStock { get; set; }
        public int? UOMStockID { get; set; }
        public decimal? FreeStock { get; set; }
        public int? FreeStockType { get; set; }
        public decimal? FreeStockUOM { get; set; }
        public int? FreeStockUOMType { get; set; }
        public decimal? ReserverQty { get; set; }
        public int? ReserverQtyType { get; set; }
        public decimal? ReserverUom { get; set; }
        public int? ReservedUOMType { get; set; }
        public decimal? ClosingStockinAllStores { get; set; }
        public int? ClosingStockinAllStoredType { get; set; }
        public decimal? ClosingStockingUOM { get; set; }
        public int? ClosingStockingUOMType { get; set; }
        public decimal? InTransitValue { get; set; }
        public int? InTransitType { get; set; }
        public decimal? IntransitUOM { get; set; }
        public int? IntransitUOMType { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public decimal? OrderQty { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsChecked { get; set; }
        public string AllIssue { get; set; }
        public List<IssueSlip> IssueSlip_SingleModelList { get; set; }
        public List<MultipleIssueSlip> multipleIssueSlipModelList { get; set; }
        public List<IssueSlip_MaterialDetails> issueSlip_MaterialDetails { get; set; }
        public List<SizeRangeQtyRate> sizeRangeQtyRateList { get; set; }
        public List<SizeItemsIssueSlip> sizeItemsIssueSlipList { get; set; }
        // public IEnumerable<string> SelectedItemId { get; set; }
        public string SelectedItemId { get; set; }
        public List<InternalOrderEntryForm> orderList { get; set; }
        public List<SPBomMaterialList> SPBomMaterialList { get; set; }
        public string PiecesRequiredQTY { get; set; }
        public string PiecesAlreadyIssue { get; set; }
        public string PiecesCurrentIssue { get; set; }
        public int? PiecesRequiredQtyType { get; set; }
        public int? PiecesAlreadyIssueType { get; set; }
        public int? PiecesCurrentType { get; set; }
        public int? BuyerMasterId { get; set; }
        public int? MaterialType { get; set; }
        public List<SizeItemMaterial> sizeItemMaterialList { get; set; }
        public string Size { get; set; }
        public string Qty { get; set; }
        public string IssueDate { get; set; }
        public string IssueTypeName { get; set; }
        public Pager Pager { get; set; }
        public bool? IsInternal { get; set; }
        public bool? IsBuyer { get; set; }
        public List<IssueSlip_SingleModel> IssueSlip_ModelList { get; set; }
        public int? Jobworktype_Id { get; set; }
        public int? Jw_Name { get; set; }
        public int? Finished_Material { get; set; }
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