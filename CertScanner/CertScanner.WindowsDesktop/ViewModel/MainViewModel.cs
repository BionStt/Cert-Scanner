using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CertScanner.Core;
using Edi.ExtensionMethods;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CertScanner.WindowsDesktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public string SystemInfo { get; set; }

        public SystemStorageCertificationScanner CertificationScanner { get; set; }

        private ObservableCollection<CertInfo> _systemStoreCerts;

        public ObservableCollection<CertInfo> SystemStoreCerts
        {
            get { return _systemStoreCerts; }
            set { _systemStoreCerts = value; RaisePropertyChanged(); }
        }

        public RelayCommand CommandOpenCertMgr { get; set; }

        public MainViewModel()
        {
            CertificationScanner = new SystemStorageCertificationScanner();
            SystemInfo = $"MachineName: {Environment.MachineName}, OS Version: {Environment.OSVersion}";
            CommandOpenCertMgr = new RelayCommand(() => { Process.Start("certmgr.msc"); });
            SystemStoreCerts = CertificationScanner.ScanCertificates().ToObservableCollection();
        }
    }
}