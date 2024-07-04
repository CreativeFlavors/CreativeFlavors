using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.IndentMaterial
{
    public class IndentMaterialModel
    {
        public int IndentHeaderId { get; set; }
        public DateTime? IndentDate { get; set; }
        public int StoreCode { get; set; }
        public int SupplierCode { get; set; }
        public string PoRefNumber { get; set; }
        public int Items { get; set; }
        public decimal Quantity { get; set; }
        public decimal ExtendedValue { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal SubtotalValue { get; set; }
        public decimal TaxValue { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime? ExpDeliveryDate { get; set; }
        public DateTime? PayByDate { get; set; }
        public DateTime? FulfillDate { get; set; }
        public string Status { get; set; }
        public string ShipmentDetails { get; set; }
        public string Notes { get; set; }
        public decimal OverallWeight { get; set; }
        public decimal For_ExtendedValue { get; set; }
        public decimal For_DiscountValue { get; set; }
        public decimal For_SubtotalValue { get; set; }
        public decimal For_TaxValue { get; set; }
        public decimal For_TotalValue { get; set; }

        // Properties from indentdetail table
        public int IndentDetailId { get; set; }
        public int MaterialId { get; set; }
        public int UomId { get; set; }
        public int TaxId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ExtendedPrice { get; set; }  
        public decimal DiscountPerc { get; set; }
        public decimal Subtotal { get; set; }  
        public decimal Total { get; set; }
        public decimal Weight { get; set; }
        public decimal For_UnitPrice { get; set; }
        public decimal For_Subtotal { get; set; }
        public decimal For_Total { get; set; }
        public decimal IndentQty { get; set; }
        public int IndentNo { get; set; }

    }
}