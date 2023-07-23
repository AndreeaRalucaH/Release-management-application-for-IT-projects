using System;
using System.Collections.Generic;

#nullable disable

namespace Relmonitor.Models
{
    public partial class Impulse
    {
        public int Idrelease { get; set; }
        public string Idimpulse { get; set; }
        public string Sysidimpulse { get; set; }
        public string Idtask { get; set; }
        public DateTime Datacreare { get; set; }
        public DateTime Datamodificare { get; set; }

        public virtual Release IdreleaseNavigation { get; set; }
    }
}
