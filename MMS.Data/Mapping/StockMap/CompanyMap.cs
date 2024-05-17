using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MMS.Core.Entities.Stock;


namespace MMS.Data.Mapping.StockMap
{
    public class CompanyMap : EntityTypeConfiguration<tbl_Company>
    {
        public CompanyMap()
        {
            HasKey(t => t.CompanyCodePK);
            Property(t => t.FullName);
            Property(t => t.ShortName);
            Property(t => t.CurrencyCodeFK);
            Property(t => t.Address1);
            Property(t => t.Address2);
            Property(t => t.CountryCodeFK);
            Property(t => t.StateCodeFK);
            Property(t => t.CityCodeFK);
            Property(t => t.Pincode);
            Property(t => t.Phone);
            Property(t => t.Fax);
            Property(t => t.EmailID);
            Property(t => t.TNGST);
            Property(t => t.CST);
            Property(t => t.TIN_No);
            Property(t => t.TAN_No);
            Property(t => t.PAN_No);
            Property(t => t.NoofDecimals);
            Property(t => t.CreatedDate);
            Property(t => t.LastUpdatedDate);
            Property(t => t.CompanyCode);
            Property(t => t.CompanyCode).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            ToTable("tbl_Company");

        }
    }
}
