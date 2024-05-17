using MMS.Core.Entities;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Managers;
using MMS.Web.Models.ContactDetailsCaptureModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class ContactDetailsCaptureController : Controller
    {
        //
        // GET: /ContactDetailsCapture/
        [HttpGet]
        public ActionResult ContactDetailsCapture()
        {
            ContactDetailsSupplierModel vm = new ContactDetailsSupplierModel();
            return View();
        }
        public ActionResult ContactDetailsCaptureGrid()
        {
            List<ContactDetailsCapture> contactList = new List<ContactDetailsCapture>();
            ContactDetailsCaptureManager ContactDetailsCaptureManager = new ContactDetailsCaptureManager();

            ContactDetailsSupplierModel vm = new ContactDetailsSupplierModel();
            contactList = ContactDetailsCaptureManager.Get();

            vm.ContactDetailsCaptureList = contactList;
            return PartialView("Partial/ContactDetailsCaptureGrid", vm);
        }
        [HttpPost]
        public ActionResult ContactDetailsCaptureDetails(int ContactDetailsCode)
        {
            ContactDetailsCaptureManager contactdetailscaptureManager = new ContactDetailsCaptureManager();
            ContactDetailsSupplierModel model = new ContactDetailsSupplierModel();
            ContactDetailsCapture ContactDetails = new ContactDetailsCapture();
            ContactDetails = contactdetailscaptureManager.Get(ContactDetailsCode);
            if (ContactDetails != null)
            {
                model.ContactDetailsCode = ContactDetails.ContactDetailsCode;

                model.CompanyName = ContactDetails.CompanyName;

                model.ContactPerson = ContactDetails.ContactPerson;
                model.LandlineNumber1 = ContactDetails.LandlineNumber1;
                model.LandlineNumber2 = ContactDetails.LandlineNumber2;
                model.ExtensionNumber = ContactDetails.ExtensionNumber;
                model.FaxNumber = ContactDetails.FaxNumber;
                model.Mobile = ContactDetails.Mobile;
                model.EmailId = ContactDetails.EmailId;
                model.WebsiteAddress = ContactDetails.WebsiteAddress;
                model.Industry = ContactDetails.Industry;
                model.Business = ContactDetails.Business;
                model.OtherDetails = ContactDetails.OtherDetails;
                model.Remarks = ContactDetails.Remarks;
                model.Address = ContactDetails.Address;
                model.VisitingCardFront = ContactDetails.VisitingCardFront;
                model.VisitingCardBack = ContactDetails.VisitingCardBack;
            }
            return PartialView("Partial/ContactDetailsCaptureDetails", model);
        }

        [HttpPost]
        public ActionResult ContactDetailsCapture(ContactDetailsCaptureModel contactDetailsCaptureModel)
        {

            ContactDetailsCapture contactDetailsCapture = new ContactDetailsCapture();
            ContactDetailsCaptureManager contactdetailsManager = new ContactDetailsCaptureManager();
            if (ModelState.IsValid)
            {
                /// update coding
                if (contactDetailsCaptureModel.ContactDetailsCode != 0)
                {

                    contactDetailsCapture = contactdetailsManager.Get(contactDetailsCaptureModel.ContactDetailsCode);
                    contactDetailsCapture.UpdatedDate = DateTime.Now;
                }
                //Insert code
                else
                {
                    contactDetailsCapture.CreatedDate = DateTime.Now;

                }


                contactDetailsCapture.CompanyName = contactDetailsCaptureModel.CompanyName;
                contactDetailsCapture.ContactPerson = contactDetailsCaptureModel.ContactPerson;
                contactDetailsCapture.LandlineNumber1 = contactDetailsCaptureModel.LandlineNumber1;
                contactDetailsCapture.LandlineNumber2 = contactDetailsCaptureModel.LandlineNumber2;
                contactDetailsCapture.ExtensionNumber = contactDetailsCaptureModel.ExtensionNumber;
                contactDetailsCapture.FaxNumber = contactDetailsCaptureModel.FaxNumber;
                contactDetailsCapture.Mobile = contactDetailsCaptureModel.Mobile;
                contactDetailsCapture.EmailId = contactDetailsCaptureModel.EmailId;
                contactDetailsCapture.WebsiteAddress = contactDetailsCaptureModel.WebsiteAddress;
                contactDetailsCapture.Industry = contactDetailsCaptureModel.Industry;
                contactDetailsCapture.OtherDetails = contactDetailsCaptureModel.OtherDetails;
                contactDetailsCapture.Business = contactDetailsCaptureModel.Business;
                contactDetailsCapture.Remarks = contactDetailsCaptureModel.Remarks;
                contactDetailsCapture.Business = contactDetailsCaptureModel.Business;
                contactDetailsCapture.Address = contactDetailsCaptureModel.Address;

                if (Session["vistingcardFrontimg"] != null && Session["vistingcardFrontimg"].ToString() != "")
                {
                    contactDetailsCapture.VisitingCardFront = Session["vistingcardFrontimg"].ToString();
                }
                else
                {
                    contactDetailsCapture.VisitingCardFront = contactDetailsCapture.VisitingCardFront;
                }

                if (Session["vistingcardBackimg"] != null && Session["vistingcardBackimg"].ToString() != "")
                {
                    contactDetailsCapture.VisitingCardBack = Session["vistingcardBackimg"].ToString();
                }
                else
                {
                    contactDetailsCapture.VisitingCardBack = contactDetailsCapture.VisitingCardBack;
                }


                contactdetailsManager.Post(contactDetailsCapture);
                Session["vistingcardFrontimg"] = null;
                Session["vistingcardBackimg"] = null;


            }
            else
            {
                return Json(contactDetailsCapture, JsonRequestBehavior.AllowGet);
            }

            return Json(contactDetailsCapture, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Need to work from update....
        /// </summary>
        /// <param name="contactDetailsCaptureModel"></param>
        /// <returns></returns>


        public ActionResult Delete(int ContactDetailsCode)
        {
            ContactDetailsCapture contactDetailsCapture = new ContactDetailsCapture();
            string status = "";
            ContactDetailsCaptureManager contactdetailsManager = new ContactDetailsCaptureManager();
            contactDetailsCapture = contactdetailsManager.Get(ContactDetailsCode);
            if (contactDetailsCapture != null)
            {
                status = "Success";
                contactdetailsManager.Delete(ContactDetailsCode);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult UploadFile()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    var cname = Request.Form["companyID"];


                    for (int i = 0; i < files.Count; i++)
                    {

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.
                        string filepath = Path.Combine(Server.MapPath("~/VisitingCards/" + cname + "/"));
                        System.IO.Directory.CreateDirectory(filepath);
                        fname = Path.Combine(Server.MapPath("~/VisitingCards/" + cname + "/"), fname);
                        if (i == 0)
                        {
                            Session["vistingcardFrontimg"] = fname;
                        }
                        else if (i == 1)
                        {
                            Session["vistingcardBackimg"] = fname;
                        }
                        else
                        {
                            Session["vistingcardimg"] = fname;
                        }
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json(true);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json(false);
            }
        }


        public ActionResult Search(string filter)
        {
            List<ContactDetailsCapture> model_list = new List<ContactDetailsCapture>();
            ContactDetailsCaptureManager ContactDetailsCaptureManager = new ContactDetailsCaptureManager();


            model_list = ContactDetailsCaptureManager.Get();
            model_list = model_list.Where(x => x.CompanyName.Trim().Contains(filter.Trim()) || x.ContactPerson.Trim().Contains(filter.Trim()) || x.EmailId.Trim().Contains(filter.Trim()) || x.Business.Trim().Contains(filter.Trim()) || x.Mobile.Trim().Contains(filter.Trim())).ToList();
            ContactDetailsSupplierModel vm = new ContactDetailsSupplierModel();
            vm.ContactDetailsCaptureList = model_list;
            return PartialView("Partial/ContactDetailsCaptureGrid", vm);




        }
    }
}
