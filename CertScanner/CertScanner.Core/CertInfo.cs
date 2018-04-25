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

        public DateTime ExpDate { get; set; }

        public string StoreLocation { get; set; }

        public bool IsExpired => ExpDate < DateTime.Now;
    }
}
