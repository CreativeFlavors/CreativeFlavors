﻿using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.SubassemblyinventoryModel
{
    public class SubassemblyinventoryModel
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public decimal? Price { get; set; }
        public decimal Cost { get; set; }
        public int StoreCode { get; set; }
        public decimal Quantity { get; set; }
        public string QuantityLock { get; set; }
        public DateTime? QuantityLockRefTime { get; set; }
        public string QuantityLockReleaseAt { get; set; }
        public string PurchaseUOM { get; set; }
        public string SaleUOM { get; set; }
        public string LastTransNo { get; set; }
        public decimal LastTransQty { get; set; }
        public DateTime? LastTransDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Batchcode { get; set; }
        public int ProductType { get; set; }
        public int ProductId { get; set; }
        public product products { get; set; }
    }
}