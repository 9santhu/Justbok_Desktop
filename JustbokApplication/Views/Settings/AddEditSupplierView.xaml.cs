using System.Windows;
using System.Windows.Controls;

namespace JustbokApplication.Views.Settings
{
    /// <summary>
    /// Interaction logic for AddEditProductView.xaml
    /// </summary>
    public partial class AddEditSupplierView : UserControl
    {
        public AddEditSupplierView()
        {
            BaseViewModel.Errors = 0;
            InitializeComponent();
            //this.Loaded += MainWindow_Loaded;
        }

        //void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var errors = grid.GetValue(Validation.ErrorsProperty);
        //}


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
