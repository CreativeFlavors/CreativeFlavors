using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.Addressdetails
{
    public class Addressdetails
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
        public BuyerMaster BuyerMaster { get; set; }
        public cities cities { get; set; }
        public states states { get; set; }
        public countries countries { get; set; }

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
        public int CityId { get; set; }

        [Column("stateid")]
        public int StateId { get; set; }

        [Column("countryid")]
        public int CountryId { get; set; }

        [Column("active")]
        public bool Active { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        public List<CustAddress> Addressdetailslist { get; set; }
    }
}