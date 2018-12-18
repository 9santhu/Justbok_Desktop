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
    /// Interaction logic for MealTimeView.xaml
    /// </summary>
    public partial class MealTimeView : UserControl
    {
        public MealTimeView()
        {
            InitializeComponent();
            DataContext = new MealTimeViewModel();
        }
    }
}
