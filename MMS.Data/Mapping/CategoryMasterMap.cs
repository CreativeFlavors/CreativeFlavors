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
    public class CategoryMasterMap : EntityTypeConfiguration<CategoryMaster>
    {
        public CategoryMasterMap() {
            HasKey(t => t.CategoryId);
            Property(t => t.CategoryId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CategoryCode);
            Property(t => t.CategoryName);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.CategoryType); 
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            ToTable("categorymaster");
        }
    }
}
