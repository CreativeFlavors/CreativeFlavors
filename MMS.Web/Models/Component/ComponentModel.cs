using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.Component
{
    public class ComponentModel
    {
        public int ComponentMasterId { get; set; }
        public string ComponentName { get; set; }
        public string UsedIn { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<ComponentMaster> ComponentMasterList { get; set; }
    }
    public class LayOffDetails
    {
        public Guid? CompanyCodeFK { get; set; }
        public DateTime? CompensationDate { get; set; }
        public DateTime? LayOffDate { get; set; }
        public Guid? UnitCodeFK { get; set; }
        public Guid? EmpDepartmentCodeFK { get; set; }
    }
}