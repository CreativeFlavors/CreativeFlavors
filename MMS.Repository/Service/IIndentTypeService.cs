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
        bool Post(IndentMaster arg);
        bool Put(IndentMaster arg);
        bool Delete(int id);
        IndentMaster Get(int id);
        List<IndentMaster> Get();
    }
}
