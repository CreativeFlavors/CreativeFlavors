using MMS.Core;
using MMS.Core.Entities.GateEntry;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.GateEntryMap
{
    public class EmployNameMap : EntityTypeConfiguration<EmployNameMaster>
    {
        public EmployNameMap()
        {
            HasKey(t => t.EmployeeCodePK);
            Property(t => t.EmployeeCodePK).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.EmployeeID);
            Property(t => t.Name);
            Property(t => t.Initial);
            Property(t => t.DOJ);
            Property(t => t.DOC);
            Property(t => t.DOR);
            Property(t => t.Education);
            Property(t => t.CompanyCodeFK);
            Property(t => t.UnitCodeFK);
            Property(t => t.EmpCategoryCodeFK);
            Property(t => t.EmpDepartmentCodeFK);
            Property(t => t.EmpDesignationCodeFK);
            Property(t => t.EmpGradeCodeFK);
            Property(t => t.ShiftCodeFK);
            Property(t => t.Active);
            Property(t => t.Shift);
            Property(t => t.OT);
            Property(t => t.FlatAmount);
            Property(t => t.OTType);
            Property(t => t.EmolumentCodeFK);
            Property(t => t.Gender);
            Property(t => t.Position);
            Property(t => t.Photo);
            Property(t => t.ResumeFile);
            Property(t => t.SettlementFile);
            Property(t => t.ReasonForLeaving);
            Property(t => t.IsSalary);
            Property(t => t.HeadMasterID);
            Property(t => t.Production_NonProduction);
            Property(t => t.DeletedON);
            Property(t => t.DeletedBy);
            Property(t => t.UAN);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);

            Property(t => t.CreatedDate);
            Property(t => t.LastUpdatedDate);


            ToTable("tbl_Employee_Official");
        }
    }
}
