using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models
{
    public class SpResult
    {
        public string SupplierName { get; set; }
        public string  Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class InteranalBuyerNo
    {
        public string BuyerPoNo { get; set; }
        public int? OrderEntryId { get; set; }      
    }
}