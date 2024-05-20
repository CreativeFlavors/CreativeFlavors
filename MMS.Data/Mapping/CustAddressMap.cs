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
    public class CustAddressMap : EntityTypeConfiguration<CustAddress>
    {
        public CustAddressMap() {
            HasKey(t => t.AddressId);
            Property(t => t.AddressId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.BuyerId).IsRequired();
            Property(t => t.AddressType).IsRequired();
            Property(t => t.Add1).IsRequired();
            Property(t => t.Add2);
            Property(t => t.Add3);
            Property(t => t.IsDefault).IsRequired();
            Property(t => t.City).IsRequired();
            Property(t => t.State).IsRequired();
            Property(t => t.Country).IsRequired();
            Property(t => t.ZipCode).IsRequired();
            Property(t => t.ContactName).IsRequired();
            Property(t => t.Email).IsRequired();
            Property(t => t.Phone).IsRequired();
            Property(t => t.Notes);
            Property(t => t.CityId).IsRequired();
            Property(t => t.StateId).IsRequired();
            Property(t => t.CountryId).IsRequired();
            Property(t => t.Active).IsRequired();
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            ToTable("CustAddress");
        }
    }
}
