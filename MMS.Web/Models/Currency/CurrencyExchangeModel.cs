using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace MMS.Web.Models.Currency
{
    public class CurrencyExchangeModel
    {
        public string Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public int Currency { get; set; }
        public int CurrencyExchangeMasterId { get; set; }
        public decimal ValueInRupees { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<CurrencyExchangeMaster> CurrencyExchangeMasterList { get; set; }
    }
}