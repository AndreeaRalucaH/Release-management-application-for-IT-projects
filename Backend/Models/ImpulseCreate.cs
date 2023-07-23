using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Models
{
    public class ImpulseCreate
    {
        [JsonProperty("description")]
        public string IssueType { get; set; }

        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }

        [JsonProperty("assignment_group")]
        public string AssigGroup { get; set; }

        [JsonProperty("business_service")]
        public string BusinessService { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("end_date")]
        public string EndDate { get; set; }
    }
}
