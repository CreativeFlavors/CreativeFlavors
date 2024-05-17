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
        bool Post(tblautogenissueslipdetails arg);
        bool Put(tblautogenissueslipdetails arg);
        bool Delete(int id);
        tblautogenissueslipdetails Get(int id);
        List<tblautogenissueslipdetails> Get();
    }
}
