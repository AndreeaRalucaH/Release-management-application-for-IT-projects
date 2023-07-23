using System;
using System.Collections.Generic;

#nullable disable

namespace Relmonitor.Models
{
    public partial class Release
    {
        public int Idrelease { get; set; }
        public int Idstatus { get; set; }
        public int Idaplicatie { get; set; }
        public int Idmediu { get; set; }
        public int Idutilizator { get; set; }
        public int Iddurata { get; set; }
        public int Imbunatatiri { get; set; }
        public int Bugs { get; set; }
        public int Esteurgenta { get; set; }
        public string Contentrelease { get; set; }
        public string Comentarii { get; set; }
        public string Brvpath { get; set; }
        public string Testpath { get; set; }
        public DateTime Datacreare { get; set; }
        public DateTime Datamodificare { get; set; }
        public int Relstatus { get; set; }


        public virtual Aplicatii IdaplicatieNavigation { get; set; }
        public virtual Duraterelease IddurataNavigation { get; set; }
        public virtual Medii IdmediuNavigation { get; set; }
        public virtual Status IdstatusNavigation { get; set; }
        public virtual Utilizatori IdutilizatorNavigation { get; set; }
        public virtual Impulse Impulse { get; set; }
    }
}
