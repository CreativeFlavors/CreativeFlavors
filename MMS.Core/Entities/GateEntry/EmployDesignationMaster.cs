using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.GateEntry
{
    public class EmployDesignationMaster:BaseEntity2
    {

        public Guid EmpDesignationCodePK { get; set; }
        public Guid? EmpDepartmentCodeFK { get; set; }
        public string DesignationName { get; set; }
        public string DivisionName { get; set; }
       
       
        public int DesignationCode { get; set; }
        public Guid? CompanyCodeFK { get; set; }
        public Guid? DivisionCodeFK { get; set; }
        public Guid? EmpCategoryCodeFK { get; set; }
        public string Deletedby { get; set; }
        public DateTime? DeletedOn { get; set; }
       

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
