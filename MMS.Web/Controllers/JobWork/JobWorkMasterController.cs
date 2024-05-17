using MMS.Common;
using MMS.Core.Entities.JobWork;
using MMS.Repository.Managers.JobWork;
using MMS.Web.Models;
using MMS.Web.Models.ColorMasterModel;
using MMS.Web.Models.JobworkModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MMS.Web.Controllers.JobWork
{
    [CustomFilter]
    public class JobWorkMasterController : Controller
    {
        //
        // GET: /JobWorkMaster/
        [HttpGet]
        public ActionResult JobWorkMaster()
        {
            MMS.Web.Models.JobworkModel.JobworkModel Pm = new JobworkModel();
            return View("~/Views/Jobwork/Job_Master/JobMaster/JobworkMaster.cshtml", Pm);
        }
        public ActionResult JobWorkGrid()
        {
            JobworkModel vm = new JobworkModel();
            JobworkManager jobworkManager = new JobworkManager();
            List<JobworkModel> JobworkModel =new List<JobworkModel>();
            vm.JobworkMasterList = jobworkManager.Get();

            return PartialView("~/Views/Jobwork/Job_Master/JobMaster/Partial/JobWorkGrid.cshtml", vm);
        }
        [HttpPost]
        public ActionResult EditJobprocessList(int JW_Id)
        {

            JobworkManager JobworkManager = new JobworkManager();
            JobworkMaster JobworkMaster = new JobworkMaster();
            JobworkModel model = new JobworkModel();
            JobworkMaster = JobworkManager.GetJobMasterMasterID(JW_Id);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getJobCodeId();


            if (JobworkMaster != null)
            {
                model.JW_Id = JobworkMaster.JW_Id;
                model.JW_Code = JobworkMaster.JW_Code;
                model.JW_Name = JobworkMaster.JW_Name;
                model.Jobworker_Type = JobworkMaster.Jobworker_Type;
                model.Currency = JobworkMaster.Currency;
                model.Address = JobworkMaster.Address;
                model.Country = JobworkMaster.Country;
                model.Contact_Person = JobworkMaster.Contact_Person;
                model.Designation = JobworkMaster.Designation;
                model.Mobile = JobworkMaster.Mobile;
                model.Phone = JobworkMaster.Phone;
                model.Fax = JobworkMaster.Fax;
                model.Email = JobworkMaster.Email;
                model.St_No_Head = JobworkMaster.St_No_Head;


                model.Cst_No_Head  = JobworkMaster.Cst_No_Head;
                model.Range = JobworkMaster.Range;
                model.Division = JobworkMaster.Division;
                model.PLA_No = JobworkMaster.PLA_No;
                model.EOC_No = JobworkMaster.EOC_No;
                model.Regn_No = JobworkMaster.Regn_No;
                model.Payment_Terms = JobworkMaster.Payment_Terms;

                model.Delivery_Terms = JobworkMaster.Delivery_Terms;
                model.Packing_Details = JobworkMaster.Packing_Details;
                model.Insurance = JobworkMaster.Insurance;
                model.Port_Of_Loading = JobworkMaster.Port_Of_Loading;
                model.Despatch_Through = JobworkMaster.Despatch_Through;
                model.Freight = JobworkMaster.Freight;
                model.FreightName = JobworkMaster.FreightName;

                model.Form = JobworkMaster.Form;
                model.FormName = JobworkMaster.FormName;
                model.Forwarder = JobworkMaster.Forwarder;
                model.BankName = JobworkMaster.BankName;
                model.BankAddress = JobworkMaster.BankAddress;
                model.BankCode = JobworkMaster.BankCode;
                model.SwiftCode = JobworkMaster.SwiftCode;

                model.AccountNumber = JobworkMaster.AccountNumber;
                model.IBAN = JobworkMaster.IBAN;
                model.OtherInformation = JobworkMaster.OtherInformation;
                model.IsDomestic = JobworkMaster.IsDomestic;
                model.IsImport = JobworkMaster.IsImport;
                model.BankCode = JobworkMaster.BankCode;
                model.SwiftCode = JobworkMaster.SwiftCode;
            }

            else
            {
                ID++;
                model.JW_Code = "JBO" + ID;
            }
            return PartialView("~/Views/Jobwork/Job_Master/JobMaster/Partial/JobWorkDetail.cshtml", model);
        }
        #region Curd Operation
        [HttpPost]
        public ActionResult saveJob(JobworkModel JobworkModel)
        {
            JobworkMaster JobworkMaster = new JobworkMaster();

            if (ModelState.IsValid)
            {
                JobworkMaster jobworkMaster = new JobworkMaster();
                JobworkManager JobworkManager = new JobworkManager();
                JobworkMaster.JW_Code = JobworkModel.JW_Code;
                JobworkMaster.JW_Name = JobworkModel.JW_Name;
                JobworkMaster.Jobworker_Type = JobworkMaster.Jobworker_Type;
                JobworkMaster.Currency = JobworkModel.Currency;
                JobworkMaster.Address = JobworkModel.Address;
                JobworkMaster.Country = JobworkModel.Country;
                JobworkMaster.Contact_Person = JobworkModel.Contact_Person;
                JobworkMaster.Designation = JobworkModel.Designation;
                JobworkMaster.Mobile = JobworkModel.Mobile;
                JobworkMaster.Phone = JobworkModel.Phone;
                JobworkMaster.Fax = JobworkModel.Fax;
                JobworkMaster.Email = JobworkModel.Email;
                JobworkMaster.St_No_Head = JobworkModel.St_No_Head;
                JobworkMaster.Cst_No_Head= JobworkModel.Cst_No_Head;
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
                JobworkManager.Post(JobworkMaster);
            }

            return Json(JobworkMaster, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateJob(JobworkModel JobworkModel)
        {
            JobworkMaster JobworkMaster = new JobworkMaster();
            if (ModelState.IsValid)
            {
                JobworkMaster JW_id = new JobworkMaster();
                JobworkManager JobworkManager = new JobworkManager();
                JW_id = JobworkManager.GetJobMasterMasterID(JobworkModel.JW_Id);
                if (JW_id != null)
                {
                    JobworkMaster.JW_Id = JobworkModel.JW_Id;
                    JobworkMaster.JW_Name = JobworkModel.JW_Name;
                    JobworkMaster.Currency = JobworkModel.Currency;
                    JobworkMaster.Jobworker_Type = JobworkModel.Jobworker_Type;
                    JobworkMaster.Address = JobworkModel.Address;
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
                    JobworkManager.Put(JobworkMaster);
                }
                else
                {
                    return Json(JobworkMaster, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(JobworkMaster, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteJob(int JW_Id)
        {
            JobworkMaster JobworkMaster = new JobworkMaster();
            string status = "";
            JobworkManager JobworkManager = new JobworkManager();
            JobworkMaster = JobworkManager.GetJobMasterMasterID(JW_Id);
            if (JobworkMaster != null)
            {
                status = "Success";
                JobworkManager.Delete(JobworkMaster.JW_Id);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
