using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.TempsalesorderModel
{
    public class Temp_salesorderModel
    {
        public int Id { get; set; }
        public int SalesOrderId { get; set; }
        public string OrderNo { get; set; }
        public int BuyerId { get; set; }
        public int ProductId { get; set; }
        public string ProductItem { get; set; }
        public int MaterialId { get; set; }
        public decimal stockrequired { get; set; }
        public decimal AvailableStock { get; set; }
        public int AvailableUnitId { get; set; }
        public decimal Consume { get; set; }
        public int ConsumeUnitId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}