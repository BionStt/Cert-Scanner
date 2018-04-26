using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace CertScanner.Core
{
    public class FileSystemCertificationScanner : CertificationScanner
    {
        public List<string> TargetPaths { get; set; }

        public FileSystemCertificationScanner()
        {
            TargetPaths = new List<string>();
        }

        public override IEnumerable<CertInfo> ScanCertificates()
        {
            foreach (var targetPath in TargetPaths)
            {
                var certFiles = Directory.GetFiles(targetPath, "*.cer", SearchOption.AllDirectories);
                foreach (var certFile in certFiles)
                {
                    var cert = new X509Certificate2();
                    cert.Import(certFile);

                    var certInfo = new CertInfo()
                    {
                        FriendlyName = cert.FriendlyName,
                        Issuer = cert.Issuer,
                        Version = cert.Version,
                        SerialNumber = cert.SerialNumber,
                        StoreLocation = certFile,
                        ExpDate = DateTime.Parse(cert.GetExpirationDateString())
                    };
                    yield return certInfo;
                }
            }
        }
    }
}