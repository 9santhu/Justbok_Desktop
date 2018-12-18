using JustbokApplication.ViewModel;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace JustbokApplication.Views.Settings
{
    /// <summary>
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class SupplierView : UserControl
    {
        public SupplierView()
        {
            InitializeComponent();
            DataContext = new SupplierViewModel();
        }
    }
}
