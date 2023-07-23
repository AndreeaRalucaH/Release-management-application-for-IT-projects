using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        IEmail iEmail = null;

        public EmailController(IEmail email)
        {
            iEmail = email;
        }

        [HttpPost("createEmail")]
        public bool SendEmailCreate([FromBody]EmailData emailData)
        {
            return iEmail.SendEmailCreate(emailData);
        }

        [HttpPost("start/createEmail")]
        public bool SendEmailStart([FromBody] EmailData emailData)
        {
            return iEmail.SendEmailStartEnd(emailData);
        }
    }
}
