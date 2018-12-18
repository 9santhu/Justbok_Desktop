﻿using System.Windows;
using System.Windows.Controls;

namespace JustbokApplication.Views
{
    /// <summary>
    /// Interaction logic for AddEditExpense.xaml
    /// </summary>
    public partial class AddEditExpense : UserControl
    {
        public AddEditExpense()
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