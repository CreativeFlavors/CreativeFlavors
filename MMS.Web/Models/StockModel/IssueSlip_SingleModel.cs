using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
//using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Models.StockModel
{
    public class IssueSlip_SingleModel
    {
        public int IssueSlipID { get; set; }
        public string IssueSlipNo { get; set; }
        public int ? IssueSlipFK { get; set; }
        public string InternalOderID { get; set; }
        public int? InternalOrderingFor { get; set; }
        public string InternalValue { get; set; }
        public string DirectIssue_Style { get; set; }
        public int IssueSlipMaterialId { get; set; }
        public int OrderEntryId { get; set; }
        public int IssueType_ { get; set; }
        public string Style { get; set; }
        public int IssueType { get; set; }
        public int CuttingIssueTypeID { get; set; }
        public string CategoryName { get; set; }
        public string GroupName { get; set; }
        public string MaterialDescription { get; set; }
        public string LotNo { get; set; }
        public string BalanceInThisListlot { get; set; }
        public int BalanceInthisListType { get; set; }
        public string Color { get; set; }
        public DateTime? Date { get; set; }
        public int MultipleIssueSlipID { get; set; }
        public int ConveyorID { get; set; }
        public int MachineName { get; set; }
        public decimal? NoOfSets { get; set; }
        public decimal? Rate { get; set; }
        public string StoreName { get; set; }
        public int SubtanceID { get; set; }
        public decimal? RequiredQty { get; set; }
        public int RequiredType { get; set; }
        public decimal? AlredayIssued { get; set; }
        public int AlreadyUsedType { get; set; }
        public decimal? Piecies { get; set; }
        public int PieciesType { get; set; }
        public decimal? CurrentIssue { get; set; }
        public int CurrentIssuesType { get; set; }
        public int TotalQty { get; set; }
        public decimal CurrentStock { get; set; }
        public int CurrentStockType { get; set; }
        public decimal UOMStock { get; set; }
        public int UOMStockID { get; set; }
        public decimal FreeStock { get; set; }
        public int FreeStockType { get; set; }
        public decimal FreeStockUOM { get; set; }
        public int FreeStockUOMType { get; set; }
        public decimal ReserverQty { get; set; }
        public int ReserverQtyType { get; set; }
        public decimal ReserverUom { get; set; }
        public int ReservedUOMType { get; set; }
        public decimal ClosingStockinAllStores { get; set; }
        public int ClosingStockinAllStoredType { get; set; }
        public decimal ClosingStockingUOM { get; set; }
        public int ClosingStockingUOMType { get; set; }
        public decimal InTransitValue { get; set; }
        public int InTransitType { get; set; }
        public decimal IntransitUOM { get; set; }
        public int IntransitUOMType { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public decimal? OrderQty { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsChecked { get; set; }
        public string AllIssue { get; set; }

        public string Type { get; set; }

        public int? Component_Id { get; set; }
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
        public int? BomStyle { get; set; }
        public int? Season { get; set; }
        public string MaterialTypes { get; set; }
        public int? Jobworktype_Id { get; set; }
        public int? jobsheet_pair_Code_id { get; set; }
        public int? Jw_Name { get; set; }
        public decimal? sheet { get; set; }
        public int? SupplierName_Lotno { get; set; }

        public int? SupplierNameId { get; set; }

        public int? Finished_Material { get; set; }
       
        public string ExcessApproval { get; set; }
    }
}