﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CertScanner.WindowsDesktop.ViewModel;
using MahApps.Metro.Controls;

namespace CertScanner.WindowsDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = DataContext as MainViewModel;
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.InitDataAsync();
        }

        private void BtnAbout_OnClick(object sender, RoutedEventArgs e)
        {
            ChdAbout.IsOpen = true;
        }

        private void HyperlinkEdi_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void BtnCfgScanDir_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.IsPathConfigWindowOpen = true;
        }
    }
}
