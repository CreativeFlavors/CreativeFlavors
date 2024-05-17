using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping
{
    public class SubAssemblyMasterMap : EntityTypeConfiguration<SubAssemblyMaster>
    {
        public SubAssemblyMasterMap()
        {

            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.StoreId).IsRequired();
            Property(t => t.ProductId);
            Property(t => t.SubAssemblyTypeId);
            Property(t => t.ProductName);
            Property(t => t.InProgress_Qty);
            Property(t => t.Uom);
            Property(t => t.MinStock);
            Property(t => t.MaxStock);
            Property(t => t.Tax);
            Property(t => t.Price);
            Property(t => t.Cost);
            Property(t => t.IsActive);
            Property(t => t.CreatedBy);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedBy);
            Property(t => t.UpdatedDate);
            ToTable("SubAssemblyMaster");
        }
    }
}
