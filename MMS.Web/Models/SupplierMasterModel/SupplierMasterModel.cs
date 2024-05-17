using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Models.SupplierMasterModel
{
    public class SupplierMasterModel
    {
        public int SupplierMasterId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public int? Currency { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Country { get; set; }
        public string ContactPerson { get; set; }
        public string Designation { get; set; }
        public string Mobile { get; set; }
        public string LandLine { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string StNo { get; set; }
        public string CstNo { get; set; }
        public string Range { get; set; }
        public string Division { get; set; }
        public string PLANo { get; set; }
        public string EOCNo { get; set; }
        public string RegnNo { get; set; }
        public string PaymentTerms { get; set; }
        public string DeliveryTerms { get; set; }
        public string PackingDetails { get; set; }
        public string Insurance { get; set; }
        public string PortOfLoading { get; set; }
        public string DespatchThrough { get; set; }
        public string Freight { get; set; }
        public string FreightName { get; set; }
        public string Form1 { get; set; }
        public string FormName { get; set; }
        public string Forwarder { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankCode { get; set; }
        public string SwiftCode { get; set; }
        public string AccountNumber { get; set; }
        public string IBAN { get; set; }
        public string OtherInformation { get; set; }
        public bool IsDomestic { get; set; }
        public bool IsImport { get; set; }
        public string MaterialMasterID { get; set; }

        public string MaterialGroupMasterID { get; set; }
        public string SupplierGSTIN { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<SupplierMaster> SupplierMasterList { get; set; }
        public IEnumerable<SelectListItem> materialList { get; set; }
        public IEnumerable<string> SelectedItemId { get; set; }
        public IEnumerable<SelectListItem> MaterialGroupList { get; set; }
        public IEnumerable<string> MaterialSelectedItemId { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string UploadError { get; set; }
    }
}