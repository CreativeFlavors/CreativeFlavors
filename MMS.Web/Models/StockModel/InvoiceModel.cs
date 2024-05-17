using MMS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StockModel
{
    [Table("Invoice", Schema = "public")]
    public class Invoice 
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("invoiceid")]
        public int InvoiceId { get; set; }

        [Column("indentid")]
        public int IndentId { get; set; }

        [Column("invoicenumber")]
        public DateTime InvoiceDate { get; set; }  // Needs correction; should likely be string

        [Column("duedate")]
        public DateTime? DueDate { get; set; }

        [Column("buyername")]
        public string BuyerName { get; set; }

        [Column("buyeraddress")]
        public string BuyerAddress { get; set; }

        [Column("itemcode")]
        public string ItemCode { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("totalprice")]
        public decimal TotalPrice { get; set; }

        [Column("subtotal")]
        public decimal Subtotal { get; set; }

        [Column("tax")]
        public decimal Tax { get; set; }

        [Column("discount")]
        public decimal Discount { get; set; }

        [Column("totalamount")]
        public decimal TotalAmount { get; set; }

        [Column("paymenttype")]
        public string PaymentType { get; set; }

        [Column("paymentstatus")]
        public string PaymentStatus { get; set; }

        [Column("paymentdate")]
        public DateTime? PaymentDate { get; set; }

        [Column("creditlimit")]
        public decimal? CreditLimit { get; set; }

        [Column("outstandingbalance")]
        public decimal? OutstandingBalance { get; set; }

        [Column("creditterms")]
        public string CreditTerms { get; set; }

        [Column("creditstatus")]
        public string CreditStatus { get; set; }

        [Column("lastpaymentdate")]
        public DateTime? LastPaymentDate { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("createdby")]
        public string Createdby { get; set; }

        [Column("updatedby")]
        [StringLength(50)]
        public string Updatedby { get; set; }

        [Column("storemasterid")]
        public int? StoreMasterId { get; set; }

        [Column("ordertypeid")]
        public int? OrderTypeID { get; set; }

        [Column("materialtype")]
        public int? MaterialType { get; set; }

        [Column("shippingmethod")]
        public string ShippingMethod { get; set; }

        [Column("billingaddress")]
        public string BillingAddress { get; set; }

        [Column("paymentterms")]
        public string PaymentTerms { get; set; }

        [Column("currency")]
        public string Currency { get; set; }

        [Column("paymentmethods")]
        public string PaymentMethods { get; set; }

        [Column("attachments")]
        public string Attachments { get; set; }

        [Column("notes")]
        public string Notes { get; set; }

        [Column("latepaymentcharges")]
        public decimal? LatePaymentCharges { get; set; }

        [Column("discountspromotions")]
        public string DiscountsPromotions { get; set; }

        [Column("customercontactinfo")]
        public string CustomerContactInfo { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("taxable")]
        public bool Taxable { get; set; }

        [Column("shippingcost")]
        public decimal? ShippingCost { get; set; }

        [Column("termsandconditions")]
        public string TermsAndConditions { get; set; }

        [Column("projectreference")]
        public string ProjectReference { get; set; }

        [Column("itemtaxrate")]
        public decimal? ItemTaxRate { get; set; }

        [Column("taxcode")]
        public string TaxCode { get; set; }

        [Column("warehouse")]
        public string Warehouse { get; set; }

        [Column("serialnumber")]
        public string SerialNumber { get; set; }

        [Column("productcategory")]
        public string ProductCategory { get; set; }

        [Column("materialid")]
        public int? MaterialID { get; set; }

        [Column("buyermasterid")]
        public int? BuyerMasterId { get; set; }

        [Column("poorderid")]
        public int? PoOrderId { get; set; }

        [Column("oeshipmentdetailsid")]
        public int? OeShipmentDetailsId { get; set; }

        [Column("shippingtrackingnumber")]
        public string ShippingTrackingNumber { get; set; }
    }
}