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
    public class MaterialNameMap : EntityTypeConfiguration<MaterialNameMaster>
    {
        public MaterialNameMap()
        {
            HasKey(t => t.MaterialMasterID);
            Property(t => t.MaterialMasterID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.MaterialGroupMasterId);
            Property(t => t.MaterialCode);
            Property(t => t.MaterialDescription);
            Property(t => t.LeatherMaterilFirstValue);
            Property(t => t.LeatherMaterialName);
            Property(t => t.LeatherMaterilLastValue);
            Property(t => t.CreatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.UpdatedDate);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.IsDeleted);
            ToTable("tbl_MaterialnameMaster");
        }
    }
}
