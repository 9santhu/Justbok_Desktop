using JustbokApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JustbokApplication.Views.Settings
{
    /// <summary>
    /// Interaction logic for AddEditBranchView.xaml
    /// </summary>
    public partial class AddEditBranchView : UserControl
    {
        public AddEditBranchView()
        {
            BaseViewModel.Errors = 0;
            InitializeComponent();
        }
    }
}
