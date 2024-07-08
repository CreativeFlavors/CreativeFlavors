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
    public class IndentheaderMap : EntityTypeConfiguration<Indentheader>
    {
        public IndentheaderMap()
        {
            // Primary Key
            HasKey(t => t.IndentHeaderId);

            // Properties
            Property(t => t.IndentHeaderId).HasColumnName("indentheaderid").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.IndentDate).HasColumnName("indentdate");
            Property(t => t.PoRefNumber).HasColumnName("porefnumber");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.DeletedDate).HasColumnName("deleteddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            Property(t => t.IsActive).HasColumnName("isactive");
            Property(t => t.CreatedBy).HasColumnName("createdby");
            Property(t => t.DeletedBy).HasColumnName("deletedby");
            Property(t => t.UpdatedBy).HasColumnName("updatedby");
            Property(t => t.IndentNo).HasColumnName("indentno");
            ToTable("indentheader");
        }
    }
}
