using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities;
using System;
using System.Collections.Generic;
namespace MMS.Repository.Service
{
    public interface  AuntoGenerateIssueService
    {
        bool Post(AutoGenIssueSlipDetails arg);
        bool Put(AutoGenIssueSlipDetails arg);
        bool Delete(int id);
        AutoGenIssueSlipDetails Get(int id);
        List<AutoGenIssueSlipDetails> Get();
    }
}
