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
    public class productprocessdeatilsMap : EntityTypeConfiguration<productprocessdeatils>
    {
        public productprocessdeatilsMap()
        {

            HasKey(t => t.ProcessID);
            Property(t => t.ProcessID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.process).IsRequired();
            Property(t => t.isactive).IsRequired();
            ToTable("productprocessdeatils");
        }
    }
}
