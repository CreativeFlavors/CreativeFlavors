using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.Currency
{
    public class CurrencyConversionModel
    {
        public int Id { get; set; }
        public int PrimaryCurrency { get; set; }
        public int SecondaryCurrency { get; set; }
        public string PrimaryCurrency1 { get; set; }
        public string SecondaryCurrency1 { get; set; }
        public decimal? ConversionValue { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public string UpdatedBy { get; set; }
    }
}