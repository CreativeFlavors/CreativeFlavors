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
        bool Post(EmailTempate arg);
        bool Put(EmailTempate arg);
        bool Delete(int id);
        EmailTempate Get(int id);
    }
}
