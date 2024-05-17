using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.GateEntry
{
    public class EmployNameMaster:BaseEntity2
    {
        public Guid EmployeeCodePK { get; set; }
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string Initial { get; set; }
        public DateTime? DOJ { get; set; }
        public DateTime? DOC { get; set; }
        public DateTime? DOR { get; set; }
        public string Education { get; set; }
        public Guid CompanyCodeFK { get; set; }
        public Guid UnitCodeFK { get; set; }
        public Guid EmpCategoryCodeFK  { get; set; }
        public Guid EmpDepartmentCodeFK { get; set; }
        public Guid EmpDesignationCodeFK { get; set; }
        public Guid EmpGradeCodeFK { get; set; }


        public Guid ShiftCodeFK { get; set; }
        public bool Active { get; set; }
        public bool Shift { get; set; }
        public bool OT { get; set; }
        public decimal FlatAmount { get; set; }
        public int OTType { get; set; }
        public Guid EmolumentCodeFK { get; set; }
        public int Gender { get; set; }
        public string Position { get; set; }
        public string Photo { get; set; }
        public string ResumeFile { get; set; }
        public string SettlementFile { get; set; }
        public string ReasonForLeaving { get; set; }
        public bool IsSalary { get; set; }
        public int HeadMasterID { get; set; }
        public string Production_NonProduction   { get; set; }
        public DateTime? DeletedON { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        public string UAN { get; set; }

    }
}
