using MMS.Common;
using MMS.Core.Entities.JobWork;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service.JobWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.JobWork
{
    public class JobworkManager
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<JobworkMaster> JobworkMasterRepository;
        public JobworkManager()
        {
            JobworkMasterRepository = unitOfWork.Repository<JobworkMaster>();
        }
        #region Add/Update/Delete Operation

        public bool Post(JobworkMaster arg)
        {
            bool result = false;
            JobworkMaster supplierMaster = new JobworkMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                //arg.UpdatedBy = username;
                JobworkMasterRepository.Insert(arg);
                supplierMaster = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }

        
        public bool Put(JobworkMaster JobworkModel)
        {
            bool result = false;
            try
            {
                JobworkMaster JobworkMaster = new JobworkMaster();
                 JobworkMaster = JobworkMasterRepository.Table.Where(x => x.JW_Id == JobworkModel.JW_Id).FirstOrDefault();
                if (JobworkMaster != null)
                {
                    JobworkMaster.JW_Id = JobworkModel.JW_Id;
                    JobworkMaster.JW_Name = JobworkModel.JW_Name;
                    JobworkMaster.Currency = JobworkModel.Currency;
                    JobworkMaster.Address = JobworkModel.Address;
                    JobworkMaster.Jobworker_Type = JobworkModel.Jobworker_Type;
                    JobworkMaster.Country = JobworkModel.Country;
                    JobworkMaster.Contact_Person = JobworkModel.Contact_Person;
                    JobworkMaster.Designation = JobworkModel.Designation;
                    JobworkMaster.Mobile = JobworkModel.Mobile;
                    JobworkMaster.Phone = JobworkModel.Phone;
                    JobworkMaster.Fax = JobworkModel.Fax;
                    JobworkMaster.Email = JobworkModel.Email;
                    JobworkMaster.St_No_Head = JobworkModel.St_No_Head;
                    JobworkMaster.Cst_No_Head = JobworkModel.Cst_No_Head;
                    JobworkMaster.Range = JobworkModel.Range;
                    JobworkMaster.Division = JobworkModel.Division;
                    JobworkMaster.PLA_No = JobworkModel.PLA_No;
                    JobworkMaster.EOC_No = JobworkModel.EOC_No;
                    JobworkMaster.Regn_No = JobworkModel.Regn_No;
                    JobworkMaster.Payment_Terms = JobworkModel.Payment_Terms;
                    JobworkMaster.Delivery_Terms = JobworkModel.Delivery_Terms;
                    JobworkMaster.Packing_Details = JobworkModel.Packing_Details;
                    JobworkMaster.Insurance = JobworkModel.Insurance;
                    JobworkMaster.Port_Of_Loading = JobworkModel.Port_Of_Loading;
                    JobworkMaster.Despatch_Through = JobworkModel.Despatch_Through;
                    JobworkMaster.Freight = JobworkModel.Freight;
                    JobworkMaster.FreightName = JobworkModel.FreightName;
                    JobworkMaster.Form = JobworkModel.Form;
                    JobworkMaster.FormName = JobworkModel.FormName;
                    JobworkMaster.Forwarder = JobworkModel.Forwarder;
                    JobworkMaster.BankName = JobworkModel.BankName;
                    JobworkMaster.BankAddress = JobworkModel.BankAddress;
                    JobworkMaster.BankCode = JobworkModel.BankCode;
                    JobworkMaster.SwiftCode = JobworkModel.SwiftCode;
                    JobworkMaster.AccountNumber = JobworkModel.AccountNumber;
                    JobworkMaster.IBAN = JobworkModel.IBAN;
                    JobworkMaster.OtherInformation = JobworkModel.OtherInformation;
                    JobworkMaster.IsDomestic = JobworkModel.IsDeleted;
                    JobworkMaster.IsImport = JobworkModel.IsImport;
                    JobworkMaster.BankCode = JobworkModel.BankCode;
                    JobworkMaster.SwiftCode = JobworkModel.SwiftCode;
                    JobworkMasterRepository.Update(JobworkMaster);
                    result = true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                JobworkMaster model = JobworkMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                JobworkMasterRepository.Update(model);
                //supplierMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }

        #endregion

        public List<JobworkMaster> Get()
        {
            List<JobworkMaster> JobworkMaster = new List<JobworkMaster>();
            try
            {
                JobworkMaster = JobworkMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return JobworkMaster;
        }
        public List<JobworkMaster> Get_contain(int[] jobsheetpair_jwname)
        {
            List<JobworkMaster> JobworkMaster = new List<JobworkMaster>();
            try
            {
                JobworkMaster = JobworkMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null && jobsheetpair_jwname.Contains(X.JW_Id)).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return JobworkMaster;
        }
        public JobworkMaster GetJobMasterMasterID(int? JW_Id)
        {
            JobworkMaster JobworkMaster = new JobworkMaster();
            JobworkMaster = JobworkMasterRepository.Table.Where(x => x.JW_Id == JW_Id).FirstOrDefault();
            return JobworkMaster;
        }
    }
}
