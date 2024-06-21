using MMS.Core;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data.StoredProcedureModel;
using MMS.Web.Models.Production;
using MMS.Web.Models.StockModel;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Models
{
    public class Salesorders 
    {
        public int SalesorderId { get; set; }
        public int SalesorderId_DT { get; set; }
        public int BuyerName { get; set; }
        public string BuyerNames { get; set; }
        public int materialname { get; set; }
        public int MaterialCategoryMasterId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductID { get; set; }
        public string Bomno { get; set; }
        public int buyerid { get; set; }
        public int UomMasterId { get; set; }
        public int TaxMasterID { get; set; }
        public decimal? TaxValue { get; set; }
        public decimal? Price { get; set; }
        public decimal? quantity { get; set; } 
        public decimal? Subtotal { get; set; }
        public decimal? Grandtotal { get; set; }
        public decimal? Total_TaxValue { get; set; }
        public decimal? Total_Price { get; set; }
        public decimal? Total_discountval { get; set; }
        public decimal? Total_Subtotal { get; set; }
        public decimal? Total_Grandtotal { get; set; }
        public decimal? discountperid { get; set; }
        public decimal? subassemblyqty { get; set; }
        public decimal? discountvalue { get; set; }
        public string Specialinstruction { get; set; }
        public string Additionalcommends { get; set; }
        public string quoteref { get; set; }
        public string Uomname { get; set; }
        public int item { get; set; }
        public DateTime quotedate { get; set; }
        public DateTime salesorderdate { get; set; }
        public int custaddcode { get; set; }
        public int custshipcode { get; set; }
        public int custbillcode { get; set; }
        public DateTime originalquotedate { get; set; }
        public bool taxinclusive { get; set; }
        public bool isactive { get; set; }
        public int createdby { get; set; }
        public decimal? SalesQuantity { get; set; }
        public decimal? AvailableStock { get; set; }
        public DateTime? ProductionDueDate { get; set; }
        public decimal? ProductionQty { get; set; }
        public string Status { get; set; }
        public DateTime createddate { get; set; }
        public UomMaster UomMaster { get; set; }
        public TaxTypeMaster TaxTypeMaster { get; set; }
        public product product { get; set; }
        public BuyerMaster BuyerMaster { get; set; }
        [Column("conversionvalue")]
        public decimal ConversionValue { get; set; } = 0;
        public string currencyOption { get; set; }
        public List<ParentBillofMaterial>  bOMMaterialListModels { get; set; }
        public List<mrp_material_list> mrp_Material_Lists { get; set; }
        public List<mrp_subassembly_list> mrp_subassembly_Lists { get; set; }
        public List<SalesorderCart> salesorderList { get; set; }



    }
}