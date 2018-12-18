using JustbokApplication.ViewModel;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace JustbokApplication.Views.DietConfig
{
    /// <summary>
    /// Interaction logic for DietPlanView.xaml
    /// </summary>
    public partial class DietPlanView : UserControl
    {
        public DietPlanView()
        {
            InitializeComponent();
            DataContext = new DietPlanViewModel();
        }
    }
}
