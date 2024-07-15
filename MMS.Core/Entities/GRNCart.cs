using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class GRNCart
    {
        public int grncartid { get; set; }

        public string ProductCode { get; set; }

        public int ProductNameId { get; set; }

        public int UomMasterId { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string BatchCode { get; set; }

        public string StoreCode { get; set; }

        public int TaxPerId { get; set; }

        public decimal TaxValue { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
        public decimal Subtotal { get; set; }

        public decimal Grandtotal { get; set; }

        public decimal DiscountPerId { get; set; }

        public decimal DiscountValue { get; set; }

        public DateTime SalesOrderDate { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int Status { get; set; }

        public bool IsDeleted { get; set; }

        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
