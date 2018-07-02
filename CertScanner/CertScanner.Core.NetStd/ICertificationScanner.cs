using System.Collections.Generic;

namespace CertScanner.Core.NetStd
{
    public interface ICertificationScanner
    {
        IEnumerable<CertInfo> ScanCertificates();
    }
}