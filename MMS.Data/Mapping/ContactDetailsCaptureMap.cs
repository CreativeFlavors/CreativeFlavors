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
   public class ContactDetailsCaptureMap : EntityTypeConfiguration<ContactDetailsCapture>
    {
        public ContactDetailsCaptureMap()
        {
            HasKey(t => t.ContactDetailsCode);
            Property(t => t.ContactDetailsCode).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CompanyName).IsRequired();
            Property(t => t.ContactPerson).IsRequired();
            Property(t => t.LandlineNumber1);
            Property(t => t.LandlineNumber2);
            Property(t => t.ExtensionNumber);
            Property(t => t.FaxNumber);
            Property(t => t.Mobile).IsRequired();
            Property(t => t.EmailId).IsRequired();
            Property(t => t.WebsiteAddress);
            Property(t => t.Industry);
            Property(t => t.Business);
            Property(t => t.OtherDetails);
            Property(t => t.Remarks);
            Property(t => t.Address);
            Property(t => t.VisitingCardFront);
            Property(t => t.VisitingCardBack);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("ContactDetails");
        }
    }
}
