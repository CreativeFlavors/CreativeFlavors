﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;

namespace MMS.Web.Models.Product
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "enter productcode")]
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public int TaxMasterId { get; set; }
        public string CategoryName { get; set; }
        public string productypeName { get; set; }
        public int UomMasterId { get; set; }
        public decimal? Price { get; set; }
        public int BomNo { get; set; }
        public string BomNo1 { get; set; }
        //public string ImageName { get; set; }
        //public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string taxname { get; set; }
        public string uom { get; set; }
        public string UpdatedBy { get; set; }
        public decimal? weight { get; set; } 
        public decimal? cost { get; set; }
        public int MaterialCategoryMasterId { get; set; }
        public int ProductType { get; set; }
        public List<product> product { get; set; }
        public List<Salesorders> salesorder { get; set; }
        public int StoreId { get; set; }
        public DateTime? productiontime { get; set; }
        public decimal MinStock { get; set; } = 0;
        public decimal MaxStock { get; set; } = 0;
        public decimal? productionperday { get; set; } = 0;
        [NotMapped]
        public int Bomid { get; set; }
    }
}