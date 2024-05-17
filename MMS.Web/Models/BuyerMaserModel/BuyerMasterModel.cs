using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.BuyerMaserModel
{
    public class BuyerMasterModel
    {
        public int BuyerMasterId { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerFullName { get; set; }
        public string BuyerShortName { get; set; }
        public int Currency { get; set; }
        public string BuyerAddress1 { get; set; }
        public string BuyerAddress2 { get; set; }
        public string BuyerPincode { get; set; }
        public int Country { get; set; }
        public string ContactPersion { get; set; }
        public string Designation { get; set; }
        public string ContactNoo { get; set; }
        public string EmailID { get; set; }
        public string STNoHead { get; set; }
        public string CGTNoHead { get; set; }
        public string DeliverAddress1 { get; set; }
        public string DeliverAddress2 { get; set; }
        public string Pincode { get; set; }
        public int AgentName { get; set; }
        public string AgentAddress1 { get; set; }
        public string AgentAddress2 { get; set; }
        public string AgentPincode { get; set; }
        public int AgentCountry { get; set; }
        public string AgentCurrency { get; set; }
        public string PaymentsTerms { get; set; }
        public string DeliveryTerms { get; set; }
        public string Insurance { get; set; }
        public string DelierTo { get; set; }
        public string Brand { get; set; }
        public string ShipmentTo { get; set; }
        public string ShimentMode { get; set; }
        public string CountryStamp { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<BuyerMaster> BuyerMasterList { get; set; }
    }
}