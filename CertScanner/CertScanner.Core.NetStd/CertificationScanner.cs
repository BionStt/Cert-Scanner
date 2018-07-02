using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertScanner.Core.NetStd;

namespace CertScanner.Core
{
    public abstract class CertificationScanner : ICertificationScanner
    {
        public abstract IEnumerable<CertInfo> ScanCertificates();
    }
}
