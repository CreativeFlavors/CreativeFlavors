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
    public class StatusHistoryMap : EntityTypeConfiguration<StatusHistory>
    {
        public StatusHistoryMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.ProductId)
                .HasColumnName("productid");

            Property(t => t.ProductCode)
                .HasColumnName("productcode");

            Property(t => t.InitiatedCode)
                .HasColumnName("initiatedcode");

            Property(t => t.InitiatedDate)
                .HasColumnName("initiateddate");

            Property(t => t.InitiatedBy)
                .HasColumnName("initiatedby");

            Property(t => t.InProgressCode)
                .HasColumnName("inprogresscode");

            Property(t => t.InProgressDate)
                .HasColumnName("inprogressdate");

            Property(t => t.InProgressBy)
                .HasColumnName("inprogressby");

            Property(t => t.PendingCode)
                .HasColumnName("pendingcode");

            Property(t => t.PendingDate)
                .HasColumnName("pendingdate");

            Property(t => t.PendingBy)
                .HasColumnName("pendingby");

            Property(t => t.QualityCheckingCode)
                .HasColumnName("qualitycheckingcode");

            Property(t => t.QualityCheckingDate)
                .HasColumnName("qualitycheckingdate");

            Property(t => t.QualityCheckingBy)
                .HasColumnName("qualitycheckingby");

            Property(t => t.SequenceCode)
                .HasColumnName("sequencecode");

            Property(t => t.SequenceDate)
                .HasColumnName("sequencedate");

            Property(t => t.SequenceBy)
                .HasColumnName("sequenceby");

            Property(t => t.PackingCode)
                .HasColumnName("packingcode");

            Property(t => t.PackingDate)
                .HasColumnName("packingdate");

            Property(t => t.PackingBy)
                .HasColumnName("packingby");

            ToTable("statushistory");
        }
    }
}
