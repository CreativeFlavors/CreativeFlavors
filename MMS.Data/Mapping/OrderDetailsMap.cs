﻿using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping
{
    public class OrderDetailsMap : EntityTypeConfiguration<orderdetails>
    {
        public OrderDetailsMap()
        {
            HasKey(t => t.invoicedt_id);
            Property(t => t.invoicedt_id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.invoicehd_id);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.SalesOrderId_dt);
            Property(t => t.ProductCode);
            Property(t => t.ProductId);
            Property(t => t.CustomerId);
            Property(t => t.UomMasterId);
            Property(t => t.IsActive);
            Property(t => t.TaxPerId);
            Property(t => t.TaxValue);
            Property(t => t.UnitPrice);
            Property(t => t.Quantity);
            Property(t => t.SubTotal);
            Property(t => t.TotalPrice);
            Property(t => t.DiscountPer);
            Property(t => t.DiscountValue);
            Property(t => t.invoicedate);
            Property(t => t.CurrencyId);
            Property(t => t.CustAddCode);
            Property(t => t.CustShipCode);
            Property(t => t.CustBillCode);
            Property(t => t.Status);
            ToTable("orderdetails");
        }
    }
}