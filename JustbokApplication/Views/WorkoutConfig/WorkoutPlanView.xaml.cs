using JustbokApplication.ViewModel;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace JustbokApplication.Views.WorkoutConfig
{
    /// <summary>
    /// Interaction logic for WorkoutPlanView.xaml
    /// </summary>
    public partial class WorkoutPlanView : UserControl
    {
        public WorkoutPlanView()
        {
            InitializeComponent();
            DataContext = new DietPlanViewModel();
        }
    }
}
