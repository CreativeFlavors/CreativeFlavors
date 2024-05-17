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
  public  class TaxTypeMasterMap:EntityTypeConfiguration<TaxTypeMaster>
    {
      public TaxTypeMasterMap()
        {
            HasKey(t => t.TaxMasterID);
            Property(t => t.TaxMasterID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.TaxName).IsRequired();
            //Property(t => t.TaxMode).IsRequired();
            Property(t => t.TaxValue).IsRequired();
            Property(t => t.TaxCode).IsRequired();
            Property(t => t.TaxRef);
            Property(t => t.Form).IsRequired();
            Property(t => t.TaxOn);
            //Property(t => t.CostOfTheProduct);
            Property(t => t.TaxValueMode).IsRequired(); ;      
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("TaxTypeMaster");
        }
    }
}
