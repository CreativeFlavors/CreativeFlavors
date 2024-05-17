using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class CompanyManager
    {
        private UnitOfWork2 unitOfWorks = new UnitOfWork2();
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Company> companyRepository;
        private Repository2<State> stateRepository;
        private Repository2<City> cityRepository;

        public CompanyManager()
        {
            companyRepository = unitOfWork.Repository<Company>();
            stateRepository = unitOfWorks.Repository<State>();
            cityRepository = unitOfWorks.Repository<City>();
        }
        public List<Company> Get()
        {
            List<Company> companyList = new List<Company>();
            try
            {
                companyList = companyRepository.Table.Where(X => X.IsDeleted == false).ToList<Company>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return companyList;
        }
        public Company GetCompanyID(int CompanyID)
        {
            Company companyList = new Company();
            try
            {
                companyList = companyRepository.Table.Where(X => X.IsDeleted == false&&X.CompanyCodePK== CompanyID).FirstOrDefault<Company>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return companyList;
        }
        public List<State> GetStates()
        {
            List<State> stateList = new List<State>();
            try
            {
                stateList = stateRepository.Table.Where(X => X.IsDeleted == false).ToList<State>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return stateList;
        }
        public List<City> GetCitys()
        {
            List<City> cityList = new List<City>();
            try
            {
                cityList = cityRepository.Table.Where(X => X.IsDeleted == false).ToList<City>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return cityList;
        }
        public Company GetCompanyCode(int CompanyCodepk)
        {
            Company company = new Company();
            try
            {
                company = companyRepository.Table.Where(X => X.IsDeleted == false&&X.CompanyCodePK==CompanyCodepk).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return company;
        }
        public Company GetCompanyDetails()
        {
            Company company = new Company();
            try
            {
                company = companyRepository.Table.Where(X => X.IsDeleted == false).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return company;
        }
        public Company IsExistCompany(string CompanyName)
        {
            Company company = new Company();
            try
            {
                company = companyRepository.Table.Where(X => X.IsDeleted == false && X.CompanyName == CompanyName).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return company;
        }
        public Company Post(Company arg)
        {
            Company result = new Company();
            try
            {
                if (arg.CompanyCodePK == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.Createdby = username;
                    //  arg.Updatedby = username;
                    companyRepository.Insert(arg);
                    result = arg;
                }
                else
                {
                    Company companyDetails = companyRepository.Table.Where(p => p.CompanyCodePK == arg.CompanyCodePK).FirstOrDefault();
                    if (companyDetails != null)
                    {
                        string username = HttpContext.Current.Session["UserName"].ToString();
                        companyDetails.CompanyCodePK = arg.CompanyCodePK;
                        companyDetails.SupplierName = arg.SupplierName;
                        companyDetails.City = arg.City;
                        companyDetails.StoreID = arg.StoreID;
                        companyDetails.Address = arg.Address;
                        companyDetails.DeliverTerms = arg.DeliverTerms;
                        companyDetails.TermsConditions = arg.TermsConditions;
                        companyDetails.CompanyName = arg.CompanyName;
                        companyDetails.Phone = arg.Phone;
                        companyDetails.PaymentTerms = arg.PaymentTerms;
                        companyDetails.OtherTerms = arg.OtherTerms;
                        companyDetails.UpdatedDate = DateTime.Now;
                        companyDetails.Updatedby = username;
                        companyRepository.Update(arg);
                        result = arg;
                    }

                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                //result = false;
            }
            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                Company model = companyRepository.GetById(id);
                model.IsDeleted = true;
                model.Deletedby = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedOn = DateTime.Now;
                companyRepository.Update(model);
                // colorRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
    }
}
