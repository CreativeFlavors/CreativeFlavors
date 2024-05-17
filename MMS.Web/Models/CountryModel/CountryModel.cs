using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.CountryModel
{
    public class CountryModel
    {
        public int CountryMasterId { get; set; }
        public string ShortCountryName { get; set; }
        public string LongCountryName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<CountryMaster> CountryList { get; set; }
    }
}