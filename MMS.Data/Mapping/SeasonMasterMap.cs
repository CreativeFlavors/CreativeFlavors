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
    class SeasonMasterMap : EntityTypeConfiguration<SeasonMaster>
    {
        public SeasonMasterMap()
        {
            HasKey(t => t.SeasonMasterId);
            Property(t => t.SeasonMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SeasonName);
            Property(t => t.SpringSummerYear);
            Property(t => t.SpringDescription);
            Property(t => t.PeriodFrom);
            Property(t => t.PeriodTo);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("SeasonMaster");
        }
    }
}
