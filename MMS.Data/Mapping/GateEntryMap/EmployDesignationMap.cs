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
    public class EmployDesignationMap : EntityTypeConfiguration<EmployDesignationMaster>
    {
        public EmployDesignationMap()
        {
            HasKey(t => t.EmpDesignationCodePK);
            Property(t => t.EmpDesignationCodePK).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.EmpDepartmentCodeFK);
            Property(t => t.DesignationName);
            Property(t => t.DivisionName);
            Property(t => t.LastUpdatedDate);
            Property(t => t.DesignationCode);
            Property(t => t.CompanyCodeFK);
            Property(t => t.DivisionCodeFK);
            Property(t => t.EmpCategoryCodeFK);
            Property(t => t.Deletedby);
            Property(t => t.DeletedOn);
            Property(t => t.CreatedBy);
            Property(t => t.UpdatedBy);
            Property(t => t.IsDeleted);
           
            Property(t => t.CreatedDate);
            Property(t => t.LastUpdatedDate);
            
            
            ToTable("tbl_EmpDesignation");
        }
    }
}
