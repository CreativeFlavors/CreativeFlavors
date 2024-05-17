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
    public class BomGridMap : EntityTypeConfiguration<bomgriddetail>
    {
        public BomGridMap()
        {
            HasKey(t => t.BomGridId);
            Property(t => t.BomGridId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);            
            Property(t => t.BOMMaterialID);
            Property(t => t.Sno);
            Property(t => t.Component);
            Property(t => t.Length);
            Property(t => t.Width);
            Property(t => t.Nos);
            Property(t => t.SampleNorms);
            Property(t => t.WastagePercent); 
            Property(t => t.WastageQtyGrid);
            Property(t => t.BomId);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.RequirementQty);
            Property(t => t.IsMRP);
            ToTable("BomGridDetail");
        }
    }
}
