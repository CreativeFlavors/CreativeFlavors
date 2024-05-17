using MMS.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Data.Mapping
{
    class CalculateMRPMap : EntityTypeConfiguration<CalculateMRP>
    {
        public CalculateMRPMap()
        {
            HasKey(t => t.MRPCalculateID);
            Property(t => t.MRPCalculateID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.MRPIdToCalculate);
            Property(t => t.CreatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.IsCalculated);
            Property(t => t.UpdatedDate);
            Property(t => t.HostName);
            ToTable("CalculateMRP");
        }
    }
}
