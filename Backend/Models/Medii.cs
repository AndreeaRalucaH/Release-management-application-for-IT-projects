using System;
using System.Collections.Generic;

#nullable disable

namespace Relmonitor.Models
{
    public partial class Medii
    {
        public Medii()
        {
            Releases = new HashSet<Release>();
        }

        public int Idmediu { get; set; }
        public string Denumire { get; set; }
        public DateTime Datacreare { get; set; }
        public DateTime Datamodificare { get; set; }

        public virtual ICollection<Release> Releases { get; set; }
    }
}
