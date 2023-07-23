using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relmonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JiraController : ControllerBase
    {
        private readonly IJira _jira;

        public JiraController(IJira jira)
        {
            _jira = jira;
        }

        [HttpGet("issue/{issueKey}")]
        public async Task<ActionResult<List<JiraIssue>>> GetIssue(string issueKey)
        {
            try
            {
                var response = await _jira.GetIssue(issueKey);
                return response;
            }
            catch (Exception ex)
            {
                return new List<JiraIssue>() ;
            }
        }
    }
}
