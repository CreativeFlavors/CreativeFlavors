using MMS.Core.Entities.GateEntry;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.GateEntryMap
{
    public class GateEntryVisitorMap : EntityTypeConfiguration<GateEntryVisitorMaster>
    {
        public GateEntryVisitorMap()
        {
            HasKey(t => t.GateEntryVisitorId);
            Property(t => t.GateEntryVisitorId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.GateEntryNo);
            Property(t => t.EntryDateTime);
            Property(t => t.EntryType);
            Property(t => t.Designation);
            Property(t => t.VisitorType);
            Property(t => t.Purpose);
            Property(t => t.VisitorName);
            Property(t => t.CompanyName);
            Property(t => t.VisitorIdNo);
            Property(t => t.VisitorId);
            Property(t => t.ComeBy);
            Property(t => t.VehicleNo);
            Property(t => t.ToMeet);
            Property(t => t.MobileNo);
            Property(t => t.ReasonForVisit);
            Property(t => t.HandCarried);
            Property(t => t.ReturnDateTime);
            Property(t => t.OtherInfo);
            Property(t => t.Remarks);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);

            ToTable("GateEntryVisitor");
        }
    }
}

