using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class PurchaseOrder : BaseEntity
    {
        public int PoOrderId { get; set; }
        public string PoNo { get; set; }
        public Guid? Unit { get; set; }
        public string FreightAmt { get; set; }
        public int? FreightType { get; set; }
        // public string FreightType { get; set; }
        public string LastAmendmentNo { get; set; }
        public DateTime? LastAmendmentDate { get; set; }
        public DateTime? PoDate { get; set; }
        public string PoType { get; set; }
        public string InsuranceAmount { get; set; }
        public int? InsureanceCurrency { get; set; }
        public string RefInfo { get; set; }
        public int? Supplier { get; set; }
        public int? CustomsDuty { get; set; }
        public string CustomsDutyType { get; set; }
        public int? Form { get; set; }
        public int? Currency { get; set; }
        public decimal? ExRate { get; set; }
        public int? PackForwardtype { get; set; }
        public string PackForward { get; set; }
        public string FormName { get; set; }
        public string InsuranceDetails { get; set; }
        public string ServiceTax { get; set; }
        public int? ServiceTaxType { get; set; }
        public decimal? ServiceTaxNumbner { get; set; }
        public bool TickToCloseOrder { get; set; }
        public int? TaxType { get; set; }
        public DateTime? OrderClosedOn { get; set; }
        public string IndentNo { get; set; }
        public int? IndentNoDirectPO { get; set; }
        public int? Category { get; set; }
        public int? Groupname { get; set; }
        public int? Material { get; set; }
        public string Description { get; set; }
        public int? Machinename { get; set; }
        public int? UOM { get; set; }
        public decimal? UOMValue { get; set; }
        public int? UOMType { get; set; }
        public int? Color { get; set; }
        public decimal? Qty { get; set; }
        public int? Substance { get; set; }
        public decimal? Weight { get; set; }
        public int? IONO { get; set; }
        public string OtherSpecification { get; set; }
        public DateTime? ReqdDate { get; set; }
        public DateTime? ETD { get; set; }
        public DateTime? ETA { get; set; }
        public string Rate { get; set; }
        public string Dis { get; set; }
        public decimal? DisAmount { get; set; }
        public decimal? ExciseDuty { get; set; }
        public decimal? ExciseDutyAmount { get; set; }
        public string Exess { get; set; }
        public decimal? ExessAmount { get; set; }
        public string SHExess { get; set; }
        public decimal? SHExessAmount { get; set; }
        public string VAT { get; set; }
        public decimal? VATAmt { get; set; }
        public string SurCharge { get; set; }
        public decimal? SurChargeAmt { get; set; }
        public decimal? AmountTax { get; set; }
        public string Remarks { get; set; }
        public string MRPMargin { get; set; }
        public decimal? MRPPrice { get; set; }
        public string AccessibleValue { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int? IndentMaterialID { get; set; }
        public decimal? StockValue { get; set; }
        public int? FrightType { get; set; }
        public string IndentValue { get; set; }
        public string OrderType { get; set; }
        public decimal? FreightCostTotal { get; set; }
        public decimal? UOMValue1 { get; set; }
        public decimal? ServiceTaxPer { get; set; }
        public string  SupplierName { get; set; }
        public string  CompanyName { get; set; }
        public string Address { get; set; }
        public string  City { get; set; }
        public string Phone { get; set; }

        public string PaymentTerms { get; set; }
        public string DeliverTerms { get; set; }
        public string OtherTerms { get; set; }
        public string TermsConditions { get; set; }
        public int? MaterialType { get; set; }
        public decimal? PoQty { get; set; }
        public decimal? PendingQty { get; set; }
        public bool ISPO_cancelled { get; set; }
        public string ISPO_cancelled_Reson { get; set; }
    }
}
