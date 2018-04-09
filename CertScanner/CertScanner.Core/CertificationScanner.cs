using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

    public class SystemStorageCertificationScanner : CertificationScanner
    {
        public StoreLocation StoreLocation { get; set; }

        public SystemStorageCertificationScanner()
        {
            StoreLocation = StoreLocation.LocalMachine;
        }

        public override IEnumerable<CertInfo> ScanCertificates()
        {
            X509Store store = new X509Store(StoreLocation);
            store.Open(OpenFlags.ReadOnly);
            foreach (var storeCertificate in store.Certificates)
            {
                var certInfo = new CertInfo()
                {
                    FriendlyName = storeCertificate.FriendlyName,
                    Issuer = storeCertificate.Issuer,
                    ExpDate = storeCertificate.GetExpirationDateString()
                };
                yield return certInfo;
            }
            store.Close();
        }
    }
}
