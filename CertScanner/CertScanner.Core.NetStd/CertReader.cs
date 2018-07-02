using System.Security.Cryptography.X509Certificates;

namespace CertScanner.Core.NetStd
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
