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
    public class customer_addressesMap : EntityTypeConfiguration<Customer_Addresses>
    {
        public customer_addressesMap()
        {
            HasKey(t => t.addressdt_id);
            Property(t => t.addressdt_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Addresshd_id);
            Property(t => t.addresstypeid).HasColumnName("addresstypeid");

            Property(t => t.Buyerid)
                .HasColumnName("buyerid"); 
            Property(t => t.supplierid)
                .HasColumnName("supplierid");

            Property(t => t.address1)
                .HasColumnName("address1");

            Property(t => t.address2)
                .HasColumnName("address2");

            Property(t => t.address3)
                .HasColumnName("address3");

            Property(t => t.ZipCode)
                .HasColumnName("zipcode");

            Property(t => t.CreatedBy)
                .HasColumnName("createdby");

            Property(t => t.UpdatedBy)
                .HasColumnName("updatedby");    
            Property(t => t.vatnumber)
                .HasColumnName("vatnumber");

            Property(t => t.isactive)
                .HasColumnName("isactive");
            ToTable("customer_addresses");
        }
    }
}
