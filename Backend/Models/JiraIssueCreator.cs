using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Models
{
    public class JiraIssueCreator
    {
        [JsonProperty("emailAddress")]
        public string Email { get; set; }

        [JsonProperty("displayName")]
        public string Name { get; set; }
    }
}
