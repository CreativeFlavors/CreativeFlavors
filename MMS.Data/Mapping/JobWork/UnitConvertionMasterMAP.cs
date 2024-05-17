using System;
using MMS.Core.Entities.JobWork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.JobWork
{
    public class UnitConvertionMasterMAP : EntityTypeConfiguration<UnitConvertionMaster>
    {
        public UnitConvertionMasterMAP()
        {
            HasKey(t => t.UC_No_Id);
            Property(t => t.UC_No_Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //  Property(t => t.UOMUnitmasteID);
            Property(t => t.UC_No_Code);
            Property(t => t.Store_id_from);
            Property(t => t.Group_From);
            Property(t => t.Category_From);
            Property(t => t.Material_id_From);
            Property(t => t.Store_id_to);
            Property(t => t.Group_To);
            Property(t => t.Category_To);
            Property(t => t.Material_id_To);
            Property(t => t.Noms);

            Property(t => t.Uom1);
            Property(t => t.Uom2); 
            Property(t => t.No_sheet);
            Property(t => t.No_sheet_Uom);
            Property(t => t.Sheet_Value);
            Property(t => t.Sheet_Value_Uom);
            Property(t => t.Value);
            Property(t => t.Value_Uom);

            Property(t => t.UpdatedBy);
            Property(t => t.UpdatedDate);
            Property(t => t.IsDeleted);
            Property(t => t.DeletedBy);
            Property(t => t.DeletedDate);
            ToTable("Job_Unit_Convertion");
        }
    }
}
