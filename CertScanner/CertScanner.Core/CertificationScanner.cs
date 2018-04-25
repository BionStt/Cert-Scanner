using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertScanner.Core
{
    public abstract class CertificationScanner : ICertificationScanner
    {
        public string GetSimpleSystemInfo()
        {
            throw new NotImplementedException();
        }

        public abstract IEnumerable<CertInfo> ScanCertificates();
    }

    public class FileSystemCertificationScanner : CertificationScanner
    {
        public string TargetPath { get; set; }

        public override IEnumerable<CertInfo> ScanCertificates()
        {
            throw new NotImplementedException();
        }
    }
}
