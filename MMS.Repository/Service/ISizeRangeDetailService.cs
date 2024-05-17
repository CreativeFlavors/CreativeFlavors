using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface ISizeRangeDetailService
    {
        bool Post(SizeRangeDetails arg);
        bool Put(SizeRangeDetails arg);
        bool Delete(int id);
        SizeRangeDetails Get(int id);
        List<SizeRangeDetails> Get();
    }
}
