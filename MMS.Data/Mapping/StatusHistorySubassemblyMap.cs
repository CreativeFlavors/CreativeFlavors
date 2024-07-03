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
    public class StatusHistorySubassemblyMap : EntityTypeConfiguration<StatusHistorySubassembly>
    {
        public StatusHistorySubassemblyMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.ProductId)
                .HasColumnName("productid");

            Property(t => t.ProductCode)
                .HasColumnName("productcode");

            Property(t => t.StartedCode)
                .HasColumnName("startedcode");

            Property(t => t.StartedDate)
                .HasColumnName("starteddate");

            Property(t => t.StartedBy)
                .HasColumnName("startedby");

            Property(t => t.InProgressCode)
                .HasColumnName("inprogresscode");

            Property(t => t.InProgressDate)
                .HasColumnName("inprogressdate");

            Property(t => t.InProgressBy)
                .HasColumnName("inprogressby");

            Property(t => t.QualityInspectionCode)
                .HasColumnName("qualityinspectioncode");

            Property(t => t.QualityInspectionDate)
                .HasColumnName("qualityinspectiondate");

            Property(t => t.QualityInspectionBy)
                .HasColumnName("qualityinspectionby");

            Property(t => t.CompletedCode)
                .HasColumnName("completedcode");

            Property(t => t.CompletedDate)
                .HasColumnName("completeddate");

            Property(t => t.CompletedBy)
                .HasColumnName("completedby");

            Property(t => t.PendingApprovalCode)
                .HasColumnName("pendingapprovalcode");

            Property(t => t.PendingApprovalDate)
                .HasColumnName("pendingapprovaldate");

            Property(t => t.PendingApprovalBy)
                .HasColumnName("pendingapprovalby");

            Property(t => t.ReleasedForAssemblyCode)
                .HasColumnName("releasedforassemblycode");

            Property(t => t.ReleasedForAssemblyDate)
                .HasColumnName("releasedforassemblydate");

            Property(t => t.ReleasedForAssemblyBy)
                .HasColumnName("releasedforassemblyby");

            ToTable("statushistorysubassembly");
        }
    }
}
