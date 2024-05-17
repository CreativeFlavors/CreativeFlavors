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
   public class InspectionTypeMasterMap:  EntityTypeConfiguration<InspectionTypeMaster>
    {
       public InspectionTypeMasterMap()
       {
           HasKey(t => t.InspectionTypeMasterId);
           Property(t => t.InspectionTypeMasterId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
           Property(t => t.Inspection);                   
           Property(t => t.InspectionReportName);
           Property(t => t.OperationId);
           Property(t => t.Code);
           Property(t => t.Parameter);
           Property(t => t.InspectionFrequency);
           Property(t => t.Type);
           Property(t => t.CommonCause);
           Property(t => t.CreatedDate);
           Property(t => t.UpdatedDate);
           Property(t => t.CreatedBy);
           Property(t => t.UpdatedBy);
           Property(t => t.IsDeleted);
           Property(t => t.DeletedBy);
           Property(t => t.DeletedDate);
           ToTable("InspectionTypeMaster");
       }
    }
}
