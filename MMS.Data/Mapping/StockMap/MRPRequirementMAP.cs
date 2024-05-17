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
  public  class MRPRequirementMAP : EntityTypeConfiguration<MRPRequirement>
    {
        public MRPRequirementMAP()
        {
            HasKey(t => t.MRPRequirementID);
            Property(t => t.MRPRequirementID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.BOMID).IsRequired();
            Property(t => t.MaterialCategoryMasterId);
            Property(t => t.MaterialGroupMasterId);
            Property(t => t.SubstanceMasterId);
            Property(t => t.MaterialName);
            Property(t => t.ColorMasterId);
            Property(t => t.SampleNorms);
            Property(t => t.Wastage);
            Property(t => t.WastageQtyUOM);
            Property(t => t.TotalNorms);
            Property(t => t.TotalNormsUOM);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.DeletedBy);
            Property(t => t.Deletedon);
            Property(t => t.IsDeleted);
            Property(t => t.RequiredQty);
            Property(t => t.IsMRP);
            Property(t => t.SimpleMRPID);
            Property(t => t.Rate);
            Property(t => t.Size);
            Property(t => t.SizeRangeMasterID);
            Property(t => t.BuyerNorms);
            Property(t => t.NoofSets);
            Property(t => t.Conversion);
            Property(t => t.ParentBOMID);
            Property(t => t.ParentCommonBOMID);
            ToTable("MRPRequirement");
        }

    }
}
