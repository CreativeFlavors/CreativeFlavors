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
    partial class SizeScheduleMasterMap : EntityTypeConfiguration<SizeScheduleMaster>
    {
        public SizeScheduleMasterMap()
        {
            HasKey(t => t.SizeScheduleMasterId);
            Property(t => t.SizeScheduleMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SizeMatchingNo);
            Property(t => t.SizeRangeName);
            Property(t => t.FromValue);
            Property(t => t.ToValue);
            Property(t => t.Equals);
            Property(t => t.SketchNo);
            Property(t => t.MaterialName);
            Property(t => t.ShortUnitID);           
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("SizeScheduleMaster");
        }
    }
}
