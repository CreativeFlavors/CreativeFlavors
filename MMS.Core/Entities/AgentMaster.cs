using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("agentmaster", Schema = "public")]
    public class AgentMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("agentmasterid")]
        public int AgentMasterId { get; set; }
        [Column("agentcode")]
        public string AgentCode { get; set; }
        [Column("agentfullname")]
        public string AgentFullName { get; set; }
        [Column("agentshortname")]
        public string AgentShortName { get; set; }
        [Column("currency")]
        public int Currency { get; set; }
        [Column("commisioncriteriaid")]
        public int CommisionCriteriaId { get; set; }
        [Column("addressline1")]
        public string AddressLine1 { get; set; }
        [Column("addressline2")]
        public string AddressLine2 { get; set; }
        [Column("addressline3")]
        public string AddressLine3 { get; set; }
        [Column("pincode")]
        public string Pincode { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("contactperson")]
        public string ContactPerson { get; set; }
        [Column("mobileno")]
        public string MobileNo { get; set; }
        [Column("emailid")]
        public string EmailID { get; set; }
        [Column("countrymasterid")]
        public int CountryMasterId { get; set; }
        [Column("commisionpercentage")]
        public int CommisionPercentage { get; set; }
        [Column("isdeleted")]
        public bool? IsDeleted { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
    }
}
