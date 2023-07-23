using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Models
{
    public class ImpulseNumber
    {
        [JsonProperty("display_value")]
        public string DisplayValue { get; set; }
    }
}
