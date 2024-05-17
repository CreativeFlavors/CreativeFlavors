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
    partial class BOMMaterialListMAP : EntityTypeConfiguration<BOMMaterialList>
    {
        public BOMMaterialListMAP()
        {
            HasKey(t => t.MaterilBomID);
            Property(t => t.MaterilBomID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.BomID);
            Property(t => t.BomNo);
            Property(t => t.Date);
            Property(t => t.ParentBomNo);

            Property(t => t.CommnBOM1);
            Property(t => t.CommnBOM2);
            Property(t => t.CommnBOM3);
            Property(t => t.CommnBOM4);
            Property(t => t.CommnBOM5);
            Property(t => t.MaterialMasterId);
            Property(t => t.MaterialCategoryMasterId);
            Property(t => t.SampleNorms);
            Property(t => t.MaterialGroupMasterId);
            Property(t => t.ColorMasterId);
            Property(t => t.Wastage);
            Property(t => t.WastageQty);
            Property(t => t.WastageQtyUOM);
            Property(t => t.SubstanceMasterId);
            Property(t => t.TotalNorms);
            Property(t => t.TotalNormsUOM);
            Property(t => t.NoOfSet);
            Property(t => t.MaterialSuppliedBY);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            ToTable("BOMMaterialList");
        }

    }
}
