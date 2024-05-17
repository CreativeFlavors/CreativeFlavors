using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping
{
    public class MaterialMasterMap : EntityTypeConfiguration<MaterialMaster>
    {
        public MaterialMasterMap()
        {
            HasKey(t => t.MaterialMasterId);
            Property(t => t.MaterialMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.StoreMasterId);
            Property(t => t.MaterialCategoryMasterId);
            Property(t => t.MaterialCode);
            Property(t => t.MaterialName);
            Property(t => t.OptionMaterialValue);
            Property(t => t.ColorMasterId);
            Property(t => t.MachineryMasterId);
            Property(t => t.GradeMasterId);
            Property(t => t.SubstanceMasterId);
            Property(t => t.LeatherSizeMasterId);
            Property(t => t.OriginMasterId);
            Property(t => t.WeightInKg);
            Property(t => t.SizeRangeMasterId);
            Property(t => t.Decimals);
            Property(t => t.Uom);
            Property(t => t.UomUnit);
            Property(t => t.Price);
            Property(t => t.CurrencyMasterId);
            Property(t => t.ManufacturerMasterId);
            Property(t => t.PacketType);
            Property(t => t.PacketTypePrice);
            Property(t => t.PacketCurrency);
            Property(t => t.TaxCategory);
            Property(t => t.PrimaryPackage);
            Property(t => t.SecondaryPackage);
            Property(t => t.LeadTime);
            Property(t => t.MaxRoLevel);
            Property(t => t.MinRoLevel);
            Property(t => t.ReOrderLevel);
            Property(t => t.Expiry);
            Property(t => t.IsActive);
            Property(t => t.Barcode);
            Property(t => t.IsPurchase);
            Property(t => t.IsIssues);
            Property(t => t.IsCustomerSupplied);
            Property(t => t.IsImportedMaterial);
            Property(t => t.IsLocalMaterial);
            Property(t => t.IsCriticalMaterial);
            Property(t => t.IsAllowNegativeStock);
            Property(t => t.IsLotNumberTracking);
            Property(t => t.IsIssueStrictly);
            Property(t => t.IsPrimaryPackage);
            Property(t => t.IsSecondaryPackage);
            Property(t => t.CreatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.UpdatedDate);
            Property(t => t.MaterialGroupMasterId);
            Property(t => t.SubGroup);
            Property(t => t.IsApplicable);
            Property(t => t.PurchasePrimary);
            Property(t => t.PurchasePacket);
            Property(t => t.PrimaryUnit);
            Property(t => t.PacketUnit);
            Property(t => t.PacketUnitType);
            Property(t => t.ImagePath);
            Property(t => t.componentcheck1sds);
            Property(t => t.primarycheckpackage);
            Property(t => t.CuttingIssueTypeID);
            Property(t => t.SameSizeForAllSize);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            Property(t => t.isLocal);
            Property(t => t.isImport);
            Property(t => t.ImportPrice);
            Property(t => t.importManafactureName);
            Property(t => t.importManafactureName);
            Property(t => t.isImportCustomer);
            Property(t => t.BuyerItemCode);
            Property(t => t.HSNCode);
            ToTable("MaterialMaster");             
    }
    }
}
