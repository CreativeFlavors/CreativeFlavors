using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities;
namespace MMS.Repository.Service
{
    public interface IIndentTypeService
    {
        bool Post(IndentTypeMaster arg);
        bool Put(IndentTypeMaster arg);
        bool Delete(int id);
        IndentTypeMaster Get(int id);
        List<IndentTypeMaster> Get();
    }
}
