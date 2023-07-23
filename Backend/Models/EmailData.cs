using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Models
{
    public class EmailData
    {
        public string EmailToID { get; set; }
        public string EmailCC { get; set; }
        public string EmailSubject { get; set; }
        public string Application { get; set; }
        public string Env { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public string Duration { get; set; }
        public string Downtime { get; set; }
        public string Contents { get; set; }
        public string Tests { get; set; }
        public string AddOn { get; set; }
        public string Title { get; set; }
        public string IsEmerg { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
