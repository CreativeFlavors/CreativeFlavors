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
        bool Post(ProjectionMaster arg);
        bool Put(ProjectionMaster arg);
        bool Delete(int id);
        ProjectionMaster Get(int id);
        List<ProjectionMaster> Get();
    }
}
