using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface IComponentService
    {
        bool Post(ComponentMaster arg);
        bool Put(ComponentMaster arg);
        bool Delete(int id);
        ComponentMaster Get(int id);
        List<ComponentMaster> Get();
    }
}
