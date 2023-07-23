using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.IRepositories
{
    public interface IJira
    {
        Task<List<JiraIssue>> GetIssue(string issue);
    }
}
