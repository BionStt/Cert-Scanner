using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CertScanner.Core.Annotations;
using GalaSoft.MvvmLight.Command;

namespace CertScanner.Core
{
    public class CertInfo : INotifyPropertyChanged
    {
        public string Subject { get; set; }

        public string FriendlyName { get; set; }

        public string Issuer { get; set; }

        public string Thumbprint { get; set; }

        public int Version { get; set; }

        public string StoreLocation { get; set; }

        public DateTime ExpDate { get; set; }

        public bool IsExpired => ExpDate < DateTime.Now;

        public string Abstract { get; set; }

        public ICommand CertDetail { get; set; }

        public ICommand OpenCertificateLocation { get; set; }

        public CertInfo()
        {
            OpenCertificateLocation = new RelayCommand(() =>
            {
                var isWinStore = StoreLocation == "CurrentUser" || StoreLocation == "LocalMachine";

                if (isWinStore)
                {
                    Process.Start("certmgr.msc");
                }
                else
                {
                    string argument = "/select, \"" + StoreLocation + "\"";
                    Process.Start("explorer.exe", argument);
                }
            });

            CertDetail = new RelayCommand(() =>
                {
                    MessageBox.Show(Abstract, "Certificate Detail", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
