﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
  public  class ContactDetailsCapture : BaseEntity
    {
        public int ContactDetailsCode { get; set; }
        //public int CompanyName { get; set; }
        public string CompanyName { get; set; }
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
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

    }
}