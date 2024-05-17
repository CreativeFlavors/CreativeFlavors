using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface IContactDetailsCaptureService
    {
        bool Post(ContactDetailsCapture arg);
       // bool Put(ContactDetailsCapture arg);
        bool Delete(int id);
        ContactDetailsCapture Get(int id);
        List<ContactDetailsCapture> Get();
    }
}
