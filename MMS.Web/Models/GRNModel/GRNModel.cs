using MMS.Core.Entities;
using MMS.Data.StoredProcedureModel;
using MMS.Web.Models.Po;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.GRNModel
{
    public class GRNModel
    {
        public int GrnCartId { get; set; }
        public int PoHeaderId { get; set; }
        public int grnnumber { get; set; }
        public int PoDetailId { get; set; }
        public decimal? PoQuantity { get; set; }
        public string ProductCode { get; set; }
        public string Productname{ get; set; }
        public string suppliername{ get; set; }
        public int ProductNameId { get; set; }
        public string UomMaster { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string BatchCode { get; set; }
        public int StoreCode { get; set; }
        public int item { get; set; }
        public decimal? TaxPer { get; set; }
        public decimal? TaxValue { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? GrandTotal { get; set; }
        public decimal? DiscountPerId { get; set; }
        public decimal? DiscountValue { get; set; }
        public DateTime GrnDate { get; set; }
        public bool IsActive { get; set; }
        public decimal ConversionValue { get; set; }
        public int currencyOption { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int GrnDetailId { get; set; } 
        public int GrnHeaderId { get; set; }
        public decimal? Total_TaxValue { get; set; }
        public decimal? Total_Price { get; set; }
        public decimal? Total_discountval { get; set; }
        public decimal? Total_Subtotal { get; set; }
        public decimal? Total_Grandtotal { get; set; }
        public string RefInvoiceNumber { get; set; }
        public DateTime? RefInvoiceDate { get; set; }
        public int SupplierId { get; set; } 
        public int MaterialId { get; set; } 
        public decimal? UnitPrice { get; set; } 
        public decimal? ExtendedValue { get; set; } 
        public decimal? SubtotalValue { get; set; } 
        public decimal? TotalValue { get; set; } 
        public decimal? Weight { get; set; } 
        public DateTime? IsFulfilled { get; set; } 
        public string ShipmentDetails { get; set; } 
        public string Notes { get; set; } 
        public decimal? OverallWeight { get; set; } 
        public decimal? ForExtendedValue { get; set; } 
        public decimal? ForDiscountValue { get; set; } 
        public decimal? ForSubtotalValue { get; set; } 
        public decimal? ForTaxValue { get; set; } 
        public decimal? ForTotalValue { get; set; }
        public List<GRNCarlist> grnCarlist { get; set; }
        public List<PODetails> polist { get; set; }
        public SupplierMaster SupplierMaster { get; set; }
        public List<GRNHeader> GRNHeaders { get; set; }
    }
}