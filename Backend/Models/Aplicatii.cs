using System;
using System.Collections.Generic;

#nullable disable

namespace Relmonitor.Models
{
    public partial class Aplicatii
    {
        public Aplicatii()
        {
            Releases = new HashSet<Release>();
        }

        public int Idaplicatie { get; set; }
        public string Denumire { get; set; }
        public string Emails { get; set; }
        public string Ownerproiect { get; set; }
        public string Managerproiect { get; set; }
        public DateTime Datacreare { get; set; }
        public DateTime Datamodificare { get; set; }
        public string Codaplicatie { get; set; }

        public virtual ICollection<Release> Releases { get; set; }
    }
}
