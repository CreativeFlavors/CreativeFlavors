using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
  public interface IProjectionService
    {
        bool Post(Projection arg);
        bool Put(Projection arg);
        bool Delete(int id);
        Projection Get(int id);
        List<Projection> Get();
    }
}
