using MMS.Core.Entities;
using MMS.Data.StoredProcedureModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.ContactDetailsCaptureModel
{
    public class ContactDetailsCaptureModel
    {
        public int ContactDetailsCode { get; set; }
        public string CompanyName { get; set; }
        public int companyID { get; set; }
        public string ContactPerson { get; set; }
        public string LandlineNumber1 { get; set; }
        public string LandlineNumber2 { get; set; }
        public string ExtensionNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public string WebsiteAddress { get; set; }
        public string Industry { get; set; }
        public string Business { get; set; }
        public string OtherDetails { get; set; }
        public string Remarks { get; set; }
        public string Address { get; set; }
        public string VisitingCardFront { get; set; }
        public string VisitingCardBack { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public List<ContactDetailsSupplierModel> ContactDetailsCaptureSupplierList { get; set; }

        public string suppliername { get; set; }
        public List<ContactDetailsCapture> ContactDetailsCaptureList { get; set; }

        public List<Company> CompanyList { get; set; }
    }
}