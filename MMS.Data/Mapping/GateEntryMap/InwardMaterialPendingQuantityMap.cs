using MMS.Core.Entities.GateEntry;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Data.Mapping.GateEntryMap
{
    public class InwardMaterialPendingQuantityMap : EntityTypeConfiguration<InwardMaterialPendingQuantityMaster>
    {
        public InwardMaterialPendingQuantityMap()
        {
            HasKey(t => t.PendingQuantityId);
            Property(t => t.PendingQuantityId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.GateEntryInwardId);
            Property(t => t.Size);
            Property(t => t.MaterialId);
            Property(t => t.DcQty);
            Property(t => t.PoOrderID);
            Property(t => t.ArrivedQty);
            Property(t => t.PendingQty);
            Property(t => t.PoQuantity);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);

            ToTable("InwardMaterialPendingQuantity");
        }
    }
}
