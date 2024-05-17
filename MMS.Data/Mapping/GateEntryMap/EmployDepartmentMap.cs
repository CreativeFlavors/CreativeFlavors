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
    class EmployDepartmentMap : EntityTypeConfiguration<EmployDepartmentMaster>
    {
        public EmployDepartmentMap()
        {
            HasKey(t => t.EmpDepartmentCodePK);
            Property(t => t.EmpDepartmentCodePK).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.EmpCategoryCodeFK);
            Property(t => t.CompanyCodeFK);
            Property(t => t.DepartmentName);
           
            Property(t => t.DepartmentCodeNo);
            Property(t => t.CompanyCodeFK);
            Property(t => t.DivisionCodeFK);
            Property(t => t.EmpCategoryCodeFK);
            Property(t => t.OTValue);
            Property(t => t.Incentive);
            Property(t => t.OT);
            Property(t => t.DivisionCodeFK);

            Property(t => t.DeletedBy);
            Property(t => t.DeletedOn);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);

            Property(t => t.CreatedDate);
            Property(t => t.LastUpdatedDate);


            ToTable("tbl_EmpDepartment");
        }
    }
}
