using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CertScanner.Core;
using CertScanner.Core.NetStd;
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

        private ObservableCollection<CertInfoWpf> _systemStoreCertsResult;

        public ObservableCollection<CertInfoWpf> SystemStoreCertsResult
        {
            get { return _systemStoreCertsResult; }
            set { _systemStoreCertsResult = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<CertInfoWpf> _fileSystemCertsResult;

        public ObservableCollection<CertInfoWpf> FileSystemCertsResult
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

            ScannerFolderPaths = ConfigurationManager.AppSettings["Path"];

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
            UpdateSettings();
            IsPathConfigWindowOpen = false;
        }

        private async Task RefreshDataAsync()
        {
            IsBusy = true;

            // 1. Clear Old Info
            SystemStoreCertsResult = new ObservableCollection<CertInfoWpf>();
            FileSystemCertsResult = new ObservableCollection<CertInfoWpf>();

            // 2. Create Scanning Tasks
            var tasks = new List<Task>();

            var t1 = Task.Run(() =>
            {
                var sysCerts = SysCertificationScanner.ScanCertificates().Select(c => new CertInfoWpf()
                {
                    Abstract = c.Abstract,
                    ExpDate = c.ExpDate,
                    FriendlyName = c.FriendlyName,
                    Issuer = c.Issuer,
                    StoreLocation = c.StoreLocation,
                    Subject = c.Subject,
                    Thumbprint = c.Thumbprint,
                    Version = c.Version
                });
                SystemStoreCertsResult = sysCerts.ToObservableCollection();
            });

            var t2 = Task.Run(() =>
            {
                var fsCerts = FileSystemCertificationScanner.ScanCertificates().Select(c => new CertInfoWpf()
                {
                    Abstract = c.Abstract,
                    ExpDate = c.ExpDate,
                    FriendlyName = c.FriendlyName,
                    Issuer = c.Issuer,
                    StoreLocation = c.StoreLocation,
                    Subject = c.Subject,
                    Thumbprint = c.Thumbprint,
                    Version = c.Version
                });
                FileSystemCertsResult = fsCerts.ToObservableCollection();
            });

            tasks.AddRange(new[] { t1, t2 });
            await Task.WhenAll(tasks);

            IsBusy = false;
        }

        private void UpdateSettings()
        {
            Configuration oConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            oConfig.AppSettings.Settings["Path"].Value = ScannerFolderPaths;
            oConfig.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("AppSettings");
        }
    }
}