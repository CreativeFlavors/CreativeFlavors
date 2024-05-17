using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities;
using MMS.Data;
using System.Web;
using MMS.Common;
using MMS.Data.Context;
using MMS.Data.StoredProcedureModel;
using System.IO;

namespace MMS.Repository.Managers
{
    public class ContactDetailsCaptureManager : IContactDetailsCaptureService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<ContactDetailsCapture> contactDetailsCaptureRepository;

        public bool Post(ContactDetailsCapture arg)
        {
            bool result = false;
            string username = HttpContext.Current.Session["UserName"].ToString();
            try
            {
                if (arg.ContactDetailsCode == 0)
                {

                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    arg.IsDeleted = false;
                    arg.UpdatedDate = null;
                    arg.UpdatedBy = string.Empty;
                    //arg.UpdatedBy = username;
                    contactDetailsCaptureRepository.Insert(arg);
                    result = true;
                }
                else
                {
                    ContactDetailsCapture model = contactDetailsCaptureRepository.Table.Where(p => p.ContactDetailsCode == arg.ContactDetailsCode).FirstOrDefault();
                    if (model != null)
                    {
                        model.ContactDetailsCode = arg.ContactDetailsCode;
                       
                        model.UpdatedDate = DateTime.Now;

                        model.UpdatedBy = username;
                      
                        contactDetailsCaptureRepository.Update(model);
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        //public bool Put(ContactDetailsCapture arg)
        //{
        //    bool result = false;
        //    try
        //    {
        //        ContactDetailsCapture model = contactDetailsCaptureRepository.Table.Where(p => p.ContactDetailsCode == arg.ContactDetailsCode).FirstOrDefault();
        //        if (model != null)
        //        {
        //            model.ContactDetailsCode = arg.ContactDetailsCode;
        //            model.CompanyName = arg.CompanyName;
        //            model.ContactPerson = arg.ContactPerson;
        //            model.LandlineNumber1 = arg.LandlineNumber1;
        //            model.LandlineNumber2 = arg.LandlineNumber2;
        //            model.ExtensionNumber = arg.ExtensionNumber;
        //            model.FaxNumber = arg.FaxNumber;
        //            model.Mobile = arg.Mobile;
        //            model.EmailId = arg.EmailId;
        //            model.WebsiteAddress = arg.WebsiteAddress;
        //            //model.CreatedDate = arg.CreatedDate;
        //            model.UpdatedDate = DateTime.Now;
        //            //model.CreatedBy = model.CreatedBy;
        //            string username = HttpContext.Current.Session["UserName"].ToString();
        //            model.UpdatedBy = username;
        //            model.Industry = arg.Industry;
        //            model.Business = arg.Business;
        //            model.Remarks = arg.Remarks;
        //            model.Address = arg.Address;
        //            model.VisitingCardFront = arg.VisitingCardFront;
        //            model.VisitingCardBack = arg.VisitingCardBack;
        //            contactDetailsCaptureRepository.Update(model);
        //            result = true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        result = false;
        //    }

        //    return result;
        //}

        public bool Delete(int ContactDetailsCode)
        {
            bool result = false;
            try
            {
                ContactDetailsCapture model = contactDetailsCaptureRepository.GetById(ContactDetailsCode);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                contactDetailsCaptureRepository.Update(model);


                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }

        public ContactDetailsCaptureManager()
        {
            contactDetailsCaptureRepository = unitOfWork.Repository<ContactDetailsCapture>();
        }


        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public List<ContactDetailsCapture> Get()
        {
            List<ContactDetailsCapture> contactDetailsCapturelist = new List<ContactDetailsCapture>();
            try
            {
                contactDetailsCapturelist = contactDetailsCaptureRepository.Table.Where(x => x.IsDeleted == false || x.IsDeleted == null).ToList<ContactDetailsCapture>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return contactDetailsCapturelist;
        }

        public ContactDetailsCapture Get(int ContactDetailsCode)
        {
            ContactDetailsCapture ContactDetails = new ContactDetailsCapture();
            if (ContactDetailsCode != 0)
            {
                ContactDetails = contactDetailsCaptureRepository.Table.Where(x => x.ContactDetailsCode == ContactDetailsCode).SingleOrDefault();
            }
            return ContactDetails;
        }

        public List<ContactDetailsSupplierModel> GetContactGrid(string SupplierName)
        {
            List<ContactDetailsSupplierModel> contactlist = new List<ContactDetailsSupplierModel>();
            try
            {
                contactlist = contactDetailsCaptureRepository.SearchContactSupplierList(SupplierName);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return contactlist;
        }


    }
}
