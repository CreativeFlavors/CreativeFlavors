using MMS.Core;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
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
        public int customerid { get; set; }
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
        public decimal? quantity { get; set; } = 0;
        public decimal? Subtotal { get; set; }
        public decimal? Grandtotal { get; set; }
        public decimal? discountperid { get; set; }
        public decimal? discountvalue { get; set; }
        public string Specialinstruction { get; set; }
        public string Additionalcommends { get; set; }
        public string quoteref { get; set; }
        public DateTime quotedate { get; set; }
        public DateTime salesorderdate { get; set; }
        public int custaddcode { get; set; }
        public int custshipcode { get; set; }
        public int custbillcode { get; set; }
        public DateTime originalquotedate { get; set; }
        public bool taxinclusive { get; set; }
        public bool isactive { get; set; }
        public int createdby { get; set; }
        public DateTime createddate { get; set; }

        public UomMaster UomMaster { get; set; }
        public TaxTypeMaster TaxTypeMaster { get; set; }
        public product product { get; set; }
        public BuyerMaster BuyerMaster { get; set; }

        public List<BOMMaterialListModel>  bOMMaterialListModels { get; set; }
    }
}