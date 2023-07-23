using System;
using System.Collections.Generic;

#nullable disable

namespace Relmonitor.Models
{
    public partial class Utilizatori
    {
        public Utilizatori()
        {
            Releases = new HashSet<Release>();
        }

        public int Idutilizator { get; set; }
        public string Nume { get; set; }
        public string Email { get; set; }
        public int Esteadmin { get; set; }
        public DateTime Datacreare { get; set; }
        public DateTime Datamodificare { get; set; }

        public virtual ICollection<Release> Releases { get; set; }
    }
}
