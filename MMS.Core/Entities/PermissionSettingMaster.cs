using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class PermissionSettingMaster : BaseEntity
    {
        public int PermissionSetID { get; set; }
        public int UserTypeID { get; set; }
        public string PermissionID { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
