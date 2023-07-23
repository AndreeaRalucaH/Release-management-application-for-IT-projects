using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Models
{
    public class JiraContentsExcel
    {
        public string TicketID { get; set; }
        public string Type { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }
        public string Requestor { get; set; }
        public string PrioritizationDate { get; set; }

    }
}
