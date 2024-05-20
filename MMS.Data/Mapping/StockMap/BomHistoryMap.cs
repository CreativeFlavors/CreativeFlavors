using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.StockMap
{
    public class BomHistoryMap : EntityTypeConfiguration<BomHistory>
    {
        public BomHistoryMap()
        {
            HasKey(t => t.BOMHistoryId);
            Property(t => t.BOMHistoryId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.BomId);
            Property(t => t.BomNo);
            Property(t => t.Description);
            Property(t => t.BuyerMasterId);
            Property(t => t.BuyerModel);
            Property(t => t.MeanSize);
            Property(t => t.Date);
            Property(t => t.ParentBomNo);
            Property(t => t.LastBomNoEntered);
            Property(t => t.LinkBomNo);
            Property(t => t.Ammendment);
            Property(t => t.CommonBomNo);
            Property(t => t.CommnBOM1);
            Property(t => t.CommnBOM2);
            Property(t => t.CommnBOM3);
            Property(t => t.CommnBOM4);
            Property(t => t.CommnBOM5);
            Property(t => t.PreparedBy);
            Property(t => t.VerifiedBy);
            Property(t => t.CheckedBy);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.MaterialCategoryMasterId);
            Property(t => t.MaterialMasterId);
            Property(t => t.MaterialGroupMasterId);
            Property(t => t.ColorMasterId);
            Property(t => t.SubstanceMasterId);
            Property(t => t.SampleNorms);
            Property(t => t.Wastage);
            Property(t => t.SupplierMasterId);
            Property(t => t.UomMasterId);
            Property(t => t.SizeRangeMasterId);
            Property(t => t.WastageQty);
            Property(t => t.WastageQtyUOM);
            Property(t => t.TotalNorms);
            Property(t => t.TotalNormsUOM);
            Property(t => t.ComponentName);
            Property(t => t.NoOfSets5);
            Property(t => t.BuyerNorms);
            Property(t => t.OurNorms);
            Property(t => t.OurNormsPercent);
            Property(t => t.PurchaseNorms);
            Property(t => t.PurchaseNormsPercent);
            Property(t => t.IssueNorms);
            Property(t => t.IssueNormsPercent);
            Property(t => t.CostingNorms);
            Property(t => t.CostingNormsPercent);       
            ToTable("BomHistory");

        }
    }
}
