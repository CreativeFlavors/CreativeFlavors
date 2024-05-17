using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface IUserService
    {
        bool Post(Users arg);
        bool Put(Users arg);
        bool Delete(int id);
        Users Get(int id);
        List<Users> Get();
    }
}
