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
    public class EmailTempateMap : EntityTypeConfiguration<EmailTempate>
    {
        public EmailTempateMap()
        {
            HasKey(t => t.EmailTemplateID);
            Property(t => t.EmailTemplateID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.TemplateName);
            Property(t => t.Subject);
            Property(t => t.Body);
            Property(t => t.FromAddress);
            Property(t => t.ToAddress);
            Property(t => t.CCAddress);          
            Property(t => t.Subject);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.IsActive);          
            ToTable("EmailTemplate");
        }
    }
}
