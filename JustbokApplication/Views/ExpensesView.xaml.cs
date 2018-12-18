using JustbokApplication.ViewModel;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace JustbokApplication.Views
{
    /// <summary>
    /// Interaction logic for ExpensesView.xaml
    /// </summary>
    public partial class ExpensesView : UserControl
    {
        public ExpensesView()
        {
            InitializeComponent();
            DataContext = new ExpenseViewModel();
        }

        #region Searching & Reset
        #endregion

    }
}
