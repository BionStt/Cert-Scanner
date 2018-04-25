using System;
using System.Collections.Generic;

namespace CertScanner.Core
{
    public class FileSystemCertificationScanner : CertificationScanner
    {
        public List<string> TargetPaths { get; set; }

        public override IEnumerable<CertInfo> ScanCertificates()
        {
            foreach (var targetPath in TargetPaths)
            {
                
            }
            throw new NotImplementedException();
        }
    }
}