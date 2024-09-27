using MMS.Core.Entities;
using MMS.Data.StoredProcedureModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.Po
{
    public class PoModel
    {
        public int IndentPoMapId { get; set; }
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public int IndentId { get; set; }
        public int IndentProductId { get; set; }
        public int StoreCode { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? IndentQty { get; set; }
        public decimal? PoQty { get; set; }
        public DateTime? PoDate { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string suppliername { get; set; }
        public string DeletedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool WithIndentReference { get; set; }   
        public int UomId { get; set; }
        public decimal? TaxValue { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? TotalValue { get; set; }
        public bool TaxInclusive { get; set; }
        public int PoNumber { get; set; }
        public int IndentNumber { get; set; }

        public int PocartId { get; set; }
        public string SupplierMaterialId { get; set; }
        public decimal? Quantity { get; set; }

        public int PoheaderId { get; set; }
        public decimal? Items { get; set; }
        public decimal? Total_DiscountValue { get; set; }
        public decimal? Total_SubtotalValue { get; set; }
        public decimal? Total_TaxValue { get; set; }
        public decimal? Total_TotalValue { get; set; }
        public DateTime? ExpDeliveryDate { get; set; }
        public DateTime? PayByDate { get; set; }
        public DateTime? FulfillDate { get; set; }
        public string ShipmentDetails { get; set; }
        public string uomname { get; set; }
        public decimal? Total_Price { get; set; }

        public int PodetailId { get; set; }
        public decimal? Weight { get; set; }

        public string ProductName {  get; set; }
        public string Productcode {  get; set; }
        public List<IndentPoMapping> DataList { get; set; }
        public List<purchaceorderheader> purchaceorderheaders { get; set; }
        public PODetails DataListsp { get; set; }
    

    public class DataItem
    {
        // Properties representing columns in the table
        public string MaterialNameId { get; set; }
        public string StoreNameId { get; set; }
        public string UomNameId { get; set; }
        public decimal? Price { get; set; }
        public int IndentQty { get; set; }
        public decimal? PoQty { get; set; }
        public string TaxNameId { get; set; }
        public decimal? TaxValue { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? TotalValue { get; set; }
       
        }

}
}