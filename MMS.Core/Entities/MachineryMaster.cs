using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class MachineryMaster:BaseEntity
    {
       public int MachineryMasterId { get; set; }
       public string MachineCode { get; set; }
       public string MachineName { get; set; }
       public string Make { get; set; }
       public string Model { get; set; }
       public string AssesListNo { get; set; }
       public string MachineSerialNo { get; set; }
       public string Kilowatt { get; set; }
       public string HorsePower { get; set; }
       public string Volt { get; set; }
       public string OperationUsedFor { get; set; }
       public string Specification { get; set; }
       public string Image { get; set; }
       public string CreatedBy { get; set; }
       public string UpdatedBy { get; set; }
       public bool? IsDeleted { get; set; }
       public string DeletedBy { get; set; }
       public DateTime? DeletedDate { get; set; }
    }
}
