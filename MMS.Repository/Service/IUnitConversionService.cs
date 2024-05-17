using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface IUnitConversionService
    {
       UnitConversation Post(UnitConversation arg);
        bool Put(UnitConversation arg);
        bool Delete(int id);
        UnitConversation Get(int id);
        List<UnitConversation> Get();
    }
}
