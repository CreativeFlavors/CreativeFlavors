using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.GateEntry
{
  public  class EmployDepartmentMaster:BaseEntity2
    {

        public Guid EmpDepartmentCodePK { get; set; }
        
        public Guid? EmpCategoryCodeFK { get; set; }
        public Guid? CompanyCodeFK { get; set; }
        public string DepartmentName { get; set; }
       
        public int DepartmentCodeNo { get; set; }
        
        public string OTValue { get; set; }
        public bool Incentive { get; set; }
        public bool OT { get; set; }
        public Guid? DivisionCodeFK { get; set; }
      
        public DateTime? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
