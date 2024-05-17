using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.StockMap
{
    public class Unit_DivisionMap : EntityTypeConfiguration<Unit_Division>
    {
        public Unit_DivisionMap()
        {
            HasKey(t => t.UnitCodePK);
            Property(t => t.FullName);
            Property(t => t.ShortName);
            Property(t => t.CompanyCodeFK);
            Property(t => t.Address1);
            Property(t => t.Address2);
            Property(t => t.CountryCodeFK);
            Property(t => t.StateCodeFK);
            Property(t => t.CityCodeFK);
            Property(t => t.Pincode);
            Property(t => t.Phone);
            Property(t => t.Fax);
            Property(t => t.EmailID);
            Property(t => t.TIN_No);
            Property(t => t.PAN_No);
            Property(t => t.NoofDecimals);
            Property(t => t.EnableAutoBackups);
            Property(t => t.CreatedDate);
            Property(t => t.LastUpdatedDate);
            Property(t => t.UnitCode);
            Property(t => t.UnitCode).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            ToTable("tbl_Unit_Division");
        }
    }
}
