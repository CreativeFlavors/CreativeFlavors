using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
   public class EmpGrade:BaseEntity2
    {
       public Guid EmpGradeCodePK { get; set; }
       //public DateTime? CreatedDate { get; set; }
       //public DateTime? LastUpdatedDate { get; set; }
       public int GradeCode { get; set; }
       public string FromEfficiency { get; set; }
       public string ToEfficiency { get; set; }
       public Guid? CompanyCodeFK { get; set; }
       public Guid? EmpCategoryCodeFK { get; set; }
       public Guid? EmpDepartmentCodeFK { get; set; }
       public Guid? EmpDesignationCodeFK { get; set; }
       public Guid? UnitCodeFK { get; set; }
       public Guid? DivisionCodeFK { get; set; }
       public string GradeName { get; set; }
      
    }
}
