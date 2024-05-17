using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models
{
    public class OpeningStockPinkModel
    {
        public int materialmasterid { get; set; }
        public string MaterialDescription { get; set; }
        public string shortunitname { get; set; }
        public string categoryname { get; set; }
        public string storename { get; set; }
        public string groupname { get; set; }
        public string MaterialType { get; set; }
        public decimal? Rate { get; set; }
        public int materialpcs { get; set; }
        public decimal? OpeningAsOnStock { get; set; }
        public decimal? Issues { get; set; }
        public decimal? Receipt { get; set; }
        public decimal? ClosingStock { get; set; }
        public decimal? ClosingStockValue { get; set; }
        public DateTime? OpeningStockDate { get; set; }
    }
}