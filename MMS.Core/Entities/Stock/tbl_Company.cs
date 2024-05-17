using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("tbl_company", Schema = "public")]
    public class tbl_Company : BaseEntity2
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("companycodepk")]
        public Guid CompanyCodePK { get; set; }

        [Column("fullname")]
        public string FullName { get; set; }

        [Column("shortname")]
        public string ShortName { get; set; }

        [Column("currencycodefk")]
        public Guid? CurrencyCodeFK { get; set; }

        [Column("address1")]
        public string Address1 { get; set; }

        [Column("address2")]
        public string Address2 { get; set; }

        [Column("countrycodefk")]
        public Guid? CountryCodeFK { get; set; }

        [Column("statecodefk")]
        public Guid? StateCodeFK { get; set; }

        [Column("citycodefk")]
        public Guid? CityCodeFK { get; set; }

        [Column("pincode")]
        public int Pincode { get; set; }

        [Column("phone")]
        public Int64 Phone { get; set; }

        [Column("fax")]
        public string Fax { get; set; }

        [Column("emailid")]
        public string EmailID { get; set; }

        [Column("tngst")]
        public string TNGST { get; set; }

        [Column("cst")]
        public string CST { get; set; }

        [Column("tin_no")]
        public string TIN_No { get; set; }

        [Column("tan_no")]
        public string TAN_No { get; set; }

        [Column("pan_no")]
        public string PAN_No { get; set; }

        [Column("noofdecimals")]
        public decimal NoofDecimals { get; set; }

        [Column("companycode")]
        public int CompanyCode { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("lastupdateddate")]
        public DateTime? LastUpdatedDate { get; set; }
    }
}
