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
    class GatePassMasterMap : EntityTypeConfiguration<GatePassMaster>
    {
        public GatePassMasterMap()
        {
            HasKey(t => t.GatePassId);
            Property(t => t.GatePassId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.IsReturnable).IsRequired();
            Property(t => t.GatePassNo).IsRequired();
            Property(t => t.Date).IsRequired();
            Property(t => t.IsSupplier).IsRequired();
            Property(t => t.PartyName).IsRequired();

            Property(t => t.DeliveryAddress).IsRequired();
            Property(t => t.Material).IsRequired();
            Property(t => t.Quantity).IsRequired();
            Property(t => t.UOM).IsRequired();
            Property(t => t.Rate).IsRequired();
            Property(t => t.Amount).IsRequired();
            Property(t => t.IfReturnable).IsRequired();
            Property(t => t.Remarks).IsRequired();
            Property(t => t.IsPrintWithRateValue);
            Property(t => t.IsPrintWithoutCompanyAddress);      

            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("GatePassDCOutward");
        }
    }
}