﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class AgentMaster:BaseEntity
    {
       public int AgentMasterId { get; set; }
       public string AgentCode { get; set; }
       public string AgentFullName { get; set; }
       public string AgentShortName { get; set; }
       public int Currency { get; set; }
       public int CommisionCriteriaId { get; set; }
       public string AddressLine1 { get; set; }
       public string AddressLine2 { get; set; }
       public string AddressLine3 { get; set; }
       public string Pincode { get; set; }     
       public string CreatedBy { get; set; }
       public string UpdatedBy { get; set; }
       public string ContactPerson { get; set; }
       public string MobileNo { get; set; }
       public string EmailID { get; set; }
       public int CountryMasterId { get; set; }
       public int CommisionPercentage { get; set; }
       public bool? IsDeleted { get; set; }
       public string DeletedBy { get; set; }
       public DateTime? DeletedDate { get; set; }
    }
}