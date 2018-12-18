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
    /// Interaction logic for PackageView.xaml
    /// </summary>
    public partial class PackageView : UserControl
    {
        public PackageView()
        {
            InitializeComponent();
            DataContext = new PackageViewModel();
        }
    }
}
