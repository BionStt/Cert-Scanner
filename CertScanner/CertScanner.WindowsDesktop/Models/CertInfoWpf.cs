using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CertScanner.Core.NetStd;
using CertScanner.WindowsDesktop.Annotations;
using GalaSoft.MvvmLight.Command;

namespace CertScanner.WindowsDesktop.Models
{
    public class CertInfoWpf : CertInfo, INotifyPropertyChanged
    {
        public ICommand CertDetail { get; set; }

        public ICommand OpenCertificateLocation { get; set; }

        public CertInfoWpf()
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
