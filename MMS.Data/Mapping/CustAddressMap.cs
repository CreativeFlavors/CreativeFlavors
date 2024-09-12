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
            HasKey(t => t.Addresshd_id);
            Property(t => t.Addresshd_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.BuyerId);
            Property(t => t.AddressType);
            Property(t => t.addressvarient);
            Property(t => t.ContactName).IsRequired();
            Property(t => t.Email).IsRequired();
            Property(t => t.Phone).IsRequired();
            Property(t => t.Notes);
            Property(t => t.CityId).IsRequired();
            Property(t => t.StateId).IsRequired();
            Property(t => t.CountryId).IsRequired();
            Property(t => t.isActive).IsRequired();
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.deletedby);     
            Property(t => t.deleteddate);
            Property(t => t.CreatedBy);
            Property(t => t.isdeleted);
            Property(t => t.UpdatedBy);
            ToTable("CustAddress");
        }
    }
}
