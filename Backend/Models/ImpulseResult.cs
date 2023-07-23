using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Models
{
    public class ImpulseResult
    {
        [JsonProperty("number")]
        public ImpulseNumber Number { get; set; }
    }
}
