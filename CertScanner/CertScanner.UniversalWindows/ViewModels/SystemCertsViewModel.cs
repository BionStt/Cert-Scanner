using System;
using System.Collections.ObjectModel;

using CertScanner.UniversalWindows.Models;
using CertScanner.UniversalWindows.Services;

using GalaSoft.MvvmLight;

namespace CertScanner.UniversalWindows.ViewModels
{
    public class SystemCertsViewModel : ViewModelBase
    {
        public ObservableCollection<SampleOrder> Source
        {
            get
            {
                // TODO WTS: Replace this with your actual data
                return SampleDataService.GetGridSampleData();
            }
        }
    }
}
