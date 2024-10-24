﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("temp_production", Schema = "public")]
    public class temp_production : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("id")]
        public int Id { get; set; } 
        [Column("producttype")]
        public int producttype { get; set; }
        [Column("salesorderid")]
        public int SalesOrderId { get; set; }
        [Column("buyerid")]
        public int BuyerId { get; set; }
        [Column("productid")]
        public int ProductId { get; set; }
        [Column("productitem")]
        public decimal? ProductItem { get; set; }
        [Column("bomid")]
        public int Bomid { get; set; }
        [Column("materialid")]
        public int MaterialId { get; set; }
        [Column("available_stock")]
        public decimal? AvailableStock { get; set; }
        [Column("availableunitid")]
        public int AvailableUnitId { get; set; }
        [Column("consume")]
        public decimal? Consume { get; set; }
        [Column("consumeunitid")]
        public int ConsumeUnitId { get; set; }
        [Column("probomid")]
        public int Probomid { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }
    }
}
