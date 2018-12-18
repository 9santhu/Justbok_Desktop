using System.Windows.Controls;

namespace JustbokApplication.Views.Settings
{
    /// <summary>
    /// Interaction logic for AddEditProductView.xaml
    /// </summary>
    public partial class AddEditProductView : UserControl
    {
        public AddEditProductView()
        {
            BaseViewModel.Errors = 0;
            InitializeComponent();
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
           if (e.Action == ValidationErrorEventAction.Added) BaseViewModel.Errors += 1;
            if (e.Action == ValidationErrorEventAction.Removed) BaseViewModel.Errors -= 1;
        }
    }
}
