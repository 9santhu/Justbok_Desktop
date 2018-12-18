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
    /// Interaction logic for WorkoutView.xaml
    /// </summary>
    public partial class WorkoutView : UserControl
    {
        public WorkoutView()
        {
            InitializeComponent();
            DataContext = new WorkoutViewModel();
        }
    }
}
