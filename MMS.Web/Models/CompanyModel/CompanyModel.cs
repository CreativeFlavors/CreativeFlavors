using MMS.Core.Entities;
using MMS.Repository.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Models.CompanyModel
{
    public class CompanyModel
    {
        public int CompanyCodePK { get; set; }
        public int? StoreName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string SupplierName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }

        //[Required(ErrorMessage = "Please Select")]
        //public Guid? CurrencyCodeFK { get; set; }

        ////[Required(ErrorMessage = "Please Select")]
        //public Guid StateCodeFK { get; set; }

        //[Required(ErrorMessage = "Please Select")]
      //  public Guid CityCodeFK { get; set; }

        [Required(ErrorMessage = "Please Enter DeliverTerms")]
        public String DeliverTerms { get; set; }

        [Required(ErrorMessage = "Please Enter TermsConditions")]
        public String TermsConditions { get; set; }

        //[Required(ErrorMessage="Please Select")]
       // public Guid CountryCodeFK { get; set; }

        //[Required(ErrorMessage = "Required")]
        //public int Pincode { get; set; }

        ////[Required(ErrorMessage = "Please Enter Mobile No")]

        ////[StringLength(10, ErrorMessage = "The Mobile must contains 10 characters", MinimumLength = 10)]
        //public Int64 Phone { get; set; }

        //   [Required(ErrorMessage = "Required")]
        public string CompanyName { get; set; }

       
        public string Phone { get; set; }

      
        public string PaymentTerms { get; set; }

      
        public string OtherTerms { get; set; }

       
      
        public List<Company> companyList { get; set; }
        public IEnumerable<SelectListItem> StateTypeSelectList(Guid stateCodePK, Guid countryCodePK)
        {
            var am = new CompanyManager();
            var CountryTypes = am.GetStates();

            return CountryTypes.Where(c => c.CountryCodeFK == countryCodePK).OrderBy(item => item.StateName)
                    .Select(item =>
                        new SelectListItem
                        {
                            Selected = (item.StateCodePK == stateCodePK),
                            Text = item.StateName,
                            Value = item.StateCodePK.ToString()
                        });
        }
        public IEnumerable<SelectListItem> CityTypeSelectList(Guid cityCodePk, Guid stateCodeFk)
        {
            var am = new CompanyManager();
            var CityTypes = am.GetCitys();

            return CityTypes.Where(c => c.StateCodeFK == stateCodeFk).OrderBy(item => item.CityName)
                    .Select(item =>
                        new SelectListItem
                        {
                            Selected = (item.CityCodePK == cityCodePk),
                            Text = item.CityName,
                            Value = item.CityCodePK.ToString()
                        });
        }

    }

}