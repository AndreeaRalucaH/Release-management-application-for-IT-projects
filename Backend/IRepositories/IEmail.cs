using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.IRepositories
{
    public interface IEmail
    {
        bool SendEmailCreate(EmailData emailData);
        bool SendEmailStartEnd(EmailData emailData);
    }
}
