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
    /// Interaction logic for StaffUserControl.xaml
    /// </summary>
    public partial class RoleView : UserControl
    {
        public RoleView()
        {
            InitializeComponent();
            DataContext = new RoleViewModel();
        }
    }
}
