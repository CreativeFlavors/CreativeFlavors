using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface IMaterialCategoryService
    {
        bool Post(MaterialCategoryMaster arg);
        bool Put(MaterialCategoryMaster arg);
        bool Delete(int id);
        MaterialCategoryMaster Get(int id);
        List<MaterialCategoryMaster> Get();
    }
}
