﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class StockAdjustmentGridDetails : BaseEntity
    {

        public int? StockAdjustmentGridId { get; set; }
        public DateTime? AsOnDate { get; set; }
        public int? StoreId { get; set; }
        public int? CategoryId { get; set; }
        public int? GroupId { get; set; }
        public int? MaterialMasterId { get; set; }
        public string MaterialDescription { get; set; }
        public decimal? Size { get; set; }
        public decimal? QTY { get; set; }
        public int? UomId { get; set; }
        public string Style { get; set; }
        public string ShortUnitName { get; set; }
        public decimal? Rate { get; set; }
        public int? CuttingIssueTypeID { get; set; }
        public string CuttingIssueType { get; set; }
        public decimal? BookStock { get; set; }
        public decimal? BookStockValue { get; set; }
        public decimal PhysicalStock { get; set; }
        public decimal? PhysicalStockValue { get; set; }
        public decimal? VariationStock { get; set; }
        public decimal? VariationStockValue { get; set; }
        public decimal? PlusStock { get; set; }
        public decimal? PlusStockValue { get; set; }
        public decimal? MinusStock { get; set; }
        public decimal? MinusStockValue { get; set; }
    }
    public class ClubIndentSizeRange
    {
        public string Size { get; set; }
        public decimal? quantity { get; set; }
    }

    [Table("approvedprice", Schema = "public")]
    public class ApprovedPrice
    {
        [Column("poexcesspercentage")]
        public decimal? POExcessPercentage { get; set; }

        [Column("pricers")]
        public decimal? PriceRs { get; set; }

        [Column("materialid")]
        public int? MaterialID { get; set; }

        [Column("supplierid")]
        public int? SupplierId { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
    }
}
