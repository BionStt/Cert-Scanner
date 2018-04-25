using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertScanner.Core
{
    public abstract class CertificationScanner : ICertificationScanner
    {
        public abstract IEnumerable<CertInfo> ScanCertificates();
    }
}
