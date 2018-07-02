using System;
using System.Collections.ObjectModel;
using CertScanner.Core.NetStd;
using Edi.UWP.Helpers.Extensions;
using GalaSoft.MvvmLight;

namespace CertScanner.UniversalWindows.ViewModels
{
    public class SystemCertsViewModel : ViewModelBase
    {
        public SystemStorageCertificationScanner SysCertificationScanner { get; set; }

        public SystemCertsViewModel()
        {
            SysCertificationScanner = new SystemStorageCertificationScanner();
        }

        public ObservableCollection<CertInfo> Source => SysCertificationScanner.ScanCertificates().ToObservableCollection();
    }
}
