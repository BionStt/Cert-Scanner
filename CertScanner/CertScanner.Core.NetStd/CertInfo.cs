using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CertScanner.Core.NetStd
{
    public class CertInfo
    {
        public string Subject { get; set; }

        public string FriendlyName { get; set; }

        public string Issuer { get; set; }

        public string Thumbprint { get; set; }

        public int Version { get; set; }

        public string StoreLocation { get; set; }

        public DateTime ExpDate { get; set; }

        public bool IsExpired => ExpDate < DateTime.Now;

        public string Abstract { get; set; }
    }
}
