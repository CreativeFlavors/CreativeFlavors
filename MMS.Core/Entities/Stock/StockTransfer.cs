﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class StockTransfer : BaseEntity
    {
        public int StockTransferID { get; set; }
        public int TypeId { get; set; }
        public int MaterialCategoryID { get; set; }
        public int MaterialGroupID { get; set; }
        public int ColorID { get; set; }
        public string QuantityAmount { get; set; }
        public int QuantityValue { get; set; }
        public int IssuedToFrom { get; set; }
        public int Stores { get; set; }
        public int DcGrnNo { get; set; }
        public string TransportDetails { get; set; }
        public int RefDcNo { get; set; }
        public string ClosingStockInAllStores { get; set; }
        public string ClosingStockInCurrentStores { get; set; }
        public string ReservedStock { get; set; }
        public string FreeStock { get; set; }
        public string Remarks { get; set; }
        public string Rate { get; set; }
        public string Value { get; set; }
        public string PartyDcNo { get; set; }
        public string InvoiceRef { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}