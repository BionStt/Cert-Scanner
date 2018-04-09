using System.Collections.Generic;

namespace CertScanner.Core
{
    public interface ICertificationScanner
    {
        string GetSimpleSystemInfo();

        IEnumerable<CertInfo> ScanCertificates();
    }
}