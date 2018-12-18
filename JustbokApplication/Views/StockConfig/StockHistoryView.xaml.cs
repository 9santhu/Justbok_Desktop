using JustbokApplication.ViewModel;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace JustbokApplication.Views.StockConfig
{
    /// <summary>
    /// Interaction logic for StockHistoryView.xaml
    /// </summary>
    public partial class StockHistoryView : UserControl
    {
        public StockHistoryView()
        {
            InitializeComponent();
            DataContext = new StockHistoryViewModel();
        }
    }
}
