using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace CertScanner.Core.NetStd
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
                if (Directory.Exists(targetPath))
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
                            Thumbprint = cert.Thumbprint,
                            StoreLocation = certFile,
                            ExpDate = DateTime.Parse(cert.GetExpirationDateString()),
                            Abstract = cert.ToString()
                        };
                        yield return certInfo;
                    }
                }
            }
        }
    }
}