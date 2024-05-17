using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class Unit_Division : BaseEntity2
    {
        public Guid UnitCodePK { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public Guid? CompanyCodeFK { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public Guid? CountryCodeFK { get; set; }
        public Guid? StateCodeFK { get; set; }
        public Guid? CityCodeFK { get; set; }
        public int? Pincode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string EmailID { get; set; }
        public string TIN_No { get; set; }
        public string PAN_No { get; set; }
        public decimal? NoofDecimals { get; set; }
        public string EnableAutoBackups { get; set; }        
        public int UnitCode { get; set; }

    }
}
