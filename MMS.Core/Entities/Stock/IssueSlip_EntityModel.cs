using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class IssueSlip_MaterialDetails : BaseEntity
    {
        public int IssueSlipId { get; set; }
        public int? IssueSlipFK { get; set; }
        public string OrderNo { get; set; }
        public string Style { get; set; }
        public string IssueType { get; set; }
        public decimal? NoOfSets { get; set; }
        public string StoreName { get; set; }
        public string MaterialName { get; set; }
        public string ColourId { get; set; }
        public string CategoryId { get; set; }
        public string GroupId { get; set; }
        public decimal? RequiredQty { get; set; }
        public decimal? AlredayIssued { get; set; }
        public decimal? CurrentIssue { get; set; }
        public decimal? Rate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsChecked { get; set; }
        public decimal? Piecies { get; set; }
        public decimal? CurrentStock { get; set; }
        public string IssueSlipType { get; set; }
        public int MaterialMasterId { get; set; }
        public int StoreMasterId { get; set; }
        public string InternalValue { get; set; }
        public string DirectIssue_Style { get; set; }
        public int? PieciesType { get; set; }
        public string LotNo { get; set; }
        public string BalanceInThisListlot { get; set; }
        public int? BalanceInthisListType { get; set; }
        public int? MachineName { get; set; }
        public int? SubtanceID { get; set; }
        public int? RequiredType { get; set; }
        public int? AlreadyUsedType { get; set; }
        public int? CurrentIssuesType { get; set; }
        public int? ConveyorID { get; set; }
        public string IssueSlipNo { get; set; }
        public decimal? PiecesRequiredQTY { get; set; }
        public decimal? PiecesAlreadyIssue { get; set; }
        public decimal? PiecesCurrentIssue { get; set; }
        public int? PiecesRequiredQtyType { get; set; }
        public int? PiecesAlreadyIssueType { get; set; }
        public int? PiecesCurrentType { get; set; }
        public int? MaterialType { get; set; }
        public int? InternalOrderingFor { get; set; }
        public int? BuyerMasterId { get; set; }
        public string IssueDate { get; set; }
        public int? TotalQty { get; set; }
        public int? BomStyle { get; set; }
        public int? Season { get; set; }
        public decimal? BalanceStock { get; set; }
        public string MaterialTypes { get; set; }
        public int? Jobworktype_Id { get; set; }
        public int? Jw_Name { get; set; }
        public decimal? sheet { get; set; }
        public string jobsheet_pair_Code_id { get; set; }
        public int? SupplierName_Lotno { get; set; }
        public int? SupplierNameId { get; set; }
        public int? Finished_Material { get; set; }

        public string ExcessApproval { get; set; }
        public int? Component_Id { get; set; }
    }

}
