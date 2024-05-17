using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
   public class tbl_Company : BaseEntity2
    {
        public Guid CompanyCodePK { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public Guid? CurrencyCodeFK { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public Guid? CountryCodeFK { get; set; }
        public Guid? StateCodeFK { get; set; }
        public Guid? CityCodeFK { get; set; }
        public int Pincode { get; set; }
        public Int64 Phone { get; set; }
        public string Fax { get; set; }
        
        public string EmailID { get; set; }
        public string TNGST { get; set; }
        
        public string CST { get; set; }
        
        public string TIN_No { get; set; }

        public string TAN_No { get; set; }
        public string PAN_No { get; set; }
        public decimal NoofDecimals { get; set; }
        
        public int CompanyCode { get; set; }

        
         
    }
}
