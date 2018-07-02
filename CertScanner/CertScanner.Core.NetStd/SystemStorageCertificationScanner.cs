using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace CertScanner.Core.NetStd
{
    public class SystemStorageCertificationScanner : CertificationScanner
    {
        public override IEnumerable<CertInfo> ScanCertificates()
        {
            foreach (StoreLocation loc in Enum.GetValues(typeof(StoreLocation)))
            {
                foreach (StoreName n in Enum.GetValues(typeof(StoreName)))
                {
                    X509Store store = new X509Store(n, loc);
                    store.Open(OpenFlags.ReadOnly);
                    foreach (var storeCertificate in store.Certificates)
                    {
                        var certInfo = new CertInfo()
                        {
                            Subject = storeCertificate.Subject,
                            FriendlyName = storeCertificate.FriendlyName,
                            Issuer = storeCertificate.Issuer,
                            Version = storeCertificate.Version,
                            Thumbprint = storeCertificate.Thumbprint,
                            StoreLocation = loc.ToString(),
                            ExpDate = DateTime.Parse(storeCertificate.GetExpirationDateString()),
                            Abstract = storeCertificate.ToString()
                        };
                        yield return certInfo;
                    }
                    store.Close();
                }
            }
        }
    }
}