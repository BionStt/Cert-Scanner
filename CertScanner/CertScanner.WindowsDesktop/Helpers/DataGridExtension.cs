using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CertScanner.WindowsDesktop.Helpers
{
    public static class DataGridExtension
    {
        #region Columns Property

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.RegisterAttached("Columns",
            typeof(ObservableCollection<DataGridColumn>),
            typeof(DataGridExtension),
            new UIPropertyMetadata(new ObservableCollection<DataGridColumn>(), OnDataGridColumnsPropertyChanged));

        public static ObservableCollection<DataGridColumn> GetColumns(DependencyObject obj)
        {
            return (ObservableCollection<DataGridColumn>)obj.GetValue(ColumnsProperty);
        }

        public static void SetColumns(DependencyObject obj, ObservableCollection<DataGridColumn> value)
        {
            obj.SetValue(ColumnsProperty, value);
        }

        #endregion

        private static void OnDataGridColumnsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d.GetType() == typeof(DataGrid))
            {
                DataGrid myGrid = d as DataGrid;

                ObservableCollection<DataGridColumn> columns = (ObservableCollection<DataGridColumn>)e.NewValue;

                if (columns != null)
                {
                    myGrid.Columns.Clear();

                    if (columns != null && columns.Count > 0)
                    {
                        foreach (DataGridColumn dataGridColumn in columns)
                        {
                            myGrid.Columns.Add(dataGridColumn);
                        }
                    }

                    columns.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs args)
                    {
                        if (args.NewItems != null)
                        {
                            foreach (DataGridColumn column in args.NewItems.Cast<DataGridColumn>())
                            {
                                myGrid.Columns.Add(column);
                            }
                        }

                        if (args.OldItems != null)
                        {
                            foreach (DataGridColumn column in args.OldItems.Cast<DataGridColumn>())
                            {
                                myGrid.Columns.Remove(column);
                            }
                        }
                    };
                }
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj)
            where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child is T variable)
                    {
                        yield return variable;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            foreach (childItem child in FindVisualChildren<childItem>(obj))
            {
                return child;
            }

            return null;
        }
    }
}
