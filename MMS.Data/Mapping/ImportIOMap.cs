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
    public class ImportIOMap : EntityTypeConfiguration<ImportIO>
    {
        public ImportIOMap()
        {
            HasKey(t => t.IoNumberID);
            Property(t => t.IoNumberID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SimpleMRPID);
            Property(t => t.Sno);
            Property(t => t.IoNO);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.DeletedBy);
            Property(t => t.Deletedon);
            Property(t => t.IsDeleted);
            ToTable("ImportIO");
        }
    }
}
