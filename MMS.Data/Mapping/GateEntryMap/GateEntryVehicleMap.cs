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
  public  class GateEntryVehicleMap : EntityTypeConfiguration<GateEntryVehicleMaster>
    {
        public GateEntryVehicleMap()
        {
            HasKey(t => t.VehicleEntryId);
            Property(t => t.VehicleEntryId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.GateEntryNo).IsRequired();
            Property(t => t.EntryDateTime).IsRequired();
            Property(t => t.VehicleId);
          
            Property(t => t.DriverName);
            Property(t => t.Destination);
            Property(t => t.Purpose);
            Property(t => t.Passengers);           
            
            Property(t => t.MaterialTaken);
            Property(t => t.DCNo);
            Property(t => t.StartingKm);
            Property(t => t.ClosingKm);
            Property(t => t.KmUsed);
            Property(t => t.ReturnDateTime);
            Property(t => t.PurposeofTravel);
            Property(t => t.Remarks);
            Property(t => t.InwardDcNo);
            Property(t => t.InwardDcDate);
            Property(t => t.InwardMaterial);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);

            ToTable("GateEntryVehicle");
        }
    }
}
