using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Models
{
    public class EmailSettings
    {
        public string EmailID { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Pass { get; set; }
        public bool UseSSL { get; set; }
    }
}
