using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface EmailTemplateService
    {
        bool Post(EmailTemplate arg);
        bool Put(EmailTemplate arg);
        bool Delete(int id);
        EmailTemplate Get(int id);
    }
}
