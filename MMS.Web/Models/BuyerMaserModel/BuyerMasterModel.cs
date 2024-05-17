using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.BuyerMaserModel
{
    [Table("buyermastermodel", Schema = "public")]
    public class BuyerMasterModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("buyermasterid")]
        public int BuyerMasterId { get; set; }
        [Column("buyercode")]
        public string BuyerCode { get; set; }
        [Column("buyerfullname")]
        public string BuyerFullName { get; set; }
        [Column("buyershortname")]
        public string BuyerShortName { get; set; }
        [Column("currency")]
        public int Currency { get; set; }
        [Column("buyeraddress1")]
        public string BuyerAddress1 { get; set; }
        [Column("buyeraddress2")]
        public string BuyerAddress2 { get; set; }
        [Column("buyerpincode")]
        public string BuyerPincode { get; set; }
        [Column("country")]
        public int Country { get; set; }
        [Column("contactpersion")]
        public string ContactPersion { get; set; }
        [Column("designation")]
        public string Designation { get; set; }
        [Column("contactnoo")]
        public string ContactNoo { get; set; }
        [Column("emailid")]
        public string EmailID { get; set; }
        [Column("stnohead")]
        public string STNoHead { get; set; }
        [Column("cgtnohead")]
        public string CGTNoHead { get; set; }
        [Column("deliveraddress1")]
        public string DeliverAddress1 { get; set; }
        [Column("deliveraddress2")]
        public string DeliverAddress2 { get; set; }
        [Column("pincode")]
        public string Pincode { get; set; }
        [Column("agentname")]
        public int AgentName { get; set; }
        [Column("agentaddress1")]
        public string AgentAddress1 { get; set; }
        [Column("agentaddress2")]
        public string AgentAddress2 { get; set; }
        [Column("agentpincode")]
        public string AgentPincode { get; set; }
        [Column("agentcountry")]
        public int AgentCountry { get; set; }
        [Column("agentcurrency")]
        public string AgentCurrency { get; set; }
        [Column("paymentsterms")]
        public string PaymentsTerms { get; set; }
        [Column("deliveryterms")]
        public string DeliveryTerms { get; set; }
        [Column("insurance")]
        public string Insurance { get; set; }
        [Column("delierto")]
        public string DelierTo { get; set; }
        [Column("brand")]
        public string Brand { get; set; }
        [Column("shipmentto")]
        public string ShipmentTo { get; set; }
        [Column("shimentmode")]
        public string ShimentMode { get; set; }
        [Column("countrystamp")]
        public string CountryStamp { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
        [Column("creditexposure")]
        public string CreditExposure { get; set; }
        [Column("creditdays")]
        public string CreditDays { get; set; }
        public List<BuyerMaster> BuyerMasterList { get; set; }
    }
}