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
    public class MaterialGroupMasterMap : EntityTypeConfiguration<MaterialGroupMaster_>
    {
        public MaterialGroupMasterMap()
        {
            HasKey(t => t.MaterialGroupMasterId);
            Property(t => t.MaterialGroupMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.GroupCode);
            Property(t => t.GroupName);
            Property(t => t.SubGroup);
            Property(t => t.MaterialCategoryMasterId);
            Property(t => t.IsSubstance);
            Property(t => t.IsSize);
            Property(t => t.IsColor);
            Property(t => t.IsConsumption);
            Property(t => t.IsInspection);
            Property(t => t.IsReservation);
            Property(t => t.IsDisplay);
            Property(t => t.IsBatchcode);
            Property(t => t.IsMultipleUnits);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.StoreName);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("MaterialGroupMaster");
        }
    }
}
