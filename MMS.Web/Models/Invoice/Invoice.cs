using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.Invoice
{
    public class order_header
    {
        public int Id { get; set; }
        public int customerid { get; set; }
        public int orderid { get; set; }
        public int doctypeid { get; set; }
        public int revision { get; set; }
        public int docstateid { get; set; }
        public string quoteref { get; set; }
        public string soref { get; set; }
        public DateTime refdate { get; set; }
        public decimal refitems { get; set; }
        public decimal refquantity { get; set; } = 0;
        public DateTime refduedate { get; set; }
        public decimal refextendedvalue { get; set; }
        public decimal refdiscountvalue { get; set; }
        public decimal refgrossvalue { get; set; }
        public decimal reftaxvalue { get; set; }
        public decimal refnetvalue { get; set; }
        public DateTime docdate { get; set; }
        public decimal docitems { get; set; }
        public decimal docquantity { get; set; }
        public DateTime docduedate { get; set; }
        public decimal docextendedvalue { get; set; }
        public decimal docdiscountvalue { get; set; }
        public decimal docgrossvalue { get; set; }
        public decimal doctaxvalue { get; set; }
        public decimal docnetvalue { get; set; }
        public int custaddcode { get; set; }
        public int custshipcode { get; set; }
        public int custbillcode { get; set; }
        public string headerdiscountperc { get; set; }
        public string shippingnotes { get; set; }
        public int status { get; set; }
        public string docdescription { get; set; }
        public DateTime originalquotedate { get; set; }
        public DateTime quotedate { get; set; }
        public DateTime duedate { get; set; }
        public bool taxinclusive { get; set; }
        public bool emailsent { get; set; }
        public string externalref { get; set; }
        public decimal creditlimit { get; set; }
        public decimal dcbalance { get; set; }
        public decimal foreigndcbalance { get; set; }
        public int currencyid { get; set; }
        public bool isactive { get; set; }
        public int createdby { get; set; }
        public DateTime createddate { get; set; }
        public List<order_header> Invoicedetailslist { get; set; }
        [NotMapped]
        public string add1 { get; set; }
        [NotMapped]
        public string add2 { get; set; }
        [NotMapped]
        public string add3 { get; set; }
        [NotMapped]
        public string CustomerName { get; set; }
        public string QtyProcessed { get; set; }
        public List<OrderEntry> OrderEntryList { get; set; }
        [NotMapped]
        public string courencyoption { get; set; }
        [NotMapped]
        public decimal netvalue { get; set; } = 0;
        [NotMapped]
        public decimal grosstotal { get; set; } = 0;
        [NotMapped]
        [Column("conversionvalue")]
        public decimal ConversionValue { get; set; } = 0;
        [NotMapped]
        [Column("unitprice")]
        public decimal UnitPrice { get; set; } = 0;
        [NotMapped]
        [Column("discontpercentage")]
        public decimal Discontpercentage { get; set; } = 0;
    }
}