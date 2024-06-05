using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface IParentbom_materialServices
    {
        parentbom_material Post(parentbom_material arg);
        bool Put(parentbom_material arg);
        bool Delete(int id);
    }
}
