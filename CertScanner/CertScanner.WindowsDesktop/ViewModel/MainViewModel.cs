using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CertScanner.Core;
using Edi.ExtensionMethods;
using GalaSoft.MvvmLight;

namespace CertScanner.WindowsDesktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public SystemStorageCertificationScanner CertificationScanner { get; set; }

        private ObservableCollection<CertInfo> _systemStoreCerts;

        public ObservableCollection<CertInfo> SystemStoreCerts
        {
            get { return _systemStoreCerts; }
            set { _systemStoreCerts = value; RaisePropertyChanged(); }
        }

        public MainViewModel()
        {
            CertificationScanner = new SystemStorageCertificationScanner();
            SystemStoreCerts = CertificationScanner.ScanCertificates().ToObservableCollection();
        }
    }
}