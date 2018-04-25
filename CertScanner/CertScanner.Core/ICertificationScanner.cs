using System.Collections.Generic;

namespace CertScanner.Core
{
    public interface ICertificationScanner
    {
        IEnumerable<CertInfo> ScanCertificates();
    }
}