using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("purchaseorder", Schema = "public")]
    public class PurchaseOrder : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("poorderid")]
        public int PoOrderId { get; set; }

        [Column("pono")]
        public string PoNo { get; set; }

        [Column("unit")]
        public Guid? Unit { get; set; }

        [Column("freightamt")]
        public string FreightAmt { get; set; }

        [Column("freighttype")]
        public int? FreightType { get; set; }

        [Column("lastamendmentno")]
        public string LastAmendmentNo { get; set; }

        [Column("lastamendmentdate")]
        public DateTime? LastAmendmentDate { get; set; }

        [Column("podate")]
        public DateTime? PoDate { get; set; }

        [Column("potype")]
        public string PoType { get; set; }

        [Column("insuranceamount")]
        public string InsuranceAmount { get; set; }

        [Column("insureancecurrency")]
        public int? InsureanceCurrency { get; set; }

        [Column("refinfo")]
        public string RefInfo { get; set; }

        [Column("supplier")]
        public int? Supplier { get; set; }

        [Column("customsduty")]
        public int? CustomsDuty { get; set; }

        [Column("customsdutytype")]
        public string CustomsDutyType { get; set; }

        [Column("form")]
        public int? Form { get; set; }

        [Column("currency")]
        public int? Currency { get; set; }

        [Column("exrate")]
        public decimal? ExRate { get; set; }

        [Column("packforwardtype")]
        public int? PackForwardtype { get; set; }

        [Column("packforward")]
        public string PackForward { get; set; }

        [Column("formname")]
        public string FormName { get; set; }

        [Column("insurancedetails")]
        public string InsuranceDetails { get; set; }

        [Column("servicetax")]
        public string ServiceTax { get; set; }

        [Column("servicetaxtype")]
        public int? ServiceTaxType { get; set; }

        [Column("servicetaxnumbner")]
        public decimal? ServiceTaxNumbner { get; set; }

        [Column("ticktocloseorder")]
        public bool TickToCloseOrder { get; set; }

        [Column("taxtype")]
        public int? TaxType { get; set; }

        [Column("orderclosedon")]
        public DateTime? OrderClosedOn { get; set; }

        [Column("indentno")]
        public string IndentNo { get; set; }

        [Column("indentnodirectpo")]
        public int? IndentNoDirectPO { get; set; }

        [Column("category")]
        public int? Category { get; set; }

        [Column("groupname")]
        public int? Groupname { get; set; }

        [Column("material")]
        public int? Material { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("machinename")]
        public int? Machinename { get; set; }

        [Column("uom")]
        public int? UOM { get; set; }

        [Column("uomvalue")]
        public decimal? UOMValue { get; set; }

        [Column("uomtype")]
        public int? UOMType { get; set; }

        [Column("color")]
        public int? Color { get; set; }

        [Column("qty")]
        public decimal? Qty { get; set; }

        [Column("substance")]
        public int? Substance { get; set; }

        [Column("weight")]
        public decimal? Weight { get; set; }

        [Column("iono")]
        public int? IONO { get; set; }

        [Column("otherspecification")]
        public string OtherSpecification { get; set; }

        [Column("reqddate")]
        public DateTime? ReqdDate { get; set; }

        [Column("etd")]
        public DateTime? ETD { get; set; }

        [Column("eta")]
        public DateTime? ETA { get; set; }

        [Column("rate")]
        public string Rate { get; set; }

        [Column("dis")]
        public string Dis { get; set; }

        [Column("disamount")]
        public decimal? DisAmount { get; set; }

        [Column("exciseduty")]
        public decimal? ExciseDuty { get; set; }

        [Column("excisedutyamount")]
        public decimal? ExciseDutyAmount { get; set; }

        [Column("exess")]
        public string Exess { get; set; }

        [Column("exessamount")]
        public decimal? ExessAmount { get; set; }

        [Column("shexess")]
        public string SHExess { get; set; }

        [Column("shexessamount")]
        public decimal? SHExessAmount { get; set; }

        [Column("vat")]
        public string VAT { get; set; }

        [Column("vatamt")]
        public decimal? VATAmt { get; set; }

        [Column("surcharge")]
        public string SurCharge { get; set; }

        [Column("surchargeamt")]
        public decimal? SurChargeAmt { get; set; }

        [Column("amounttax")]
        public decimal? AmountTax { get; set; }

        [Column("remarks")]
        public string Remarks { get; set; }

        [Column("mrpmargin")]
        public string MRPMargin { get; set; }

        [Column("mrpprice")]
        public decimal? MRPPrice { get; set; }

        [Column("accessiblevalue")]
        public string AccessibleValue { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("indentmaterialid")]
        public int? IndentMaterialID { get; set; }

        [Column("stockvalue")]
        public decimal? StockValue { get; set; }

        [Column("frighttype")]
        public int? FrightType { get; set; }

        [Column("indentvalue")]
        public string IndentValue { get; set; }

        [Column("ordertype")]
        public string OrderType { get; set; }

        [Column("freightcosttotal")]
        public decimal? FreightCostTotal { get; set; }

        [Column("uomvalue1")]
        public decimal? UOMValue1 { get; set; }

        [Column("servicetaxper")]
        public decimal? ServiceTaxPer { get; set; }

        [Column("suppliername")]
        public string SupplierName { get; set; }

        [Column("companyname")]
        public string CompanyName { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("paymentterms")]
        public string PaymentTerms { get; set; }

        [Column("deliverterms")]
        public string DeliverTerms { get; set; }

        [Column("otherterms")]
        public string OtherTerms { get; set; }

        [Column("termsconditions")]
        public string TermsConditions { get; set; }

        [Column("materialtype")]
        public int? MaterialType { get; set; }

        [Column("poqty")]
        public decimal? PoQty { get; set; }

        [Column("pendingqty")]
        public decimal? PendingQty { get; set; }

        [Column("ispo_cancelled")]
        public bool ISPO_cancelled { get; set; }

        [Column("ispo_cancelled_reson")]
        public string ISPO_cancelled_Reson { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
