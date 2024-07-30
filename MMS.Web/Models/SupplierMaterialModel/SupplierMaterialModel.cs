using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.SupplierMaterialModel
{
    public class SupplierMaterialModel
    {
        public int SupplierMaterialId { get; set; }
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public int UomId { get; set; } 
        public int category { get; set; }
        public int TaxId { get; set; }
        public string ProductName { get; set; }
        public string Productcode { get; set; }
        public string suppliername { get; set; }
        public string tax { get; set; }
        public string uom { get; set; }
        public string categoryname { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}