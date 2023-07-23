using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Models
{
    public class IssueFields
    {
        [JsonProperty("issuetype")]
        public IssueTypeField IssueType { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("status")]
        public JiraIssueStatus Status { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("creator")]
        public JiraIssueCreator Creator { get; set; }
    }
}
