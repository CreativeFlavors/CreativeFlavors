using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MMS.Core.Entities.Stock;

namespace MMS.Data.Mapping.StockMap
{
    public class IssueSlipMap : EntityTypeConfiguration<tbl_issueslipdetails>
    {
        public IssueSlipMap()
        {
            HasKey(t => t.IssueSlipId);
            Property(t => t.IssueSlipId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.IssueSlipFK).IsRequired();
            Property(t => t.OrderNo);
            Property(t => t.Style);
            Property(t => t.IssueType);
            Property(t => t.NoOfSets);
            Property(t => t.StoreName);
            Property(t => t.MaterialName);
            Property(t => t.ColourId);
            Property(t => t.CategoryId);
            Property(t => t.GroupId);
            Property(t => t.RequiredQty);
            Property(t => t.AlredayIssued);
            Property(t => t.CurrentIssue);
            Property(t => t.Piecies);
            Property(t => t.CurrentStock);
            Property(t => t.Rate);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsChecked);
            Property(t => t.IssueSlipType);
            Property(t => t.StoreMasterId);
            Property(t => t.MaterialMasterId);
            Property(t => t.InternalValue);
            Property(t => t.DirectIssue_Style);
            Property(t => t.PieciesType);
            Property(t => t.LotNo);
            Property(t => t.BalanceInThisListlot);
            Property(t => t.BalanceInthisListType);
            Property(t => t.MachineName);
            Property(t => t.SubtanceID);
            Property(t => t.RequiredType);
            Property(t => t.AlreadyUsedType);
            Property(t => t.CurrentIssuesType);
            Property(t => t.ConveyorID);
            Property(t => t.IssueSlipNo);
            Property(t => t.PiecesRequiredQTY);
            Property(t => t.PiecesAlreadyIssue);
            Property(t => t.PiecesCurrentIssue);
            Property(t => t.PiecesRequiredQtyType);
            Property(t => t.PiecesAlreadyIssueType);
            Property(t => t.PiecesCurrentType);
            Property(t => t.MaterialType);
            Property(t => t.InternalOrderingFor);
            Property(t => t.BuyerMasterId);
            Property(t => t.IssueDate);
            Property(t => t.TotalQty);
            Property(t => t.BomStyle);
            Property(t => t.Season);
            Property(t => t.MaterialTypes);
            Property(t => t.Jobworktype_Id);
            Property(t => t.Jw_Name);
            Property(t => t.Sheet);
            Property(t => t.Jobsheet_pair_Code_id);
            Property(t => t.SupplierNameId);
            Property(t => t.SupplierName_Lotno);
            Property(t => t.Finished_Material);
            Property(t => t.ExcessApproval);
            Property(t => t.Component_Id);
            ToTable("tbl_IssueSlipDetails");
        }
    }
}
