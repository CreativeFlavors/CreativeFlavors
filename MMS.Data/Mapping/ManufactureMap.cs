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
  public  class ManufactureMap: EntityTypeConfiguration<ManufacturerMaster>
    {
      public ManufactureMap()
        {
            HasKey(t => t.ManufacturerMasterId);
            Property(t => t.ManufacturerMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ManufacturerCode).IsRequired();
            Property(t => t.ManufacturerName).IsRequired();           
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("ManufacturerMaster");
        }
    }
}