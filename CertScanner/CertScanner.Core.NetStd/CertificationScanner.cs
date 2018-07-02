using System.Collections.Generic;

namespace CertScanner.Core.NetStd
{
    public abstract class CertificationScanner : ICertificationScanner
    {
        public abstract IEnumerable<CertInfo> ScanCertificates();
    }
}
