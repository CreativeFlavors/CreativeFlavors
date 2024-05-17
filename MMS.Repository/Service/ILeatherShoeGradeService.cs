using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface ILeatherShoeGradeService
    {
        bool Post(LeatherShoesGradeMaster arg);
        bool Put(LeatherShoesGradeMaster arg);
        bool Delete(int id);
        LeatherShoesGradeMaster Get(int id);
        List<LeatherShoesGradeMaster> Get();
    }
}
