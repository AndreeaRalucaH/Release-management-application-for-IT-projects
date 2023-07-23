using System;
using System.Collections.Generic;

#nullable disable

namespace Relmonitor.Models
{
    public partial class Detaliirelease
    {
        public int Idrelease { get; set; }
        public int Idstatus { get; set; }
        public int Idaplicatie { get; set; }
        public int Idmediu { get; set; }
        public int Idutilizator { get; set; }
        public int Iddurata { get; set; }
        public int Relstatus { get; set; }
        public string Denumireaplicatie { get; set; }
        public string Codaplicatie { get; set; }
        public int? Imbunatatiri { get; set; }
        public int? Bugs { get; set; }
        public int? Esteurgenta { get; set; }
        public string Contentrelease { get; set; }
        public string Brvpath { get; set; }
        public string Testpath { get; set; }
        public string Denumirestatus { get; set; }
        public string Denumiremediu { get; set; }
        public string Creator { get; set; }
        public string Idimpulse { get; set; }
        public string Sysidimpulse { get; set; }
        public string Luna { get; set; }
        public string Saptamana { get; set; }
        public DateTime? Datarelease { get; set; }
        public DateTime? Duratarelease { get; set; }
        public DateTime? Downtime { get; set; }
        public DateTime? Datastart { get; set; }
        public DateTime? Dataend { get; set; }
    }
}
