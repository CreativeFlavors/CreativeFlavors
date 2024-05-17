using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.Currency
{
    public class CurrencyModel
    {
        public int CurrencyMasterId { get; set; }
        public string Symbol { get; set; }
        public string ShortForm { get; set; }
        public string LongForm { get; set; }
        public string LowerDenomination { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<CurrencyMaster> CurrencyMasterList { get; set; }
    }
}