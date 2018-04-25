using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace CertScanner.Core
{
    public class SystemStorageCertificationScanner : CertificationScanner
    {
        public override IEnumerable<CertInfo> ScanCertificates()
        {
            foreach (StoreLocation loc in Enum.GetValues(typeof(StoreLocation)))
            {
                X509Store store = new X509Store(loc);
                store.Open(OpenFlags.ReadOnly);
                foreach (var storeCertificate in store.Certificates)
                {
                    var certInfo = new CertInfo()
                    {
                        FriendlyName = storeCertificate.FriendlyName,
                        Issuer = storeCertificate.Issuer,
                        StoreLocation = loc.ToString(),
                        ExpDate = DateTime.Parse(storeCertificate.GetExpirationDateString())
                    };
                    yield return certInfo;
                }
                store.Close();
            }
        }
    }
}