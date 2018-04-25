using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
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
        private bool _isBusy;

        public ObservableCollection<CertInfo> SystemStoreCerts
        {
            get { return _systemStoreCerts; }
            set { _systemStoreCerts = value; RaisePropertyChanged(); }
        }

        public RelayCommand CommandOpenCertMgr { get; set; }

        public RelayCommand CommandRefresh { get; set; }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            CertificationScanner = new SystemStorageCertificationScanner();
            SystemInfo = $"MachineName: {Environment.MachineName}, OS Version: {Environment.OSVersion}";
            CommandOpenCertMgr = new RelayCommand(() => { Process.Start("certmgr.msc"); });
            CommandRefresh = new RelayCommand(async () => { await RefreshDataAsync(); });
        }

        public async Task InitDataAsync()
        {
            IsBusy = true;
            await Task.Delay(2000); // for UI debug
            await RefreshDataAsync();
            IsBusy = false;
        }

        private async Task RefreshDataAsync()
        {
            SystemStoreCerts = new ObservableCollection<CertInfo>();
            SystemStoreCerts = CertificationScanner.ScanCertificates().ToObservableCollection();
            await Task.Yield();
        }
    }
}