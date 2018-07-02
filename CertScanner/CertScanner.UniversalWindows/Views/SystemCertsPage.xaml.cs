using System;

using CertScanner.UniversalWindows.ViewModels;

using Windows.UI.Xaml.Controls;

namespace CertScanner.UniversalWindows.Views
{
    public sealed partial class SystemCertsPage : Page
    {
        private SystemCertsViewModel ViewModel
        {
            get { return DataContext as SystemCertsViewModel; }
        }

        // TODO WTS: Change the grid as appropriate to your app.
        // For help see http://docs.telerik.com/windows-universal/controls/raddatagrid/gettingstarted
        // You may also want to extend the grid to work with the RadDataForm http://docs.telerik.com/windows-universal/controls/raddataform/dataform-gettingstarted
        public SystemCertsPage()
        {
            InitializeComponent();
        }
    }
}
