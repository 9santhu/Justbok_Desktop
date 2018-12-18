using System.Windows;
using System.Windows.Controls;

namespace JustbokApplication.Views.Settings
{
    /// <summary>
    /// Interaction logic for AddEditCategoryView.xaml
    /// </summary>
    public partial class AddEditCategoryView : UserControl
    {
        public AddEditCategoryView()
        {
            BaseViewModel.Errors = 0;
            InitializeComponent();
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added) BaseViewModel.Errors += 1;
            if (BaseViewModel.Errors > 0)
            {
                if (e.Action == ValidationErrorEventAction.Removed) BaseViewModel.Errors -= 1;
            }
        }
    }
}
