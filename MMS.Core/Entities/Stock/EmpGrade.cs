using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("empgrate", Schema = "public")]
    public class EmpGrade : BaseEntity2
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("empgradecodepk")]
        public Guid EmpGradeCodePK { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? LastUpdatedDate { get; set; }
        [Column("gradecode")]
        public int GradeCode { get; set; }
        [Column("fromefficiency")]
        public string FromEfficiency { get; set; }
        [Column("toefficiency")]
        public string ToEfficiency { get; set; }
        [Column("companycodefk")]
        public Guid? CompanyCodeFK { get; set; }
        [Column("empcategorycodefk")]
        public Guid? EmpCategoryCodeFK { get; set; }
        [Column("empdepartmentcodefk")]
        public Guid? EmpDepartmentCodeFK { get; set; }
        [Column("empdesignationcodefk")]
        public Guid? EmpDesignationCodeFK { get; set; }
        [Column("unitcodefk")]
        public Guid? UnitCodeFK { get; set; }
        [Column("divisioncodefk")]
        public Guid? DivisionCodeFK { get; set; }
        [Column("gratename")]
        public string GradeName { get; set; }

    }
}
