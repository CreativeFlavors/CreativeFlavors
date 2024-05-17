using MMS.Core.Entities.Stock;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.StockMap
{
    public class MaterialRequirementPlanMap : EntityTypeConfiguration<MaterialRequirementPlanning>
    {
        public MaterialRequirementPlanMap()
        {
            HasKey(t => t.MaterialRequirementPlanId);
            Property(t => t.MaterialRequirementPlanId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.IsProductionPlanBasis);
            Property(t => t.IsOrderBasis);
            Property(t => t.IsRejectionOrReplacement);
            Property(t => t.IsOverConsumption);
            Property(t => t.MrpNo).IsRequired();
            Property(t => t.Buyer).IsRequired();
            Property(t => t.SizeRangeMasterId).IsRequired();
            Property(t => t.Date).IsRequired();
            Property(t => t.MrpType).IsRequired();
            Property(t => t.WeekNo).IsRequired();
            Property(t => t.SeasonMasterId).IsRequired();
            Property(t => t.LotNo).IsRequired();
            Property(t => t.FROM).IsRequired();
            Property(t => t.TO).IsRequired();
            Property(t => t.CustPlan).IsRequired();
            Property(t => t.ListOfCategory).IsRequired();
            Property(t => t.ListOfGroup).IsRequired();
            Property(t => t.IsCritical);
            Property(t => t.IsCustomerSupplied);
            Property(t => t.IsGeneral);
            Property(t => t.IsImported);
            Property(t => t.IsReOrder);
            Property(t => t.IsSelectAll);
            Property(t => t.IsBalToOrder);
            Property(t => t.MaterialList).IsRequired();
            Property(t => t.TotalNoOfIos).IsRequired();
            Property(t => t.TotalNoOfPrs).IsRequired();
            Property(t => t.ETA).IsRequired();
            Property(t => t.Weight);
            Property(t => t.ShinkagePercent);
            Property(t => t.WastagePercent);
            Property(t => t.ShortagePercent);
            Property(t => t.Category).IsRequired();
            Property(t => t.Material).IsRequired();
            Property(t => t.BomQty).IsRequired();
            Property(t => t.ConversionTable).IsRequired();
            Property(t => t.Wastage);
            Property(t => t.IsDetailed);
            Property(t => t.IsConsolidated);
            Property(t => t.MultipleIO).IsRequired();
            Property(t => t.DisplayIO).IsRequired();
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsConversionInhouse);

            ToTable("MaterialRequirementPlann");

        }
    }
}
