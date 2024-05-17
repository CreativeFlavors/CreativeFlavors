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
  public  class BuyerModelMap:EntityTypeConfiguration<BuyerModel>
    {
      public BuyerModelMap()
        {
            HasKey(t => t.BuyerModelID);
            Property(t => t.BuyerModelID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.BuyerModelName).IsRequired();
            Property(t => t.Remarks).IsRequired();           
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("BuyerModel");
        }
    }
}
