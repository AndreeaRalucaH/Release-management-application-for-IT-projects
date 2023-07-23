using System;
using System.Collections.Generic;

#nullable disable

namespace Relmonitor.Models
{
    public partial class Duraterelease
    {
        public Duraterelease()
        {
            Releases = new HashSet<Release>();
        }

        public int Iddurata { get; set; }
        public string Luna { get; set; }
        public string Saptamana { get; set; }
        public DateTime Datarelease { get; set; }
        public DateTime Durata { get; set; }
        public DateTime Downtime { get; set; }
        public DateTime Datastart { get; set; }
        public DateTime Dataend { get; set; }
        public DateTime Datacreare { get; set; }
        public DateTime Datamodificare { get; set; }

        public virtual ICollection<Release> Releases { get; set; }
    }
}
