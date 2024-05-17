using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface SizeMatchingService
    {
        SizeMatching Post(SizeMatching arg);
        bool Put(SizeMatching arg);
        bool Delete(int id);
        SizeMatching Get(int id);
        List<SizeMatching> Get();
    }
}
