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
    public class StatusProductionMap : EntityTypeConfiguration<StatusProduction>
    {
        public StatusProductionMap()
        {
            HasKey(t => t.StatusId);
            Property(t => t.StatusId).HasColumnName("statusid").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            Property(t => t.CreatedBy).HasColumnName("createdby");
            Property(t => t.UpdatedBy).HasColumnName("updatedby");
            Property(t => t.ProductType).HasColumnName("producttype");
            Property(t => t.AffectIssue).HasColumnName("affectissue");
            Property(t => t.AffectInventory).HasColumnName("affectinventory");
            ToTable("statusproduction");
        }
    }
}
