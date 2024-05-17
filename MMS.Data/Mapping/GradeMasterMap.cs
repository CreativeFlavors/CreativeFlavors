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
    class GradeMasterMap : EntityTypeConfiguration<GradeMaster>
    {
        public GradeMasterMap()
        {
            HasKey(t => t.GradeMasterID);
            Property(t => t.GradeMasterID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CodeNo).IsRequired();
            Property(t => t.Category).IsRequired();
            Property(t => t.Designation).IsRequired();
            Property(t => t.Grade).IsRequired();
            Property(t => t.Efficiency).IsRequired();                 
            Property(t => t.CreatedDate);
            //Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            ToTable("GradeMaster");
        }
    }
}
