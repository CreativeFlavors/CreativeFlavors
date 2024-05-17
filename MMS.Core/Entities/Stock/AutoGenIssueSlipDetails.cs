using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class AutoGenIssueSlipDetails :BaseEntity
    {
        public int AutoGenerateId { get; set; }
        public string IssueSlipDetailsId { get; set; }
    }
}
