using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Models
{
    public class BrvDetails
    {
        public string IdImpulse { get; set; }
        public string CreationDate { get; set; }
        public string Author { get; set; }
        public string Application { get; set; }
        public string PoApp { get; set; }
        public string ItApp { get; set; }
        public string PoDate { get; set; }
        public string ItDate { get; set; }
        public List<JiraContentsExcel> JiraContents { get; set; }
    }
}
