using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertScanner.Core
{
    public class CertInfo
    {
        public string FriendlyName { get; set; }

        public string Issuer { get; set; }

        public string ExpDate { get; set; }
    }
}
