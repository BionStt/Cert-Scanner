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

        public SystemStorageCertificationScanner SysCertificationScanner { get; set; }

        public FileSystemCertificationScanner FileSystemCertificationScanner { get; set; }

        private bool _isBusy;

        private ObservableCollection<CertInfo> _systemStoreCertsResult;

        public ObservableCollection<CertInfo> SystemStoreCertsResult
        {
            get { return _systemStoreCertsResult; }
            set { _systemStoreCertsResult = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<CertInfo> _fileSystemCertsResult;

        public ObservableCollection<CertInfo> FileSystemCertsResult
        {
            get { return _fileSystemCertsResult; }
            set { _fileSystemCertsResult = value; RaisePropertyChanged(); }
        }

        private string _version;
        private string _scannerFolderPaths;
        private bool _isPathConfigWindowOpen;

        public string Version
        {
            get => _version;
            set { _version = value; RaisePropertyChanged(); }
        }

        public RelayCommand CommandOpenCertMgr { get; set; }

        public RelayCommand CommandRefresh { get; set; }

        public RelayCommand CommandSavePath { get; set; }

        public string ScannerFolderPaths
        {
            get => _scannerFolderPaths;
            set { _scannerFolderPaths = value; RaisePropertyChanged(); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        public bool IsPathConfigWindowOpen
        {
            get => _isPathConfigWindowOpen;
            set { _isPathConfigWindowOpen = value; RaisePropertyChanged(); }
        }

        public MainViewModel()
        {
            Version = GetType().Assembly.GetName().Version.ToString();

            SysCertificationScanner = new SystemStorageCertificationScanner();
            FileSystemCertificationScanner = new FileSystemCertificationScanner();

            ScannerFolderPaths = @"C:\Certs;D:\Something"; // for debug

            var dirs = ScannerFolderPaths.Split(';');
            FileSystemCertificationScanner.TargetPaths.AddRange(dirs);

            SystemInfo = $"Machine Name: {Environment.MachineName}, OS Version: {Environment.OSVersion}, User Name: {Environment.UserName}";
            CommandOpenCertMgr = new RelayCommand(() => { Process.Start("certmgr.msc"); });
            CommandRefresh = new RelayCommand(async () => { await RefreshDataAsync(); });
            CommandSavePath = new RelayCommand(() => { SavePath(); });
        }

        public async Task InitDataAsync()
        {
            await RefreshDataAsync();
        }

        private void SavePath()
        {
            // TODO
            IsPathConfigWindowOpen = false;
        }

        private async Task RefreshDataAsync()
        {
            IsBusy = true;

            await Task.Delay(2000); // for UI debug
            SystemStoreCertsResult = new ObservableCollection<CertInfo>();
            var sysCerts = SysCertificationScanner.ScanCertificates();
            SystemStoreCertsResult = sysCerts.ToObservableCollection();

            var fsCerts = FileSystemCertificationScanner.ScanCertificates();
            FileSystemCertsResult = fsCerts.ToObservableCollection();

            await Task.Yield();

            IsBusy = false;
        }
    }
}