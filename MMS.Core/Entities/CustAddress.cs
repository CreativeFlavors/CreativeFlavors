using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("custaddress", Schema = "public")]
    public class CustAddress : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("addressid")]
        public int AddressId { get; set; }

        [Column("buyerid")]
        public int BuyerId { get; set; }

        [Column("addresstype")]
        public int AddressType { get; set; }

        [Column("add1")]
        public string Add1 { get; set; }

        [Column("add2")]
        public string Add2 { get; set; }

        [Column("add3")]
        public string Add3 { get; set; }

        [Column("isdefault")]
        public bool IsDefault { get; set; }

        [Column("city")]
        public int City { get; set; }

        [Column("state")]
        public int State { get; set; }

        [Column("country")]
        public int Country { get; set; }

        [Column("zipcode")]
        public string ZipCode { get; set; }

        [Column("contactname")]
        public string ContactName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("notes")]
        public string Notes { get; set; }

        [Column("cityid")]
        public int CityId { get; set; } = 0;

        [Column("stateid")]
        public int StateId { get; set; } = 0;

        [Column("countryid")]
        public int CountryId { get; set; } = 0;

        [Column("active")]
        public bool Active { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

    }
}
