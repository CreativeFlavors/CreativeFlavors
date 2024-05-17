using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MMS.Core.Entities.Stock;

namespace MMS.Web.Controllers.Stock
{
    public class AutoGenPoMap : EntityTypeConfiguration<AutoGenPo>
    {
        public AutoGenPoMap()
        {
            HasKey(t => t.AutoGenerateId);
            Property(t => t.AutoGenerateId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.PoId);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            ToTable("tblAutoGenPo");
        }
    }
}