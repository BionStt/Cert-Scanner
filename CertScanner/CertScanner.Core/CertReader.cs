using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CertScanner.Core
{
    public class CertReader
    {
        public CertInfo ReadCertFile(string filePath)
        {
            var cert = new X509Certificate2();
            cert.Import(filePath);

            return new CertInfo()
            {

            };
        }
    }
}
