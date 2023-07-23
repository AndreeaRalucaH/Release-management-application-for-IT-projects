using System;
using System.Collections.Generic;

#nullable disable

namespace Relmonitor.Models
{
    public partial class Status
    {
        public Status()
        {
            Releases = new HashSet<Release>();
        }

        public int Idstatus { get; set; }
        public string Denumire { get; set; }
        public DateTime Datacreare { get; set; }
        public DateTime Datamodificare { get; set; }

        public virtual ICollection<Release> Releases { get; set; }
    }
}
