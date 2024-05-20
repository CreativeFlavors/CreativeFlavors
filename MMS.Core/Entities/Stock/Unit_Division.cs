using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("unit_division", Schema = "public")]
    public class Unit_Division : BaseEntity2
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("unitcodepk")]
        public Guid UnitCodePK { get; set; }

        [Column("fullname")]
        public string FullName { get; set; }

        [Column("shortname")]
        public string ShortName { get; set; }

        [Column("companycodefk")]
        public Guid? CompanyCodeFK { get; set; }

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
        public int? Pincode { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("fax")]
        public string Fax { get; set; }

        [Column("emailid")]
        public string EmailID { get; set; }

        [Column("tin_no")]
        public string TIN_No { get; set; }

        [Column("pan_no")]
        public string PAN_No { get; set; }

        [Column("noofdecimals")]
        public decimal? NoofDecimals { get; set; }

        [Column("enableautobackups")]
        public string EnableAutoBackups { get; set; }

        [System.ComponentModel.DataAnnotations.Key]
        [Column("unitcode")]
        public int UnitCode { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("lastupdateddate")]
        public DateTime? LastUpdatedDate { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("isdeleted")]
        public bool? IsDeleted { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("deletedon")]
        public DateTime? DeletedOn { get; set; }
    }
}
